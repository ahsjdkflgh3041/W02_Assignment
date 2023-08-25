using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JWCameraController : MonoBehaviour
{
	#region PublicVariables
	public Camera main;
	public Transform target;
	#endregion

	#region PrivateVariables
	private JWRigidBody rb;

	private Vector3 offset = new Vector3(25, 20, -25);
	[SerializeField] private float camSpeed;
	[SerializeField] private float zoomSpeed;
	[SerializeField] private float zoomMin;
	[SerializeField] private float zoomMax;

	private float magnitudeMax = 0.3f;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		target.TryGetComponent(out rb);
	}
	private void FixedUpdate()
	{
		FollowTarget();
	}
	private void FollowTarget()
	{
		Vector3 destination = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, destination, camSpeed * Time.fixedDeltaTime);
		float zoom = Mathf.Lerp(zoomMin, zoomMax, rb.GetSpeed() / magnitudeMax);
		main.fieldOfView = Mathf.Lerp(main.fieldOfView, zoom, zoomSpeed * Time.fixedDeltaTime);
	}
	#endregion
}
