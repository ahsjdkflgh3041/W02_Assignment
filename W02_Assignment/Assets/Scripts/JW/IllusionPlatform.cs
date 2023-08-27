using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionPlatform : MonoBehaviour
{ 
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator anim;
	[SerializeField] private List<Collider> colliders = new List<Collider>();
	[SerializeField] private float respawnTime;
	#endregion

	#region PublicMethod
	public void Disappear()
	{
		anim.Play("Disappear");
		Invoke(nameof(Appear), respawnTime);
	}
	public void DeactiveColliders()
	{
		foreach (Collider c in colliders)
		{
			c.enabled = false;
		}
	}
	public void Appear()
	{
		anim.Play("Appear");
	}
	public void ActiveColliders()
	{
		foreach (Collider c in colliders)
		{
			c.enabled = true;
		}
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out anim);
	}
	#endregion
}
