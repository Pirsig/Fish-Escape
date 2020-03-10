using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    //Instance variable since this will be a singleton
    private static GameObject instance;

    private AudioSource audioSource;
    private AudioMixer mixer;
    [SerializeField]
    private string mixerVolumeKey; //The key we want to use in playerprefs to store this mixer's volume.
    [SerializeField]
    private float defaultVolume = 0.7f;

    private void Awake()
    {
        //Protects us from another instance of SoundController running
        CheckInstance();
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        try
        {
            mixer = audioSource.outputAudioMixerGroup.audioMixer;

            //loads the volume setting from playerprefs if it exists
            if (PlayerPrefs.HasKey(mixerVolumeKey))
            {
                mixer.SetFloat(mixerVolumeKey, Mathf.Log10(PlayerPrefs.GetFloat(mixerVolumeKey)) * 20);
                DebugCompareActualAndLoaded();
            }
            //If no mixerKey was found in PlayerPrefs then we create one with the value from defaultVolume and save it to PlayerPrefs
            else
            {
                Debug.Log("No mixerKey was loaded for SoundController, a new one will be set");
                //sets the mixerKey in mixer to the defaultVolume and then saves it to player prefs
                mixer.SetFloat(mixerVolumeKey, Mathf.Log10(defaultVolume) * 20);
                PlayerPrefs.SetFloat(mixerVolumeKey, defaultVolume);
                DebugCompareActualAndLoaded();
                PlayerPrefs.Save();
            }
        }
        catch (System.NullReferenceException exception)
        {
            Debug.LogWarning("No audio mixer was found");
            Debug.Log(exception);
        }
    }


    //Checks if this is the only instance of itself, if not, then destroy it, otherwise mark it as don't destroy and assign it to instance
    private void CheckInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = gameObject;
            DontDestroyOnLoad(this);
        }
    }

    public void PlaySound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }

    //gives a simple debug message to compare if the value in the mixer is the same as the value in PlayerPrefs
    private void DebugCompareActualAndLoaded()
    {
        Debug.LogWarning(mixerVolumeKey + " = " + PlayerPrefs.GetFloat(mixerVolumeKey).ToString() + " was loaded as the mixerKey for SoundController");
        mixer.GetFloat(mixerVolumeKey, out float actualValue);
        Debug.Log(nameof(actualValue) + " = " + actualValue);
    }

    public static SoundController FindSoundController()
    {
        return GameObject.Find("SoundController").GetComponent<SoundController>();
    }
}
