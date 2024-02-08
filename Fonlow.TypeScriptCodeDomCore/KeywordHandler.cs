﻿using System;

namespace Fonlow.TypeScriptCodeDom
{
	/// <summary>
	/// Keywoards not good for identifier in TS
	/// </summary>
	internal static class KeywordHandler
	{
		static readonly System.Collections.Generic.HashSet<string> keywords = new System.Collections.Generic.HashSet<string>(
		   new string[] {
			"break",     "case",      "catch",     "class",
			"const",     "continue",  "debugger",  "default",
			"delete",    "do","else",      "enum",
			"export",    "extends",   "false",     "finally",
			"for",       "function",  "if","import",
			"in","instanceof","new",       "null",
			"return",    "super",     "switch",    "this",
			"throw",     "true",      "try",       "typeof",
			"var",       "void",      "while",     "with",

			//Strict Mode Reserved Words
			"as", "implements","interface", "let",
			"package", "private", "protected", "public",
			"static", "yield", //total 46
		   }

		);

		public static bool IsKeyword(string s)
		{
			return keywords.Contains(s);
		}

		public static string CreateEscapedIdentifier(string value)
		{
			return $"_{value}";
		}

		/// <summary>
		/// Chick if not keyword, and leave the compiler to validate other factors
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsValidIdentifier(string value)
		{
			return !IsKeyword(value); 
		}

		public static string CreateValidIdentifier(string value)
		{
			return keywords.Contains(value) ? CreateEscapedIdentifier(value) : value;
		}

		public static void ValidateIdentifier(string value)
		{
			if (keywords.Contains(value))
				throw new ArgumentException($"Keyword {value} cannot be used as an identifier.", nameof(value));
		}

	}

}
