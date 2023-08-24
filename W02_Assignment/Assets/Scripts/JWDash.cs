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
	private JWRigidBody rb;
	[SerializeField] private float magnitude;
	[SerializeField] private float duration;
	#endregion

	#region PublicMethod
	public void Dash()
	{
		rb.Dash(magnitude, duration);
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out controller);
		TryGetComponent(out rb);
	}
	#endregion
}
