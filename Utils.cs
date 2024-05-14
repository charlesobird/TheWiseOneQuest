using System;
using System.Threading;
using Microsoft.Xna.Framework;

namespace TheWiseOneQuest.Utils
{
    // This is used to find the related animation in the sprite atlas / sheet for the wizard, e.g. Idle is found at height 0 to 128, Death is found at 128 to 256
    enum WIZARD_SPRITE_SHEET_LOCATIONS
    {
        Idle = 0,
        Death = 128,
        Hurt = 256,
        CastSpell = 512
    }

    public enum Element {
        Fire,
        Air,
        Water,
        Earth
    }

    public class Utils
    {
        public static byte DEFAULT_STARTER_POINTS = 20;
        public static byte DEFAULT_MAX_HEALTH = 100;
        public static int DEFAULT_ROUNDS_WON_THRESHOLD = 5; // Determines how many rounds you need to win to get the title
        public static string[] ELEMENTS = { "Fire", "Air", "Water", "Earth" };
        public static string WIZARD_STORE_FILE_NAME = "./Content/wizards.json";

        // public static string WIZARD_STORE_FILE_NAME = "wizards.json";
        public static Vector2 WIZARD_SPRITE_SIZE = new Vector2(0.25f);

        public static string FIRE_WZARD_SPRITE_ATLAS = "Sprites/FireWizard";
        public static string AIR_WIZARD_SPRITE_ATLAS = "Sprites/AirWizard";
        public static string WATER_WIZARD_SPRITE_ATLAS = "Sprites/WaterWizard";
        public static string EARTH_WIZARD_SPRITE_ATLAS = "Sprites/EarthWizard";
        public static string MINUS_ICON = "icons/minus";
        public static string PLUS_ICON = "icons/plus";

        public Utils() { }

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
    }
};
