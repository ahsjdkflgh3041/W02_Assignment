using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JWRigidBody))]
public class JWDash : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private JWRigidBody rb;
	[SerializeField] private float magnitude;
	[SerializeField] private float duration;
	[SerializeField] private float cooldown;
	private bool isReady = true;
	#endregion

	#region PublicMethod
	public void Dash()
	{
		if(isReady == true)
		{
			isReady = false;
			rb.Dash(magnitude, duration);
			StartCoroutine(nameof(IE_DashReady), cooldown);
		}
	}
	public void RestoreDash()
	{
		isReady = true;
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
	}
	private IEnumerator IE_DashReady(float _cooldown)
	{
		yield return new WaitForSeconds(_cooldown);
		while(rb.IsGrounded() == false)
		{
			yield return null;
		}
		isReady = true;
	}
	#endregion
}
