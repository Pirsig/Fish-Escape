using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip sound;
    private SoundController soundController;

    private Button button { get { return GetComponent<Button>(); } }

    private void Start()
    {
        button.onClick.AddListener(() => PlaySound());
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
    }

    private void PlaySound()
    {
        //checks to make sure a SoundController is actually present and then plays the sound assigned.
        if (soundController != null)
        {
            Debug.LogWarning("Click Sound");
            soundController.PlaySound(sound);
        }
        //If a soundcontroller is not found we simply print a warning, normal program execution should continue
        else
        {
            Debug.LogWarning("SoundController was not found!");
        }
    }
}
