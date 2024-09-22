using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool IsRaceInProgress = false;
    public bool IsRaceOver = false;
    public Action RaceHasStarted;
    public bool IsRestartingTheRace { get; private set; } = false;
    [SerializeField] private Lamp lamp;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public void StartCountDown()
    {
        lamp.StartCountDown();
    }
    void StartTheRace()
    {
        IsRaceInProgress = true;
        if (RaceHasStarted != null)
        {
            RaceHasStarted.Invoke();
        }
    }
    private void OnEnable()
    {
        lamp.RaceHasStarted += StartTheRace;
    }
    private void OnDisable()
    {
        lamp.RaceHasStarted -= StartTheRace;
    }
    public bool GetIsCountdownInProgress()
    {
        return lamp.IsCountDownInProgress;
    }
    public void RunnerMadeAFoul()
    {
        StartCoroutine(RestartRace());
    }
    private IEnumerator RestartRace()
    {
        IsRestartingTheRace = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        
    }
}
