using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class FPSCounter : MonoBehaviour
{
	Text text;

	void Awake()
	{
		text = GetComponent<Text> ();
	}

	void Start()
	{
		StartCoroutine (UpdateCounter ());
	}

	IEnumerator UpdateCounter()
	{
		while (true)
		{
			text.text = "FPS : " + Mathf.RoundToInt(1 / Time.deltaTime);
			yield return new WaitForSeconds (0.25f);
		}
	}
}
