using System;

namespace CertMSGame
{
	public static class Program
	{
		public static void Main(string[] args) => Console.WriteLine(Router.Route(args).Execute());
	}
}
