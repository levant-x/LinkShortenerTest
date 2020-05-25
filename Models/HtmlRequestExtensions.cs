using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Models
{
    public static class HtmlRequestExtensions
    {
        public static string ReadRawBodyString(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            using (var reader = new StreamReader(request.Body, encoding)) return reader.ReadToEnd();
        }
    }
}
