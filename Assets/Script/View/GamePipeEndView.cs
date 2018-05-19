using UnityEngine;
using System.Collections;

public class GamePipeEndView : MonoBehaviour
{
	public GameObject pipeEndParent;

	public GameEndPipeController gameEndPipeController;
    
    public GameObject bottomCircle;
    public GameObject bottomSquare;
    public GameObject bottomTriangle;
	private GameObject[] pipeEndObjects;
    // Use this for initialization
    void Start()
    {

		GamePipeEnd[] gamePipeEnds = gameEndPipeController.RegisterOnPipeEndsUpdated(OnPipeEndUpdated);
		if (gamePipeEnds != null)
        {
			OnPipeEndUpdated(gamePipeEnds);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnPipeEndUpdated(GamePipeEnd[] pipeEnds) {
		if (pipeEndObjects != null)
        {
			foreach (GameObject pipeEnd in pipeEndObjects)
            {
				Destroy(pipeEnd);
            }
        }
		pipeEndObjects = new GameObject[pipeEnds.Length];
		for (int i = 0; i < pipeEnds.Length; i++)
        {
			GamePipeEnd pipeEnd = pipeEnds[i];
			pipeEndObjects[i] = CreatePipeEndObject(pipeEnd);
        }

	}
   
	private GameObject CreatePipeEndObject(GamePipeEnd pipeEnd) {
		GameObject pipeEndObj = GetPipeEndPrefab(pipeEnd.Type);
		pipeEndObj = Instantiate(pipeEndObj, pipeEnd.Position, Quaternion.identity);
        pipeEndObj.transform.parent = pipeEndParent.transform;
		return pipeEndObj;
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
