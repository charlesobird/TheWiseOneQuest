namespace TheWiseOneQuest.Utils
{
    public class EnemyResources
    {
        public static string[] names = {
        "Merlin Darkstar",
        "Astrid Spellweaver",
        "Thaddeus Shadowcloak",
        "Isadora Moonwhisper",
        "Alaric Frostbeard",
        "Seraphina Starfury",
        "Ignatius Stormcaller",
        "Cassandra Nightshade",
        "Oberon Fireheart",
        "Morgana Silverwind",
        "Zephyr Skyweaver",
        "Valeria Stardust",
        "Ragnar Blackthorn",
        "Lysander Spellbound",
        "Selene Moonlight",
        "Octavius Thunderhand",
        "Lyra Frostglow",
        "Darius Shadowblade",
        "Seraphim Phoenixwing",
        "Azura Mysticfire"
    };
        public static string[] descriptors = {
            "The Great",
        "The Wise",
        "The Mighty",
        "The Magnificent",
        "The Enigmatic",
        "The Legendary",
        "The Illustrious",
        "The Revered",
        "The Masterful",
        "The Mystical",
        "The Arcane",
        "The Supreme",
        "The Eternal",
        "The Transcendent",
        "The Radiant",
        "The Sovereign",
        "The Fabled"
    };
    }
    // This is used to find the related animation in the sprite atlas / sheet for the wizard
    //e.g. Idle is found at height 0 to 128, Death is found at 128 to 256
    public enum WIZARD_SPRITE_SHEET_LOCATIONS
    {
        Idle = 0,
        Death = 128,
        CastSpell = 256,
        Hurt = 384
    }

    public enum Element
    {
        Fire,
        Air,
        Water,
        Earth
    }

    public class Utils
    {
        public static ContentManager Content;
        public static byte DEFAULT_STARTER_POINTS = 30; // Total points you can allocate to stats as a player
        public static byte DEFAULT_MAX_HEALTH = 100; // base max health, hp points increase this
        public static int DEFAULT_ROUNDS_WON_THRESHOLD = 5; // Determines how many rounds you need to win to get the title
        public static string WIZARD_STORE_FILE_NAME = "./Content/wizards.json";
        // Sprite Sizes
        public static Vector2 WIZARD_SPRITE_SIZE = new Vector2(256);
        public static Vector2 DEFAULT_PROJECTILE_SIZE = new Vector2(64);
        // Used to determine a Complete or Glancing Blow block
        public static double PERFECT_BLOCK_PERCENTAGE_CHANCE = 0.2;
        // Content Pipeline Asset Paths
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

        public static int GenerateRandomInteger(int minValue = 0, int maxValue = int.MaxValue)
        {
            Thread.Sleep(20);
            return new Random().Next(minValue,maxValue);
        }
    }
};