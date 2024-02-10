using Fonlow.TypeScriptCodeDom;
using System;
using Xunit;

namespace TypeScriptCodeDomTestsCore
{
	public class TypeMapperTests
	{
		[Fact]
		public void TestIsSimpleSystemType()
		{
			Assert.True(TypeMapper.IsSimpleSystemType(typeof(DateTime).FullName));
		}

		[Fact]
		public void TestIsSimpleSystemTypeWithVoid()
		{
			Assert.True(TypeMapper.IsSimpleSystemType(typeof(void).FullName));
		}

		[Fact]
		public void TestIsSimpleSystemTypeWithNull()
		{
			Assert.Throws<ArgumentNullException>(()=>TypeMapper.IsSimpleSystemType(null));
		}

		[Fact]
		public void TestIsSimpleSystemTypeWithSomethingElse()
		{
			Assert.False(TypeMapper.IsSimpleSystemType("System.SomethingElse"));
		}

	}
}
