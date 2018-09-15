using System;
using UnityEngine;

public class GameSpawner
{
	public GameShapeType[] SpawnTypes { get; private set; }
	public Vector3 SpawnPosition { get; private set; }
	public GamePipe AttachedPipe { get; private set; }
	public int CurrentShapes = 0;

	public GameSpawner(GameShapeType[] spawnTypes, Vector3 spawnPosition)
    {
		this.SpawnTypes = spawnTypes;
		this.SpawnPosition = spawnPosition;
	}
    public GameShapeType GetRandomShapeType()
    {
		int index = UnityEngine.Random.Range(0, SpawnTypes.Length);
		return SpawnTypes[index];
    }
	public void AttachPipe(GamePipe pipe) {
		AttachedPipe = pipe;
	}
	public void RemovePipe() {
		if(AttachedPipe != null) {
			AttachedPipe.SetState(GamePipeState.FINISHED);
			AttachedPipe = null;
        }
	}
}
