﻿using NUnit.Framework;
using Jt.Common.Tool.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System.Net;

namespace Jt.Common.Tool.Extension.Tests
{
    [TestFixture()]
    public class HttpClientExtensionTests
    {
        private HttpClient _httpClient;

        public HttpClientExtensionTests()
        {
            _httpClient = new HttpClient();
        }

        [Test()]
        public async Task DoGetAsyncTest()
        {
            var param = new Dictionary<string, object>
            {
                { "name", "apple" },
                { "age", 12 }
            };
            var data = await _httpClient.DoGetAsync("http://localhost:5000/WeatherForecast/GetParam", param);
            Debug.WriteLine(data);
            Assert.IsNotEmpty(data);
        }

        [Test()]
        public async Task DoPostAsyncTest()
        {
            var param = new Dictionary<string, object>
            {
                { "name", "apple" },
                { "age", 12 }
            };
            var data = await _httpClient.DoPostAsync("http://localhost:5000/WeatherForecast/PostParam", param.ToJson());
            Debug.WriteLine(data);
            Assert.IsNotEmpty(data);
        }

        [Test()]
        public async Task DownloadImageAsyncTest()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
            string name = await _httpClient.DownloadImageAsync("https://s4.cdn.jiaonizuocai.com/zhishi/201402/171848104813.jpg/NjQweDA", path,"a.webp");
            Debug.WriteLine(name);
            Assert.IsNotEmpty(name);
        }
    }
}