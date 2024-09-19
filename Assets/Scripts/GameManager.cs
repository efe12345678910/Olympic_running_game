using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool HasGameStarted = false;
    public bool IsRaceOver = false;
    public bool IsCountDownOver = false;
    [SerializeField] private Lamp lamp;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
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
    
}
