using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips = new AudioClip[3];
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlayLampSound(int soundIndex)
    {
        if (soundIndex < 3)
        {
            _audioSource.PlayOneShot(_audioClips[soundIndex]);
        }
    }
    
}
