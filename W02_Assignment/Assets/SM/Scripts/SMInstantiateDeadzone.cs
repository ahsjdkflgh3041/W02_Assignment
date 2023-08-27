using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMInstantiateDeadzone : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private List<GameObject> movingDeadzones;
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void OnEnable()
    {
        StartCoroutine("IE_InstantiateMovingDeadzone");
    }

    IEnumerator IE_InstantiateMovingDeadzone()
    {
        while (true)
        {
            foreach (GameObject deadzone in movingDeadzones)
            {
                deadzone.SetActive(true);
                yield return new WaitForSeconds(3f);
            }
        }
    }
    #endregion
}
