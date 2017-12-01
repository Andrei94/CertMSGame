using System;

namespace CertMSGame
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var response = string.Empty;
			if(args[0].Equals("sign"))
			{
				var pcGame = new PcGame(Convert.FromBase64String(args[1]), GAFacade.GetPublicKey());
				var game = pcGame.CreateGame(Convert.FromBase64String(GAFacade.Sign(pcGame.GenerateGameRequest())));
				response = Convert.ToBase64String(game.Signature);
			}
			else if(args[0].Equals("verify"))
			{
				response = GAFacade.Verify(new Game(Convert.FromBase64String(args[1]), Convert.FromBase64String(args[2])));
			}

			Console.WriteLine(response);
		}
	}
}
