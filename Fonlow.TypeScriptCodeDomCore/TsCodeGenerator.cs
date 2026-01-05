using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace Fonlow.TypeScriptCodeDom
{
	public sealed class TsCodeGenerator : ICodeGenerator
	{
		public TsCodeGenerator(CodeObjectHelper codeObjectHelper)
		{
			this.codeObjectHelper = codeObjectHelper;
		}

		readonly CodeObjectHelper codeObjectHelper;

		string ICodeGenerator.CreateEscapedIdentifier(string value)
		{
			return KeywordHandler.CreateEscapedIdentifier(value);
		}

		string ICodeGenerator.CreateValidIdentifier(string value)
		{
			return KeywordHandler.CreateValidIdentifier(value);
		}

		void ICodeGenerator.GenerateCodeFromCompileUnit(CodeCompileUnit ccu, TextWriter w, CodeGeneratorOptions o)
		{
			for (int i = 0; i < ccu.ReferencedAssemblies.Count; i++)
			{
				w.WriteLine(ccu.ReferencedAssemblies[i]);
			}

			for (int i = 0; i < ccu.Namespaces.Count; i++)
			{
				(this as ICodeGenerator).GenerateCodeFromNamespace(ccu.Namespaces[i], w, o);
				w.WriteLine();
			}
		}

		void ICodeGenerator.GenerateCodeFromExpression(CodeExpression ce, TextWriter w, CodeGeneratorOptions o)
		{
			codeObjectHelper.GenerateCodeFromExpression(ce, w, o);
		}

		void ICodeGenerator.GenerateCodeFromNamespace(CodeNamespace cn, TextWriter w, CodeGeneratorOptions o)
		{
			codeObjectHelper.GenerateCodeFromNamespace(cn, w, o);
		}

		void ICodeGenerator.GenerateCodeFromStatement(CodeStatement cs, TextWriter w, CodeGeneratorOptions o)
		{
			codeObjectHelper.GenerateCodeFromStatement(cs, w, o);
		}

		void ICodeGenerator.GenerateCodeFromType(CodeTypeDeclaration ctd, TextWriter w, CodeGeneratorOptions o)
		{
			codeObjectHelper.GenerateCodeFromType(ctd, w, o);
		}

		string ICodeGenerator.GetTypeOutput(CodeTypeReference ctr)
		{
			return TypeMapper.MapCodeTypeReferenceToTsText(ctr);
		}

		bool ICodeGenerator.IsValidIdentifier(string value)
		{
			return KeywordHandler.IsValidIdentifier(value);
		}

		bool ICodeGenerator.Supports(GeneratorSupport gs)
		{
			return (gs & supported) != 0;
		}

		const GeneratorSupport supported = GeneratorSupport.ArraysOfArrays
			| GeneratorSupport.MultidimensionalArrays
			| GeneratorSupport.TryCatchStatements
			| GeneratorSupport.DeclareValueTypes
			| GeneratorSupport.DeclareEnums
		   // | GeneratorSupport.GotoStatements
			| GeneratorSupport.StaticConstructors
			| GeneratorSupport.DeclareInterfaces
			| GeneratorSupport.DeclareDelegates
		   // | GeneratorSupport.DeclareEvents
			| GeneratorSupport.NestedTypes
			| GeneratorSupport.MultipleInterfaceMembers
			| GeneratorSupport.ComplexExpressions
		   // | GeneratorSupport.PartialTypes
			| GeneratorSupport.GenericTypeReference
			| GeneratorSupport.GenericTypeDeclaration
		   // | GeneratorSupport.DeclareIndexerProperties
		   ;

		void ICodeGenerator.ValidateIdentifier(string value)
		{
			KeywordHandler.ValidateIdentifier(value);
		}

	}
}
