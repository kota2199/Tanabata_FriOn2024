using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SEData
{
    public AudioClip seClip;
}

[System.Serializable]
public class BGMData
{
    public AudioClip bgmClip;
}


[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 2)]
public class AudioData : ScriptableObject
{
    public List<SEData> ses;
    public List<BGMData> bgms;
}
