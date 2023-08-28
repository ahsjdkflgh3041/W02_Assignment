using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SMGhostRecorder : MonoBehaviour
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

    [SerializeField] private SMGhostManager ghostManager;
    #endregion

    #region PublicMethod
    public void SetInput(Vector2 _input)
    {
        inputValue = _input;
    }
    #endregion

    #region PrivateMethod

    private void FixedUpdate()
    {
        if (ghostManager.startRecording)
        {
            ghostManager.GetTransformValue(transform.position, inputValue);
        }
    }
    #endregion
}
