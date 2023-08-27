using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMFollowingGhostController : SMGhostController
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] SMGhostManager ghostManager;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnTriggerEnter(Collider _other)
	{
		JWPlayer player;
		_other.TryGetComponent(out player);
		if (player != null)
		{
			player.Die();
			ghostManager.startRecording = false;
			ghostManager.ResetGhosts();
		}
	}
	#endregion
}
