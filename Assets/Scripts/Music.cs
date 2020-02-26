using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Music
{
    public string name;

    public AudioClip musicClip;

    //corresponds to AudioSource settings of same names
    public bool loop;
    [Range(0f, 1f)]
    public float volume;
    [Range(-3f, 3f)]
    public float pitch;

    //Whether or not we want to override the default timesToLoop supplied in BGMController
    [SerializeField]
    private bool useDefaultLoops;
    //Amount of times to loop this Music
    public int timesToLoop;

    public bool UseDefaultLoops { get => useDefaultLoops; }

    //public float speed;

    public Music()
    {
        name = "empty";
        loop = false;
        volume = 1f;
        pitch = 1f;
        useDefaultLoops = true;
        timesToLoop = 0;
    }

    public override string ToString()
    {
        return name;
    }
}