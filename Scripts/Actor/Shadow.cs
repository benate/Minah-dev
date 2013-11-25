using UnityEngine;
using System.Collections;

public class Shadow : BaseBehaviour
{
	private PerformActor m_actor = null;

	void Start ()
	{
		m_actor = transform.parent.GetComponent<PerformActor>();
	}
	
	void FixedUpdate ()
	{
		Vector3 pos = cachedTransform.position;
		pos.y = Game.GroundYPos;
		cachedTransform.position = pos;
	}
}
