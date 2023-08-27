using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerFollow : MonoBehaviour
{
#region PublicVariables
    
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player1")
        {
            collision.transform.SetParent(transform);
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);   
    }
    #endregion
}
