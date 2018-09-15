using UnityEngine;
using System;

public enum GameShapeType{
    TRIANGLE = 0,
    SQUARE = 1,
    CIRCLE = 2
}

public enum GameShapeState {
	SPAWNING = 0,
    MOVING = 1,
    CORRECT = 2,
    WRONG = 3,
    EXPLODING = 4,
    CORRECT_MOVING = 5,
    FINISHED = 6,
    TO_DESTROY = 7
}

public class GameShape
{
	public GameShapeState State { get; private set; }
	public float Speed { get; private set; }
	public float SpawnSpeed { get; private set; }
	public float PercentualTraveled { get; private set; }
	public GameShapeType Type { get; private set; }
	public GameShapeType CurrentEndType { get; private set; }
	public Vector3 Position { get; private set; }
	public GameSpawner Spawner { get; private set; }
	public bool CanMove { get { return State == GameShapeState.MOVING || State == GameShapeState.CORRECT_MOVING; }}
	public bool HasFinished { get { return State == GameShapeState.TO_DESTROY; }}

	private Action<Vector3> onGameShapePositionUpdated;
	private Action<GameShape> onStateChanged;

	public GameShape(GameShapeType type, GameSpawner spawner, float speed, float spawnSpeed) {
		PercentualTraveled = 0;
		Spawner = spawner;
		this.Type = type;
		this.Speed = speed;
		SpawnSpeed = spawnSpeed;
		this.Position = spawner.SpawnPosition;
		State = GameShapeState.SPAWNING;
	}

	public void UpdatePosition(float newPercentual, Vector3 newPosition) {
		PercentualTraveled = newPercentual;
		Position = newPosition;

		if(onGameShapePositionUpdated != null) {
			onGameShapePositionUpdated(newPosition);
		}
	}   
    public void RegisterOnStateChanged(Action<GameShape> action)
    {
		onStateChanged += action;
    }
	public void RemoveOnStateChanged(Action<GameShape> action)
    {
		onStateChanged -= action;
    }
    public void RegisterOnPositionUpdated(Action<Vector3> action)
    {
        onGameShapePositionUpdated += action;
    }
	public void RemoveOnPositionUpdated(Action<Vector3> action) {
		onGameShapePositionUpdated -= action;
	}
   
	public void SetState(GameShapeState newState) {
		if(newState != State) {
			State = newState;
			if(onStateChanged != null) {
				onStateChanged(this);
			}
		}
	}

    //this function indicates what it will do when an instance of Shape is finished, in case of other types of shapes
    //but the controller which has to call it
	public void OnShapeFinished(bool correct) {
		if(correct) {
			LifeController.Instance.Award();
		} else {
			LifeController.Instance.Penality();
		}
	}
}
