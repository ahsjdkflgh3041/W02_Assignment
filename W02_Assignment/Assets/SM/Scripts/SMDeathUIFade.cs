using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMDeathUIFade : MonoBehaviour
{
    #region PublicVariables
    private Animator anim;
    #endregion

    #region PrivateVariables
    [SerializeField] private JWPlayer player;
    #endregion

    #region PublicMethod
    public void PlayFadeAnimation()
    {
        anim.SetTrigger("FadeStart");
    }

    public void RespawnPlayer()
    {
        player.Respawn();
    }
    #endregion

    #region PrivateMethod
    private void OnEnable()
    {
        TryGetComponent(out anim);
    }
    #endregion
}
