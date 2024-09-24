using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager2P : UIManager
{
    [SerializeField] RectTransform _speedGaugeP2;
    [SerializeField] RectTransform _staminaGaugeP2;
    protected override void UpdateSpeedBar()
    {
        base.UpdateSpeedBar();
        _speedGaugeP2.offsetMax = new Vector2(_speedGaugeP2.offsetMax.x, (_runnerStats2.Speed - _runnerStats2.MaxSpeedPossible) * _runnerStats2.StartingSpeed);
    }
    protected override void UpdateStaminaBar()
    {
        base.UpdateStaminaBar();
        _staminaGaugeP2.offsetMax = new Vector2(_staminaGaugeP2.offsetMax.x, 310 * (_runnerStats2.CurrentStamina / _runnerStats2.MaxStamina - 1));

    }
}
