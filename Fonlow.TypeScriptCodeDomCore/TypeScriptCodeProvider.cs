using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace Fonlow.TypeScriptCodeDom
{
	/// <summary>
	/// TypeScriptCodeProvider has ICodeGenerator implemented for TypeScript. TypeScript CodeDOM provider.
	/// </summary>
	/// <remarks>As stated in msdn, when implementing ICodeGenerator, "you must not call the corresponding method of the base class."</remarks>
	public sealed class TypeScriptCodeProvider : CodeDomProvider, ICodeGenerator
	{
		public TypeScriptCodeProvider(ICodeGenerator generator)
		{
			this.generator = generator;
		}

		readonly ICodeGenerator generator;

		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public override ICodeCompiler CreateCompiler()
		{
			throw new NotImplementedException("TypeScript compiler is not to be supported in CodeDOM.");
		}

		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public override ICodeGenerator CreateGenerator()
		{
			throw new NotImplementedException("CreateGenerator is not to be supported in CodeDOM.");

		}

		public override string FileExtension
		{
			get
			{
				return "ts";
			}
		}

		#region ICodeGenerator

		public override string CreateEscapedIdentifier(string value)
		{
			return generator.CreateEscapedIdentifier(value);
		}

		public override string CreateValidIdentifier(string value)
		{
			return generator.CreateEscapedIdentifier(value);
		}

		public override void GenerateCodeFromCompileUnit(CodeCompileUnit compileUnit, TextWriter writer, CodeGeneratorOptions options)
		{
			generator.GenerateCodeFromCompileUnit(compileUnit, writer, options);
		}

		public override void GenerateCodeFromExpression(CodeExpression expression, TextWriter writer, CodeGeneratorOptions options)
		{
			generator.GenerateCodeFromExpression(expression, writer, options);
		}

		public override void GenerateCodeFromNamespace(CodeNamespace codeNamespace, TextWriter writer, CodeGeneratorOptions options)
		{
			generator.GenerateCodeFromNamespace(codeNamespace, writer, options);
		}

		public override void GenerateCodeFromStatement(CodeStatement statement, TextWriter writer, CodeGeneratorOptions options)
		{
			generator.GenerateCodeFromStatement(statement, writer, options);
		}

		public override void GenerateCodeFromType(CodeTypeDeclaration codeType, TextWriter writer, CodeGeneratorOptions options)
		{
			generator.GenerateCodeFromType(codeType, writer, options);
		}

		public override string GetTypeOutput(CodeTypeReference type)
		{
			return generator.GetTypeOutput(type);
		}

		public override bool IsValidIdentifier(string value)
		{
			return generator.IsValidIdentifier(value);
		}


		public override bool Supports(GeneratorSupport generatorSupport)
		{
			return generator.Supports(generatorSupport);
		}

		/// <summary>
		/// Chick if not keyword, and leave the compiler to validate other factors
		/// </summary>
		/// <exception cref="ArgumentException"/>
		public void ValidateIdentifier(string value)
		{
			generator.ValidateIdentifier(value);
		}

		#endregion
	}
}
