using UnityEngine;
using System.Collections;

public class GamePlumbingShadowView : MonoBehaviour
{
	public GameObject pipeShadowPrefab;
	public GameObject pipeShadowParent;
	public GamePlumbingController gamePlumbingController;
	public GameShapeSpawnerController gameShapeSpawnerController;
    // Use this for initialization
    void Start()
    {
		gamePlumbingController.RegisterOnPipeUpdated(OnPipeUpdated);
    }

    // Update is called once per frame
    void Update()
    {

    }
	void OnPipeUpdated(GamePipe pipe) {
		if(pipe.State == GamePipeState.CORRECT) {
			CreatePipeShadow(pipe);
		}
	}

	void AddPipeShadow() {
		//GamePipeView pipeView = GamePipeView.CreatePipe(pipePrefab, pipe, pipeParent, gamePlumbingController, gamePlumbingDragView);
        //pipeViewList.Add(pipeView);

        //if (onPipeViewAdded != null)
        //{
        //    onPipeViewAdded(pipeView);
        //}
	}
    
	void CreatePipeShadow(GamePipe pipe) {
		GamePipeShadowView.CreateShadow(pipeShadowPrefab, pipe, pipeShadowParent);
        
	}
}
