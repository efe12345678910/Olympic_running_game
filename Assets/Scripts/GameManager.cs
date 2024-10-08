using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Collections.Unicode;

public class GameManager : MonoBehaviour
{
    public bool IsRaceInProgress = false;
    public bool IsRaceOver = false;
    public float RaceStartTime { get; private set; }
    public Action RaceHasStarted;
    //This event is to trigger audience SFX and VFX
    public Action VictoryEvent;
    public bool IsRestartingTheRace { get; private set; } = false;
    public bool IsEndingTheRace { get; private set; } = false;
    [SerializeField] private Lamp lamp;
    [SerializeField] private Runner _runner1;
    //_runner2 has type GameObject instead of Runner because it could be RunnerAI instead of Runner
    [SerializeField] private GameObject _runner2;
    private bool _runner1HasFinished = false;
    private bool _runner2HasFinished = false;
    private float _runner1Time = 0;
    private float _runner2Time = 0;
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
    private void Start()
    {
        StartCountDown();
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
        RaceStartTime = Time.time;
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
    public void RunnerMadeAFoul(int runnerNo)
    {
        lamp.CountdownHasBeenInterrupted();
        if (Data.GetFoulCount(runnerNo) < 2)
        {
            Data.AddFouls(runnerNo);
        }
        else
        {
            Data.AddFouls(runnerNo);
        }
        if (!CheckForLossByFoul())
        {
            StartCoroutine(RestartRace());
        }
        else
        {
            StartCoroutine(GoToDisqualificationScreen());
        }
    }
    private bool CheckForLossByFoul()
    {
        if (Data.Runner1FoulCount >= 3)
        {
            Data.AddDisqualifiedPlayer(1);
            return true;

        }
        else if (Data.Runner2FoulCount >= 3)
        {
            Data.AddDisqualifiedPlayer(2);
            return true;
        }
        else return false;
    }
    
    private IEnumerator RestartRace()
    {
        IsRestartingTheRace = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RunnerFinished(int runnerNo)
    {
        if (runnerNo == 1)
        {
            _runner1HasFinished = true;
            if (_runner2HasFinished)
            {
                StartCoroutine(FindOutTheVictor());
            }
        }
        else if (runnerNo == 2)
        {
            _runner2HasFinished = true;
            if (_runner1HasFinished)
            {
                StartCoroutine(FindOutTheVictor());
            }
        }
    }
    public IEnumerator FindOutTheVictor()
    {
        yield return new WaitForSeconds(2);
        IsRaceInProgress = false;
         _runner1Time = _runner1.RunnerStatsInfo.Time;
        if (_runner2.TryGetComponent<RunnerAI>(out RunnerAI runnerAI))
        {
            _runner2Time = runnerAI.RunnerStatsInfo.Time;
            if (_runner1Time < _runner2Time)
            {
                //runner1 wins
                _runner1.Animator.SetTrigger("has_both_runners_finished");
                _runner1.Animator.SetBool("has_won", true);
                runnerAI.Animator.SetTrigger("has_both_runners_finished");
                runnerAI.Animator.SetBool("has_won", false);
                Data.SetWinnerData(_runner1Time,1);
            }
            else
            {
                //runner2 wins
                _runner1.Animator.SetTrigger("has_both_runners_finished");
                _runner1.Animator.SetBool("has_won", false);
                runnerAI.Animator.SetTrigger("has_both_runners_finished");
                runnerAI.Animator.SetBool("has_won", true);
                Data.SetWinnerData(_runner2Time,2);
            }
        }
        else if (_runner2.TryGetComponent<Runner>(out Runner runner))
        {
            _runner2Time = runner.RunnerStatsInfo.Time;
            if (_runner1Time < _runner2Time)
            {
                //runner1 wins
                _runner1.Animator.SetTrigger("has_both_runners_finished");
                _runner1.Animator.SetBool("has_won", true);
                runner.Animator.SetTrigger("has_both_runners_finished");
                runner.Animator.SetBool("has_won", false);
                Data.SetWinnerData(_runner1Time,1);
            }
            else
            {
                //runner2 wins
                _runner1.Animator.SetTrigger("has_both_runners_finished");
                _runner1.Animator.SetBool("has_won", false);
                runner.Animator.SetTrigger("has_both_runners_finished");
                runner.Animator.SetBool("has_won", true);
                Data.SetWinnerData(_runner2Time,2);
            }

        }
       if (VictoryEvent != null)
        {
            VictoryEvent.Invoke();
        }
        StartCoroutine(GoToEndingScreen());
    }
    private void Update()
    {
    }
    private IEnumerator GoToEndingScreen()
    {
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(SceneNames.Scenes.EndingScreen.ToString());
    }
    private IEnumerator GoToDisqualificationScreen()
    {
        IsEndingTheRace = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneNames.Scenes.DisqualificationScreen.ToString());
    }
}
