using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTrigger : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    [SerializeField] private Transform hiddenStar;
    private Vector3 originPosition;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float speed = 0.1f;
    private float duration = 20f;
    private float startTime;
    private bool isStart = false;

#endregion

#region PublicMethods

#endregion

#region PrivateMethods
    void Start()
    {

        originPosition = hiddenStar.position;
    }
    void Update()
    {
        if (isStart)
        {
            float elapsedTime = Time.time - startTime;
            if(elapsedTime <= duration)
            {
                float t = elapsedTime / duration;
                hiddenStar.position = Vector3.Lerp(initialPosition, targetPosition, t) + Vector3.down * (speed * t * Time.deltaTime);
            }
            else
            {
                isStart = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("OnTrigger");
            startTime = Time.time;
            isStart = true;
            initialPosition = hiddenStar.position;
            targetPosition = gameObject.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isStart = false;
            hiddenStar.position = originPosition;
        }
    }
    #endregion
}
