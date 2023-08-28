using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : TriggerPlayerInEvent
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator anim;

	[SerializeField] private bool isGet;
	#endregion

	#region PublicMethod
	public void DisableThisIfGetBefore()
	{
		gameObject.SetActive(!isGet);
	}
	public void GameObjectSetDeactive()
	{
		gameObject.SetActive(false);
	}
	public bool IsGet() => isGet;
	#endregion

	#region PrivateMethod
	private void Start()
	{
		TryGetComponent(out anim);
		DisableThisIfGetBefore();
	}
	protected override void OnPlayerIn(JWPlayer _player)
	{
		anim.Play("Get");
		isGet = true;
	}
	#endregion
}
