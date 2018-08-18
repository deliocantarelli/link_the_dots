using UnityEngine;
using System;
using System.Collections;

public class GamePlumbingController : MonoBehaviour
{
	public GamePipeEndController pipeEndController;
	public GameShapeSpawnerController spawnerController;
    private ArrayList pipes;
	private Action<GamePipe> onPipeAdded;

	private int currentTouchIndex = 0;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void StartPipes(GameSpawner[] spawners, GamePipeEnd[] ends) {
        int length = spawners.Length;
		pipes = new ArrayList(length);
		for (int x = 0; x < length; x ++) {
			GamePipe pipe = AddPipe(spawners[x], ends[x]);

			spawnerController.AttachPipeToSpawner(pipe, spawners[x]);
		}
	}

	private GamePipe AddPipe(GameSpawner spawner, GamePipeEnd end) {
		GamePipe newPipe = new GamePipe(spawner.SpawnPosition, end.Position, end.Type);
        pipes.Add(newPipe);
		if(onPipeAdded != null) {
			onPipeAdded(newPipe);
        }
		return newPipe;
	}

	public void UpdatePipeEnd(GamePipe pipe, Vector3 pipeEnd, GameShapeType newEndType) {
		pipe.UpdateGamePipeEnd(pipeEnd, newEndType);
	}

	public ArrayList RegisterOnPipesAdded(Action<GamePipe> action)
    {
		onPipeAdded += action;
		if(pipes == null) {
			return new ArrayList();
		} else {
			return pipes;
        }
    }

	public void RegisterOnPipeUpdated(Action<GamePipe> action) {
		foreach(GamePipe pipe in pipes) {
			pipe.RegisterOnPipeUpdated(action);
		}
		Action<GamePipe> onAddAction = pipe =>
		{
			pipe.RegisterOnPipeUpdated(action);
		};
		RegisterOnPipesAdded(onAddAction);
	}

	public void GetNumberOfPipes() {
		
	}

	public int GetCurrentTouchIndex() {
		return ++currentTouchIndex;
	}
}
