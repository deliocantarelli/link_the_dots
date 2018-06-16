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

	private Action<float> onGameShapePositionUpdated;

	public GameShape(GameShapeType type, Vector3 initialPosition, float speed) {
		this.Type = type;
		this.Speed = speed;
		this.Position = initialPosition;
	}

	public void UpdatePosition(float dt) {
		float distanceTraveled = dt * Speed;
		PercentualTraveled += distanceTraveled;
		if(onGameShapePositionUpdated != null) {
			onGameShapePositionUpdated(PercentualTraveled);
		}
	}
	public void RegisterOnPositionUpdated(Action<float> action) {
		onGameShapePositionUpdated += action;
	}
}
