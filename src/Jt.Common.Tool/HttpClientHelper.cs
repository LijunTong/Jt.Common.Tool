using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Common.Tool
{
    public class HttpClientHelper
    {
        //public static HttpClient httpClient;
        //public stat
        public static string HttpPost(string url, string postData, string postDataName = "PostData", string contentType = "form-data", Dictionary<string, Stream> files = null)
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent multipartContent = new MultipartFormDataContent();
            if (files != null && files.Keys.Count() > 0)
            {
                foreach (var file in files)
                {
                    multipartContent.Add(new StreamContent(file.Value), file.Key, file.Key);
                }
            }
            multipartContent.Add(new StringContent(postData), postDataName);
            var task = httpClient.PostAsync(url, multipartContent);

            if (task.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return task.Result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public static string HttpPost(string url, string contentType = "form-data")
        {
            HttpClient httpClient = new HttpClient();

            var task = httpClient.GetAsync(url);

            if (task.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return task.Result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }
        }

        public static bool HttpDownload(string url, string saveFilePath)
        {
            var stream = HttpDownload(url);
            if (stream != null)
            {
                using (FileStream fileStream = new FileStream(saveFilePath, FileMode.OpenOrCreate))
                {
                    stream.CopyTo(fileStream);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Stream HttpDownload(string url)
        {
            HttpClient httpClient = new HttpClient();
            var task = httpClient.GetAsync(url);
            if (task.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return task.Result.Content.ReadAsStreamAsync().Result;
            }
            else
            {
                return null;
            }
        }
    }
}
