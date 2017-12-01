using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CertMSGame.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			Program.Main("sign " + Convert.ToBase64String(Encoding.ASCII.GetBytes("call of duty")));
		}
	}
}
