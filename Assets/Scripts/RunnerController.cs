using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerController : MonoBehaviour
{
    private InputAction run;
    private InputActionMap map;
    [SerializeField] Rigidbody2D rb;
    private bool _isHoldingRunKey;
    private float _currentSpeed;
    private float _startingSpeed = 10;
    private Animator animator;
    private float _thresholdSpeed = 20;
    private float _maxSpeed = 35;
    private float _maxAnimSpeed = 6;
    private void Awake()
    {
        map = new InputActionMap("playerControls");
        run = map.AddAction("runAction", binding: "<Keyboard>/space");
        animator = GetComponent<Animator>();
        _currentSpeed = _startingSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHoldingRunKey)
        {
            if (_currentSpeed < _maxSpeed)
            {
                if (_currentSpeed < _thresholdSpeed)
                {
                    _currentSpeed += Time.deltaTime * 2;
                    if (animator.speed < _maxAnimSpeed)
                    {
                        animator.speed += _currentSpeed *2* _maxAnimSpeed/_maxSpeed * Time.deltaTime/11;
                    }
                }
                else if (_currentSpeed >= _thresholdSpeed)
                {
                    _currentSpeed += Time.deltaTime;
                    
                    if (animator.speed < _maxAnimSpeed)
                    {
                        animator.speed += _currentSpeed * _maxAnimSpeed / _maxSpeed * Time.deltaTime/11;
                    }
                }
            }
            else
            {
                Debug.Log("max speed has been achieved");
            }
            
        }
        Debug.Log($"current speed => {_currentSpeed}\n animator speed => {animator.speed}");
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
    void StartHoldingRunKey(InputAction.CallbackContext ct)
    {
        _isHoldingRunKey = true;
    }
    void StopHoldingRunKey(InputAction.CallbackContext ct)
    {
        _isHoldingRunKey = false;
    }

    private void FixedUpdate()
    {
        if (_isHoldingRunKey)
        {
            Debug.Log("move runner clled");
            rb.MovePosition(rb.position + (Vector2.right * _currentSpeed * Time.deltaTime * 3));
        }
    }


}
