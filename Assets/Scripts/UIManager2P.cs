using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager2P : UIManager
{
    [SerializeField] RectTransform _speedGaugeP2;
    [SerializeField] RectTransform _staminaGaugeP2;
    protected override void UpdateSpeedBar()
    {
        base.UpdateSpeedBar();
        _speedGaugeP2.offsetMax = new Vector2(_speedGaugeP2.offsetMax.x, _fullGaugeHeight * ((_runnerStats2.Speed) / _runnerStats2.MaxSpeedPossible - 1));

    }
    protected override void UpdateStaminaBar()
    {
        base.UpdateStaminaBar();
        _staminaGaugeP2.offsetMax = new Vector2(_staminaGaugeP2.offsetMax.x, _fullGaugeHeight * (_runnerStats2.CurrentStamina / _runnerStats2.MaxStamina - 1));

    }
    protected override void UpdateRunnerInfoDisplays()
    {
        _runner1Info.text = $"Speed : {Math.Round(_runnerStats1.Speed, 2)}\nMax Speed : {Math.Round(_runnerStats1.MaxSpeed, 2)}\n" +
            $"Total Distance Traveled : {Math.Round(_runnerStats1.DistanceTraveled, 2)} m\nFauls : {(Data.GetFoulCount(1))}\nTime : {Math.Round(_runnerStats1.Time, 2)} sec";
        _runner2Info.text = $"Speed : {Math.Round(_runnerStats2.Speed, 2)}\nMax Speed : {Math.Round(_runnerStats2.MaxSpeed, 2)}\n" +
            $"Total Distance Traveled : {Math.Round(_runnerStats2.DistanceTraveled, 2)} m\nFauls : {(Data.GetFoulCount(2))}\nTime : {Math.Round(_runnerStats2.Time, 2)} sec";
    }
}
