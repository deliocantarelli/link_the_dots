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
		GameSpawner[] gameShapeSpawners = spawnerController.RegisterOnSpawnersUpdated(OnSpawnersUpdated);
		if(gameShapeSpawners != null) {
			OnSpawnersUpdated(gameShapeSpawners);
		}
    }

    // Update is called once per frame
    void Update()
    {

    }

	void OnSpawnersUpdated(GameSpawner[] spawners) {
		if(spawnerObjects != null) {
			foreach (GameObject spawner in spawnerObjects) {
				Destroy(spawner);
			}
        }
		spawnerObjects = new GameObject[spawners.Length];
		for (int i = 0; i < spawners.Length; i ++) {
			GameSpawner spawn = spawners[i];
			spawnerObjects[i] = CreateSpawnerObject(spawn);
		}
	}

	GameObject CreateSpawnerObject(GameSpawner spawner) {
		GameObject spawnerObj = Instantiate(spawnerPrefab, spawner.SpawnPosition, Quaternion.identity);
		spawnerObj.transform.parent = spawnersParent.transform;
		return spawnerObj;
	}
}
