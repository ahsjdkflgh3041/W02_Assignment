using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerPlayerInEvent : MonoBehaviour
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
		_other.gameObject.TryGetComponent(out player);
		if (player != null)
		{
			OnPlayerIn(player);
		}
	}
	protected abstract void OnPlayerIn(JWPlayer _player);
	#endregion
}
