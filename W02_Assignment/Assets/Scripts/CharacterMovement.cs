using System.Collections;
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
    [Header("Jump")]
    private bool isJump;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravitationalAcceleration = 10f;

    #endregion

    #region Dash
    [Header("Dash")]
    private bool onDash;
    private float dashSpeed = 30f;
    private float dashMaintainTime = 0.2f; // 대쉬 유지 시간
    private float dashCooltime = 0.5f;
    private bool isReady = true;//쿨타임 도는동안 false
    public bool isDashing = false;
    [SerializeField]
    private int countDash = 0; // 대쉬 횟수 제한
    #endregion

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (onDash && countDash < 1 && isReady)  //대쉬 누르면 
        {
            DashFunction();  // 대쉬
            countDash++;  // 하고 카운트 추가
        }

        if (IsGrounded())
        {
            countDash = 0;   // 땅에 떨어지면 대쉬 카운트 초기화

        }
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
    #region Dash
    private void DashFunction()
    {
        StartCoroutine(dashCoroutine());
    }
    IEnumerator dashCoroutine()
    {
        Debug.Log("Dash");

        float initialGravity = gravitationalAcceleration;
        isDashing = true;
        isReady = false;
        gravitationalAcceleration = 0f;
        currentVelocity = Vector3.forward.normalized * dashSpeed;
        yield return new WaitForSeconds(dashMaintainTime);
        isDashing = false;
        currentVelocity = Vector3.zero;
        gravitationalAcceleration = initialGravity;
        yield return new WaitForSeconds(dashCooltime);
        isReady = true;
    }
    #endregion

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

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onDash = true;
        }
        else if (context.canceled)
        {
            onDash = false;
        }
    }

    #endregion
}