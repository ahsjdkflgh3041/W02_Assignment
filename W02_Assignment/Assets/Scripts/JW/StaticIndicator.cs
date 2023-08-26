using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticIndicator : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Transform parent;
	private GameObject line;
	private GameObject indicator;

	private const float LINE_SCALE_X = 0.03f;
	private const float LINE_SCALE_Z = 0.03f;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		line = transform.Find("Line").gameObject;
		indicator = transform.Find("Base").gameObject;
		DrawIndicator();
	}
	private void DrawIndicator()
	{
		DrawBase();
		DrawLine();
	}
	private void DrawBase()
	{
		float targetPosY = -parent.transform.position.y - 1;
		indicator.transform.localPosition = new Vector3(0, targetPosY, 0);
	}
	private void DrawLine()
	{
		float lineScaleY = Mathf.Abs(indicator.transform.localPosition.y / 2);
		line.transform.localScale = new Vector3(LINE_SCALE_X, lineScaleY, LINE_SCALE_Z);
		line.transform.localPosition = new Vector3(0, -lineScaleY);
	}
	#endregion
}
