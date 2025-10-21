namespace Code.Sample
{
	public static class Identifiers
	{
		private static int nextId = 0;
		
		public static int NextId() => ++nextId;
	}
}