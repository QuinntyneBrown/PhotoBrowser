﻿using MediatR;
using PhotoBrowser.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PhotoBrowser.Features.DigitalAssets.UploadHandlers;
using System.Collections.Specialized;
using System.Net.Http;
using System.IO;
using PhotoBrowser.Model;
using PhotoBrowser.Features.Core;
using static PhotoBrowser.Features.DigitalAssets.Constants;

namespace PhotoBrowser.Features.DigitalAssets
{
    public class UploadDigitalAssetCommand
    {
        public class Request : IRequest<UploadDigitalAssetResponse>
        {
            public InMemoryMultipartFormDataStreamProvider Provider { get; set; }
        }

        public class UploadDigitalAssetResponse
        {
            public ICollection<DigitalAssetApiModel> DigitalAssets { get; set; }
        }

        public class UploadDigitalAssetHandler : IAsyncRequestHandler<Request, UploadDigitalAssetResponse>
        {
            public UploadDigitalAssetHandler(IPhotoBrowserContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<UploadDigitalAssetResponse> Handle(Request request)
            {
                NameValueCollection formData = request.Provider.FormData;
                IList<HttpContent> files = request.Provider.Files;
                List<DigitalAsset> digitalAssets = new List<DigitalAsset>();
                foreach (var file in files)
                {
                    var filename = new FileInfo(file.Headers.ContentDisposition.FileName.Trim(new char[] { '"' })
                        .Replace("&", "and")).Name;
                    Stream stream = await file.ReadAsStreamAsync();
                    var bytes = StreamHelper.ReadToEnd(stream);
                    var digitalAsset = new DigitalAsset();
                    digitalAsset.FileName = filename;
                    digitalAsset.Bytes = bytes;
                    digitalAsset.ContentType = System.Convert.ToString(file.Headers.ContentType);
                    _context.DigitalAssets.Add(digitalAsset);
                    digitalAssets.Add(digitalAsset);
                }

                await _context.SaveChangesAsync();

                _cache.Add(null, DigitalAssetCacheKeys.DigitalAssets);

                return new UploadDigitalAssetResponse()
                {
                    DigitalAssets = digitalAssets.Select(x => DigitalAssetApiModel.FromDigitalAsset(x)).ToList()
                };
            }

            private readonly IPhotoBrowserContext _context;
            private readonly ICache _cache;
            
        }

    }

}
