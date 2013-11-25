using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fx : Actor
{
	protected override void OnUpdate()
	{
		if (false == anim.IsPlaying(anim.GetCurrentAnimationName()))
		{
			world.RemoveFx(this);
		}
	}
}
