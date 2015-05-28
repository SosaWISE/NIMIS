using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NXS.Lib.Web.Test
{
	public class WebConfigTests
	{
		[Fact]
		public void TestWebConfigInit()
		{
			var dir = AppDomain.CurrentDomain.BaseDirectory;
			System.IO.File.WriteAllText(Path.Combine(dir, "testenv"), @"bob");
			System.IO.File.WriteAllText(Path.Combine(dir, "testconfig.b.json"), @"{
  ""InheritEnv"": """",
  ""MiddleName"": ""B"",
}");
			System.IO.File.WriteAllText(Path.Combine(dir, "testconfig.bobbins.json"), @"{
  ""InheritEnv"": ""b"",
  ""FirstName"": ""Hank"",
  ""LastName"": ""Bobbins"",
}");
			System.IO.File.WriteAllText(Path.Combine(dir, "testconfig.bob.json"), @"{
  ""InheritEnv"": ""bobbins"",
  ""FirstName"": ""Bob"",
}");

			WebConfig.Init(dir, filePrefix: "test");
			Assert.Equal(null, WebConfig.Instance.GetConfig("BaseEnv"));
			Assert.Equal("Bob", WebConfig.Instance.GetConfig("FirstName"));
			Assert.Equal("B", WebConfig.Instance.GetConfig("MiddleName"));
			Assert.Equal("Bobbins", WebConfig.Instance.GetConfig("LastName"));
		}
	}
}
