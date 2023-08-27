using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SMGhostManager : MonoBehaviour
{
	#region PublicVariables
	public bool startRecording;
	#endregion

	#region PrivateVariables

	[Header("Chasing Ghost Options")]
	// ó������ �����̱� ������ ��, ��Ʈ�� �����̱� �����ϴ� �ʼ�
	[SerializeField, Tooltip("ó������ �����̱� ������ ��, ��Ʈ�� �����̱� �����ϴ� �ʼ�")] private float recordingTime = 3f;
	// ��Ʈ�� �������Ͻ� ��Ʈ�� �����̴� ������ ��Ÿ���� �ʼ�
	[SerializeField, Tooltip("��Ʈ�� �������Ͻ� ��Ʈ�� �����̴� ������ ��Ÿ���� �ʼ�")] private float playDuration = 0.5f;
	// ���� �����ϴ� ��� ��Ʈ���� ��� ����Ʈ
	[SerializeField, Tooltip("���� �����ϴ� ��� ��Ʈ���� ��� ����Ʈ")] private List<SMGhostController> ghostControllers = new List<SMGhostController>();
	// �����Ӱ� �Է°��� �޴� MovingInfomation�� queue
	private Queue<MovingInformation> movingInfomationQueue = new Queue<MovingInformation>();

	[Header("Heading Ghost Options")]
	private Queue<MovingInformation> headingInfomationQueue = new Queue<MovingInformation>();
	private string jsonFileName = "./SaveData.json";
	private string jsonString = "";
	[SerializeField] SMGhostController headingGhost;
	#endregion

	#region PublicMethod
	// ���� ����
	public void SetStart() => startRecording = true;

	public void RecordLapTime() => File.WriteAllText(jsonFileName, jsonString);

	// SmGhostRecorder ��ũ��Ʈ�������� FixedUpdate() �� ƽ�� ȣ���
	public void GetTransformValue(Vector3 _position, Vector2 _rotation)
	{
		MovingInformation info = new MovingInformation(_position, _rotation);
		movingInfomationQueue.Enqueue(info);
		SetJson(info);
		PlayGhostMoving();
	}
	public void ResetGhosts()
	{
		StopAllCoroutines();
		startRecording = false;
		movingInfomationQueue.Clear();
		jsonString = "";
		// ���� �� ��� ��Ʈ�� �Ⱥ��̰� ��
		headingGhost.gameObject.SetActive(false);
		foreach (SMGhostController var in ghostControllers)
		{
			var.gameObject.SetActive(false);
		}

		if (File.Exists(jsonFileName))
		{
			ParsingJsonData();
			PlayHeadingGhost();
		}
	}
	#endregion

	#region PrivateMethod
	private void SetJson(MovingInformation info)
	{
		jsonString += JsonUtility.ToJson(info);
		//File.WriteAllText(jsonFileName, jsonString);
	}

	// �� FixedUpdate() ƽ���� ȣ��ȴ�
	private void PlayGhostMoving()
	{
		StartCoroutine("IE_MoveFollwingPosition");
	}

    private void OnEnable()
	{
		ResetGhosts();
    }

	private void PlayHeadingGhost()
	{
		headingGhost.gameObject.SetActive(true);
		StartCoroutine("IE_MoveHeadingPosition");
	}

	private void ParsingJsonData()
	{
		headingInfomationQueue.Clear();
		string jsonDataString = File.ReadAllText(jsonFileName);

		List<string> jsonObjects = ExtractJSONObjects(jsonDataString);

		foreach (string jsonObject in jsonObjects)
		{
			MovingInformation info = JsonUtility.FromJson<MovingInformation>(jsonObject);
			headingInfomationQueue.Enqueue(info);
		}
	}

	private void FindIndex(ref int startIndex, ref int endIndex ,string jsonString)
	{
		Stack<char> checkStack = new Stack<char>();
		for (int i = startIndex; i < jsonString.Length; i++)
		{
			if (jsonString[i] == '{')
			{
				if (checkStack.Count == 0)
				{
					startIndex = i;
				}
				checkStack.Push(jsonString[i]);
			}
			else if (jsonString[i] == '}')
			{
				checkStack.Pop();
				if (checkStack.Count == 0)
				{
					endIndex = i;
					return;
				}
			}
		}
	}

	private List<string> ExtractJSONObjects(string jsonString)
	{
		List<string> jsonObjects = new List<string>();
		int currentIndex = 0;
		int startIndex = 0;
		int endIndex = 0;

		while (currentIndex < jsonString.Length)
		{
			startIndex = currentIndex;
			FindIndex(ref startIndex, ref endIndex, jsonString);

			if (startIndex != -1 && endIndex != -1)
			{
				int objectLength = endIndex - startIndex + 1;
				string jsonObject = jsonString.Substring(startIndex, objectLength);
				jsonObjects.Add(jsonObject);
				currentIndex = endIndex + 1;
			}
			else
			{
				break;
			}
		}

		return jsonObjects;
	}

	IEnumerator IE_MoveFollwingPosition()
	{
		// ��ȭ �ð� �Ŀ� ���۵�
		yield return new WaitForSeconds(recordingTime);
		MovingInformation info = movingInfomationQueue.Dequeue();
		foreach (SMGhostController var in ghostControllers)
		{
			// ��Ʈ �ϳ��� playDuration * index �� �� �����
			var.gameObject.SetActive(true);
			var.MovePosition(info);
			yield return new WaitForSeconds(playDuration);
		}
	}

	IEnumerator IE_MoveHeadingPosition()
	{
		 while(headingInfomationQueue.Count != 0)
		{
			MovingInformation info = headingInfomationQueue.Dequeue();
			headingGhost.MovePosition(info);
			yield return new WaitForFixedUpdate();
		}
	}
	#endregion
}


    [System.Serializable]
public class MovingInformation
{
	public Vector3 position;
	public Vector2 rotation;

	public MovingInformation(Vector3 _position, Vector2 _rotation)
    {
		position = _position;
		rotation = _rotation;
    }
}
