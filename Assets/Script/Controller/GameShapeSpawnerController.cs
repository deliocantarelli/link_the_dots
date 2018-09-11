using UnityEngine;
using System.Collections;
using System;

public class GameShapeSpawnerController : MonoBehaviour
{
	public GameShapeController shapeController;
	public float startDelay;
	public bool spawnAfterFinish = true;
	private float delay = 6f;
	private GameSpawner[] spawners;
	private float speed = 0.6f;
	private float spawnSpeed = 0.5f;
	private float afterExplodeDelay = 0.5f;
	private float afterFinishDelay = 0.5f;
	private Action<GameShape> onShapeCreated;
	private Action<GameSpawner[]> onSpawnersUpdated;

	private String SpawnShapeFunction = "SpawnShape";

	// Update is called once per frame
	void Update()
	{
		
	}

	void LoadLevelConfig() {
		
	}
    
    void SpawnShape() {
        int spawnerIndex = UnityEngine.Random.Range(0, spawners.Length);
		GameSpawner spawner = spawners[spawnerIndex];

		GameShapeType newShapeType = spawner.GetRandomShapeType();

		GameShape shape = shapeController.CreateShape(newShapeType, spawner, speed, spawnSpeed);

		shape.RegisterOnStateChanged(OnShapeStateChanged);
		
		if(onShapeCreated != null) {
			onShapeCreated(shape);
		}
    }

	private void OnShapeStateChanged(GameShape shape) {
		if(spawnAfterFinish){
			if (shape.State == GameShapeState.EXPLODING)
            {
				Invoke(SpawnShapeFunction, afterExplodeDelay);
			} else if(shape.State == GameShapeState.FINISHED) {
				Invoke(SpawnShapeFunction, afterFinishDelay);
			}
		}else if (shape.State == GameShapeState.EXPLODING)
        {
			CancelInvoke(SpawnShapeFunction);

            StartShapeSpawn(afterExplodeDelay);
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

		if(spawnAfterFinish) {
			Invoke(SpawnShapeFunction, startDelay);
		} else {
			StartShapeSpawn(startDelay);
        }
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

	public void StartShapeSpawn(float toStartDelay) {
		InvokeRepeating(SpawnShapeFunction, toStartDelay, delay);
	}
}
