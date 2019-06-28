using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [Header("Disabled Sprites")]
    public Sprite musicOnDisabled;
    public Sprite musicOffDisabled;
    [Header("Enabled Sprites")]
    public Sprite musicOnEnabled;
    public Sprite musicOffEnabled;
    [Header("Buttons Images")]
    public Image musicOnImage;
    public Image musicOffImage;

    private void OnEnable()
    {
        //On enable check if PlayerPrefs has Music key (just to make sure)
        if (PlayerPrefs.HasKey(Music.MUSIC_KEY))
        {
            //then handle music buttons
            HandleMusicButtons();
        }
    }

    /// <summary>
    /// Enable application music  
    /// </summary>
    public void EnableMusic()
    {
        //Set music as 1(true) in PlayerPrefs
        PlayerPrefs.SetInt(Music.MUSIC_KEY, 1);
        //Enable music in Music.cs
        Music.instance.HandleMusic();
        //handle buttons 
        HandleMusicButtons();
    }

    /// <summary>
    /// Disable application music
    /// </summary>
    public void DisableMusic()
    {
        //Set music as 0(false) in PlayerPrefs
        PlayerPrefs.SetInt(Music.MUSIC_KEY, 0);
        //Disable music in Music.cs
        Music.instance.HandleMusic();
        //handle buttons
        HandleMusicButtons();
    }

    /// <summary>
    /// Set On and Off buttons sprites according to state of music in app
    /// </summary>
    void HandleMusicButtons()
    {
        //if music is disabled
        if(PlayerPrefs.GetInt(Music.MUSIC_KEY) == 0)
        {
            //change musicOn sprite to disabled
            musicOnImage.sprite = musicOnDisabled;
            //change musicOff sprite to enabled
            musicOffImage.sprite = musicOffEnabled;
        }
        //if music is enabled
        else
        {
            //change musicOn sprite to enabled
            musicOnImage.sprite = musicOnEnabled;
            //change musicOff sprite to disabled
            musicOffImage.sprite = musicOffDisabled;
        }
    }
}
