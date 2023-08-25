using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmGhostRecorder : MonoBehaviour
{
    #region PublicVariables
    #endregion

    #region PrivateVariables
    private Vector2 _inputValue;
    private Vector2 inputValue
    {
        get
        {
            return _inputValue;
        }
        set
        {
            // Input Value가 처음으로 바뀔 때 고스트가 움직이기 시작한다.
            if (_inputValue == Vector2.zero)
            {
                ghostManager.SetStart();
            }
            _inputValue = value;
        }
    }

    [SerializeField] private SmGhostManager ghostManager;
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod

    private void OnMove(InputValue _value)
    {
        inputValue = _value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (ghostManager.startRecording)
        {
            ghostManager.GetTransformValue(transform.position, inputValue);
        }
    }
    #endregion
}
