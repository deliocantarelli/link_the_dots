using UnityEngine;
using System.Collections;

public class GameShapeGroupView : MonoBehaviour
{
	public GameObject shapeParent;

    public GameObject squarePrefab;
	public GameObject circlePrefab;
    public GameObject trianglePrefab;

	public GameShapeSpawnerController shapeSpawnerController;
    // Use this for initialization
    void Start()
    {
		shapeSpawnerController.RegisterOnShapeCretedCB(OnShapeCreated);
    }

	void OnShapeCreated(GameShape shape) {
		GameObject shapeObj = GetShapePrefab(shape.Type);
		shapeObj = Instantiate(shapeObj, shape.Position, Quaternion.identity);
		shapeObj.transform.SetParent(shapeParent.transform);
	}

    private GameObject GetShapePrefab(GameShapeType type)
    {
        switch (type)
        {
			case GameShapeType.SQUARE:
				return squarePrefab;
			case GameShapeType.CIRCLE:
				return circlePrefab;
			case GameShapeType.TRIANGLE:
				return trianglePrefab;
            default:
                Debug.Log("invalid shape type");
                return null;
        }
    }
}
