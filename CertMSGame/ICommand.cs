using System.Collections.Generic;
using static CertMSGame.Base64Converter;

namespace CertMSGame
{
	public interface ICommand
	{
		string Execute();
	}

	public class SignCommand : ICommand
	{
		private readonly IReadOnlyList<string> args;

		internal SignCommand(IReadOnlyList<string> args)
		{
			this.args = args;
		}

		public string Execute()
		{
			var pcGame = new PcGame(FromBase64String(args[1]), GAFacade.GetPublicKey(args[2]));
			var game = pcGame.CreateGame(FromBase64String(GAFacade.Sign(pcGame.GenerateGameRequest(), args[2])));
			return ToBase64String(game.Signature);
		}
	}

	public class VerifyCommand : ICommand
	{
		private readonly IReadOnlyList<string> args;

		internal VerifyCommand(IReadOnlyList<string> args)
		{
			this.args = args;
		}

		public string Execute()
		{
			return GAFacade.Verify(new Game(FromBase64String(args[1]), FromBase64String(args[2])), args[3]);
		}
	}

	public class MalformedCommand : ICommand
	{
		public string Execute()
		{
			return string.Empty;
		}
	}
}