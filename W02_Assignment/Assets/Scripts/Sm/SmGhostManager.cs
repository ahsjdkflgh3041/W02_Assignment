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
	// ó������ �����̱� ������ ��, ��Ʈ�� �����̱� �����ϴ� �ʼ�
	[SerializeField, Tooltip("ó������ �����̱� ������ ��, ��Ʈ�� �����̱� �����ϴ� �ʼ�")] private float recordingTime = 3f;
	// ��Ʈ�� �������Ͻ� ��Ʈ�� �����̴� ������ ��Ÿ���� �ʼ�
	[SerializeField, Tooltip("��Ʈ�� �������Ͻ� ��Ʈ�� �����̴� ������ ��Ÿ���� �ʼ�")] private float playDuration = 0.5f;
	// ���� �����ϴ� ��� ��Ʈ���� ��� ����Ʈ
	[SerializeField, Tooltip("���� �����ϴ� ��� ��Ʈ���� ��� ����Ʈ")] private List<SmGhostController> ghostControllers = new List<SmGhostController>();
	// �����Ӱ� �Է°��� �޴� MovingInfomation�� queue
	private Queue<MovingInfoamtion> movingInfomationQueue = new Queue<MovingInfoamtion>();
	#endregion

	#region PublicMethod
	// ���� ����
	public void SetStart() => startRecording = true;

	// SmGhostRecorder ��ũ��Ʈ�������� FixedUpdate() �� ƽ�� ȣ���
	public void GetTransformValue(Vector3 _position, Vector2 _rotation)
	{
		MovingInfoamtion info = new MovingInfoamtion(_position, _rotation);
		movingInfomationQueue.Enqueue(info);
		PlayGhostMoving();
	}
	#endregion

	#region PrivateMethod
	// �� FixedUpdate() ƽ���� ȣ��ȴ�
	private void PlayGhostMoving()
	{
		StartCoroutine("IEMovePosition");
	}

    private void OnEnable()
    {
		// ���� �� ��� ��Ʈ�� �Ⱥ��̰� ��
		foreach (SmGhostController var in ghostControllers)
		{
			var.gameObject.SetActive(false);
		}
    }

    IEnumerator IEMovePosition()
	{
		// ��ȭ �ð� �Ŀ� ���۵�
		yield return new WaitForSeconds(recordingTime);
		MovingInfoamtion info = movingInfomationQueue.Dequeue();
		foreach (SmGhostController var in ghostControllers)
		{
			// ��Ʈ �ϳ��� playDuration * index �� �� �����
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
