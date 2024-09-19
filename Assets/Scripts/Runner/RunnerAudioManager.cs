using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private bool _runningSoundCycleInProgress =false;
    [SerializeField] private AudioClip[] _audioClips = new AudioClip[2];
    
    public void Play2RunningAudioWithIntervalsVoid(float interval)
    {
        if (!_runningSoundCycleInProgress)
        {
            StartCoroutine(Play2RunningAudioWithIntervals(interval));
            _runningSoundCycleInProgress = true;
        }
    }
    IEnumerator  Play2RunningAudioWithIntervals(float interval)
    {
        _audioSource.PlayOneShot(_audioClips[0]);
        yield return new WaitForSeconds(interval);
        _audioSource.PlayOneShot(_audioClips[1]);
        yield return new WaitForSeconds(interval);
        _runningSoundCycleInProgress=false;
    }
}
