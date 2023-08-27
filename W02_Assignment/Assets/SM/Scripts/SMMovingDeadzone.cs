using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMMovingDeadzone : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float minZPosition = -5f;
    [SerializeField] private float movingDuration = 3f;

    private float t = 0f;

    private Vector3 initialLocalPosition;
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    private void OnEnable()
    {
        initialLocalPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        transform.localPosition = Vector3.Lerp(initialLocalPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, minZPosition), t / movingDuration);
        if (t / movingDuration > 1)
        {
            t = 0;
            transform.localPosition = initialLocalPosition;
            gameObject.SetActive(false);
        }
    }
    #endregion
}
