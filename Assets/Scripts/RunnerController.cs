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
    private bool _isRunning;
    
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
        
    }
    private void OnEnable()
    {
        map.Enable();
        run.performed += StartRunning;
        run.canceled += StopRunning;
        
    }
    private void OnDisable()
    {
        map.Disable();
        run.performed -= StartRunning;
        run.canceled -= StopRunning;

    }
    void StartRunning(InputAction.CallbackContext ct)
    {
        _isRunning = true;
    }
    void StopRunning(InputAction.CallbackContext ct)
    {
        _isRunning = false;
    }
    private void FixedUpdate()
    {
        if (_isRunning)
        {
            Debug.Log("move runner clled");
            rb.MovePosition(rb.position + (Vector2.right * Time.deltaTime * 10));
        }
    }


}
