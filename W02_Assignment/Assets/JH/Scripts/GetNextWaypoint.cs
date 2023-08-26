using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNextWaypoint: MonoBehaviour
{
#region PublicVariables
    
#endregion

#region PrivateVariables

#endregion

#region PublicMethods
    public Transform GetWayPoint(int _waypointIndex)
    {
        return transform.GetChild(_waypointIndex);
    }

    public int GetNextWaypointIndex(int _currentWaypointIndex)
    {
        int nextWaypointIndex = _currentWaypointIndex + 1;

        if(nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
#endregion

#region PrivateMethods
    void Start()
    {
        
    }
    void Update()
    {
        
    }
#endregion
}
