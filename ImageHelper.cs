using System;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.StaticFiles;

namespace LanguageExchangeHub1
{
	public static class ImageHelper
	{
        public static byte[] ConvertIFormFileToByteArray(this IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static IFormFile ConvertByteArrayToIFormFile(this byte[] array, string fileName)
        {

            using (var memoryStream = new MemoryStream(array))
            {
                var contentTypeProvider = new FileExtensionContentTypeProvider();
                if(!contentTypeProvider.TryGetContentType(fileName,out string contentType))
                {
                    contentType = "application/octet-stream";
               }
                return new FormFile(memoryStream, 0, memoryStream.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType,
                    ContentDisposition =  $"form-data; name=\"ImagePreview\"; filename=\"{fileName}\""
                };
            }
        }

        public static string GetContentType(this string fileName)
        {
            var contentTypeProvider = new FileExtensionContentTypeProvider();
            if (!contentTypeProvider.TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/oktet-stream";
            }
            return contentType;
        }



    }
}

