using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerAI : MonoBehaviour
{
    [SerializeField] private int _runnerNO;
    [SerializeField] Rigidbody2D rb;
    private bool _isHoldingRunKey = true;
    private float _currentSpeed;
    private float _currentStamina;
    private int _foulCount = 0;
    private bool _aiAudioIsDisabled = true;
    private bool _hasRunnerFinished = false;
    [SerializeField] GameObject _finPos;
    public RunnerStatsInfo RunnerStatsInfo { get; private set; }
    public float CurrentSpeed
    {
        get
        {
            return _currentSpeed;
        }
        private set
        {
            //going faster
            if (_currentSpeed < value)
            {
                if (value < MaxSpeed)
                {
                    _currentSpeed = value;
                    ChangeAnimationSpeed();
                }
            }
            //slowing down
            else
            {
                if (value > StartingSpeed)
                {
                    _currentSpeed = value;
                    ChangeAnimationSpeed();

                }


            }

        }
    }

    public float StartingSpeed { get; private set; } = 10;
    private Animator animator;
    public Animator Animator { get { return animator; } }
    private float _thresholdSpeed = 13;
    public float MaxSpeed { get; private set; } = 20;
    private bool _hasStartedRunning = false;
    private float _slowDownAmountWhenNotPressingRunKey = 10f;
    [SerializeField] RunnerAudioManager _runnerAudio;
    private void Awake()
    {
        MaxSpeed = UnityEngine.Random.Range(18, 21);
        animator = GetComponent<Animator>();
        _runnerAudio = GetComponent<RunnerAudioManager>();
        RunnerStatsInfo = new RunnerStatsInfo(_runnerNO);
        UIManager.Instance.SetRunnerStatRef(RunnerStatsInfo,_runnerNO);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsRaceInProgress)
        {
            UpdateRealtimeRunnerStats();
            RunnerStatsInfo.CalculateAndSetDistanceTraveled(gameObject.transform.position.x);
            if (_hasStartedRunning)
            {

                if (gameObject.transform.position.x < _finPos.transform.position.x)
                {
                    ChangePace();
                    ChangeAnimationSpeed();
                    PlayRunningAudio();
                }
                else
                {
                    gameObject.transform.position = new Vector2(_finPos.transform.position.x, gameObject.transform.position.y);
                    animator.SetTrigger("has_finished");
                    _hasRunnerFinished = true;
                    GameManager.Instance.RunnerFinished(_runnerNO);
                }

            }
        }
    }
    void PlayRunningAudio()
    {
        if (_hasStartedRunning&&!_aiAudioIsDisabled)
        {
            _runnerAudio.Play2RunningAudioWithIntervalsVoid(5 / CurrentSpeed);
        }
    }
    /// <summary>
    /// Note: We do not require to use Time.deltaTime for this one because _currentSpeed value already takes Time.deltaTime into account.
    /// </summary>
    void ChangeAnimationSpeed()
    {
        animator.speed = 1 + (_currentSpeed - StartingSpeed) / 5;
    }
    void ChangePace()
    {
            if (_isHoldingRunKey)
            {
                
                if (CurrentSpeed < _thresholdSpeed)
                {
                    CurrentSpeed += Time.deltaTime * 2;
                }
                else if (CurrentSpeed >= _thresholdSpeed)
                {
                    CurrentSpeed += Time.deltaTime;
                }
                
            }
            else
            {
                CurrentSpeed -= _slowDownAmountWhenNotPressingRunKey * Time.deltaTime;
            }
    }
    private void OnEnable()
    {
        GameManager.Instance.RaceHasStarted += StartHoldingRunKey;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RaceHasStarted -= StartHoldingRunKey;
        }
    }
    void StartHoldingRunKey()
    {
        _isHoldingRunKey = true;
        if (!_hasStartedRunning)
        {
            CurrentSpeed = StartingSpeed;
            _hasStartedRunning = true;
            animator.SetTrigger("start_running");
        }
    }
    void StopHoldingRunKey(InputAction.CallbackContext ct)
    {
        _isHoldingRunKey = false;
    }

    private void FixedUpdate()
    {
        if (_hasStartedRunning && GameManager.Instance.IsRaceInProgress)
        {
            rb.MovePosition(rb.position + (Vector2.right * CurrentSpeed * Time.deltaTime * 3));
        }
    }
    //These are the stats that are displayed in the HUD
    private void UpdateRealtimeRunnerStats()
    {
        if (!_hasRunnerFinished)
        {
            RunnerStatsInfo.Speed = _currentSpeed;
            RunnerStatsInfo.Time = Time.time - GameManager.Instance.RaceStartTime;
        }
    }
    private void CommitFoul()
    {

        if (_foulCount < 3)
        {
            _foulCount++;
        }
        else
        {
            _foulCount++;
            Debug.Log("GAME OVER! (too many fouls");
        }
    }

}
