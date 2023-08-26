using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Transform parent;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		transform.parent.TryGetComponent(out parent);
		float yScale = Mathf.Clamp(parent.position.y, 1, float.MaxValue);
		float yPos = Mathf.Clamp(-(yScale / 2) - 1f, float.MinValue, -0.5f);
		transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
		transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
	}
	#endregion
}
