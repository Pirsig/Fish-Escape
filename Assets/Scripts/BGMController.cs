﻿using UnityEngine;
using UnityEngine.Audio;

public class BGMController : MonoBehaviour
{
    private AudioSource musicSource;

    public AudioMixer mixer;
    public string mixerKey; //The key we want to use in playerprefs to store this mixer's volume.

    private float timePlayed = 0f;
    public int defaultTimesToLoop = 1;
    private int timesLooped = 0;

    public Music[] music;
    private int musicIndex; //the current index from music[] that is being played



    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        //loads the volume setting from playerprefs if it exists
        if ( PlayerPrefs.HasKey(mixerKey))
        {
            mixer.SetFloat(mixerKey, PlayerPrefs.GetFloat(mixerKey));
        }

        //initializes musicSource with the first item in music[0]
        SetMusic(music[0]);
        musicIndex = 0;
        musicSource.Play();
    }

    private void Update()
    {
        //check if we have finished playing the audio track and restarted
        if (musicSource.time < timePlayed)
        {
            //stop the audio source prevent overlapping and note that we have completed a loop of it
            timesLooped++;
            musicSource.Stop();

            //if we've looped through the amount of times given by timesToLoop we set the next music track
            //If useDefaultLoops is true for this track we will use defaultTimesToLoop, otherwise we use the timesToLoop from this music track
            if ( music[musicIndex].UseDefaultLoops? timesLooped > defaultTimesToLoop : timesLooped > music[musicIndex].timesToLoop)
            {
                SetNextMusic();
            }

            musicSource.Play();
        }

        timePlayed = musicSource.time;
    }

    //sets relevant values from music to the AudioSource musicSource
    public void SetMusic(Music music)
    {
        musicSource.clip = music.musicClip;
        musicSource.loop = music.loop;
        musicSource.pitch = music.pitch;
        musicSource.volume = music.volume;
    }

    //Sets the next music in music[] using musicIndex
    //if we are not on the last index of music we go to index+1, otherwise we reset to index 0 and set the first track
    public void SetNextMusic()
    {
        if (musicIndex + 1 < music.Length)
        {
            musicIndex++;
        }
        else if (musicIndex + 1 == music.Length)
        {
            musicIndex = 0;
        }
        else
        {
            Debug.LogError("!BGMController has suffered a fatal error in SetNextMusic()!");
            Debug.Log("musicIndex {" + musicIndex.ToString() + "} exceeded the length of the music[] array.");
            return;
        }

        SetMusic(music[musicIndex]);
    }
}