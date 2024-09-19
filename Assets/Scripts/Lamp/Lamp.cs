using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] GameObject[] countdownLamps = new GameObject[3];
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void StartCountDown()
    {
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        for (int i = 0; i < countdownLamps.Length; i++)
        {
            foreach (var item in countdownLamps)
            {
                item.SetActive(false);
            }
            countdownLamps[i].SetActive(true);
            yield return new WaitForSeconds(2);
        }
        foreach (var item in countdownLamps)
        {
            item.SetActive(false);
        }
    }
}
