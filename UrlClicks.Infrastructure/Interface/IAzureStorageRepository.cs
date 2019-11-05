using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UrlClicks.Infrastructure.Interface
{
    public interface IAzureStorageRepository
    {
        Task AddQueueAsync(string data, string queueName);
        Task AddQueueListAsync(IEnumerable<string> data, string queueName);
        Task AddTabelAsync(TableEntity azureTable, string tableName);
        Task AddOrUpdateTabelAsync(TableEntity azureTable, string tableName);
        Task AddOrUpdateTabelAsync(IEnumerable<TableEntity> azureTable, string tableName);
        Task<List<T>> GetDataAsync<T>(dynamic tableQuery, string tableName);
        Task RemoveTableAsync(IEnumerable<TableEntity> azureTable, string tableName);
        Task<List<T>> QueryAsync<T>(dynamic tableQuery, string tableName);
    }
}
