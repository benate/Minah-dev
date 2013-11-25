using UnityEngine;
using System.Collections;

public class SelectedManager : MonoBehaviour
{
	public void Start()
	{
	
	}

	public void Update()
	{
	
	}

	public void OnClickGameScene()
	{
		Debug.Log("OnClickGameScene");

		Application.LoadLevel("InGameScene");
	}
}
