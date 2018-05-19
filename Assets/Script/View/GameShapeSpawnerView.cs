using UnityEngine;
using System.Collections;

public class GameShapeSpawnerView : MonoBehaviour
{
	private GameObject[] spawnerObjects;

	public GameShapeSpawnerController spawnerController;

	public GameObject spawnersParent;
    public GameObject spawnerPrefab;
    // Use this for initialization
    void Start()
    {
		GameShapeSpawnerModel[] gameShapeSpawners = spawnerController.RegisterOnSpawnersUpdated(OnSpawnersUpdated);
		if(gameShapeSpawners != null) {
			OnSpawnersUpdated(gameShapeSpawners);
		}
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnSpawnersUpdated(GameShapeSpawnerModel[] spawners) {
		if(spawnerObjects != null) {
			foreach (GameObject spawner in spawnerObjects) {
				Destroy(spawner);
			}
        }
		spawnerObjects = new GameObject[spawners.Length];
		for (int i = 0; i < spawners.Length; i ++) {
			GameShapeSpawnerModel spawn = spawners[i];
			spawnerObjects[i] = CreateSpawnerObject(spawn);
		}
	}

	GameObject CreateSpawnerObject(GameShapeSpawnerModel spawner) {
		GameObject spawnerObj = Instantiate(spawnerPrefab, spawner.GetSpawnStartPosition(), Quaternion.identity);
		spawnerObj.transform.parent = spawnersParent.transform;
		return spawnerObj;
	}
}
