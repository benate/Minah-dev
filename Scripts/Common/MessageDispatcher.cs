using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageDispatcher
{
	private Dictionary<long, MessageReceiver> m_receivers = new Dictionary<long, MessageReceiver>();
	private List<PacketData.Message> m_messagesSingle = new List<PacketData.Message>();
	private List<PacketData.Message> m_messagesBroadcast = new List<PacketData.Message>();

	public void UpdateMessages()
	{
		foreach (PacketData.Message msg in m_messagesSingle)
		{
			MessageSingle(m_receivers, msg);
		}
		m_messagesSingle.Clear();

		foreach (PacketData.Message msg in m_messagesBroadcast)
		{
			MessageBroadCast(m_receivers, msg);
		}
		m_messagesBroadcast.Clear();
	}

	public void AddMessageReceiver(long index, MessageReceiver receiver)
	{
		m_receivers[index] = receiver;
		receiver.OnCreate();
	}

	public void RemoveMessageReceiver(long index)
	{
		MessageReceiver receiver = null;
		if (m_receivers.TryGetValue(index, out receiver))
		{
			receiver.OnRelease();
			m_receivers.Remove(index);
		}
	}

	public void OnMessage(PacketData.Message msg)
	{
		m_messagesSingle.Add(msg);
	}

	public void OnMessageBroadCast(PacketData.Message msg)
	{
		m_messagesBroadcast.Add(msg);
	}

	protected void MessageSingle(Dictionary<long, MessageReceiver> receivers, PacketData.Message msg)
	{
		MessageReceiver receiver = null;
		if (receivers.TryGetValue(msg.GetReceiverID(), out receiver))
		{
			receiver.OnMessage(msg);
		}
	}

	protected void MessageBroadCast(Dictionary<long, MessageReceiver> receivers, PacketData.Message msg)
	{
		foreach (KeyValuePair<long, MessageReceiver> pair in receivers)
		{
			pair.Value.OnMessage(msg);
		}
	}
}
