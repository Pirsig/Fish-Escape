using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using BaseVariables;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup mixerGroup;
    private AudioMixer mixer { get => mixerGroup.audioMixer; }
    [SerializeField]
    private StringReference keyToLoad;

    private Slider slider;

    private void OnEnable()
    {
        slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
        slider.value = PlayerPrefs.GetFloat(keyToLoad);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float volume)
    {
        mixer.SetFloat(keyToLoad, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(keyToLoad, volume);
        Debug.Log("volume = " + volume.ToString());
    }
}
