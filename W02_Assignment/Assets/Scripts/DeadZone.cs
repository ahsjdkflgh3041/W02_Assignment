using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnTriggerEnter(Collider _other)
	{
		JWPlayer player;
		_other.TryGetComponent(out player);
		if(player != null )
		{
			player.Die();
		}
	}
	#endregion
}
