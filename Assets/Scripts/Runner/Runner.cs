using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Runner : MonoBehaviour
{
    [SerializeField] private int _runnerNO;
    private InputAction run;
    private InputActionMap map;
    [SerializeField] Rigidbody2D rb;
    private bool _isHoldingRunKey;
    private float _currentSpeed;
    private float _currentStamina;
    private float _runnerRaceStartTime;
    private float _runnerRaceEndTime;
    private int _foulCount = 0;
    public RunnerStatsInfo RunnerStatsInfo { get; private set; }
    public float CurrentStamina { get { return _currentStamina; } private set 
        {
            if (value >= 0)
            {
                _currentStamina = value;
            }
            else
            {
                _currentStamina = 0;
            }

        } 
    }
    public float MaxStamina { get; private set; } = 10000;
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
    private float _thresholdSpeed = 20;
    public float MaxSpeed { get; private set; } = 35;
    private bool _hasStartedRunning = false;
    private float _slowDownAmountWhenNotPressingRunKey = 10f;
    [SerializeField] RunnerAudioManager _runnerAudio;
    private void Awake()
    {
        map = new InputActionMap("playerControls");
        run = map.AddAction("runAction", binding: "<Keyboard>/space");
        animator = GetComponent<Animator>();
        _runnerAudio = GetComponent<RunnerAudioManager>();
        CurrentStamina = MaxStamina;
        RunnerStatsInfo = new RunnerStatsInfo();
        UIManager.Instance.SetRunnerStatRef(RunnerStatsInfo,_runnerNO);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"speed => {CurrentSpeed} , anim speed => {animator.speed}");
        if (GameManager.Instance.IsRaceInProgress)
        {
            UpdateRealtimeRunnerStats();
            RunnerStatsInfo.CalculateAndSetDistanceTraveled(gameObject.transform.position.x);
            if (_hasStartedRunning)
            {
                ChangePace();
                ChangeAnimationSpeed();
                PlayRunningAudio();
                DecreaseStamina();
            }
        }
    }
    private void OnEnable()
    {
        map.Enable();
        run.performed += StartHoldingRunKey;
        run.canceled += StopHoldingRunKey;
        
    }
    private void OnDisable()
    {
        map.Disable();
        run.performed -= StartHoldingRunKey;
        run.canceled -= StopHoldingRunKey;

    }
    void PlayRunningAudio()
    {
        if (_hasStartedRunning)
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
    void DecreaseStamina()
    {
        CurrentStamina -= Mathf.Pow(_currentSpeed,2)*Time.deltaTime;
    }
    void ChangePace()
    {
            if (_isHoldingRunKey&&CurrentStamina!=0)
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
    void StartHoldingRunKey(InputAction.CallbackContext ct)
    {
        if (!GameManager.Instance.IsRaceInProgress)
        {
            CommitFoul();
            animator.SetTrigger("start_running");
        }
        _isHoldingRunKey = true;
        if (!_hasStartedRunning&&GameManager.Instance.IsRaceInProgress)
        {
            CurrentSpeed = StartingSpeed;
            _hasStartedRunning = true;
            _runnerRaceStartTime = Time.time;
            animator.SetTrigger("start_running");
        }
    }
    void StopHoldingRunKey(InputAction.CallbackContext ct)
    {
        _isHoldingRunKey = false;
    }

    private void FixedUpdate()
    {
        if (_hasStartedRunning)
        {
            rb.MovePosition(rb.position + (Vector2.right * CurrentSpeed * Time.deltaTime * 3));
        }
    }
    //These are the stats that are displayed in the HUD
    private void UpdateRealtimeRunnerStats()
    {
        RunnerStatsInfo.Speed = _currentSpeed;
        RunnerStatsInfo.Time = Time.time-_runnerRaceStartTime;

    }
    private void CommitFoul()
    {
        if (!GameManager.Instance.IsRestartingTheRace)
        {
            animator.SetBool("not_fouled", false);
            GameManager.Instance.RunnerMadeAFoul();
            if (_foulCount < 3)
            {
                _foulCount++;
                RunnerStatsInfo.Fauls = _foulCount;
                Debug.Log("Foul committed");
            }
            else
            {
                RunnerStatsInfo.Fauls = _foulCount;
                _foulCount++;
                Debug.Log("GAME OVER! (too many fouls");
            }
        }
    }



}
