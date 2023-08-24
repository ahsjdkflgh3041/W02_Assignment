using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] private Transform movementOrientation;
    [SerializeField] private Transform characterMesh;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float initialGravitationalAcceleration;
    [SerializeField] private float fallingGravitationalAcceleration;
    [SerializeField] private float gravitationalAcceleration;
    [SerializeField] private float jumpForce;
    [Space(10.0f)]
    [SerializeField, Range(0.0f, 1.0f)] private float lookForwardThreshold;
    [SerializeField] private float lookForwardSpeed;

    private float horizontalInput;
    private float verticalInput;
    private bool jumpFlag;

    private CharacterController m_characterController;
    private Vector3 velocity;
    private Vector3 lastFixedPosition;
    private Quaternion lastFixedRotation;
    private Vector3 nextFixedPosition;
    private Quaternion nextFixedRotation;



    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        velocity = new Vector3(0, 0, 0);
        lastFixedPosition = transform.position;
        lastFixedRotation = transform.rotation;
        nextFixedPosition = transform.position;
        nextFixedRotation = transform.rotation;

        gravitationalAcceleration = initialGravitationalAcceleration;
        horizontalInput = 0.0f;
        verticalInput = 0.0f;
        jumpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

        float interpolationAlpha = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        m_characterController.Move(Vector3.Lerp(lastFixedPosition, nextFixedPosition, interpolationAlpha) - transform.position);
        characterMesh.rotation = Quaternion.Slerp(lastFixedRotation, nextFixedRotation, interpolationAlpha);
    }

    private void FixedUpdate()
    {

        lastFixedPosition = nextFixedPosition;
        lastFixedRotation = nextFixedRotation;

        Vector3 planeVelocity = GetXZVelocity(horizontalInput, verticalInput);
        float yVelocity = GetYVelocity();
        Debug.Log(yVelocity);
        velocity = new Vector3(planeVelocity.x, yVelocity, planeVelocity.z);

        if (planeVelocity.magnitude / speed >= lookForwardThreshold)
        {
            nextFixedRotation = Quaternion.Slerp(characterMesh.rotation, Quaternion.LookRotation(planeVelocity), lookForwardSpeed * Time.fixedDeltaTime);
        }

        nextFixedPosition += velocity * Time.fixedDeltaTime;
    }

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
            jumpFlag = true;
            Debug.Log("Jump");
        }
        else if(context.canceled)
        {
            velocity = new Vector3(velocity.x, 0f, velocity.z);
            gravitationalAcceleration = fallingGravitationalAcceleration;
            jumpFlag = false;
            Debug.Log("Leave");
        }
    }

    private Vector3 GetXZVelocity(float horizontalInput, float verticalInput)
    {
        Vector3 moveVelocity = movementOrientation.forward * verticalInput + movementOrientation.right * horizontalInput;
        Vector3 moveDirection = moveVelocity.normalized;
        float moveSpeed = Mathf.Min(moveVelocity.magnitude, 1.0f) * speed;

        return moveDirection * moveSpeed;
    }

    private float GetYVelocity()
    {
        if (!IsGrounded())
        {
            return velocity.y - gravitationalAcceleration * Time.fixedDeltaTime;
        }

        gravitationalAcceleration = initialGravitationalAcceleration;
        if (jumpFlag)
        {
            return velocity.y + jumpForce;
        }
        else
        {
            return 0f;
        };
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody)
        {
            hit.rigidbody.AddForce(velocity / hit.rigidbody.mass);
        }
    }
    public bool IsGrounded()
    {
        if (m_characterController.isGrounded)
            return true;
        float maxDistance = 1.5f;
        var ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * maxDistance, Color.red);
        return Physics.Raycast(ray, maxDistance, 6);
    }
}
