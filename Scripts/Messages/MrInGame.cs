using UnityEngine;
using System.Collections;

public class MrInGame : MessageReceiver
{
	public InGameManager m_owner;

	public override void OnCreate()
	{
		base.OnCreate();

		RegistMessage<PacketData.OnBattle>(OnBattle);
		RegistMessage<PacketData.OffBattle>(OffBattle);		
	}

	private void OnBattle(PacketData.OnBattle packet)
	{
		m_owner.ChangeTurn(GameData.RelationType.Friend);
	}

	private void OffBattle(PacketData.OffBattle packet)
	{
		m_owner.ChangeTurn(GameData.RelationType.None);
	}
}
