using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    [SerializeField] private Transform platform;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed;
    private int direction = 1;
    #endregion

    #region PublicMethods

    #endregion

    #region PrivateMethods
    void Start()
    {

    }
    void FixedUpdate()
    {
        Vector3 target = CurrentMovementTarget();
        platform.position = Vector3.Lerp(platform.position, target, speed * Time.fixedDeltaTime);

        float distance = (target - (Vector3)platform.position).magnitude;

        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    private Vector3 CurrentMovementTarget()
    {
        if (direction == 1)
        {
            return startPoint.position;
        }
        else
        {
            return endPoint.position;
        }
    }
    private void OnDrawGizmos()
    {
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }

    }


    #endregion
}

