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
    private float _currentSpeed=1;
    
    private void Awake()
    {
        map = new InputActionMap("playerControls");
        run = map.AddAction("runAction", binding: "<Keyboard>/space");
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
            _currentSpeed += Time.deltaTime;
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
            rb.MovePosition(rb.position + (Vector2.right * _currentSpeed*Time.deltaTime));
        }
    }


}
