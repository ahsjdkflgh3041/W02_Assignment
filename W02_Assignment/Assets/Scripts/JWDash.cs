using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JWGravity))]
public class JWDash : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Rigidbody rb;
	private JWGravity gravity;
	[SerializeField] private float dashPower;
	#endregion

	#region PublicMethod
	public void Dash()
	{
		gravity.SetBodyTypeKinematic(true);
		rb.velocity += transform.forward * dashPower;
		Invoke(nameof(DashEnd), 0.15f);
	}
	public void DashEnd()
	{
		gravity.SetBodyTypeKinematic(false);
		rb.velocity = Vector3.zero;
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
		TryGetComponent(out gravity);
	}
	#endregion
}
