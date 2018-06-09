using System;

public class GamePipe
{
	private Action<GamePipe> OnPipeUpdated;
	public GameSpawner StartPoint { get; private set; }
	public GamePipeEnd CurrentEnd { get; private set; }

	public GamePipe(GameSpawner spawner, GamePipeEnd startEnd) {
		StartPoint = spawner;
		CurrentEnd = startEnd;
	}

	public void UpdateGamePipeEnd(GamePipeEnd newEnd) {
		CurrentEnd = newEnd;

		OnPipeUpdated(this);
	}

	public void RegisterOnPipeUpdated(Action<GamePipe> action) {
		OnPipeUpdated += action;
	}
}
