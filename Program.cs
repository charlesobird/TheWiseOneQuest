namespace TheWiseOneQuest
{
    class Program
    {
        static bool debug = false;

        public static void Main()
        {
            if (debug)
            {
                using (var game = new GeonBitUI_Examples())
                {
                    game.Run();
                }
            }
            else
            {
                using (var game = new TheWiseOneQuest())
                {
                    game.Run();
                }
            }
        }
    }
}
