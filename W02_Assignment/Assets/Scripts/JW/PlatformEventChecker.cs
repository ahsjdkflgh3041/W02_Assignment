using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformEventChecker : PlatformLandingEvent
{
	#region PublicVariables
	public UnityEvent onLanding;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	protected override void OnPlayerLanding(JWPlayer _player)
	{
		onLanding.Invoke();
	}
	#endregion
}
