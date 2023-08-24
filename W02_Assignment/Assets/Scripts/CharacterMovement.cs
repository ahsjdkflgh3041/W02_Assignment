using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    #region Movement
    
    [Header("Walk Speed")]
    // Walk Speed
    [SerializeField, Range(0f, 10f)] private float walkMaxSpeed = 5.0f;
    [SerializeField, Range(0f, 2f)] private float walkAccelerationTime = 0.15f;
    [SerializeField, Range(0f, 2f)] private float walkDecelerationTime = 0.2f;

    [Header("Run Speed")]
    // Run Speed
    [SerializeField, Range(0f, 10f)] private float runMaxSpeed = 10.0f;
    [SerializeField, Range(0f, 2f)] private float runAccelerationTime = 0.2f;
    [SerializeField, Range(0f, 2f)] private float runDecelerationTime = 0.5f;

    /*
    [Header("Air Speed")]
    // Aireal Speed
    [SerializeField, Range(0f, 10f)] private float airMaxSpeed = 3f;
    [SerializeField, Range(0f, 2f)] private float airAccelerationTime = 0.8f;
    [SerializeField, Range(0f, 2f)] private float airDecelerationTime = 1f;
    */

    [SerializeField] private bool isRunning = false;

    private Vector3 currentVelocity;
    private CharacterController controller;

    private float horizontalInput;
    private float verticalInput;
    #endregion

    #region Jump
    private bool isJump;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravitationalAcceleration = 10f;

    #endregion
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Debug.Log(IsGrounded());
        SetHorizontalVelocity();
        SetVerticalVelocity();

        controller.Move(currentVelocity * Time.deltaTime);
    }
    private void SetHorizontalVelocity()
    {
        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (IsGrounded())
        {
            if (inputDirection.magnitude > 0)
            {
                if (isRunning)
                {
                    // Acceleration
                    currentVelocity += inputDirection * (runMaxSpeed / runAccelerationTime) * Time.deltaTime;
                    currentVelocity = Vector3.ClampMagnitude(currentVelocity, runMaxSpeed);
                }
                else
                {
                    // Acceleration
                    currentVelocity += inputDirection * (walkMaxSpeed / walkAccelerationTime) * Time.deltaTime;
                    currentVelocity = Vector3.ClampMagnitude(currentVelocity, walkMaxSpeed);
                }
            }
            else
            {
                if (isRunning)
                {
                    // Deceleration
                    currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, (walkMaxSpeed / walkDecelerationTime) * Time.deltaTime);
                    if (currentVelocity == Vector3.zero)
                    {
                        isRunning = false;
                    }
                }
                else
                {
                    // Deceleration
                    currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, (walkMaxSpeed / walkDecelerationTime) * Time.deltaTime);
                }
            }
        }
    }

    private void SetVerticalVelocity()
    {
        if (!IsGrounded())
        {
            if (isJump)
            {
                currentVelocity.y -= gravitationalAcceleration * Time.deltaTime;
            }
            else
            {
                currentVelocity.y -= gravitationalAcceleration * 2 * Time.deltaTime;
            }
            return;
        }

        if (isJump)
        {
            currentVelocity.y = jumpForce;
        }
        else              
        {
            currentVelocity.y = Mathf.Max(0f, currentVelocity.y);
        }
    }

    public bool IsGrounded()
    {
        if (controller.isGrounded)
            return true;
        float maxDistance = 1.2f;
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
        return Physics.Raycast(ray, maxDistance);
    }

    #region Get Input
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        horizontalInput = inputVector.x;
        verticalInput = inputVector.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJump = true;
            Debug.Log("Jump");
        }
        else if (context.canceled)
        {
            isJump = false;
            Debug.Log("Leave");
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = !isRunning;
        }
    }
    #endregion
}