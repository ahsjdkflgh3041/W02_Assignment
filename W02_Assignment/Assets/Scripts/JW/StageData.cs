using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] List<Star> stars = new List<Star>();
	#endregion

	#region PublicMethod
	public int GetObtainedStarCount()
	{
		int result = 0;
		foreach (Star st in stars)
		{
			if (st.IsGet() == true)
				++result;
		}
		return result;
	}
	public int GetTotalStarCount()
	{
		return stars.Count;
	}
	#endregion

	#region PrivateMethod
	#endregion
}
