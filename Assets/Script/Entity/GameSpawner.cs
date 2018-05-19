using System;
using UnityEngine;

public class GameSpawner
{
    public GameShapeType[] spawnTypes;
    public Vector3 spawnPosition;

	public GameSpawner(GameShapeType[] spawnTypes, Vector3 spawnPosition)
    {
		this.spawnTypes = spawnTypes;
		this.spawnPosition = spawnPosition;
    }
}
