#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Sytel.Common.WinForms.ContentEditors
{
	public class ContentFormats
	{
		public enum ContentFormatEnum
		{
			//must match same letters as in string constants!
			//This enum is created for PropertyGrid
			Text, RTF, HTML, Worksheet, Hierarchy
		}

		public static string ContentFormatEnumToString(ContentFormatEnum noteFormatEnumToConvert)
		{
			string stringValueForEnum;
			switch (noteFormatEnumToConvert)
			{
				case ContentFormatEnum.Hierarchy:
					stringValueForEnum = FORMAT_HIERARCHY;
					break;
				case ContentFormatEnum.HTML:
					stringValueForEnum = FORMAT_HTML;
					break;
				case ContentFormatEnum.RTF:
					stringValueForEnum = FORMAT_RTF;
					break;
				case ContentFormatEnum.Text:
					stringValueForEnum = FORMAT_TEXT;
					break;
				case ContentFormatEnum.Worksheet:
					stringValueForEnum = FORMAT_WORKSHEET;
					break;
				default:
					throw new System.ArgumentOutOfRangeException("noteFormatEnumToConvert", noteFormatEnumToConvert, "Can not convert enum value to string because this value is not recognized");
			}
			return stringValueForEnum;
		}
		public static ContentFormatEnum StringToContentFormatEnum(string stringToConvert)
		{
			ContentFormatEnum enumValueForString;
			switch (stringToConvert)
			{
				case ContentFormats.FORMAT_HIERARCHY:
					enumValueForString = ContentFormatEnum.Hierarchy;
					break;
				case ContentFormats.FORMAT_HTML:
					enumValueForString = ContentFormatEnum.HTML;
					break;
				case ContentFormats.FORMAT_RTF:
					enumValueForString = ContentFormatEnum.RTF;
					break;
				case ContentFormats.FORMAT_TEXT:
					enumValueForString = ContentFormatEnum.Text;
					break;
				case ContentFormats.FORMAT_WORKSHEET:
					enumValueForString = ContentFormatEnum.Worksheet;
					break;
				default:
					throw new System.ArgumentOutOfRangeException("stringToConvert", stringToConvert, "Can not convert string value to enum because this value is not recognized");
			}
			return enumValueForString;
		}


		public const string FORMAT_DEFAULT = "rtf";
		public const string FORMAT_TEXT = "text";
		public const string FORMAT_RTF = "rtf";
		public const string FORMAT_HTML = "html";
		public const string FORMAT_WORKSHEET = "worksheet";
		public const string FORMAT_HIERARCHY = "hierarchy";

		public static string GetFriendlyNameForFormat(string formatConstValue)
		{
			return formatConstValue;	//TODO : Put big Swicth here to return friendly name
		}
	}
}
