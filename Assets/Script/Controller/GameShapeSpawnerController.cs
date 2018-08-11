using UnityEngine;
using System.Collections;
using System;

public class GameShapeSpawnerController : MonoBehaviour
{
	public float startDelay;
	public float delay;
	private GameSpawner[] spawners;
	private float speed = 1;
	private Action<GameShape> onShapeCreated;
	private Action<GameSpawner[]> onSpawnersUpdated;
	private ArrayList gameShapes;
	// Use this for initialization
	void Start()
	{
		gameShapes = new ArrayList();
	}

	// Update is called once per frame
	void Update()
	{
		for (int i = gameShapes.Count-1; i >= 0; i --) {
			GameShape shape = gameShapes[i] as GameShape;
			float percent = shape.UpdatePosition(Time.deltaTime);
			if(percent >= 1) {
				gameShapes.RemoveAt(i);
			}
        }
	}

	void LoadLevelConfig() {
		
	}
    
    void SpawnShape() {
        int spawnerIndex = UnityEngine.Random.Range(0, spawners.Length);
		GameSpawner spawner = spawners[spawnerIndex];
		if (spawners[spawnerIndex].AttachedPipe != null)
        {
			GameShapeType newShapeType = spawner.GetRandomShapeType();
			Vector3 position = spawner.SpawnPosition;
			GameShape shape = new GameShape(newShapeType, position, speed, spawner.AttachedPipe);
			gameShapes.Add(shape);
			
			if(onShapeCreated != null) {
				onShapeCreated(shape);
			}
        }

    }


    public void SetSpawnersConfig()
    {
		Vector3 vector3 = new Vector3(0.0f, 3,0);
		GameShapeType[] types = { GameShapeType.CIRCLE, GameShapeType.SQUARE, GameShapeType.TRIANGLE };
		GameSpawner spawnerModel1 = new GameSpawner(types, vector3);
        vector3 = new Vector3(2f, 3, 0);
        
		GameSpawner spawnerModel2 = new GameSpawner(types, vector3);
        vector3 = new Vector3(-2f, 3, 0);
        
		GameSpawner spawnerModel3 = new GameSpawner(types, vector3);

		GameSpawner[] spawnersModel = { spawnerModel1, spawnerModel2, spawnerModel3 };
		spawners = spawnersModel;

		if(onSpawnersUpdated != null) {
			onSpawnersUpdated(spawners);
        }

		InvokeRepeating("SpawnShape", startDelay, delay);
    }
	public GameSpawner[] GetSpawners() {
		return spawners;
	}
	public void RegisterOnShapeCretedCB(Action<GameShape> action) {
		onShapeCreated += action;
	}
	public GameSpawner[] RegisterOnSpawnersUpdated(Action<GameSpawner[]> action) {
		onSpawnersUpdated += action;

        //be aware that this can return null if not initialized yet
		return spawners;
	}
	public void AttachPipeToSpawner(GamePipe pipe, GameSpawner spawner) {
		spawner.AttachPipe(pipe);
	}
}
