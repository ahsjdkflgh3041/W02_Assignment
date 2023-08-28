using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class JWRigidBody : MonoBehaviour
{
	#region PublicVariables
	public enum EBodyType
	{
		Dynamic = 0,
		Kinematic = 1,
		Static = 2
	}
	public float linearDrag = 12f;
	#endregion

	#region PrivateVariables
	private CharacterController controller;

	private Vector3 direction;
	private float moveSpeed;
	//[SerializeField] private float linearDrag;

	[SerializeField] private EBodyType bodyType;
	[SerializeField] private float fallOffMaxVelocity;
	[SerializeField] private float fallOffScale;
	[SerializeField] private float gravityScale = 0.15f;
	private const float GRAVITY = -9.81f;
	[SerializeField] private float headingCheckRayDistance;
	[SerializeField] private float rotationLerpSpeed;
	[SerializeField] private float rotationPowMult;
	private float targetAngle;
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
		bodyType = EBodyType.Kinematic;
		finalVector.y = 0f;
		finalVector = transform.forward * _magnitude;
		Invoke(nameof(DashEnd), _duration);
	}
	public void Jump(float _jumpPower)
	{
		JWCameraController.instance.TargetJumped(true);
		finalVector.y = _jumpPower;
	}
	public void AddForce(Vector3 _direction, float _magnitude)
	{
		finalVector += _direction.normalized * _magnitude;
	}
	public void SetVectorZero() => finalVector = Vector3.zero;
	public bool IsGrounded() => controller.isGrounded;
	public float GetSpeed() => finalVector.magnitude;
	public void SetBodyType(EBodyType _type)
	{
		CancelInvoke(nameof(DashEnd));
		if(_type == EBodyType.Static)
		{
			finalVector = Vector3.zero;
			direction = Vector3.zero;
		}	
		bodyType = _type;
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out controller);
	}
	private void FixedUpdate()
	{
		if (bodyType == EBodyType.Static)
			return;
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
			LookRotation();
			MoveForward();
		}
		else
		{
			Stop();
		}
		CalculateGravity();
		HeadingCheck();
	}
	private void LookRotation()
	{
		targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
		float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationLerpSpeed * Time.fixedDeltaTime);
		transform.rotation = Quaternion.Euler(0f, angle, 0f);
	}
	private void MoveForward()
	{
		float modifiedMoveSpeed = moveSpeed * Mathf.Pow(Mathf.Clamp01(Vector3.Dot(transform.forward.normalized, direction.normalized)), rotationPowMult);
		finalVector.x = transform.forward.x * modifiedMoveSpeed * Time.fixedDeltaTime;
		finalVector.z = transform.forward.z * modifiedMoveSpeed * Time.fixedDeltaTime;
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
		if (bodyType == EBodyType.Kinematic || bodyType == EBodyType.Static)
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
		bodyType = EBodyType.Dynamic;
		finalVector.x = 0f;
		finalVector.z = 0f;
	}
	#endregion
}
