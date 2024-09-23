using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();

            }
            return _instance;
            

        }
        set
        {

        }
    }

    [SerializeField] Runner _runnerController;
    [SerializeField] GameObject _speedBar;
    [SerializeField] RectTransform _speedGauge;
    [SerializeField] GameObject _staminaBar;
    [SerializeField] RectTransform _staminaGauge;
    [SerializeField] private RunnerStatsInfo _runnerStats1;
    [SerializeField] private RunnerStatsInfo _runnerStats2;
    [SerializeField] TextMeshProUGUI _runner1Info;
    [SerializeField] TextMeshProUGUI _runner2Info;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeedBar();
        UpdateStaminaBar();
        UpdateRunnerInfoDisplays();
    }
    /// <summary>
    /// This is a formula to adjust Speed Bar position to current speed value
    /// </summary>
    void UpdateSpeedBar()
    {
        _speedGauge.offsetMax = new Vector2(_speedGauge.offsetMax.x, (_runnerController.CurrentSpeed - _runnerController.MaxSpeed) * _runnerController.StartingSpeed);
    }
    /// <summary>
    /// This is a formula to adjust Stamina Bar position to current stamina value
    /// </summary>
    void UpdateStaminaBar()
    {
        _staminaGauge.offsetMax = new Vector2(_staminaGauge.offsetMax.x, 310*(_runnerController.CurrentStamina / _runnerController.MaxStamina - 1));
    }
    /// <summary>
    /// RunnerStatsInfo reference is passed through this method and stored in UIManager fields
    /// </summary>
    /// <param name="runnerStats">
    /// This file is created by Runner scripts on each runner </param>
    /// <param name="runnerNo"></param>
    public void SetRunnerStatRef(RunnerStatsInfo runnerStats,int runnerNo)
    {
        if (runnerNo == 1)
        {
            _runnerStats1 = runnerStats;
        }
        else
        {
            _runnerStats2 = runnerStats;
        }
    }
    /// <summary>
    /// Updates Runner Data Information display on the HUD
    /// </summary>
    private void UpdateRunnerInfoDisplays()
    {
        _runner1Info.text = $"Speed : {Math.Round(_runnerStats1.Speed,2)}\nMax Speed : {Math.Round(_runnerStats1.MaxSpeed,2)}\n" +
            $"Total Distance Traveled : {Math.Round(_runnerStats1.DistanceTraveled,2)} m\nFauls : {(Data.GetFoulCount(1))}\nTime : {Math.Round(_runnerStats1.Time,2)} sec";
        _runner2Info.text = $"Speed : {Math.Round(_runnerStats2.Speed,2)}\nMax Speed : {Math.Round(_runnerStats2.MaxSpeed,2)}\n" +
            $"Total Distance Traveled : {Math.Round(_runnerStats2.DistanceTraveled,2)} m\nFauls : {(Data.GetFoulCount(2))}\nTime : {Math.Round(_runnerStats2.Time, 2)} sec";
    }
}
