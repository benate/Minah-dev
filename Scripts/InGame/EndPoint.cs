using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndPoint : MonoBehaviour
{
	void FixedUpdate ()
	{
		int deadCount = 0;
		foreach (PerformActor actor in World.instance.enemies)
		{
			if (Game.FsmType.Death == actor.fsm.curFsmType)
				++deadCount;
		}

		if (deadCount == World.instance.enemies.Count)
		{
			Directing.instance.StartEndDirecting(this);
		}
	}

	public void Excute()
	{
		List<long> removeIndex = new List<long>();
		foreach (PerformActor actor in World.instance.friends)
		{
			if (Game.FsmType.Death == actor.fsm.curFsmType)
				removeIndex.Add(actor.index);
		}

		foreach (PerformActor actor in World.instance.enemies)
		{
			removeIndex.Add(actor.index);
		}

		foreach (long index in removeIndex)
		{
			World.instance.RemoveActor(index);
		}
	}
}
