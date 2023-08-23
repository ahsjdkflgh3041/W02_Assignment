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
		CheckGround();
		if(tryJump == true)
		{
			Jump();
		}
	}
	public void Jump()
	{
		if (isOnAir == true)
			return;
		Debug.Log("Jump");
		isOnAir = true;
		rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
	}
	private void CheckGround()
	{
		if (rb.velocity.y > 0)
		{
			return;
		}
		RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.down, groundCheckRayLength, 1 << LayerMask.NameToLayer("Ground"));
		Debug.DrawRay(transform.position, Vector3.down * groundCheckRayLength, Color.red);
		if (hit.Length == 0)
		{
			Invoke(nameof(SetJumpDeactive), coyoteTime);
		}
		else
		{
			Debug.Log(rb.velocity.y);
			isOnAir = false;
		}
	}
	private void SetJumpDeactive()
	{
		isOnAir = true;
	}
	#endregion
}
