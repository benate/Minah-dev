using UnityEngine;
using System.Collections;

public class ActionMrUser : MrPerformActor
{
	private ActionUserActor m_user;
	public override void OnCreate()
	{
		base.OnCreate();

		m_user = actor as ActionUserActor;

		RegistMessage<PacketData.OnTargeting>(OnTargeting);
		RegistMessage<PacketData.OnBackDash>(OnBackDash);
		RegistMessage<PacketData.OnJumpAttack>(OnJumpAttack);
		RegistMessage<PacketData.OnStrongAttack>(OnStrongAttack);
	}

	private void OnTargeting(PacketData.OnTargeting packet)
	{
		m_user.OnTargeting(packet.Target);
	}

	private void OnBackDash(PacketData.OnBackDash packet)
	{
		m_user.OnBackDash(packet.IsPressed);
	}

	private void OnJumpAttack(PacketData.OnJumpAttack packet)
	{
		m_user.OnJumpAttack();
	}

	private void OnStrongAttack(PacketData.OnStrongAttack packet)
	{
		m_user.OnStrongAttack();
	}
}
