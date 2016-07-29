using System;
using UnityEngine;

[Serializable]
public struct Wave {
	public enum WaveType{
		Enemy01, Enemy02, Meteor
	}
	public WaveType type;

	public bool active;
	public int count;
	public Time spawnTime;

	public GameObject prefab;
}

