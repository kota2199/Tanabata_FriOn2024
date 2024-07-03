using System.Collections.Generic;

using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace OccaSoftware.Fireworks.Runtime
{
    [RequireComponent(typeof(VisualEffect))]
    [RequireComponent(typeof(AudioSource))]
    class PlayVFXAudioEvent : VFXOutputEventAbstractHandler
    {
        [Tooltip("Sets whether the firework sound effect will play when in edit mode.")]
        public override bool canExecuteInEditor => true;

        [Tooltip("Provide an audiosource that will play the audio clip.")]
        public AudioSource audioSource;

        [Tooltip(
            "You can supply a list of clips. The VFX will randomly pick one to play when the event is triggered. Alternatively, you can directly supply the AudioSource with a clip. Or, you can provide a single clip in the optional clip list and the system will always pick that one."
        )]
        public List<AudioClip> optionalClipList = new List<AudioClip>();

        public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
        {
            if (audioSource != null)
            {
                if (optionalClipList.Count > 0)
                {
                    audioSource.clip = optionalClipList[Random.Range(0, optionalClipList.Count)];
                }
                audioSource.Play();
            }
        }
    }
}
