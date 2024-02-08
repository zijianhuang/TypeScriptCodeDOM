using Fonlow.TypeScriptCodeDom;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using Xunit;

namespace TypeScriptCodeDomTestsCore
{
	public class TypeMapperTests
	{
		[Fact]
		public void TestIsSimpleSystemType()
		{
			Assert.True(TypeMapper.IsSimpleSystemType(typeof(DateTime)));
		}

		[Fact]
		public void TestIsSimpleSystemTypeWithTypeFullName()
		{
			Assert.True(TypeMapper.IsSimpleSystemType("System.DateTime"));
		}
	}
}
