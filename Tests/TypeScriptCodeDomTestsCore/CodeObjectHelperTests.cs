using Fonlow.TypeScriptCodeDom;
using System;
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

		[Fact]
		public void TestIsSimpleTypeWithNull()
		{
			Assert.Throws<ArgumentNullException>(()=>CodeObjectHelper.IsSimpleType(null));
		}

		[Fact]
		public void TestIsSimpleTypeWithCustomgeneric()
		{
			Assert.False(CodeObjectHelper.IsSimpleType(typeof(DemoWebApi.DemoData.MimsResult<string>)));
		}
	}
}
