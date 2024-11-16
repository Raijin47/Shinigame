using Plugins.Audio.Core;
using Plugins.Audio.Utils;
using UnityEngine;

public class AudioFix : MonoBehaviour
{
    [SerializeField] private SourceAudio _source;
    [SerializeField] private AudioDataProperty _clip;

    private void Start()
    {
        _source.Play(_clip.Key);
        _source.Loop = true;
    }
}