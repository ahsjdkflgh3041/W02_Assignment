using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	#region PublicVariables
	public static GameManager instance;
	public UnityEvent onPlayerDead;
	#endregion

	#region PrivateVariables
	[SerializeField] private JWPlayer player;
	[SerializeField] private Camera mainCamera;
	#endregion

	#region PublicMethod
	public JWPlayer GetPlayer() => player;
	public Camera GetMainCamera() => mainCamera;
	public void OnPlayerDead()
	{
		onPlayerDead.Invoke();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		if(instance == null)
			instance = this;
	}
	#endregion
}
