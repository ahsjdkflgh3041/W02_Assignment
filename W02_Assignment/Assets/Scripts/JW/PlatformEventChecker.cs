using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformEventChecker : MonoBehaviour
{
	#region PublicVariables
	public UnityEvent onLanding;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnTriggerEnter(Collider _other)
	{
		CallEvent(_other);
	}
	private void CallEvent(Collider _other)
	{
		JWPlayer player;
		_other.gameObject.TryGetComponent(out player);
		if (player != null)
		{
			onLanding.Invoke();
		}
	}
	#endregion
}
