﻿using System.Collections.Specialized;
using System.Net;
using Proxy.Services.Interfaces;

namespace Proxy.Services
{
    public class WebClientWrapper : IWebClientWrapper
    {
        public void UploadValues(string url, NameValueCollection values)
        {
            using (var wc = new WebClient())
                wc.UploadValues(url, values);
        }
    }
}
