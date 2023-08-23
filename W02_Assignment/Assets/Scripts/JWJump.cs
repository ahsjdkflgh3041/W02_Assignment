using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JWJump : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Rigidbody rb;
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float groundCheckRayLength = 1.1f;
	[SerializeField] private float coyoteTime;
	private bool isOnAir;
	#endregion

	#region PublicMethod
	public void Jump()
	{
		if (isOnAir == true)
			return;
		rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
	}
	private void Update()
	{
		CheckGround();
	}
	private void CheckGround()
	{
		RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.down, groundCheckRayLength, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawRay(transform.position, Vector3.down * groundCheckRayLength, Color.red);
		if (hit.Length == 0)
		{
			Invoke(nameof(SetJumpDeactive), coyoteTime);
		}
		else
		{
			isOnAir = false;
		}
	}
	private void SetJumpDeactive()
	{
		isOnAir = true;
	}
	#endregion
}
