using System;
using System.Collections.Generic;
using Xunit;
using static CertMSGame.Base64Converter;
using static Xunit.Assert;

namespace CertMSGame.Tests
{
	public class RouterTest
	{
		[Fact]
		public void SignatureIsValid()
		{
			var signature = Router.Route(new List<string> {"sign", ToBase64String("call of duty")}).Execute();
			var isValid = Router.Route(new List<string> {"verify", ToBase64String("call of duty"), signature}).Execute();
			True(Convert.ToBoolean(isValid));
		}

		[Fact]
		public void InvalidSignature()
		{
			var signature = Router.Route(new List<string> {"sign", ToBase64String("call of duty")}).Execute();
			var isValid = Router.Route(new List<string> {"verify", ToBase64String("call of duty 2"), signature}).Execute();
			False(Convert.ToBoolean(isValid));
		}

		[Fact]
		public void UnknownCommand()
		{
			var signature = Router.Route(new List<string> {"kill"}).Execute();
			True(string.IsNullOrEmpty(signature));
		}
	}
}