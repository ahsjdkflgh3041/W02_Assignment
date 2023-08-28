using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMLockedDoor : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private List<SMKey> keys = new List<SMKey>();
    [SerializeField] private float yPositionMax = 13f;
    [SerializeField] private float initialYPosition;
    #endregion

    #region PublicMethod
    public void CheckEveryKey()
    {
        foreach (SMKey key in keys)
        {
            if (!key.isCollected)
            {
                return;
            }
        }
        Open();
    }

    public void ResetDoor()
    {
        StopAllCoroutines();
        gameObject.transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
    }
   
    public void ResetAllKey()
    {
        foreach (SMKey key in keys)
        {
            key.gameObject.SetActive(true);
            key.isCollected = false;
        }
    }
    #endregion

    #region PrivateMethod

    private void OnEnable()
    {
        initialYPosition = transform.position.y;
    }

    private void Open()
    {
        StartCoroutine("IE_OpenCoroutine");
    }

    IEnumerator IE_OpenCoroutine()
    {
        float yPosition = initialYPosition;
        while (yPosition < yPositionMax)
        {
            yPosition = Mathf.Lerp(yPosition, yPositionMax, Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
            yield return new WaitForFixedUpdate();
        }
    }    
    #endregion
}
