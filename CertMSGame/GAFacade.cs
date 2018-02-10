using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace CertMSGame
{
    public static class GAFacade
    {
        public static RsaKeyParameters GetPublicKey(string correlationId)
        {
            var pbkElements = CallGameAuthorityWith(AppProperties.Pbk, correlationId).Split(';');
            return new RsaKeyParameters(false, new BigInteger(pbkElements[0]), new BigInteger(pbkElements[1]));
        }

        public static string Sign(byte[] request, string correlationId)
        {
            return CallGameAuthorityWith(AppProperties.Sign, Convert.ToBase64String(request), correlationId);
        }

        public static string Verify(Game game, string correlationId)
        {
            return CallGameAuthorityWith(AppProperties.Verify, Convert.ToBase64String(game.Data),
                Convert.ToBase64String(game.Signature), correlationId);
        }

        private static string CallGameAuthorityWith(params string[] arguments)
        {
            var client = new HttpClient();
            var values = new Dictionary<string, string>();
            for (var i = 0; i < arguments.Length - 1; i++)
                values.Add(i.ToString(), arguments[i]);
            values.Add("correlationId", arguments[arguments.Length - 1]);
            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync($"http://localhost:8080/start/{AppProperties.GameAuthority}", content)
                .Result;

            var responseString = response.Content.ReadAsStringAsync().Result;
            return responseString;
        }
    }
}