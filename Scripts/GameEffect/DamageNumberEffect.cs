using UnityEngine;
using System.Collections;

public class DamageNumberEffect : BaseEffect
{
	private SpriteEffect m_spriteEffect;
	private UILabel m_label;
	private bool m_isPlay = false;

	void Awake()
	{
		m_spriteEffect = gameObject.AddComponent<SpriteEffect>();
		m_label = gameObject.GetComponentInChildren<UILabel>();

		m_spriteEffect.applyWidget = m_label;
	}

	void FixedUpdate()
	{
		if (m_isPlay && !m_spriteEffect.isPlaying)
		{
			Destroy(gameObject);
		}
	}

	public void Play(Transform trans, int damage)
	{
		if (m_isPlay)
			return;

		m_label.text = "" + damage;
		Camera gameCamera = NGUITools.FindCameraForLayer(trans.gameObject.layer);
		Camera uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);

		Vector3 pos = gameCamera.WorldToViewportPoint(trans.position);
		cachedTransform.position = uiCamera.ViewportToWorldPoint(pos);
		pos = cachedTransform.localPosition;
		pos.x = Mathf.RoundToInt(pos.x);
		pos.y = Mathf.RoundToInt(pos.y);
		pos.z = 0f;
		cachedTransform.localPosition = pos;

		SpriteEffect.EffectData effectData = new SpriteEffect.EffectData();
		effectData.playTime = 0.5f;
		effectData.isColor = true;
		effectData.startColor = new Color(1.0f, 1.0f, 0.0f, 1.0f);
		effectData.endColor = new Color(1.0f, 1.0f, 0.0f, 0.0f);
		effectData.isPosition = true;
		effectData.startPosition = pos;
		effectData.endPosition = new Vector3(pos.x, pos.y + 100.0f, pos.z);

		m_spriteEffect.effectDatas.Add(effectData);

		m_spriteEffect.Play();
		m_isPlay = true;
	}
}
