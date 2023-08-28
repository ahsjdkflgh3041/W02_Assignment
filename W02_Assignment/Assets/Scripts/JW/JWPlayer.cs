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
	public void CanAct()
	{
		canAct = true;
	}
	public void CantAct(float _duration)
	{
		canAct = false;
		Invoke(nameof(CanAct), _duration);
	}
	public void CantAct()
	{
		canAct = false;
	}
	public void Die()
	{
		GameManager.instance.OnPlayerDead();
		Respawn();
		CantAct(1.3f);
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
		if (canAct == false)
			return;
		move.Move(value.Get<Vector2>());
	}
	private void OnJump(InputValue value)
	{
		if (canAct == false)
			return;
		jump.Jump();
	}
	private void OnDash(InputValue value)
	{
		if (canAct == false)
			return;
		dash.Dash();
	}
	#endregion
}
