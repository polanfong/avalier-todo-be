
```bash
# Go to src folder #
cd src

# Create a test project (xunit) #
dotnet new xunit -n Avalier.Todo.Host.Tests

# Add project to solution #
dotnet sln add ./Avalier.Todo.Host.Tests/Avalier.Todo.Host.Tests.csproj

# Run to make sure everything is ok #
dotnet run --project ./Avalier.Todo.Host.Tests/Avalier.Todo.Host.Tests.csproj

# Add reference to Avalier.Todo.Host #
cd Avalier.Todo.Host.Tests
dotnet add reference ../Avalier.Todo.Host/Avalier.Todo.Host.csproj

# Add dependencies #
dotnet add package Shouldly
```

Create file NoopTest.cs...

```cs
using System;
using Xunit;
using Shouldly;

namespace Avalier.Todo.Host.Tests
{
    public class NoopTest
    {
        [Fact]
        public void TrueShouldBeTrue()
        {
            true.ShouldBeTrue();
        }
    }
}
```

Create file Controllers/VersionControllerTest.cs

```cs
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Avalier.Todo.Host.Controllers.Version
{
    public class VersionControllerTests
    {
        private readonly ITestOutputHelper _output;

        public VersionControllerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CanGet()
        {
            var controller = new VersionController();
            var response = (OkObjectResult)controller.Get();

            _output.WriteLine($"{response.Value}");

            var version = typeof(VersionController).Assembly.GetName().Version;
            response.ShouldNotBeNull();
            response.Value.ShouldBe($"{version.Major}.{version.Minor}");
        }
    }
}
```