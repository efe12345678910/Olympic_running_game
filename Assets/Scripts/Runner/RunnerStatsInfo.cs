using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class RunnerStatsInfo
{
    private float _speed;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
            if (_speed > MaxSpeed)
            {
                MaxSpeed = _speed;
            }
        }
    }
    public float MaxSpeed { get; private set; } 
    public float DistanceTraveled { get; private set; }
    public int Fauls { get; set; } = 0;
    public float Time { get; set; }
    public void CalculateAndSetDistanceTraveled(float xPositionRunner)
    {
        if (xPositionRunner > -30 && xPositionRunner<=1650)
        {
            if (DistanceTraveled < 499)
            {
                DistanceTraveled = (xPositionRunner + 30) * 50 / 168;
            }
            else
            {
                DistanceTraveled = 500;
            }
        }
    }
}
