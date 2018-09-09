using UnityEngine;
using System;
using System.Collections;

public class GamePlumbingController : MonoBehaviour
{
	public GamePipeEndController pipeEndController;
	public GameShapeSpawnerController spawnerController;
	public GameShapeController shapeController;
	private ArrayList pipes = new ArrayList();
	private Action<GamePipe> onPipeAdded;

	private int currentTouchIndex = 0;
    // Use this for initialization
    void Start()
    {
		spawnerController.RegisterOnShapeCretedCB(OnShapeCreated);
    }

    // Update is called once per frame
    void Update()
    {

    }
	private void OnShapeCreated(GameShape shape) {
		shape.RegisterOnStateChanged(OnShapeStateChanged);
		if(shape.Spawner.AttachedPipe != null) {
			shape.Spawner.AttachedPipe.SetState(GamePipeState.WRONG);
		}
    }
    private void OnShapeStateChanged(GameShape shape) {
		if(shape.State == GameShapeState.CORRECT_MOVING) {
			if (shapeController.GetNumberOfValidShapesOnPipe(shape) == 0) {
				shapeController.GetPipeOfShape(shape).SetState(GamePipeState.CORRECT);
			}
		} else if(shape.State == GameShapeState.FINISHED || shape.State == GameShapeState.TO_DESTROY) {
			if (shapeController.GetNumberOfShapesOnPipe(shape) == 1)
			{
				shape.Spawner.RemovePipe();
			}
		}
	}

	private GamePipe AddPipe(GameSpawner spawner, GamePipeEnd end) {
		GamePipe newPipe = new GamePipe(spawner, end);
        pipes.Add(newPipe);
		spawner.AttachPipe(newPipe);
		if(onPipeAdded != null) {
			onPipeAdded(newPipe);
        }
		return newPipe;
	}

	public void UpdatePipeEnd(GamePipe pipe, GamePipeEnd pipeEnd) {
		pipe.UpdateGamePipeEnd(pipeEnd, pipeEnd.Type);
		shapeController.OnPipeUpdated(pipe);
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
	public void SetPipeFromSpawner(GameSpawner spawner, GamePipeEnd pipeEnd) {
		if(spawner.AttachedPipe == null) {
			AddPipe(spawner, pipeEnd);
		} else {
			UpdatePipeEnd(spawner.AttachedPipe, pipeEnd);
		}
	}
}
