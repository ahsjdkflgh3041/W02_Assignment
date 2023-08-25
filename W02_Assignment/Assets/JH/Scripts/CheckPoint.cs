using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    private Vector3 respawnPoint;
    #endregion

#region PublicMethods

#endregion

#region PrivateMethods
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

        }
    }
    #endregion
}
