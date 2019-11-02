using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UrlClicks.Persistence;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static void AddPersistance(this
            IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<UrlClickDbContext>(opt =>
                opt.UseSqlServer(connectionString));
        }
    }
}
