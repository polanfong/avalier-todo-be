using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Avalier.Todo.Host.Controllers.Healthz
{
    public class HealthzControllerTests
    {
        private readonly ITestOutputHelper _output;

        public HealthzControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CanGet()
        {
            var controller = new HealthzController();
            var response = controller.Get();
            var redirectToActionResult = Assert.IsType<OkResult>(response);
        }
    }
}