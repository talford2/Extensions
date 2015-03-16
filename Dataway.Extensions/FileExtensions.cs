using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dataway.Extensions
{
	public static class FileExtensions
	{
		public static string GetFriendlyFileSize(long totalBytes)
		{
			if (totalBytes.ToString().Length > 9)
			{
				return (totalBytes / 1000000000) + " GB";
			}
			if (totalBytes.ToString().Length > 6)
			{
				return (totalBytes / 1000000) + " MB";
			}
			if (totalBytes.ToString().Length > 3)
			{
				return (totalBytes / 1000) + " KB";
			}

			return totalBytes + " B";
		}

		public static string GetFriendlyFileSize(int totalBytes)
		{
			if (totalBytes.ToString().Length > 9)
			{
				return (totalBytes / 1000000000) + " GB";
			}
			if (totalBytes.ToString().Length > 6)
			{
				return (totalBytes / 1000000) + " MB";
			}
			if (totalBytes.ToString().Length > 3)
			{
				return (totalBytes / 1000) + " KB";
			}

			return totalBytes + " B";
		}
	}
}
