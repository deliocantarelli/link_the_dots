using UnityEngine;
using System.Collections;

public class GamePipeEndView : MonoBehaviour
{
    public GameObject bottomHexagonSpawner;
    public GameObject bottomStarSpawner;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject GetSpawnerPrefab(GameShapeType type)
    {
        switch (type)
        {
            case GameShapeType.HEXAGON:
                return bottomHexagonSpawner;
            case GameShapeType.STAR:
                return bottomStarSpawner;
            default:
                Debug.Log("invalid shape type");
                return null;
        }
    }
}
