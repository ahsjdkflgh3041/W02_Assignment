using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SMEndSetter : MonoBehaviour
{

    #region PublicVariables
    #endregion

    #region PrivateVariables
    [SerializeField] private string targetScene = "SM_test";
    [SerializeField] private bool isInputKey = true;
    [SerializeField] private TMP_Text text;
    [SerializeField] private StageData starData;

    private JWPlayer player;
    #endregion

    #region PublicMethod
    public void OnGetInputKey()
    {
        isInputKey = true;
    }
    #endregion

    #region PrivateMethod

    private void OnEnable()
    {
        player = GameObject.FindAnyObjectByType<JWPlayer>();
        text.text = "X " + starData.GetObtainedStarCount() + " / " + starData.GetTotalStarCount();
        player.CanNotAct();
    }

    private void Update()
    {
        if (Input.anyKeyDown && isInputKey)
        {
            SMSceneManager.Instance.ChangeScene(targetScene);
        }
    }
    #endregion
}
