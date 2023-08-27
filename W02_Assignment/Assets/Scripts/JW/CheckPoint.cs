using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : PlatformLandingEvent
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
	protected override void OnPlayerLanding(JWPlayer _player)
	{
		CheckPointOn(_player);
	}
	#endregion
}
