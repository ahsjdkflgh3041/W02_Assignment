using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmGhostManager : MonoBehaviour
{
	#region PublicVariables
	public bool startRecording = false;
	#endregion

	#region PrivateVariables
	[Header("Ghost Options")]
	// 처음으로 움직이기 시작한 후, 고스트가 움직이기 시작하는 초수
	[SerializeField, Tooltip("처음으로 움직이기 시작한 후, 고스트가 움직이기 시작하는 초수")] private float recordingTime = 3f;
	// 고스트가 여러개일시 고스트가 움직이는 간격을 나타내는 초수
	[SerializeField, Tooltip("고스트가 여러개일시 고스트가 움직이는 간격을 나타내는 초수")] private float playDuration = 0.5f;
	// 씬에 존재하는 모든 고스트들을 담는 리스트
	[SerializeField, Tooltip("씬에 존재하는 모든 고스트들을 담는 리스트")] private List<SmGhostController> ghostControllers = new List<SmGhostController>();
	// 움직임과 입력값을 받는 MovingInfomation형 queue
	private Queue<MovingInfoamtion> movingInfomationQueue = new Queue<MovingInfoamtion>();
	#endregion

	#region PublicMethod
	// 시작 조건
	public void SetStart() => startRecording = true;

	// SmGhostRecorder 스크립트에서부터 FixedUpdate() 한 틱당 호출됨
	public void GetTransformValue(Vector3 _position, Vector2 _rotation)
	{
		MovingInfoamtion info = new MovingInfoamtion(_position, _rotation);
		movingInfomationQueue.Enqueue(info);
		PlayGhostMoving();
	}
	#endregion

	#region PrivateMethod
	// 매 FixedUpdate() 틱마다 호출된다
	private void PlayGhostMoving()
	{
		StartCoroutine("IEMovePosition");
	}

    private void OnEnable()
    {
		// 시작 시 모든 고스트를 안보이게 함
		foreach (SmGhostController var in ghostControllers)
		{
			var.gameObject.SetActive(false);
		}
    }

    IEnumerator IEMovePosition()
	{
		// 녹화 시간 후에 시작됨
		yield return new WaitForSeconds(recordingTime);
		MovingInfoamtion info = movingInfomationQueue.Dequeue();
		foreach (SmGhostController var in ghostControllers)
		{
			// 고스트 하나당 playDuration * index 초 후 재생됨
			var.gameObject.SetActive(true);
			var.MovePosition(info);
			yield return new WaitForSeconds(playDuration);
		}
		#endregion
	}
}

public class MovingInfoamtion
{
	public Vector3 position { get; private set; }
	public Vector2 rotation { get; private set; }

	public MovingInfoamtion(Vector3 _position, Vector2 _rotation)
    {
		position = _position;
		rotation = _rotation;
    }
}
