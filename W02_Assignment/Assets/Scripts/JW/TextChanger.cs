using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private TextMeshPro tm;
	private Animator anim;

	[SerializeField] List<string> texts = new List<string>();
	private int index = 0;
	#endregion

	#region PublicMethod
	public void PrintNext()
	{
        if ((index < texts.Count - 1))
        {
			++index;
			anim.Play("Highlight");
			tm.text = texts[index];
		}
	}
	public void InitializeText()
	{
		if(index != 0)
		{
			index = 0;
			anim.Play("Highlight");
			tm.text = texts[index];
		}
	}
	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		TryGetComponent(out tm);
		TryGetComponent(out anim);
	}
	#endregion
}
