using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInputUpdater
{
	protected GameInputReceiver m_receiver = null;
	public GameInputReceiver receiver { set { m_receiver = value; } }

	public virtual void Update() { }
}

public class GameInputReceiver
{
	public virtual void TouchDown(Vector2 pos) { }
	public virtual void TouchUp(Vector2 pos) { }
	public virtual void TouchSlideDiff(Vector2 pos) { }	
	public virtual void TouchHold(Vector2 pos) { }	
}

public class GameInput : BaseBehaviour
{
	public enum InputType
	{
		Normal,
		Gesture,		
	}

	Dictionary<InputType, GameInputReceiver> m_receivers = null;
	GameInputUpdater m_updater = null;

	public InputType inputType { set { m_updater.receiver = m_receivers[value]; } }

	void Awake ()
	{		
		RuntimePlatform platform = Application.platform;
		if (platform == RuntimePlatform.Android
		|| platform == RuntimePlatform.IPhonePlayer)
		{
			m_updater = new GameInput_Mobile();
		}
		else
		{
			m_updater = new GameInput_Pc();
		}

		m_receivers = new Dictionary<InputType, GameInputReceiver>();
		m_receivers[InputType.Normal] = new GameInputReceiver_Normal();
		m_receivers[InputType.Gesture] = new GameInputReceiver_Gesture();

		//m_updater.receiver = m_receivers[InputType.Normal];
		m_updater.receiver = m_receivers[InputType.Gesture];

	}
	
	void Update ()
	{
		if (null != m_updater)
		{
			m_updater.Update();
		}
	}


}
