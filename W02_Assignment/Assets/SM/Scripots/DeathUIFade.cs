using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUIFade : MonoBehaviour
{
    #region PublicVariables
    private Animator anim;
    #endregion

    #region PrivateVariables
    #endregion

    #region PublicMethod
    public void PlayFadeAnimation()
    {
        anim.SetTrigger("FadeStart");
    }
    #endregion

    #region PrivateMethod
    private void OnEnable()
    {
        TryGetComponent(out anim);
    }
    #endregion
}
