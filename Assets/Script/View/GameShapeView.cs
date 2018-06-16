using UnityEngine;
using System.Collections;

public class GameShapeView : MonoBehaviour
{
	GamePipeView pipeView;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnPositionUpdated(float newPosition) {
		Debug.Log(newPosition);
		gameObject.transform.position = pipeView.GetPercentualPosition(newPosition);
	}

	private void InitShapeView(GameShape gameShape, GamePipeView pipeAttached) {
		gameShape.RegisterOnPositionUpdated(OnPositionUpdated);
		pipeView = pipeAttached;
	}
    
	public static void CreateShape(GameObject shapePrefab, GameShape shape, GameObject parent, GamePipeView pipeAttached) {
		GameObject shapeObj = Instantiate(shapePrefab, shape.Position, Quaternion.identity);
		shapeObj.transform.SetParent(parent.transform);
		GameShapeView shapeView = shapeObj.AddComponent<GameShapeView>();
		shapeView.InitShapeView(shape, pipeAttached);
	}
}
