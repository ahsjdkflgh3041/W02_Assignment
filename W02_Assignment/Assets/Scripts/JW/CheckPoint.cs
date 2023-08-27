using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator anim;
	private float yAxisAmount = 3;
	#endregion

	#region PublicMethod
	public void CheckPointOn(JWPlayer _player)
	{
		if(_player.GetRespawnPoint() != this)
		{
			anim.SetBool("on", true);
			_player.SetRespawnPoint(this);
		}
	}
	public void CheckPointOff()
	{
		anim.SetBool("on", false);
	}
	public Vector3 GetRespawnPoint()
	{
		return transform.position + Vector3.up * yAxisAmount;
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		transform.parent.TryGetComponent(out anim);
	}
	private void OnTriggerEnter(Collider _other)
	{
		SetCheckPoint(_other);
	}
	private void SetCheckPoint(Collider _other)
	{
		JWPlayer player;
		_other.gameObject.TryGetComponent(out player);
		if (player != null)
		{
			CheckPointOn(player);
		}
	}
	#endregion
}
