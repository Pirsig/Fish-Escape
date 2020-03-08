using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip sound;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource audioSource { get { return GetComponent<AudioSource>(); } }

    private void Start()
    {
        gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = sound;

        button.onClick.AddListener(() => PlaySound());
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(sound);
        Debug.LogWarning("Click Sound");
    }
}
