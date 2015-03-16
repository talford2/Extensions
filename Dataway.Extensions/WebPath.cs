using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Dataway.Extensions
{
	public static class WebPath
	{
		public static string Combine(string path, params string[] paths)
		{
			string outval = path.TrimEnd('/').Replace("\\", "/");
			foreach (var addedPath in paths)
			{
				outval += "/" + addedPath.TrimEnd('/').Replace("\\", "/");
			}

			return outval;
		}

		public static string GetFileName(string path)
		{
			var bits = path.Split('/');
			return bits[bits.Length - 1];
		}

		public static string GetFileNameWithoutExtension(string path)
		{
			var filename = GetFileName(path);
			return Path.GetFileNameWithoutExtension(filename);
		}
	}
}
