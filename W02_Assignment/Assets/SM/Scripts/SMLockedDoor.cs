using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMLockedDoor : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private List<SMKey> keys = new List<SMKey>();

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
        
    }

    IEnumerator IE_OpenCorutine()
    {
        yield return new WaitForFixedUpdate();
    }
    #endregion
}
