using UnityEngine;
using System.Collections;

public class GameShapeView : MonoBehaviour
{
	public GameObject shapeParent;

    public GameObject starPrefab;
    public GameObject hexagonPrefab;

	public GameShapeSpawnerController shapeSpawnerController;
    // Use this for initialization
    void Start()
    {
		shapeSpawnerController.RegisterOnShapeCretedCB(OnShapeCreated);
    }

	void OnShapeCreated(GameShape shape) {
		GameObject shapeObj = GetShapePrefab(shape.Type);
		shapeObj = Instantiate(shapeObj, shape.Position, Quaternion.identity);
		shapeObj.transform.parent = shapeParent.transform;
	}

    private GameObject GetShapePrefab(GameShapeType type)
    {
        switch (type)
        {
            case GameShapeType.HEXAGON:
                return hexagonPrefab;
            case GameShapeType.STAR:
                return starPrefab;
            default:
                Debug.Log("invalid shape type");
                return null;
        }
    }
}
