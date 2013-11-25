using UnityEngine;
using System.Collections;

public class ActionGUIListener : MonoBehaviour
{
	public UIImageButton defenceButton;

	void OnPressedBackDash()
	{
		World.instance.OnMsg(PacketData.OnBackDash.Create(GameEnum.UserIndex, true));
	}

	void OnReleaseBackDash()
	{
		World.instance.OnMsg(PacketData.OnBackDash.Create(GameEnum.UserIndex, false));
	}
}
