using System;
using System.Collections;
using UnityEngine;

public class GamePipe
{
	private Action<GamePipe> OnPipeUpdated;
	public GameSpawner spawner;
	public GamePipeEnd pipeEnd;
	public ArrayList shapeList = new ArrayList();
	public Vector3 StartPoint { get { return spawner.SpawnPosition; } }
	public Vector3 CurrentEnd { get { return pipeEnd.Position; } }
	public GameShapeType CurrentEndType { get { return pipeEnd.Type; } }

	public GamePipe(GameSpawner spawner, GamePipeEnd startEnd) {
		this.spawner = spawner;
		pipeEnd = startEnd;
	}

	public void UpdateGamePipeEnd(GamePipeEnd newEnd, GameShapeType newEndType) {
		pipeEnd = newEnd;
		Debug.Log("updating");
		OnPipeUpdated(this);
	}

	public void RegisterOnPipeUpdated(Action<GamePipe> action) {
		OnPipeUpdated += action;
	}
    public void RemoveOnPipeUpdated(Action<GamePipe> action)
    {
        OnPipeUpdated -= action;
    }
    public Vector3 GetPercentualPosition(float percentual)
    {
		return Vector3.Lerp(StartPoint, CurrentEnd, percentual);
    }
	public void AttachShape(GameShape shape) {
		shapeList.Add(shape);
	}
	public void RemoveShape(GameShape shape) {
		shapeList.Remove(shape);
	}
}
