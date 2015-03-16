using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Dataway.Extensions.Encryption
{
	public static class Encryption
	{
		private static string EncryptionKey
		{
			get
			{
				return "EncryptionKeyGoesHere";
			}
		}

		private static string Salt
		{
			get
			{
				return "ThisIsSalt";
			}
		}

		public static string GetEncrypted(string value)
		{
			byte[] plainText = Encoding.UTF8.GetBytes(value);

			using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
			{
				PasswordDeriveBytes secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes(EncryptionKey), Encoding.ASCII.GetBytes(Salt));
				using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
						{
							cryptoStream.Write(plainText, 0, plainText.Length);
							cryptoStream.FlushFinalBlock();
							string base64 = Convert.ToBase64String(memoryStream.ToArray());

							return base64;
						}
					}
				}
			}
		}

		public static string GetDecrypted(string value)
		{
			byte[] encryptedData = Convert.FromBase64String(value);
			PasswordDeriveBytes secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes(EncryptionKey), Encoding.ASCII.GetBytes(Salt));

			using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
			{
				using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
				{
					using (MemoryStream memoryStream = new MemoryStream(encryptedData))
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
						{
							byte[] plainText = new byte[encryptedData.Length];

							String outputValue = String.Empty;
							using (MemoryStream outputStream = new MemoryStream())
							{
								byte[] buffer = new byte[1024];
								int readCount = 0;
								while ((readCount = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
								{
									outputStream.Write(buffer, 0, readCount);
								}

								return System.Text.Encoding.UTF8.GetString(outputStream.ToArray());
							}
						}
					}
				}
			}
		}

		public static string GetOneWayEncryption(string value)
		{
			return Convert.ToBase64String(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.Default.GetBytes(value)));
		}

		public static string GenerateRandomString(int minChars, int maxChars)
		{
			string chars = "1234567890abcdefghijklmnopqrstuvwxyz";

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
	}
}
