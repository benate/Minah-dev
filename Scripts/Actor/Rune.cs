using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rune : MonoBehaviour
{
	public int index { get; set; }

	[HideInInspector][SerializeField]public UIAtlas atlas;
	[HideInInspector][SerializeField]public string spriteName;

	public List<int> factorIndices;
}