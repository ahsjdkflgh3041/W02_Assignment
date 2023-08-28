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
	private JWRigidBody rb;
	private JWMove move;
	private JWJump jump;
	private JWDash dash;

	private bool canAct = true;

	[SerializeField] private CheckPoint checkPoint;
	#endregion

	#region PublicMethod
	public void Die()
	{
		Respawn();
	}
	public void Respawn()
	{
		if(checkPoint != null)
		{
			transform.position = checkPoint.GetRespawnPoint();
		}
		else
		{
			transform.position = Vector3.zero + Vector3.up * 2f;
		}
		Physics.SyncTransforms();
		dash.RestoreDash();
	}
	public CheckPoint GetRespawnPoint() => checkPoint;
	public void SetRespawnPoint(CheckPoint _point)
	{
		if (checkPoint != null)
		{
			checkPoint.CheckPointOff();
		}
		checkPoint = _point;
	}
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
