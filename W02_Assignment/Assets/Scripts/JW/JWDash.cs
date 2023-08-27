using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JWRigidBody))]
public class JWDash : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private JWRigidBody rb;
	private Renderer rendBody;
	private Renderer rendGoggle;
	[SerializeField] private float magnitude;
	[SerializeField] private float duration;
	[SerializeField] private float cooldown;
	private Color32 colorIdle = new Color32(203, 231, 195, 255);
	private Color32 colorCantDash = new Color32(202, 86, 36, 255);
	private Color32 colorGoggleIdle = new Color32(129, 197, 150, 255);
	private Color32 colorGoggleCantDash = new Color32(123, 9, 9, 255);
	private bool isReady = true;
	#endregion

	#region PublicMethod
	public void Dash()
	{
		if(isReady == true)
		{
			isReady = false;
			rb.Dash(magnitude, duration);
			rendBody.material.color = colorCantDash;
			rendGoggle.material.color = colorGoggleCantDash;
			StartCoroutine(nameof(IE_DashReady), cooldown);
		}
	}
	public void RestoreDash()
	{
		isReady = true;
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out rb);
		TryGetComponent(out rendBody);
		transform.Find("Goggle").TryGetComponent(out rendGoggle);
	}
	private IEnumerator IE_DashReady(float _cooldown)
	{
		yield return new WaitForSeconds(_cooldown);
		while(rb.IsGrounded() == false)
		{
			yield return null;
		}
		isReady = true;
		rendBody.material.color = colorIdle;
		rendGoggle.material.color = colorGoggleIdle;
	}
	#endregion
}
