using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JWMove : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private JWRigidBody rb;
	[SerializeField] private float moveSpeed;
	#endregion

	#region PublicMethod
	public void Move(Vector2 _direction)
	{
		rb.MoveToDirection(_direction, moveSpeed);
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
	}

	#endregion
}
