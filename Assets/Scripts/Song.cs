using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Song")]
public class Song : ScriptableObject
{

    public Music[] audioClips;

}
