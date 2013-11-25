using UnityEngine;
using System.Collections;

public class MrPerformActor : MessageReceiver
{
	public PerformActor actor;

	public override void OnCreate()
	{
		base.OnCreate();

		RegistMessage<PacketData.OnDamage>(OnDamage);
		RegistMessage<PacketData.OnPause>(OnPause);
	}

	public virtual void OnPause(PacketData.OnPause packet)
	{
		actor.pause = !actor.pause;
	}

	public virtual void OnDamage(PacketData.OnDamage packet)
	{
		actor.AddFactor(packet.DamageFactor);
	}
}
