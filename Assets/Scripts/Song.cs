using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Song")]
public class Song : ScriptableObject
{

    public Music[] musicClips;

    public Music[] LoadSong()
    {
        return musicClips;
    }

    /*public static implicit operator Music[](Song song)
    {
        return song.audioClips;
    }*/

    public override string ToString()
    {
        string clipsInSong = "";
        int index = 0;

        while(index < musicClips.Length)
        {
            clipsInSong += (musicClips[index].ToString() + ", ");
            index++;
        }
        return clipsInSong;
    }

}
