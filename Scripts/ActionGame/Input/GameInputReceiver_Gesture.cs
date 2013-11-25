using UnityEngine;
using System;
using System.Collections;

public class GameInputReceiver_Gesture : GameInputReceiver
{
	int m_touchCount = 0;
	float m_lastTouchTime = 0.0f;

	public void Clear()
	{
		m_touchCount = 0;
		m_lastTouchTime = 0.0f;
	}

	public override void TouchDown(Vector2 pos)
	{
		if (true == checkTime())
		{
			m_touchCount++;
			m_lastTouchTime = Time.time;

			if (2 == m_touchCount)
			{
			//	MessageGenerator.use.OnMessage(PacketData.AgentCommand.Create(GameEnum.UserIndex, m_touchCount, GameEnum.Direction.None));

				Clear();
			}
		}
		else
		{
			Clear();
		}
	}

	public override void TouchSlideDiff(Vector2 diff)
	{
		GameEnum.Direction dir = GameEnum.Direction.None;
		if (0.0f != diff.x)
		{
			if (0.0f < diff.x)
			{
				if (Game.SlideDistance < diff.x)
				{
					dir |= GameEnum.Direction.Left;
				}
			}

			if (0.0f > diff.x)
			{
				if (-Game.SlideDistance > diff.x)
				{
					dir |= GameEnum.Direction.Right;
				}
			}
		}

		if (0.0f != diff.y)
		{
			if (0.0f < diff.y)
			{
				if (Game.SlideDistance < diff.y)
				{
					dir |= GameEnum.Direction.Down;
				}
			}

			if (0.0f > diff.y)
			{
				if (-Game.SlideDistance > diff.y)
				{
					dir |= GameEnum.Direction.Up;
				}
			}
		}

		if (GameEnum.Direction.None != dir)
		{
			if (GameEnum.Direction.Up == dir)
			{
				World.instance.OnMsg(PacketData.OnJumpAttack.Create(GameEnum.UserIndex));
			}

			if (GameEnum.Direction.Right == dir)
			{
				World.instance.OnMsg(PacketData.OnStrongAttack.Create(GameEnum.UserIndex));
			}
			
			Clear();
		}
	}

	bool checkTime()
	{
		if (0.0f == m_lastTouchTime)
			return true;

		float timeGap = Time.time - m_lastTouchTime;
		if (timeGap <= 0.5f)
		{
			return true;
		}

		return false;
	}
}
