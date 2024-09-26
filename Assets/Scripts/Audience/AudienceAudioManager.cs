using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceAudioManager : MonoBehaviour
{
    AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Applause()
    {
        _audioSource.Play();
    }
    private void OnEnable()
    {
        GameManager.Instance.VictoryEvent += Applause;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.VictoryEvent -= Applause;
        }
    }
}
