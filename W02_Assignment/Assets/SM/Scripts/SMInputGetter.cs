using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SMInputGetter : MonoBehaviour
{

    #region PublicVariables
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SMSceneManager.Instance.ChangeScene("SM_test");
        }
    }
    #endregion
}
