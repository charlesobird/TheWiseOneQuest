namespace TheWiseOneQuest
{
	class Program
	{
		public static void Main()
		{
			using (var game = new TheWiseOneQuest())
			{
				game.Run();
			}
			// using (var game = new GeonBitUI_Examples()) {
			//     game.Run();
			// }
		}
	}
}