using System;

namespace Fonlow.TypeScriptCodeDom
{
	/// <summary>
	/// Used in UserData for passing type info of a field to a code generator.
	/// </summary>
	public class FieldTypeInfo
	{
		public bool IsComplex { get; set; }

		public bool IsArray { get; set; }
		//public string TypeFullName { get; set; }
		/// <summary>
		/// Optional store Clr type for better mapping in code generators
		/// </summary>
		public Type ClrType { get; set; }
	}
}
