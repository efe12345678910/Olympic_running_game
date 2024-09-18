using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private bool _runningSoundCycleInProgress =false;
    
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
        Debug.Log($"Corot => {Time.time}");
        _audioSource.PlayOneShot(SceneAudioBank.Instance.RunnerSounds[0].AudioClip);
        yield return new WaitForSeconds(interval);
        _audioSource.PlayOneShot(SceneAudioBank.Instance.RunnerSounds[1].AudioClip);
        yield return new WaitForSeconds(interval);
        _runningSoundCycleInProgress=false;
        Debug.Log($"Corot2 => {Time.time}");
    }
}
