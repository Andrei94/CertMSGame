using System;
using System.Text;

namespace CertMSGame
{
	public static class Base64Converter
	{
		public static byte[] FromBase64String(string data) => Convert.FromBase64String(data);
		public static string ToBase64String(string data) => ToBase64String(Encoding.ASCII.GetBytes(data));
		public static string ToBase64String(byte[] data) => Convert.ToBase64String(data);
	}
}
