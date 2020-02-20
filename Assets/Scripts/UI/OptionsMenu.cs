using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public string mixerKey; //the playerprefs key for the volume of the associated mixer

    public void SetVolume(float volume)
    {
        mixer.SetFloat(mixerKey, volume);
        PlayerPrefs.SetFloat(mixerKey, volume);
        Debug.Log("volume = " + volume.ToString());
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.Save();
    }
}
