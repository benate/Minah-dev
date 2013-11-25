using UnityEngine;
using System.Collections;

public class ActorCreator
{
	protected World m_world = null;
	public World world { set { m_world = value; } }

	public PerformActor CreateActor(long actorIndex, int tableIndex, Vector3 pos)
	{
		GameData.PerformActor data = GameData.PerformActor.GetData(tableIndex);
		if (data == null)
			return null;

		GameObject go = GameObject.Instantiate(Resources.Load(data.ResourcePath), pos, Quaternion.identity) as GameObject;
		PerformActor actor = go.GetComponent<PerformActor>();
		if (m_world.projectileParentNode != null)
			actor.cachedTransform.parent = m_world.characterParentNode;

		actor.OnCreateData(m_world, actorIndex);

		return actor;
	}
}
