using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class MessageDelegate
{
	public virtual void Execute(PacketData.Message msg) { }
}

public class MessageDelegateImpl<MessageT> : MessageDelegate
	where MessageT : PacketData.Message
{
	public delegate void OnMessageDelegate(MessageT msg);

	public OnMessageDelegate func = null;

	public override void Execute(PacketData.Message msg)
	{
		if (func != null)
			func(msg as MessageT);
	}
}

public class MessageReceiver : BaseBehaviour
{
	public delegate void OnMessageFunc(PacketData.Message msg);

	protected Dictionary<int, MessageDelegate> m_messages = new Dictionary<int, MessageDelegate>();

	public virtual void OnCreate() { }
	public virtual void OnRelease() { }

	protected void RegistMessage<MessageT>(MessageDelegateImpl<MessageT>.OnMessageDelegate func)
		where MessageT : PacketData.Message
	{
		System.Type msgType = typeof(MessageT);
		FieldInfo msgField = msgType.GetField("ID");
		int index = (int)msgField.GetValue(msgType);

		MessageDelegateImpl<MessageT> msg = new MessageDelegateImpl<MessageT>();
		msg.func = func;
		m_messages[index] = msg;
	}

	public void OnMessage(PacketData.Message msg)
	{
		if (m_messages.ContainsKey(msg.GetID()))
		{
			MessageDelegate msgDelegate = m_messages[msg.GetID()];
			if (msgDelegate != null)
			{
				msgDelegate.Execute(msg);
			}
		}
	}
}
