using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JWRigidBody))]
public class JWDash : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private CharacterController controller;
	private JWRigidBody gravity;
	[SerializeField] private float dashPower;
	#endregion

	#region PublicMethod
	public void Dash()
	{
		gravity.SetBodyTypeKinematic(true);
		//rb.velocity += transform.forward * dashPower;
		Invoke(nameof(DashEnd), 0.15f);
	}
	public void DashEnd()
	{
		gravity.SetBodyTypeKinematic(false);
		//rb.velocity = Vector3.zero;
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out controller);
		TryGetComponent(out gravity);
	}
	#endregion
}
