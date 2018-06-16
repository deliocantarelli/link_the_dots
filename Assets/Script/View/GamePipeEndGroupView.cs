using UnityEngine;
using System.Collections;

public class GamePipeEndGroupView : MonoBehaviour
{
	public GameObject pipeEndParent;

	public GamePipeEndController gameEndPipeController;
    
    public GameObject bottomCircle;
    public GameObject bottomSquare;
    public GameObject bottomTriangle;
    // Use this for initialization
    void Start()
    {
        
		gameEndPipeController.RegisterOnPipeEndAdded(OnPipeEndAdded);
		GamePipeEnd[] gamePipeEnds = gameEndPipeController.GetPipeEnds();
		if (gamePipeEnds != null)
        {
			foreach(GamePipeEnd gamePipeEnd in gamePipeEnds) {
				OnPipeEndAdded(gamePipeEnd);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
	private void OnPipeEndAdded(GamePipeEnd pipeEnd) {
		GameObject prefab = GetPipeEndPrefab(pipeEnd.Type);
		GamePipeEndView.CreatePipeEnd(prefab, pipeEndParent, pipeEnd);
	}

    private GameObject GetPipeEndPrefab(GameShapeType type)
    {
        switch (type)
        {
			case GameShapeType.CIRCLE:
				return bottomCircle;
			case GameShapeType.SQUARE:
				return bottomSquare;
			case GameShapeType.TRIANGLE:
				return bottomTriangle;
            default:
                Debug.Log("invalid shape type");
                return null;
        }
    }
}
