using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JWJump : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private JWRigidBody rb;

	[SerializeField] private float coyoteTime;
	private float coyoteTimer;
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float doubleJumpSpeed;
	private bool doubleJumped;
	#endregion

	#region PublicMethod
	public void Jump()
	{
		if (IsGrounded())
		{
			rb.Jump(jumpSpeed);
		}
		else if (doubleJumped == false)
		{
			doubleJumped = true;
			rb.Jump(doubleJumpSpeed);
		}
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
	}
	private void Update()
	{
		CoyoteControl();
	}
	private void CoyoteControl()
	{
		if(rb.IsGrounded() == true)
		{
			coyoteTimer = 0f;
			doubleJumped = false;
		}
		else
		{
			coyoteTimer += Time.deltaTime;
		}
	}
	private bool IsGrounded()
	{
		return coyoteTimer < coyoteTime;
	}
	#endregion
}
