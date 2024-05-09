using System;
using System.Threading;
using Microsoft.Xna.Framework;

namespace TheWiseOneQuest.Utils
{
    class Utils
    {
        public static int DEFAULT_HP_STAT = 5;
        public static int DEFAULT_DEX_STAT = 5;
        public static int DEFAULT_WIS_STAT = 5;
        public static int DEFAULT_STARTER_POINTS = 20;
        public static int DEFAULT_MAX_HEALTH = 100;
        public static int DEFAULT_ROUNDS_WON_THRESHOLD = 5;
        public static string[] ELEMENTS = { "Water", "Earth", "Fire", "Air" };
        public static string WIZARD_STORE_FILE_NAME = "./Content/wizards.json";
        // public static string WIZARD_STORE_FILE_NAME = "wizards.json";
        public static Vector2 WIZARD_SPRITE_SIZE = new Vector2(0.25f);
        public Utils() { }

        public static float GetPercentageOfScreenWidth(double percentage)
        {
            return (float)Convert.ToDouble(TheWiseOneQuest.screenWidth * percentage);
        }

        public static float GetPercentageOfScreenHeight(double percentage)
        {
            return (float)Convert.ToDouble(TheWiseOneQuest.screenHeight * percentage);
        }

        public static double GenerateRandomDouble()
        {
            Thread.Sleep(20);
            return new Random().NextDouble();
        }

        public static int GenerateRandomInteger(int maxValue = int.MaxValue)
        {
            Thread.Sleep(20);
            return new Random().Next(maxValue);
        }

        // public static void ToggleFullscreen() {
        //     GraphicsDeviceManager _graphics = TheWiseOneQuest.graphics;
        //     // _graphics.ToggleFullScreen();
        //     // TheWiseOneQuest.screenHeight = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;
        //     // TheWiseOneQuest.screenWidth = _graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
        // }
    }
};
