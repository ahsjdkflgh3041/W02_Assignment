using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JWGravity : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Rigidbody rb;
	[SerializeField] private float gravityScale = 1f;
	private bool isKinematic;
	private const float GRAVITY = -9.81f;
	#endregion

	#region PublicMethod
	public void SetBodyTypeKinematic(bool b) => isKinematic = b;
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
		rb.useGravity = false;
	}
	private void FixedUpdate()
	{
		if (isKinematic)
			return;
		Vector3 gravity = GRAVITY * gravityScale * Vector3.up;
		rb.AddForce(gravity, ForceMode.Acceleration);
	}
	#endregion
}
