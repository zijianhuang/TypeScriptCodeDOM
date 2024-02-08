﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace Fonlow.TypeScriptCodeDom
{
	/// <summary>
	/// Map CLR types to TS types
	/// </summary>
	public static class TypeMapper
	{
		static readonly Dictionary<string, string> typeMap = new Dictionary<string, string>()
		{
			{typeof(int).FullName, "number"},
			{typeof(uint).FullName, "number"},
			{typeof(long).FullName, "number"},
			{typeof(ulong).FullName, "number"},
			{typeof(short).FullName, "number"},
			{typeof(ushort).FullName, "number"},
			{typeof(byte).FullName, "number"},
			{typeof(sbyte).FullName, "number"}, //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types

			{typeof(float).FullName, "number"},
			{typeof(double).FullName, "number"},
			{typeof(decimal).FullName, "number"}, //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types

			{typeof(string).FullName, "string"},
			{typeof(char).FullName, "string"},
			{typeof(bool).FullName, "boolean"}, //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types

			{typeof(Guid).FullName, "string"},
			{typeof(void).FullName, "void"},
			{typeof(object).FullName, "any"},
			{typeof(DateTime).FullName, "Date"},
			{typeof(DateTimeOffset).FullName, "Date"},
			{typeof(DateOnly).FullName, "Date"},
			{typeof(Uri).FullName, "string"},
			//TimeSpan is not so supported in Javascript
		};


		static bool IsArrayType(CodeTypeReference codeTypeReference)
		{
			return codeTypeReference.ArrayElementType != null;
		}

		static bool IsKeyValuePairType(string typeName)
		{
			return typeName.Contains("System.Collections.Generic.KeyValuePair");
		}

		static readonly string typeNameOfObject = typeof(object).FullName;

		internal static bool IsValidTypeForDerivation(CodeTypeReference type)
		{
			return !type.BaseType.Equals(typeNameOfObject);
		}

		public static string MapToTsBasicType(Type type)
		{
			if (type == null)
				throw new ArgumentNullException(nameof(type));

			if (String.IsNullOrEmpty(type.FullName)) //Custom generic type has fullname empty at this point, since it is just a template defined in the assembly.
			{
				System.Diagnostics.Debug.WriteLine("In TypeMapper, The type is an argument of a generic definition: " + type.ToString());
				return type.ToString();
			}

			if (typeMap.TryGetValue(type.FullName, out string tsTypeName))
				return tsTypeName;

			if (type.IsEnum) // Generally this is .net System. enum types.
			{
				return "number";
			}

			return null;
		}

		/// <summary>
		/// Get the TypeScript text of the CodeTypeReference
		/// </summary>
		/// <param name="codeTypeReference">CodeTypeReference to a CLR type</param>
		/// <returns></returns>
		public static string MapCodeTypeReferenceToTsText(CodeTypeReference codeTypeReference)
		{
			if (codeTypeReference == null)
				return null;

			System.Diagnostics.Debug.WriteLine($"parameter.type.BaseType: {codeTypeReference.BaseType}  ArgumentCount: {codeTypeReference.TypeArguments.Count}, ArrayRank: {codeTypeReference.ArrayRank}");
			System.Diagnostics.Debug.WriteLineIf(codeTypeReference.BaseType == "System.Void", "For this void type :" + codeTypeReference.ToString());
			if (IsArrayType(codeTypeReference))
			{
				var rank = codeTypeReference.ArrayRank;
				if (rank > 1)
				{
					return codeTypeReference.BaseType + new System.Text.StringBuilder().Insert(0, "[]", rank).ToString();
				}

				var elementTypeName = MapCodeTypeReferenceToTsText(codeTypeReference.ArrayElementType);
				return $"Array<{elementTypeName}>";
			}

			if (typeMap.TryGetValue(codeTypeReference.BaseType, out string tsTypeName))
				return tsTypeName;


			if (codeTypeReference.TypeArguments.Count == 0)
			{
				if (codeTypeReference is CodeSnipetTypeReference codeSnipetTypeReference)
				{
					return codeSnipetTypeReference.BaseType;
				}

				return codeTypeReference.BaseType;
			}

			if (IsKeyValuePairType(codeTypeReference.BaseType))
			{
				System.Diagnostics.Debug.Assert(codeTypeReference.TypeArguments.Count == 2);
				var keyTypeReferenceText = MapCodeTypeReferenceToTsText(codeTypeReference.TypeArguments[0]);
				var valueTypeReferenceText = MapCodeTypeReferenceToTsText(codeTypeReference.TypeArguments[1]);
				return TsCodeGenerationOptions.Instance.CamelCase ?
					$"{{key: {keyTypeReferenceText}, value: {valueTypeReferenceText} }}"
					: $"{{Key: {keyTypeReferenceText}, Value: {valueTypeReferenceText} }}";
			}

			if (codeTypeReference.BaseType.Contains("System.Collections.Generic.Dictionary"))
			{
				System.Diagnostics.Debug.Assert(codeTypeReference.TypeArguments.Count == 2);
				var keyTypeReferenceText = MapCodeTypeReferenceToTsText(codeTypeReference.TypeArguments[0]);
				var valueTypeReferenceText = MapCodeTypeReferenceToTsText(codeTypeReference.TypeArguments[1]);
				return TsCodeGenerationOptions.Instance.CamelCase ?
					$"{{[id: {keyTypeReferenceText}]: {valueTypeReferenceText} }}"
					: $"{{[Id: {keyTypeReferenceText}]: {valueTypeReferenceText} }}";
			}

			if (codeTypeReference.BaseType.Contains("System.Tuple"))
			{
				return $"{{{MapCodeTypeReferenceCollectionToTupleTsText(codeTypeReference.TypeArguments)}}}";
			}

			if (codeTypeReference.TypeArguments.Count > 0)
			{
				var baseTypeIndexOf = codeTypeReference.BaseType.IndexOf('`');
				if (baseTypeIndexOf >= 0)
				{
					var genericTypeName = codeTypeReference.BaseType.Substring(0, baseTypeIndexOf);
					return $"{genericTypeName}<{MapCodeTypeReferenceCollectionToTsText(codeTypeReference.TypeArguments)}>";
				}
			}

			System.Diagnostics.Trace.TraceWarning($"{codeTypeReference.BaseType} is mapped to any.");
			return "any";
		}

		/// <summary>
		/// Get text of CodeTypeReferenceCollection in CSV
		/// </summary>
		/// <param name="collection"></param>
		/// <returns></returns>
		internal static string MapCodeTypeReferenceCollectionToTsText(CodeTypeReferenceCollection collection)
		{
			string[] ss = new string[collection.Count];
			for (int i = 0; i < collection.Count; i++)
			{
				ss[i] = MapCodeTypeReferenceToTsText(collection[i]);
			}
			return String.Join(", ", ss);
		}

		internal static string MapCodeTypeReferenceCollectionToTupleTsText(CodeTypeReferenceCollection collection)
		{
			if (collection.Count > 8)
				throw new ArgumentException("Current supports only up to 8 members for tuple.", nameof(collection));
			string[] ss = new string[collection.Count];
			for (int i = 0; i < collection.Count; i++)
			{
				var typeName = MapCodeTypeReferenceToTsText(collection[i]);
				var propertyName = (i < 7) ? "Item" + (i + 1).ToString() : "Rest";
				ss[i] = (TsCodeGenerationOptions.Instance.CamelCase ? Fonlow.Text.StringExtensions.ToCamelCase(propertyName) : propertyName)
					+ ": " + typeName;
			}
			return String.Join(", ", ss);
		}
	}
}
