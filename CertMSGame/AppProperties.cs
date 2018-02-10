using System.Configuration;

namespace CertMSGame
{
	internal static class AppProperties
	{
		internal static string GameAuthority => ConfigurationManager.AppSettings["GA"];
		internal static string Pbk => ConfigurationManager.AppSettings["pbk"];
		internal static string Sign => ConfigurationManager.AppSettings["sign"];
		internal static string Verify => ConfigurationManager.AppSettings["verify"];
	}
}
