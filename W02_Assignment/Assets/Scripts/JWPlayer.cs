using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JWPlayer : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Rigidbody rb;

	private JWMove move;
	private JWJump jump;
	private JWDash dash;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
		TryGetComponent(out move);
		TryGetComponent(out jump);
		TryGetComponent(out dash);
	}
	private void OnMove(InputValue value)
	{
		move.Move(value.Get<Vector2>());
	}
	private void OnJump(InputValue value)
	{
		jump.Jump();
	}
	private void OnDash(InputValue value)
	{
		dash.Dash();
	}
	#endregion
}
