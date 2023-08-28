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
	private SMGhostRecorder recorder;

	private bool canAct = true;

	[SerializeField] private CheckPoint checkPoint;
	#endregion

	#region PublicMethod
	public void CanAct()
	{
		canAct = true;
		rb.SetBodyType(JWRigidBody.EBodyType.Dynamic);
	}
	public void CanNotAct(float _duration)
	{
		canAct = false;
		rb.SetBodyType(JWRigidBody.EBodyType.Static);
		Invoke(nameof(CanAct), _duration);
	}
	public void CanNotAct()
	{
		canAct = false;
		rb.SetBodyType(JWRigidBody.EBodyType.Static);
	}
	public void Die()
	{
		GameManager.instance.OnPlayerDead();
		rb.DashEnd();
		Respawn();
		CanNotAct();
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
		dash.ResetDash();
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
		TryGetComponent(out recorder);
	}
	private void OnMove(InputValue value)
	{
		if (canAct == false)
			return;
		move.Move(value.Get<Vector2>());
		if (recorder != null)
		{
			recorder.SetInput(value.Get<Vector2>());
		}
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
