using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    [SerializeField] private float distance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitTime;
    private bool isMoved = true;
    private Vector3 originalPos;
    private Vector3 targetPos;
    private float timer;
#endregion

#region PublicMethods

#endregion

#region PrivateMethods
    void Start()
    {
        originalPos = transform.position;
        targetPos = originalPos - new Vector3(0f, distance, 0f);
        timer = waitTime;
    }
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isMoved = !isMoved;
            timer = waitTime;
        }

        Vector3 currentTarget = isMoved ? targetPos : originalPos;

        Vector3 currentPosition = transform.position;
        transform.position = Vector3.MoveTowards(currentPosition, currentTarget, moveSpeed * Time.deltaTime);
    }


    #endregion
}
