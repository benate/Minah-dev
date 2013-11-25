using UnityEngine;
using System.Collections;

public class BaseBehaviour : MonoBehaviour
{
	private Transform m_transform = null;
	public Transform cachedTransform 
	{ get { if (null == m_transform) m_transform = transform; return m_transform; } }
}
