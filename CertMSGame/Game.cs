namespace CertMSGame
{
	public class Game
	{
		public byte[] Data { get; }
		public byte[] Signature { get; }

		public Game(byte[] data, byte[] signature)
		{
			Data = data;
			Signature = signature;
		}
	}
}