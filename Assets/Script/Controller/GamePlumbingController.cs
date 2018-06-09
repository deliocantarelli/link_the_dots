using UnityEngine;
using System;
using System.Collections;

public class GamePlumbingController : MonoBehaviour
{
	private ArrayList pipes;
	private Action<GamePipe> onPipeAdded;
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
			AddPipe(spawners[x], ends[x]);
		}
	}

	private GamePipe AddPipe(GameSpawner spawner, GamePipeEnd end) {
		GamePipe newPipe = new GamePipe(spawner, end);
        pipes.Add(newPipe);
        onPipeAdded(newPipe);
		return newPipe;
	}

	private void UpdatePipeEnd(GamePipe pipe, GamePipeEnd pipeEnd) {
		pipe.UpdateGamePipeEnd(pipeEnd);
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
}
