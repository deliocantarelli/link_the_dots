using UnityEngine;
using System.Collections;

public class GamePlumbingView : MonoBehaviour
{
    public GameObject pipeParent;

	public GamePlumbingController gamePlumbingController;
	public GamePlumbingDragView gamePlumbingDragView;

    public GameObject pipePrefab;
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
		GamePipeView.CreatePipe(pipePrefab, pipe, pipeParent, gamePlumbingController, gamePlumbingDragView);
    }
}
