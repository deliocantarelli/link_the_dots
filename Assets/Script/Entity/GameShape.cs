using UnityEngine;
using System;

public enum GameShapeType{
    TRIANGLE = 0,
    SQUARE = 1,
    CIRCLE = 2
}

public class GameShape
{
	public float Speed { get; private set; }
	public float PercentualTraveled { get; private set; }
	public GameShapeType Type { get; private set; }
	public Vector3 Position { get; private set; }
	public GamePipe Pipe { get; private set; }

	private Action<Vector3> onGameShapePositionUpdated;
	private Action<GameShape, Vector3> onGameShapeFinished;

	public GameShape(GameShapeType type, Vector3 initialPosition, float speed, GamePipe attachedPipe) {
		this.Type = type;
		this.Speed = speed;
		this.Position = initialPosition;
		Pipe = attachedPipe;
	}

	public float UpdatePosition(float dt) {
		float distanceTraveled = dt * Speed;
		PercentualTraveled += distanceTraveled;
		Vector3 newPosition = Pipe.GetPercentualPosition(PercentualTraveled);
		if(PercentualTraveled >= 1) {
			if(onGameShapeFinished != null) {
				onGameShapeFinished(this, newPosition);
            }
		}
		else if(onGameShapePositionUpdated != null) {
			onGameShapePositionUpdated(newPosition);
		}
		return PercentualTraveled;
	}
	public void RegisterOnPositionUpdated(Action<Vector3> action) {
		onGameShapePositionUpdated += action;
	}
	public void RegisterOnShapeFinished(Action<GameShape, Vector3> action) {
		onGameShapeFinished += action;
	}
	public void RemoveOnShapeFinished(Action<GameShape, Vector3> action) {
		onGameShapeFinished -= action;
	}
	public void RemoveOnPositionUpdated(Action<Vector3> action) {
		onGameShapePositionUpdated -= action;
	}
}
