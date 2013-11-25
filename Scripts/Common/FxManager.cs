using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FxManager
{
	protected World m_world = null;
	public World world { set { m_world = value; } }

	List<Fx> m_fxs = new List<Fx>();

	public Fx CreateFx(GameObject origin, Actor trigger)
	{
		GameObject go = GameObject.Instantiate(origin, trigger.pos, Quaternion.identity) as GameObject;
		go.transform.rotation = trigger.cachedTransform.rotation;
		go.transform.position = trigger.cachedTransform.position;

		if (m_world.fxParentNode != null)
			go.transform.parent = m_world.fxParentNode;

		Fx fx = go.GetComponent<Fx>();
		fx.OnCreateData(m_world, GameEnum.NewIndex);
		fx.TurnDir(trigger.dir);

		m_fxs.Add(fx);

		return fx;
	}

	public void RemoveFx(Fx fx)
	{		
		m_fxs.Remove(fx);

		GameObject.Destroy(fx.gameObject);
	}

}
