using Fonlow.TypeScriptCodeDom;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using Xunit;

namespace TypeScriptCodeDomTestsCore
{
	public class CodeObjectHelperTests
	{
		[Fact]
		public void TestDateTimeIsSimpleType()
		{
			Assert.True(CodeObjectHelper.IsSimpleType(typeof(DateTime)));
			Assert.True(CodeObjectHelper.IsSimpleType(typeof(DateTime?)));
			Assert.True(CodeObjectHelper.IsSimpleType(typeof(Nullable<DateTime>)));
		}

		[Fact]
		public void TestStringIsSimpleType()
		{
			Assert.True(CodeObjectHelper.IsSimpleType(typeof(string)));
			Assert.True(CodeObjectHelper.IsSimpleType(typeof(System.String)));
		}

		[Fact]
		public void TestEnumIsSimpleType()
		{
			Assert.True(CodeObjectHelper.IsSimpleType(typeof(DemoWebApi.DemoData.AddressType)));
			Assert.True(CodeObjectHelper.IsSimpleType(typeof(DemoWebApi.DemoData.AddressType?)));
			Assert.True(CodeObjectHelper.IsSimpleType(typeof(Nullable<DemoWebApi.DemoData.AddressType>)));
		}



	}
}
