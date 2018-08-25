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
    EXPLODING = 4
}

public class GameShape
{
	public GameShapeState State { get; private set; }
	public float Speed { get; private set; }
	public float PercentualTraveled { get; private set; }
	public GameShapeType Type { get; private set; }
	public Vector3 Position { get; private set; }
	public GamePipe Pipe { get; private set; }

	private Action<Vector3> onGameShapePositionUpdated;
	private Action<GameShape, Vector3, bool> onGameShapeFinished;
	private Action<GameShape> onStateChanged;

	public GameShape(GameShapeType type, Vector3 initialPosition, float speed) {
		this.Type = type;
		this.Speed = speed;
		this.Position = initialPosition;
		State = GameShapeState.SPAWNING;
	}

	public void UpdateAttachedPipe(GamePipe pipe) {
		if(Pipe != null) {
			Pipe.RemoveShape(this);
        }
		Pipe = pipe;
		Pipe.AttachShape(this);
	}

	public float UpdatePosition(float dt) {
		float distanceTraveled = dt * Speed;
		PercentualTraveled += distanceTraveled;
		Vector3 newPosition = Pipe.GetPercentualPosition(PercentualTraveled);
		if(PercentualTraveled >= 1) {
			OnShapeFinished(newPosition);
		}
		else if(onGameShapePositionUpdated != null) {
			onGameShapePositionUpdated(newPosition);
		}
		return PercentualTraveled;
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
    public void RegisterOnShapeFinished(Action<GameShape, Vector3, bool> action)
    {
        onGameShapeFinished += action;
    }
	public void RemoveOnShapeFinished(Action<GameShape, Vector3, bool> action) {
		onGameShapeFinished -= action;
	}
	public void RemoveOnPositionUpdated(Action<Vector3> action) {
		onGameShapePositionUpdated -= action;
	}
	private void OnShapeFinished(Vector3 newPosition) {
        if (onGameShapeFinished != null)
        {
            onGameShapeFinished(this, newPosition, IsCorrect());
		}
        Pipe.RemoveShape(this);
		LifeController.Instance.OnShapeFinished(this, IsCorrect());
	}
	private bool IsCorrect() {
		return Pipe.CurrentEndType == Type;
	}


	public void UpdateState(GameShapeState shapeState) {
		switch (State) {
			case GameShapeState.SPAWNING:
				FromSpawning(shapeState);
				break;
			default:

				break;
		}
	}

	private void FromSpawning(GameShapeState shapeState) {
		switch (shapeState) {
			case GameShapeState.MOVING:
				if(this.Pipe == null) {
					SetState(GameShapeState.EXPLODING);
				} else {
					SetState(GameShapeState.MOVING);
                }
				break;
			default:
				break;
		}
	}
	private void SetState(GameShapeState newState) {
		if(newState != State) {
			State = newState;
			if(onStateChanged != null) {
				onStateChanged(this);
			}
		}
	}
}
