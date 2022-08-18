using Factum.Shared.Abstractions.Blob;
using Factum.Shared.Infrastructure.Blob.Local;
using Factum.Shared.Infrastructure.Cache;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.Blob
{
    public static class Extensions
    {
        public static IServiceCollection AddLocalBlobStorate(this IServiceCollection services)
        {
            var options = services.GetOptions<LocalBlobStorageOptions>("localBlobStorage");
            services.AddSingleton(options);
            services.AddScoped<IBlobStorage, LocalBlobStorage>();
            return services;
        }
    }
}
