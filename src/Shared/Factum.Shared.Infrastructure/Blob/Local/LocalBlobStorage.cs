using Factum.Shared.Abstractions.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Shared.Infrastructure.Blob.Local
{
    internal class LocalBlobStorage : IBlobStorage
    {
        private readonly LocalBlobStorageOptions _options;

        public LocalBlobStorage(LocalBlobStorageOptions options)
        {
            _options = options;
        }
        public async Task DownloadAsync(Stream stream, string containerName, string fileName, CancellationToken canellationToken = default)
        {
            var filePath = Path.Combine(_options.RootDirectory,containerName,fileName);

            using var fileStream = File.Open(filePath, FileMode.Open);

            fileStream.Seek(0, SeekOrigin.Begin);
            await stream.CopyToAsync(stream, canellationToken);
        }

        public async Task UploadAsync(Stream stream, string containerName, string fileName, CancellationToken canellationToken = default)
        {
            var containerPath = Path.Combine(_options.RootDirectory, containerName);
            var filePath = Path.Combine(containerPath, fileName);

            if(!Directory.Exists(containerPath))
            {
                Directory.CreateDirectory(containerPath);
            }

            using var fileStream = File.Create(filePath);

            stream.Seek(0, SeekOrigin.Begin);
            await stream.CopyToAsync(fileStream,canellationToken);
        }
    }
}
