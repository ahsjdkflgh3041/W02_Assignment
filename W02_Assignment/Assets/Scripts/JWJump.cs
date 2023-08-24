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

	[SerializeField] private float jumpSpeed;
	[SerializeField] private float coyoteTime;
	private bool tryJump;
	#endregion

	#region PublicMethod
	public void TryToJump(float input)
	{
		tryJump = input == 1 ? true : false;
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
	}
	private void Update()
	{
		if(tryJump == true && rb.IsGrounded())
		{
			rb.Jump(jumpSpeed);
		}
	}
	#endregion
}
