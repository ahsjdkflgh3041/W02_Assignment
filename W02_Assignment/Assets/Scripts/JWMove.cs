using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JWMove : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Vector2 direction;
	[SerializeField] private float moveSpeed;
	private float turnSmoothTime = 0;
	private float turnSmoothVelocity;
	#endregion

	#region PublicMethod
	public void Move(Vector2 _direction)
	{
		direction = _direction;
	}
	#endregion

	#region PrivateMethod
	private void FixedUpdate()
	{
		MoveCharacter();
	}

	private void MoveCharacter()
	{
		if (direction == Vector2.zero)
		{
			return;
		}
		float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
		float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
		transform.rotation = Quaternion.Euler(0f, angle, 0f);
		transform.Translate(Vector3.forward  * Time.deltaTime * moveSpeed);
	}
	#endregion
}
