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
            // Input Value�� ó������ �ٲ� �� ��Ʈ�� �����̱� �����Ѵ�.
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
