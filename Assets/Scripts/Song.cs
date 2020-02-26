using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Song")]
public class Song : ScriptableObject
{

    public Music[] audioClips;

    public Music[] LoadSong()
    {
        return audioClips;
    }

    /*public static implicit operator Music[](Song song)
    {
        return song.audioClips;
    }*/

    public override string ToString()
    {
        string clipsInSong = "";
        int index = 0;

        while(index < audioClips.Length)
        {
            clipsInSong += (audioClips[index].ToString() + ", ");
            index++;
        }
        return clipsInSong;
    }

}
