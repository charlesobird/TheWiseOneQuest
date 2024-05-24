using Microsoft.Xna.Framework.Media;

namespace TheWiseOneQuest.Handlers;

public class MusicHandler
{

    //private static string SONG_NAME = "WindsOfMagic";
    private static string SONG_NAME = "MysticalMelodies";
    private static Song song = _Utils.Content.Load<Song>(SONG_NAME); // Fetch the built file for the music from the Content Pipeline (WindsOfMagic.xnb)
    private bool isPaused = false;
    public static float Volume
    {
        get { return MediaPlayer.Volume; }
    }
    public bool Paused
    {
        get { return isPaused; }
        set { isPaused = value; }
    }
    public MusicHandler() { }
    public void Play()
    {
        MediaPlayer.Play(song);
        // Loop the song
        MediaPlayer.IsRepeating = true;
        //the below makes it so that the music isn't stupidly loud when you first load the game
        MediaPlayer.Volume = 0.35f; 
    }

    public void Pause()
    {
        MediaPlayer.Pause();
        isPaused = true;
    }
    public void Resume()
    {
        MediaPlayer.Resume();
        isPaused = false;
    }
    public void SetVolume(int volume)
    {
        // Set the volume based on the 1 to 100 value from the Slider
        MediaPlayer.Volume = (float)volume / 100;
    }
}