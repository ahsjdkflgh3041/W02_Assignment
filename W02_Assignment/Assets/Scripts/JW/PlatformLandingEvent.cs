using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformLandingEvent : MonoBehaviour
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
			OnPlayerLanding(player);
		}
	}
	protected abstract void OnPlayerLanding(JWPlayer _player);
	#endregion
}
