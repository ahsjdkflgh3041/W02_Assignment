using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmGhostController : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	// ���� ������ ��Ʈ�� �̵���Ų��
	public void MovePosition(MovingInfoamtion _info)
	{
		transform.position = _info.position;
		transform.rotation = Quaternion.LookRotation(new Vector3(_info.rotation.x, 0f, _info.rotation.y));
	}
	#endregion

	#region PrivateMethod
    #endregion
}
