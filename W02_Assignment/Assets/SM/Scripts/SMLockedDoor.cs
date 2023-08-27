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

    private Animator anim;
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
    #endregion

    #region PrivateMethod

    private void OnEnable()
    {
        TryGetComponent(out anim);
    }

    private void Open()
    {
        StartCoroutine("IE_OpenCoroutine");
    }

    IEnumerator IE_OpenCoroutine()
    {
        float yPosition = transform.position.y;
        while (yPosition < yPositionMax)
        {
            yPosition = Mathf.Lerp(yPosition, yPositionMax, Time.fixedDeltaTime);
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
}
