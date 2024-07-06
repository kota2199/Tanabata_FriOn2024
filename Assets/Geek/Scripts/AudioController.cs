using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;

    public AudioData audioData;

    [SerializeField]
    private AudioSource audioSourceForSe;

    [SerializeField]
    private AudioSource audioSourceForBgm;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlaySE(int index)
    {
        if (audioSourceForSe)
        {
            audioSourceForSe.Stop();
            audioSourceForSe.PlayOneShot(audioData.ses[index].seClip);
        }
    }

    public void PlayBGM(int index)
    {
        if (audioSourceForBgm)
        {
            audioSourceForBgm.Stop();
            audioSourceForBgm.clip = audioData.bgms[index].bgmClip;
            audioSourceForBgm.Play();
        }
    }

    public void StopBGM()
    {
        audioSourceForBgm.Stop();
    }
}
