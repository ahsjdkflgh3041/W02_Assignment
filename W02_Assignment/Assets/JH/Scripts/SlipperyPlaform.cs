using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyPlaform : MonoBehaviour
{
    #region PublicVariables
    public JWRigidBody player;
    #endregion

    #region PrivateVariables
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
        if (other.gameObject.CompareTag("Player"))
        {

            player.linearDrag = 0;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.linearDrag = 12f;
        }

    }

    #endregion
}
