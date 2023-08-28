using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SMSceneManager : MonoBehaviour
{

    #region PublicVariable
    public static SMSceneManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    #region PrivateVariables\
    private static SMSceneManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region PublicMethod
    public void ChangeScene(string _sceneName)
	{
        SceneManager.LoadScene(_sceneName);
	}
	#endregion

	#region PrivateMethod
	#endregion
}
