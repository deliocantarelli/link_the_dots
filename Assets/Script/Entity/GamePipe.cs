using System;
using UnityEngine;

public class GamePipe
{
	private Action<GamePipe> OnPipeUpdated;
	public Vector3 StartPoint { get; private set; }
	public Vector3 CurrentEnd { get; private set; }

	public GamePipe(Vector3 spawner, Vector3 startEnd) {
		StartPoint = spawner;
		CurrentEnd = startEnd;
	}

	public void UpdateGamePipeEnd(Vector3 newEnd) {
		CurrentEnd = newEnd;

		OnPipeUpdated(this);
	}

	public void RegisterOnPipeUpdated(Action<GamePipe> action) {
		OnPipeUpdated += action;
	}
    public Vector3 GetPercentualPosition(float percentual)
    {
		return Vector3.Lerp(StartPoint, CurrentEnd, percentual);
    }
}
