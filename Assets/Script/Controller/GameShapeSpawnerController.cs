using UnityEngine;
using System.Collections;
using System;

public class GameShapeSpawnerController : MonoBehaviour
{
	public float startDelay;
	public float delay;
    private GameShapeSpawnerModel[] spawners;
	private float speed = 5;
	private Action<GameShape> onShapeCreated;
	private Action<GameShapeSpawnerModel[]> onSpawnersUpdated;
	// Use this for initialization
	void Start()
	{
		SetSpawnersConfig();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void LoadLevelConfig() {
		
	}
    
    void SpawnShape() {
        int spawnerIndex = UnityEngine.Random.Range(0, spawners.Length);
        GameShapeSpawnerModel spawner = spawners[spawnerIndex];
		GameShapeType newShapeType = spawner.GetRandomShapeType();
		Vector3 position = spawner.GetSpawnStartPosition();
		GameShape shape = new GameShape(newShapeType, position, speed);

		onShapeCreated(shape);
    }


    private void SetSpawnersConfig()
    {
        Vector3 vector3 = Vector3.zero;
		GameShapeType[] types = { GameShapeType.HEXAGON, GameShapeType.STAR };
        GameShapeSpawnerModel spawnerModel = new GameShapeSpawnerModel(types, vector3);

		GameShapeSpawnerModel[] spawnersModel = { spawnerModel };
		spawners = spawnersModel;
		onSpawnersUpdated(spawners);

		InvokeRepeating("SpawnShape", startDelay, delay);
    }
	public void RegisterOnShapeCretedCB(Action<GameShape> action) {
		onShapeCreated += action;
	}
	public GameShapeSpawnerModel[] RegisterOnSpawnersUpdated(Action<GameShapeSpawnerModel[]> action) {
		onSpawnersUpdated += action;

        //be aware that this can return null if not initialized yet
		return spawners;
	}
}
