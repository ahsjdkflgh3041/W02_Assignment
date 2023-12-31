using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SMGhostRecorder : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private bool startWhenInput = true;

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
            if (_inputValue != value && startWhenInput)
            {
                ghostManager.SetStart();
            }
            _inputValue = value;
        }
    }

    [SerializeField] private SMGhostManager ghostManager;
	#endregion

	#region PublicMethod
	public void SetStartWhenInput(bool _value)
	{
		startWhenInput = _value;
	}

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
