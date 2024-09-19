using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerAI : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public float CurrentSpeed { get; private set; }
    private float _startingSpeed = 10f;
    private bool _hasStartedRunning = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentSpeed = _startingSpeed;
    }
    void ChangePace()
    {
        //if (_hasStartedRunning)
        //{
        //        if (CurrentSpeed < _thresholdSpeed)
        //        {
        //            CurrentSpeed += Time.deltaTime * 2;
        //        }
        //        else if (CurrentSpeed >= _thresholdSpeed)
        //        {
        //            CurrentSpeed += Time.deltaTime;
        //        }
        //}

    }
}
