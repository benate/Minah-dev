using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour
{
	public void Start()
	{
	
	}

	public void Update()
	{
	
	}

	public void OnClickNextScene()
	{
		Application.LoadLevel("GameBaseCampScene");
	}
}
