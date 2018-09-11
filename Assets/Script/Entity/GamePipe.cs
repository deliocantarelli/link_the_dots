using System;
using System.Collections;
using UnityEngine;

public enum GamePipeState {
	WRONG = 0,
    CORRECT = 1,
    FINISHED = 2
}

public class GamePipe
{
	private Action<GamePipe> OnPipeUpdated;
	private Action<GamePipe> OnPipeRemoved;
	public GameSpawner spawner;
	public GamePipeEnd pipeEnd;
	public ArrayList shapeList = new ArrayList();
	public Vector3 StartPoint { get { return spawner.SpawnPosition; } }
	public Vector3 CurrentEnd { get { return pipeEnd.Position; } }
	public GameShapeType CurrentEndType { get { return pipeEnd.Type; } }
	public GamePipeState State { get; private set; }
	private float pipeSize = 0;

	public GamePipe(GameSpawner spawner, GamePipeEnd startEnd) {
		this.spawner = spawner;
		pipeEnd = startEnd;
		State = GamePipeState.WRONG;
	}

	public void UpdateGamePipeEnd(GamePipeEnd newEnd, GameShapeType newEndType) {
		pipeEnd = newEnd;
		pipeSize = Vector3.Distance(StartPoint, CurrentEnd);
		OnPipeUpdated(this);
	}

	public void RegisterOnPipeUpdated(Action<GamePipe> action) {
		OnPipeUpdated += action;
	}
    public void RemoveOnPipeUpdated(Action<GamePipe> action)
    {
        OnPipeUpdated -= action;
    }
	public void RegisterOnPipeRemoved(Action<GamePipe> action) {
		OnPipeRemoved += action;
	}
	public float GetPercentualPosition(Vector3 position) {
		float posSize = Vector3.Distance(StartPoint, position);
		return posSize / pipeSize;
	}
    public Vector3 GetPositionFromPercentual(float percentual)
    {
		return Vector3.Lerp(StartPoint, CurrentEnd, percentual);
    }
	public void AttachShape(GameShape shape) {
		shapeList.Add(shape);
	}
	public void RemoveShape(GameShape shape) {
		shapeList.Remove(shape);
		if(shapeList.Count == 0 && OnPipeRemoved != null) {
			OnPipeRemoved(this);
		}
	}
	public void SetState(GamePipeState state) {
		State = state;
		OnPipeUpdated(this);
	}
}
