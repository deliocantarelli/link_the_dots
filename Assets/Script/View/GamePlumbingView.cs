using UnityEngine;
using System.Collections;
using System;

public class GamePlumbingView : MonoBehaviour
{
    public GameObject pipeParent;

	public GamePlumbingController gamePlumbingController;
	public GamePlumbingDragView gamePlumbingDragView;

    public GameObject pipePrefab;
	private ArrayList pipeViewList = new ArrayList();
	private Action<GamePipeView> onPipeViewAdded;

    // Use this for initialization
    void Start()
    {
		ArrayList pipes = gamePlumbingController.RegisterOnPipesAdded(AddPipe);
		if(pipes.Count > 0) {
			for (int x = 0; x < pipes.Count; x ++) {
				AddPipe(pipes[x] as GamePipe);
			}
		}
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddPipe(GamePipe pipe)
    {
		GamePipeView pipeView = GamePipeView.CreatePipe(pipePrefab, pipe, pipeParent, gamePlumbingController, gamePlumbingDragView);
		pipeViewList.Add(pipeView);

		if(onPipeViewAdded != null) {
			onPipeViewAdded(pipeView);
		}
    }

    public void RegisterOnPipeViewAdded(Action<GamePipeView> action)
    {
        onPipeViewAdded += action;
    }

    public void RegisterOnPipeViewUpdated(Action<GamePipeView> action)
    {
		foreach (GamePipeView pipe in pipeViewList)
        {
            pipe.RegisterOnPipeViewUpdated(action);
		}
        Action<GamePipeView> onAddAction = pipe =>
        {
			pipe.RegisterOnPipeViewUpdated(action);
        };
        RegisterOnPipeViewAdded(onAddAction);
    }

	public GamePipeView GetLastTouchedWithType(GameShapeType type) {
		GamePipeView lastPipeView = null;
		int highestIndex = -1;
		foreach (GamePipeView pipeView in pipeViewList) {
			if(pipeView.GetTypeAttached() == type && pipeView.LastTouchIndex > highestIndex) {
				lastPipeView = pipeView;
				highestIndex = pipeView.LastTouchIndex;
			}
		}
		return lastPipeView;
	}
}
