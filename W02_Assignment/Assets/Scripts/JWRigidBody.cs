using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class JWRigidBody : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private CharacterController controller;

	private Vector3 direction;
	private float moveSpeed;
	[SerializeField] private float linearDrag;

	[SerializeField] private bool isKinematic;
	[SerializeField] private float fallOffMaxVelocity;
	[SerializeField] private float fallOffScale;
	[SerializeField] private float gravityScale = 0.15f;
	private const float GRAVITY = -9.81f;
	[SerializeField] private float headingCheckRayDistance;
	private bool isDashed;

	private Vector3 finalVector;
	#endregion

	#region PublicMethod
	public void MoveToDirection(Vector2 _direction, float _moveSpeed)
	{
		direction = new Vector3(_direction.x - _direction.y, 0, _direction.x + _direction.y);
		moveSpeed = _moveSpeed;
	}
	public void Dash(float _magnitude, float _duration)
	{
		isDashed = true;
		isKinematic = true;
		finalVector.y = 0f;
		finalVector = transform.forward * _magnitude;
		Invoke(nameof(DashEnd), _duration);
	}
	public void Jump(float _jumpPower)
	{
		JWCameraController.instance.TargetJumped(true);
		finalVector.y = _jumpPower;
	}
	public void AddForce(Vector3 _direction, float magnitude)
	{
		finalVector += _direction.normalized * magnitude;
	}
	public void SetVectorZero() => finalVector = Vector3.zero;
	public bool IsGrounded() => controller.isGrounded;
	public float GetSpeed() => finalVector.magnitude;
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out controller);
	}
	private void FixedUpdate()
	{
		MoveBody();
		if (isDashed == false)
		{
			MovementWithPhysics();
		}

	}
	private void MovementWithPhysics()
	{
		if (direction != Vector3.zero)
		{
			LookRotation(direction);
			MoveForward();
		}
		else
		{
			Stop();
		}
		CalculateGravity();
		HeadingCheck();
	}
	private void LookRotation(Vector2 _direction)
	{
		transform.rotation = Quaternion.LookRotation(direction);
	}
	private void MoveForward()
	{
		finalVector.x = transform.forward.x * moveSpeed * Time.fixedDeltaTime;
		finalVector.z = transform.forward.z *moveSpeed * Time.fixedDeltaTime;
	}
	private void Stop()
	{
        if(finalVector.x == 0 && finalVector.z == 0)
        {
			return;
        }
        finalVector.x = Mathf.Lerp(finalVector.x, 0, linearDrag * Time.fixedDeltaTime);
		finalVector.z = Mathf.Lerp(finalVector.z, 0, linearDrag * Time.fixedDeltaTime);
	}
	private void CalculateGravity()
	{
		if (isKinematic == true)
			return;
		if (controller.isGrounded == false)
		{
			if (finalVector.y > fallOffMaxVelocity && finalVector.y < 0)
			{
				finalVector += Vector3.up * fallOffScale * GRAVITY * Time.fixedDeltaTime;
			}
			else
			{
				finalVector += Vector3.up * gravityScale * GRAVITY * Time.fixedDeltaTime;
			}
		}
		ResetYVelocityWhenGrounded();
	}
	private void ResetYVelocityWhenGrounded()
	{
		if(finalVector.y < 0.1f && controller.isGrounded == true)
		{
			JWCameraController.instance.TargetJumped(false);
			finalVector.y = 0f;
		}
	}
	private void MoveBody()
	{
		controller.Move(finalVector);
	}
	private void HeadingCheck()
	{
		if(finalVector.y > 0)
		{
			bool heading = Physics.Raycast(transform.position, Vector3.up, headingCheckRayDistance, 1 << LayerMask.NameToLayer("Ground"));
			Debug.DrawRay(transform.position, Vector3.up * headingCheckRayDistance, Color.red);
			if (heading == true)
			{
				finalVector.y = 0f;
			}
		}
	}
	private void DashEnd()
	{
		isDashed = false;
		isKinematic = false;
		finalVector.x = 0f;
		finalVector.z = 0f;
	}
	#endregion
}
