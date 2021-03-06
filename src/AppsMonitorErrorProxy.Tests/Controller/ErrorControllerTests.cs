﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Proxy.Controllers;
using Proxy.Models;
using Proxy.Services.Interfaces;

namespace Proxy.Tests.Controller
{
    [TestFixture]
    public class GivenErrorInformation
    {
        private IHttpActionResult _result;
        private Mock<IErrorPoster> _errorPoster;
        private ErrorInformation _errorInfo;
        private Mock<IProxyConfiguration> _configuration;
        private List<string> _urls;

        [TestFixtureSetUp]
        public void WhenPosting()
        {
            _errorInfo = new ErrorInformation
            {
                ErrorId = "errorId",
                Error = "SomeError",
                InfoUrl = "http://error",
                SourceId = "srcId"
            };

            _urls = new List<string>
            {
                "url1",
                "url2"
            };

            _configuration = new Mock<IProxyConfiguration>();
            _configuration
                .SetupGet(c => c.AppMonitorUrls)
                .Returns(_urls);

            _errorPoster = new Mock<IErrorPoster>();

            var controller = new ErrorController(_configuration.Object, _errorPoster.Object);
            _result = controller.Post(_errorInfo);
        }

        [Test]
        public void ThenTheErrorIsAggregated()
        {
            _errorPoster.Verify(p => p.Post(_urls, _errorInfo));
        }

        [Test]
        public void ThenAnOkResponseIsReturned()
        {
            Assert.IsInstanceOf(typeof (OkResult) , _result);
        }
    }
}
