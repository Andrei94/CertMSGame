using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;

namespace CertMSGame
{
	public class PcGame
	{
		private readonly byte[] gameData;
		private readonly RsaBlindingParameters blindingParams;

		public PcGame(byte[] gameData, RsaKeyParameters pub)
		{
			this.gameData = gameData;

			var blindingFactorGenerator = new RsaBlindingFactorGenerator();
			blindingFactorGenerator.Init(pub);

			var blindingFactor = blindingFactorGenerator.GenerateBlindingFactor();
			blindingParams = new RsaBlindingParameters(pub, blindingFactor);
		}

		public byte[] GenerateGameRequest()
		{
			var signer = new PssSigner(new RsaBlindingEngine(), new Sha512Digest(), 20);
			signer.Init(true, blindingParams);

			signer.BlockUpdate(gameData, 0, gameData.Length);

			return signer.GenerateSignature();
		}

		public Game CreateGame(byte[] signature)
		{
			var blindingEngine = new RsaBlindingEngine();
			blindingEngine.Init(false, blindingParams);

			return new Game(gameData, blindingEngine.ProcessBlock(signature, 0, signature.Length));
		}
	}
}