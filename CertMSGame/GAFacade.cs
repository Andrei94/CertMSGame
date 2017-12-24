using System;
using System.Collections.Generic;
using System.Net.Http;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace CertMSGame
{
	public class GAFacade
	{
		private const string Path = "CertMSGA";

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
			var client = new HttpClient();
			var values = new Dictionary<string, string>();
			for(var i=0; i < arguments.Length; i++)
				values.Add(i.ToString(), arguments[i]);
			var content = new FormUrlEncodedContent(values);

			var response = client.PostAsync($"http://localhost:8080/start/{program}", content).Result;

			var responseString = response.Content.ReadAsStringAsync().Result;
			return responseString;
		}
	}
}
