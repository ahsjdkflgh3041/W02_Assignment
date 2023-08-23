using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JWIndicator : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private LineRenderer lineIndicator;
	[SerializeField] private GameObject groundIndicator;

	private float groundIndicatorHeight;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out lineIndicator);
	}
	private void Update()
	{
		IndicateGround();
	}
	private void IndicateGround()
	{
		Vector3 groundPosition = GetGroundPosition();
		if (groundPosition != Vector3.zero)
		{
			DrawLineIndicator(groundPosition);
			DrawGroundIndicator(groundPosition);
		}
		else
		{
			EraseLineIndicator();
			EraseGroundIndicator();
		}
	}
	private Vector3 GetGroundPosition()
	{
		RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.down, float.MaxValue, 1 << LayerMask.NameToLayer("Ground"));
		if(hit.Length > 0)
		{
			return hit[hit.Length - 1].point;
		}
		return Vector3.zero;
	}
	private void DrawLineIndicator(Vector3 _position)
	{
		lineIndicator.SetPosition(0, transform.position);
		lineIndicator.SetPosition(1, _position);
	}
	private void EraseLineIndicator()
	{
		lineIndicator.SetPosition(0, transform.position);
		lineIndicator.SetPosition(1, transform.position);
	}
	private void DrawGroundIndicator(Vector3 _position)
	{
		groundIndicatorHeight = groundIndicator.transform.localScale.y / 2;
		Vector3 targetPosition = _position + new Vector3(0, groundIndicatorHeight, 0);
		groundIndicator.transform.position = targetPosition;
		groundIndicator.SetActive(true);
	}
	private void EraseGroundIndicator()
	{
		groundIndicator.SetActive(false);
	}
	#endregion
}
