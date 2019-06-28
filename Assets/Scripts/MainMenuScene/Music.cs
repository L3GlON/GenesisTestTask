using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour
{
    public static Music instance;

    AudioSource _audioSource;

    public const string MUSIC_KEY = "Music";

    private void Start()
    {
        //Set static singleton of this component
        instance = GetComponent<Music>();
        //Get attached AudioSource
        _audioSource = GetComponent<AudioSource>();


        //if PlayerPrefs doesn't have music key (f.e. during first launch)
        if(!PlayerPrefs.HasKey(MUSIC_KEY))
        {
            //create one an set its value as 1 (for this key: 1 - true(music on), 0 - false(music off), just because PlayerPrefs does't store bool)
            PlayerPrefs.SetInt(MUSIC_KEY, 1);
        }

        HandleMusic();
    }

    /// <summary>
    /// Enable or disable music according to value in PlayerPrefs
    /// </summary>
    public void HandleMusic()
    {
        //check key value (0 ~ false)
        if(PlayerPrefs.GetInt(MUSIC_KEY) == 0)
        {
            //turning music off
            _audioSource.Stop();
        }
        else
        {
            //turning music on
            _audioSource.Play();
        }
    }

}
