using UnityEngine;
using UnityEngine.Audio;
using BaseVariables;

[RequireComponent(typeof(AudioSource))]
public class BGMController : MonoBehaviour
{
    private AudioSource musicSource;
    private AudioMixer mixer;
    [SerializeField]
    private StringReference mixerKey; //The key we want to use in playerprefs to store this mixer's volume.
    [SerializeField]
    private float defaultVolume = 0.7f;


    private float timePlayed = 0f;
    public int defaultTimesToLoop = 1;
    private int timesLooped = 0;

    public Song[] songs;
    private Music[] currentTrack;
    private int musicIndex; //the current index from music[] that is being played

    private static GameObject instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = gameObject;
            DontDestroyOnLoad(this);
        }
        musicSource = GetComponent<AudioSource>();
        mixer = musicSource.outputAudioMixerGroup.audioMixer;
    }

    private void Start()
    {
        //find a better solution for this later
        InitializeVolume("MasterVolume");

        InitializeVolume(mixerKey);

        ChangeMusicTrack(0);
    }

    private void InitializeVolume(string mixerKey)
    {
        //loads the volume setting from playerprefs if it exists
        if (PlayerPrefs.HasKey(mixerKey))
        {
            mixer.SetFloat(mixerKey, Mathf.Log10(PlayerPrefs.GetFloat(mixerKey)) * 20);
            Debug.LogWarning(mixerKey + " = " + PlayerPrefs.GetFloat(mixerKey).ToString() + " was loaded as the mixerKey");

            mixer.GetFloat(mixerKey, out float actualValue);
            Debug.Log(nameof(actualValue) + " = " + actualValue);
        }
        //If no mixerKey was found in PlayerPrefs then we create one with the value from defaultVolume and save it to PlayerPrefs
        else
        {
            Debug.Log("No mixerKey was loaded, a new one will be set");
            mixer.SetFloat(mixerKey, Mathf.Log10(defaultVolume) * 20);
            PlayerPrefs.SetFloat(mixerKey, defaultVolume);
            mixer.GetFloat(mixerKey, out float actualValue);
            Debug.Log(nameof(actualValue) + " = " + actualValue);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        Debug.Log("Update in BGMController has started");
        //check if we have finished playing the audio track and restarted
        if (musicSource.time < timePlayed)
        {
            //stop the audio source prevent overlapping and note that we have completed a loop of it
            timesLooped++;
            musicSource.Stop();
            Debug.LogWarning("Music Source has been stopped and a loop has played");

            //if we've looped through the amount of times given by timesToLoop we set the next music track
            //If useDefaultLoops is true for this track we will use defaultTimesToLoop, otherwise we use the timesToLoop from this music track
            if ( currentTrack[musicIndex].UseDefaultLoops? timesLooped > defaultTimesToLoop : timesLooped > currentTrack[musicIndex].timesToLoop)
            {
                Debug.Log("about to set next music");
                SetNextMusic();
            }

            musicSource.Play();
            Debug.LogWarning("Music Source has been started");
        }

        timePlayed = musicSource.time;
        Debug.Log("Is musicSource playing? " + musicSource.isPlaying.ToString());
        DebugMessages.SimpleVariableOutput(this, timePlayed, nameof(timePlayed));
        DebugMessages.SimpleVariableOutput(this, musicSource.time, nameof(musicSource.time));
    }

    //sets relevant values from music to the AudioSource musicSource
    private void SetMusic(Music music)
    {
        musicSource.clip = music.musicClip;
        musicSource.loop = music.loop;
        musicSource.pitch = music.pitch;
        musicSource.volume = music.volume;
    }

    //Sets the next music in music[] using musicIndex
    //if we are not on the last index of music we go to index+1, otherwise we reset to index 0 and set the first track
    private void SetNextMusic()
    {
        if (musicIndex + 1 < currentTrack.Length)
        {
            musicIndex++;
            Debug.Log("musicIndex increased");
        }
        else if (musicIndex + 1 == currentTrack.Length)
        {
            musicIndex = 0;
            Debug.Log("musicIndex reset");
        }
        else
        {
            Debug.LogError("!BGMController has suffered a fatal error in SetNextMusic()!");
            Debug.Log("musicIndex {" + musicIndex.ToString() + "} exceeded the length of the music[] array.");
            return;
        }

        SetMusic(currentTrack[musicIndex]);
    }

    public void ChangeMusicTrack(int trackToPlay)
    {
        musicSource.Stop();
        currentTrack = songs[trackToPlay].LoadSong();
        DebugTrackChange(trackToPlay);
        SetMusic(currentTrack[0]);
        Debug.Log(musicSource.clip.name + " is the currently loaded clip.");
        musicIndex = 0;
        musicSource.Play();
        Debug.Log("Is musicSource playing?" + musicSource.isPlaying.ToString());
        Debug.LogWarning("ChangeMusicTrack() has completed, music should be playing!");
    }

    public void ChangeVolume(float newVolume)
    {
        musicSource.volume = newVolume;
    }

    private void DebugTrackChange(int songLoaded)
    {
        int index = 0;
        while (index < songs[songLoaded].musicClips.Length)
        {
            DebugMessages.SimpleVariableOutput(this, currentTrack[index], nameof(currentTrack) + '[' + index + ']');
            index++;
        }
        DebugMessages.SimpleVariableOutput(this, currentTrack, nameof(currentTrack));
        DebugMessages.SimpleVariableOutput(this, songs[0], "songs[" + songLoaded + "]");
    }
}