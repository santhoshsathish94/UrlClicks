using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlClicks.Infrastructure.Interface;

namespace UrlClicks.Infrastructure.Implemention
{
    public class AzureStorageRepository : IAzureStorageRepository
    {
        public CloudStorageAccount _storageAccount { get; private set; }
        private CloudQueueClient _queueClient { get; set; }
        private CloudTableClient _tableClient { get; set; }

        public AzureStorageRepository(string connectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(connectionString);
            _queueClient = _storageAccount.CreateCloudQueueClient();
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public async Task AddQueueAsync(string data,string queueName)
        {
            var queue = _queueClient.GetQueueReference(queueName);
            var message = new CloudQueueMessage(data);
            try
            {
                await queue.AddMessageAsync(message);
            }
            catch
            {
                await queue.CreateIfNotExistsAsync();
                await queue.AddMessageAsync(message);
            }
        }

        public async Task AddQueueListAsync(IEnumerable<string> data, string queueName)
        {
            var queue = _queueClient.GetQueueReference(queueName);
            foreach (var message in data)
            {
                await queue.AddMessageAsync(new CloudQueueMessage(message));
            }
        }

        public async Task AddTabelAsync(TableEntity azureTable, string tableName)
        {            
            CloudTable cloudTable = _tableClient.GetTableReference(tableName);
            var operation = TableOperation.Insert(azureTable);
            try
            {
                await cloudTable.ExecuteAsync(operation);
            }
            catch
            {
                await cloudTable.CreateIfNotExistsAsync();
                await cloudTable.ExecuteAsync(operation);
            }
        }

        public async Task AddOrUpdateTabelAsync(TableEntity azureTable, string tableName)
        {            
            CloudTable cloudTable = _tableClient.GetTableReference(tableName);
            var operation = TableOperation.InsertOrReplace(azureTable);
            try
            {
                await cloudTable.ExecuteAsync(operation);
            }
            catch
            {
                await cloudTable.CreateIfNotExistsAsync();
                await cloudTable.ExecuteAsync(operation);
            }
        }

        public async Task AddOrUpdateTabelAsync(IEnumerable<TableEntity> azureTable, string tableName)
        {                        
            CloudTable cloudTable = _tableClient.GetTableReference(tableName);
            foreach (var item in azureTable)
            {
                var operation = TableOperation.InsertOrReplace(item);
                await cloudTable.ExecuteAsync(operation);
            }
        }

        public async Task<List<T>> GetDataAsync<T>(dynamic tableQuery, string tableName)
        {            
            TableContinuationToken token = null;            
            CloudTable cloudTable = _tableClient.GetTableReference(tableName);
            var entities = new List<T>();
            do
            {
                var queryResult = await cloudTable.ExecuteQuerySegmentedAsync(tableQuery, token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);
            return entities;
        }

        public async Task RemoveTableAsync(IEnumerable<TableEntity> azureTable, string tableName)
        {            
            CloudTable cloudTable = _tableClient.GetTableReference(tableName);
            var data = azureTable.GroupBy(x => x.PartitionKey).ToList();
            foreach (var item in data)
            {
                int offset = 0;
                var groupData = item.ToList();
                while (offset < groupData.Count)
                {
                    TableBatchOperation batch = new TableBatchOperation();
                    var rows = groupData.Skip(offset).Take(100).ToList();
                    foreach (var row in rows)
                    {
                        row.ETag = "*";
                        batch.Delete(row);
                    }
                    await cloudTable.ExecuteBatchAsync(batch);
                    offset += rows.Count;
                }
            }
        }

        public async Task<List<T>> QueryAsync<T>(dynamic tableQuery, string tableName)
        {                        
            TableContinuationToken token = null;            
            CloudTable cloudTable = _tableClient.GetTableReference(tableName);
            var entities = new List<DynamicTableEntity>();
            do
            {
                var queryResult = await cloudTable.ExecuteQuerySegmentedAsync(tableQuery, token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);
            return entities.ConvertTableEntity<T>();
        }
    }
}
