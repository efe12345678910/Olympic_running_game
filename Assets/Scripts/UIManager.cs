using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] RunnerController _runnerController;
    [SerializeField] GameObject _speedBar;
    [SerializeField] RectTransform _speedGauge;
    [SerializeField] GameObject _staminaBar;
    [SerializeField] RectTransform _staminaGauge;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpeedBar();
        UpdateStaminaBar();
        Debug.Log(310 * (_runnerController.CurrentStamina / _runnerController.MaxStamina - 1));
    }
    /// <summary>
    /// This is a formula to adjust Speed Bar position to current speed value
    /// </summary>
    void UpdateSpeedBar()
    {
        _speedGauge.offsetMax = new Vector2(_speedGauge.offsetMax.x, (_runnerController.CurrentSpeed - _runnerController.MaxSpeed) * _runnerController.StartingSpeed);
    }
    
    void UpdateStaminaBar()
    {
        _staminaGauge.offsetMax = new Vector2(_staminaGauge.offsetMax.x, 310*(_runnerController.CurrentStamina / _runnerController.MaxStamina - 1));
    }
}
