using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JWCameraController : MonoBehaviour
{
	#region PublicVariables
	public static JWCameraController instance;
	public Camera main;
	public Transform target;
	#endregion

	#region PrivateVariables
	private JWRigidBody rb;

	private Vector3 offset = new Vector3(25, 20, -25);
	private Vector3 rotation = new Vector3(30, -45, 0);
	[SerializeField] private float jumpXRotation = -20;
	[SerializeField] private float jumpYOffset = -13;
	[SerializeField] private float camSpeed;
	[SerializeField] private float zoomSpeed;
	[SerializeField] private float zoomMin;
	[SerializeField] private float zoomMax;
	private bool targetJumped;

	private float magnitudeMax = 0.3f;
	#endregion

	#region PublicMethod
	public void TargetJumped(bool b) => targetJumped = b;
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}
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
		float offsetYAdditive = targetJumped ? jumpYOffset : 0;
		float rotationXAdditive = targetJumped ? jumpXRotation : 0;
		Vector3 targetPosition = target.position + new Vector3(offset.x, offset.y + offsetYAdditive, offset.z);
		Vector3 targetRotation = new Vector3(rotation.x + rotationXAdditive, rotation.y, rotation.z);
		transform.position = Vector3.Lerp(transform.position, targetPosition, camSpeed * Time.fixedDeltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), camSpeed * Time.fixedDeltaTime);
		float zoom = Mathf.Lerp(zoomMin, zoomMax, rb.GetSpeed() / magnitudeMax);
		main.fieldOfView = Mathf.Lerp(main.fieldOfView, zoom, zoomSpeed * Time.fixedDeltaTime);
	}
	#endregion
}
