﻿using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace Fonlow.TypeScriptCodeDom
{
	public class TsCodeGenerator : ICodeGenerator
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

		void ICodeGenerator.GenerateCodeFromCompileUnit(CodeCompileUnit e, TextWriter w, CodeGeneratorOptions o)
		{
			for (int i = 0; i < e.ReferencedAssemblies.Count; i++)
			{
				w.WriteLine(e.ReferencedAssemblies[i]);
			}

			for (int i = 0; i < e.Namespaces.Count; i++)
			{
				(this as ICodeGenerator).GenerateCodeFromNamespace(e.Namespaces[i], w, o);
				w.WriteLine();
			}
		}

		void ICodeGenerator.GenerateCodeFromExpression(CodeExpression e, TextWriter w, CodeGeneratorOptions o)
		{
			codeObjectHelper.GenerateCodeFromExpression(e, w, o);
		}

		void ICodeGenerator.GenerateCodeFromNamespace(CodeNamespace e, TextWriter w, CodeGeneratorOptions o)
		{
			codeObjectHelper.GenerateCodeFromNamespace(e, w, o);
		}

		void ICodeGenerator.GenerateCodeFromStatement(CodeStatement e, TextWriter w, CodeGeneratorOptions o)
		{
			codeObjectHelper.GenerateCodeFromStatement(e, w, o);
		}

		void ICodeGenerator.GenerateCodeFromType(CodeTypeDeclaration e, TextWriter w, CodeGeneratorOptions o)
		{
			codeObjectHelper.GenerateCodeFromType(e, w, o);
		}

		string ICodeGenerator.GetTypeOutput(CodeTypeReference type)
		{
			return TypeMapper.MapCodeTypeReferenceToTsText(type);
		}

		bool ICodeGenerator.IsValidIdentifier(string value)
		{
			return KeywordHandler.IsValidIdentifier(value);
		}

		bool ICodeGenerator.Supports(GeneratorSupport supports)
		{
			return (supports & supported) != 0;
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
