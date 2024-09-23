using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] GameObject[] countdownLamps = new GameObject[3];
    private LampAudioManager _lampAudioManager;
    private bool _hasCountdownBeenInterrupted = false;
    public Action RaceHasStarted;
    public void CountdownHasBeenInterrupted()
    {
        _hasCountdownBeenInterrupted = true;
        _lampAudioManager.PlayLampSound(3);
    }
    public bool IsCountDownInProgress { get; private set; } = false;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _lampAudioManager = GetComponent<LampAudioManager>();
    }
    public void StartCountDown()
    {
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        foreach (var item in countdownLamps)
        {
            item.SetActive(false);
        }
        _lampAudioManager.PlayLampSound(0);
        IsCountDownInProgress = true;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < countdownLamps.Length; i++)
        {
            foreach (var item in countdownLamps)
            {
                item.SetActive(false);
            }
            countdownLamps[i].SetActive(true);
            if (i < 2)
            {
                if (_hasCountdownBeenInterrupted)
                {
                    break;
                }
                _lampAudioManager.PlayLampSound(1);
                yield return new WaitForSeconds(2);
            }
            else
            {
                if (_hasCountdownBeenInterrupted)
                {
                    break;
                }
                _lampAudioManager.PlayLampSound(2);
                IsCountDownInProgress =false;
            }
        }
        foreach (var item in countdownLamps)
        {
            item.SetActive(false);
        }
        if (!_hasCountdownBeenInterrupted)
        {
            if (RaceHasStarted != null)
            {
                RaceHasStarted.Invoke();
            }
        }
        else if (_hasCountdownBeenInterrupted)
        {

        }
        
        
    }
}
