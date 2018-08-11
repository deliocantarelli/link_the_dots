using UnityEngine;
using System.Collections;

public class GameShapeView : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnPositionUpdated(Vector3 newPosition) {
		gameObject.transform.position = newPosition;
	}
	private void OnGameShapeFinished(GameShape shape, Vector3 newPosition) {
		gameObject.transform.position = newPosition;
		Destroy(this);
		shape.RemoveOnShapeFinished(OnGameShapeFinished);
		shape.RemoveOnPositionUpdated(OnPositionUpdated);
	}

	private void InitShapeView(GameShape gameShape) {
		gameShape.RegisterOnPositionUpdated(OnPositionUpdated);
		gameShape.RegisterOnShapeFinished(OnGameShapeFinished);
	}
    
	public static void CreateShape(GameObject shapePrefab, GameShape shape, GameObject parent) {
		GameObject shapeObj = Instantiate(shapePrefab, shape.Position, Quaternion.identity);
		shapeObj.transform.SetParent(parent.transform);
		GameShapeView shapeView = shapeObj.AddComponent<GameShapeView>();
		shapeView.InitShapeView(shape);
	}
}
