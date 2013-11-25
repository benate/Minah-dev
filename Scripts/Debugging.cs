using UnityEngine;
using System.Collections;

public class Debugging : MonoBehaviour {

	static Debugging ms_instance = null;
	public static Debugging instance { get { return ms_instance; } }

	public int touchDownCount = 0;
	public Vector2 prevPos = Vector2.zero;
	public Vector2 lastPos = Vector2.zero;	

	// bool pause = false;

	void Awake()
	{
		ms_instance = this;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			World.instance.OnMsg(PacketData.OnPause.Create(0), true);

			//pause = !pause;

			//if(true == pause)
			//    Time.timeScale = 0.0f;
			//else
			//    Time.timeScale = 1.0f;

		}
	}
}
