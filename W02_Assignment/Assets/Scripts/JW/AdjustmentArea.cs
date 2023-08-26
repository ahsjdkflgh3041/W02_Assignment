using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustmentArea : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Vector3 direction;
	[SerializeField] private float magnitude;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	protected void OnTriggerEnter(Collider _other)
	{
		OperationAdjustment(_other);
	}
	private void OperationAdjustment(Collider _other)
	{
		JWRigidBody rb;
		_other.TryGetComponent(out rb);
		if (rb != null)
		{
			rb.AddForce(direction, magnitude);
		}
	}
	#endregion
}