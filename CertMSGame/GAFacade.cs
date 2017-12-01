using System;
using System.Diagnostics;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace CertMSGame
{
	public class GAFacade
	{
		private const string Path = @"D:\Programming\Master\CertMSGA\CertMSGA\bin\Debug\CertMSGA.exe";

		public static RsaKeyParameters GetPublicKey()
		{
			var pbkElements = CallProgramWith(Path, "pbk").Split(';');
			return new RsaKeyParameters(false, new BigInteger(pbkElements[0]), new BigInteger(pbkElements[1]));
		}

		public static string Sign(byte[] request)
		{
			return CallProgramWith(Path, "sign", Convert.ToBase64String(request));
		}

		public static string Verify(Game game)
		{
			return CallProgramWith(Path, "verify", Convert.ToBase64String(game.Data), Convert.ToBase64String(game.Signature));
		}

		private static string CallProgramWith(string program, params string[] arguments)
		{
			var proc = new ProcessStartInfo
			{
				FileName = program,
				Arguments = string.Join(" ", arguments),
				UseShellExecute = false,
				RedirectStandardOutput = true
			};
			string response;
			using (var process = Process.Start(proc))
			{
				using (var reader = process?.StandardOutput)
				{
					response = reader?.ReadToEnd();
				}
			}
			return response;
		}
	}
}
