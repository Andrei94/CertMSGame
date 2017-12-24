using System.Collections.Generic;

namespace CertMSGame
{
	public static class Router
	{
		public static ICommand Route(IReadOnlyList<string> args)
		{
			if (args[0].Equals("sign"))
				return new SignCommand(args);
			if (args[0].Equals("verify"))
				return new VerifyCommand(args);
			return new MalformedCommand();
		}
	}
}
