using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dataway.Extensions
{
	public static class StringExtensions
	{
		private static string Alphabet
		{
			get
			{
				return "abcdegfhijklmnopqrstuvwxyz";
			}
		}

		#region String Extensions

		public static string LimitToCharacterCount(this string stringObj, int characterLimit)
		{
			return LimitToCharacterCount(stringObj, characterLimit, true);
		}

		public static string LimitToCharacterCount(this string stringObj, int characterLimit, bool toNearestWord)
		{
			if (characterLimit < 1)
			{
				throw new ArgumentOutOfRangeException("Character limit must be greater than 0.");
			}

			if (stringObj == null || stringObj.Length <= characterLimit)
			{
				return stringObj;
			}

			var fixStr = stringObj.Trim();
			if (toNearestWord)
			{
				int endIndex = 0;
				for (int i = characterLimit; i > 0; i--)
				{
					if (stringObj[i] == ' ')
					{
						endIndex = i;
						break;
					}
				}

				if (endIndex == 0)
				{
					if (stringObj.Length > characterLimit)
					{
						endIndex = characterLimit;
					}
				}

				fixStr = stringObj.Substring(0, endIndex);
			}
			else
			{
				fixStr = stringObj.Substring(0, characterLimit);
			}

			return string.Format("{0}...", fixStr.TrimEnd(',').TrimEnd('.').TrimEnd());
		}

		public static string LimitToWordCount(this string stringObj, int wordCount)
		{
			if (wordCount < 1)
			{
				throw new ArgumentOutOfRangeException("Word count must greater than 0.");
			}

			if (stringObj == null || stringObj.Length == 0)
			{
				return stringObj;
			}

			var fixStr = stringObj.Trim();

			var words = stringObj.Split(' ');

			if (words.Length <= wordCount)
			{
				return fixStr;
			}
			else
			{
				fixStr = "";
				for (int i = 0; i < wordCount; i++)
				{
					fixStr += words[i] + " ";
				}
				fixStr = string.Format("{0}...", fixStr.TrimEnd());
			}

			return fixStr;
		}

		public static string ReplaceNth(this string stringObj, string findString, string replaceString, int nth)
		{
			string finalStr = stringObj;
			int foundCount = 0;

			for (int i = 0; i < stringObj.Length; i++)
			{
				if (stringObj.Substring(i, findString.Length) == findString)
				{
					if (foundCount == nth)
					{
						finalStr = stringObj.Substring(0, i) + replaceString + stringObj.Substring(i + findString.Length);
						break;
					}
					foundCount++;
				}
			}
			return finalStr;
		}

		public static string ToTitleCase(this string stringObj)
		{
			if (string.IsNullOrWhiteSpace(stringObj))
			{
				return stringObj;
			}

			string newString = stringObj;
			newString = stringObj;

			var bits = newString.Split(' ');

			string rebuiltString = "";
			for (int i = 0; i < bits.Length; i++)
			{
				if (!string.IsNullOrWhiteSpace(bits[i]))
				{
					rebuiltString += string.Format("{0}{1} ", bits[i][0].ToString().ToUpper(), bits[i].Substring(1));
				}
			}

			return rebuiltString.TrimEnd();
		}

		public static string ToCamelCase(this string stringObj)
		{
			var pascal = ToPascalCase(stringObj);
			return pascal[0].ToString().ToUpper() + pascal.Substring(1);
		}

		public static string ToPascalCase(this string stringObj)
		{
			if (string.IsNullOrWhiteSpace(stringObj))
			{
				return stringObj;
			}

			string cleanedStr = stringObj.ToLower();

			foreach (var letter in cleanedStr.Distinct())
			{
				if (!(Alphabet + " ").Contains(letter))
				{
					cleanedStr = cleanedStr.Replace(letter.ToString(), "");
				}
			}

			var words = cleanedStr.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

			string newString = "";

			foreach (var word in words)
			{
				newString += word[0].ToString().ToUpper() + word.Substring(1);
			}

			return newString;
		}

		public static string Slugify(this string stringObj)
		{
			if (!string.IsNullOrWhiteSpace(stringObj))
			{
				string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXWZ1234567890 -";

				string slugified = stringObj.ToLower().Trim().Replace("  ", " ").Replace("/", "-").Replace(" - ", "-").Replace(' ', '-').Replace("--", "-").Replace("&", "and").Replace("%", "percent");

				foreach (var character in slugified)
				{
					if (!validCharacters.Contains(character))
					{
						slugified = slugified.Replace(character + "", "");
					}
				}

				return slugified;
			}
			return stringObj;
		}

		public static string HtmlToText(this string stringObj)
		{
			return Regex.Replace(stringObj, @"<(.|\n)*?>", string.Empty);
		}

		public static string MakePluralName(this string stringObj)
		{
			if ((stringObj.EndsWith("x", StringComparison.OrdinalIgnoreCase) || stringObj.EndsWith("ch", StringComparison.OrdinalIgnoreCase)) || (stringObj.EndsWith("ss", StringComparison.OrdinalIgnoreCase) || stringObj.EndsWith("sh", StringComparison.OrdinalIgnoreCase)))
			{
				stringObj = stringObj + "es";
				return stringObj;
			}
			if ((stringObj.EndsWith("y", StringComparison.OrdinalIgnoreCase) && (stringObj.Length > 1)) && !stringObj[stringObj.Length - 2].IsVowel())
			{
				stringObj = stringObj.Remove(stringObj.Length - 1, 1);
				stringObj = stringObj + "ies";
				return stringObj;
			}
			if (!stringObj.EndsWith("s", StringComparison.OrdinalIgnoreCase))
			{
				stringObj = stringObj + "s";
			}
			return stringObj;
		}

		public static string[] Split(this string stringObj, string splitString)
		{
			return stringObj.Split(new string[] { splitString }, StringSplitOptions.None);
		}

		public static byte[] GetBytes(this string stringObj)
		{
			byte[] bytes = new byte[stringObj.Length * sizeof(char)];
			System.Buffer.BlockCopy(stringObj.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		#endregion

		public static string GenerateRandomString(int minChars, int maxChars)
		{
			return GenerateRandomString("1234567890abcdefghijklmnopqrstuvwxyz", minChars, maxChars);
		}

		public static string GenerateRandomString(string chars, int minChars, int maxChars)
		{
			Random r = new Random(DateTime.Now.Millisecond);

			int charCount = r.Next(minChars, maxChars);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < charCount; i++)
			{
				int index = r.Next(0, chars.Length - 1);
				sb.Append(chars[index]);
			}

			return sb.ToString();
		}

		private static bool IsVowel(this char c)
		{
			switch (c)
			{
				case 'O':
				case 'U':
				case 'Y':
				case 'A':
				case 'E':
				case 'I':
				case 'o':
				case 'u':
				case 'y':
				case 'a':
				case 'e':
				case 'i':
					return true;
			}
			return false;
		}
	}
}
