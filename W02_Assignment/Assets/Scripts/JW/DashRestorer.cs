using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRestorer : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator anim;
	private bool isReady;
	#endregion

	#region PublicMethod
	public void Ready() => isReady = true;
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out anim);
		isReady = true;
	}
	private void OnTriggerEnter(Collider other)
	{
		JWDash dash;
		other.TryGetComponent(out dash);
		if(dash != null && isReady == true)
		{
			isReady = false;
			dash.RestoreDash();
			anim.Play("Consume");
		}
	}
	#endregion
}
