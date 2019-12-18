using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boost
{
	public BoostType        type		= BoostType.FastResearch;
	public float            value		= 0f;
	public float            time		= 0f;
	public int              chance		= 1;
	public Sprite           sprite		= null;
	public string           description	= string.Empty;
}

[SerializeField]
public enum BoostType
{
	FastResearch	= 0,
	CreditMultipler	= 1,
	SpawnHigher		= 2,
	MergeHigher		= 3,
	AutoMerge		= 4,
};
