using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMKey : MonoBehaviour
{
	#region PublicVariables
	public bool _isCollected = false;
	public bool isCollected
	{
		get 
		{
			return _isCollected;
		}
		set
		{
			if (value != _isCollected)
			{
				door.CheckEveryKey();
				_isCollected = value;
			}
		}
	}
	#endregion

	#region PrivateVariables
	[SerializeField] private SMLockedDoor door;
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void OnEnable()
    {
		TryGetComponent(out door);
    }

    private void OnTriggerEnter(Collider other)
    {
		JWPlayer player;
		other.TryGetComponent(out player);
		if (player != null)
		{
			isCollected = true;
			door.CheckEveryKey();
		}
    }
    #endregion
}