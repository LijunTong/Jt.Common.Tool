using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Jt.Common.Tool.Extension
{
    public static class HttpClientExtension
    {
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="url">请求地址</param>
        /// <param name="query">请求参数</param>
        /// <param name="header">请求头</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">请求地址为空异常</exception>
        /// <exception cref="TimeoutException">超时异常</exception>
        /// <exception cref="Exception">其它异常</exception>
        public static async Task<string> DoGetAsync(this HttpClient client, string url, Dictionary<string, object> query = null, Dictionary<string,string> header = null, int timeout = 10000, CancellationToken cancellationToken = default)
        {
            if (url.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("url");
            }

            if (query != null)
            {
                url = CombineUrlWithQueryString(url, query);
            }

            if (header != null)
            {
                foreach (var item in header)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            HttpResponseMessage respone;

            // 为了尽量减少等待消耗，设置超时（HttpClient默认是100s）
            var ctsTimeout = new CancellationTokenSource();
            ctsTimeout.CancelAfter(TimeSpan.FromMilliseconds(timeout));
            try
            {
                if (cancellationToken == default)
                {
                    respone = await client.GetAsync(url, ctsTimeout.Token);
                }
                else
                {
                    var ctsAltogather = CancellationTokenSource.CreateLinkedTokenSource(ctsTimeout.Token, cancellationToken);
                    respone = await client.GetAsync(url, ctsAltogather.Token);
                }
            }
            catch (Exception ex)
            {
                if (ex is TaskCanceledException && ctsTimeout.Token.IsCancellationRequested)
                {
                    throw new TimeoutException("请求服务器超时", ex);
                }
                else
                {
                    throw new Exception("请求服务器错误或网络错误", ex);
                }
            }

            var resultData = await respone.Content.ReadAsStringAsync();
            try
            {
                respone.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception("服务器响应错误，内容：" + resultData, ex);
            }

            return resultData;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="url">请求地址</param>
        /// <param name="content">请求参数</param>
        /// <param name="header">请求头</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">请求地址为空异常</exception>
        /// <exception cref="TimeoutException">超时异常</exception>
        /// <exception cref="Exception"></exception>
        public static async Task<string> DoPostAsync(this HttpClient client, string url, string content, Dictionary<string, string> header = null, int timeOut = 10000, CancellationToken cancellationToken = default)
        {
            if (url.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("url");
            }

            if (header != null)
            {
                foreach (var item in header)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            var reqContent = new StringContent(content, Encoding.UTF8, "application/json");

            // 为了尽量减少等待消耗，设置超时（HttpClient默认是100s）
            var ctsTimeout = new CancellationTokenSource();
            ctsTimeout.CancelAfter(TimeSpan.FromMilliseconds(timeOut));

            HttpResponseMessage resp;
            try
            {
                if (cancellationToken == default)
                {
                    resp = await client.PostAsync(url, reqContent, ctsTimeout.Token);
                }
                else
                {
                    var ctsAltogather = CancellationTokenSource.CreateLinkedTokenSource(ctsTimeout.Token, cancellationToken);
                    resp = await client.PostAsync(url, reqContent, ctsAltogather.Token);
                }
            }
            catch (Exception ex)
            {
                if (ex is TaskCanceledException && ctsTimeout.Token.IsCancellationRequested)
                {
                    throw new TimeoutException("请求服务器超时", ex);
                }
                else
                {
                    throw new Exception("请求服务器错误或网络错误", ex);
                }
            }

            var resultData = await resp.Content.ReadAsStringAsync();
            try
            {
                resp.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception("服务器响应错误，内容：" + resultData, ex);
            }

            return resultData;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="url">请求地址</param>
        /// <param name="content">请求参数</param>
        /// <param name="header">请求头</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">请求地址为空异常</exception>
        /// <exception cref="TimeoutException">超时异常</exception>
        /// <exception cref="Exception"></exception>
        public static async Task<string> DoPostAsync(this HttpClient client, string url, HttpContent content, Dictionary<string, string> header = null, int timeOut = 10000, CancellationToken cancellationToken = default)
        {
            if (url.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("url");
            }

            if (header != null)
            {
                foreach (var item in header)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            // 为了尽量减少等待消耗，设置超时（HttpClient默认是100s）
            var ctsTimeout = new CancellationTokenSource();
            ctsTimeout.CancelAfter(TimeSpan.FromMilliseconds(timeOut));

            HttpResponseMessage resp;
            try
            {
                if (cancellationToken == default)
                {
                    resp = await client.PostAsync(url, content, ctsTimeout.Token);
                }
                else
                {
                    var ctsAltogather = CancellationTokenSource.CreateLinkedTokenSource(ctsTimeout.Token, cancellationToken);
                    resp = await client.PostAsync(url, content, ctsAltogather.Token);
                }
            }
            catch (Exception ex)
            {
                if (ex is TaskCanceledException && ctsTimeout.Token.IsCancellationRequested)
                {
                    throw new TimeoutException("请求服务器超时", ex);
                }
                else
                {
                    throw new Exception("请求服务器错误或网络错误", ex);
                }
            }

            var resultData = await resp.Content.ReadAsStringAsync();
            try
            {
                resp.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw new Exception("服务器响应错误，内容：" + resultData, ex);
            }

            return resultData;
        }

        /// <summary>
        /// 从远程下载图片
        /// </summary>
        /// <param name="client">客户端</param>
        /// <param name="url">请求地址</param>
        /// <param name="saveFold">保存路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static async Task<string> DownloadImageAsync(this HttpClient client, string url, string saveFold, string fileName)
        {
            saveFold = Path.Combine(saveFold, fileName);
            byte[] imageBytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(saveFold, imageBytes);
            return saveFold;
        }

        private static string CombineUrlWithQueryString(string url, Dictionary<string, object> paramDic)
        {
            var isEmptyParamDic = paramDic == null || !paramDic.Any();

            if (isEmptyParamDic)
            {
                return url;
            }

            var builder = new UriBuilder(url);

            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (var item in paramDic)
            {
                query[item.Key] = item.Value.ToString();
            }

            builder.Query = query.ToString();
            return builder.ToString();
        }
    }
}
