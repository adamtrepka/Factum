using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Factum.Shared.Abstractions.Blob
{
    public interface IBlobStorage
    {
        public Task UploadAsync(Stream stream, string containerName, string fileName, CancellationToken canellationToken = default);
        public Task DownloadAsync(Stream stream, string containerName, string fileName, CancellationToken canellationToken = default);
    }
}
