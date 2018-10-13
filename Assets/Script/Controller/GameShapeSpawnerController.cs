using UnityEngine;
using System.Collections;
using System;

public enum SpawnType {
	TIME = 0,
	FINISH,
	TIME_NO_OVERLAP
}

public class GameShapeSpawnerController : MonoBehaviour
{
	public GameShapeController shapeController;
	public float startDelay;
	public SpawnType spawnType;
	public SpeedConfigController speedConfigController;
	protected float Delay { get { return speedConfigController.SpawnDelay; }}
	protected GameSpawner[] spawners;
	protected float afterExplodeDelay = 0.5f;
	protected float afterFinishDelay = 0.5f;
	protected Action<GameShape> onShapeCreated;
	protected Action<GameSpawner[]> onSpawnersUpdated;

	protected bool repeatSpawn = false;
	protected String SpawnShapeFunction = "SpawnShape";

	protected delegate void OnShapeStateChanged(GameShape shape);
	protected OnShapeStateChanged onShapeStateChanged;

	private void Start()
	{
		if(spawnType == SpawnType.TIME_NO_OVERLAP) {
			Debug.Log("this class is not supposed to use this type, please, change this component to TimeOrFinishSpawnController!");
		}
	}
	// Update is called once per frame
	void Update()
	{
		
	}
    
	protected GameSpawner GetRandomEmptySpawner() {
		ArrayList empties = new ArrayList();
		foreach(GameSpawner spawner in spawners) {
			if(spawner.CurrentShapes == 0) {
				empties.Add(spawner);
			}
		}
		if(empties.Count > 0) {
			int spawnerIndex = UnityEngine.Random.Range(0, empties.Count);
			return empties[spawnerIndex] as GameSpawner;
		}
		return null;
	}

	protected void SpawnShape(GameSpawner spawner = null) {
		if(spawner == null) {
			int spawnerIndex = UnityEngine.Random.Range(0, spawners.Length);
			spawner = spawners[spawnerIndex];
        }

		GameShapeType newShapeType = spawner.GetRandomShapeType();

		float speed = speedConfigController.ShapeSpeed;
		float spawnCurrentSpeed = speedConfigController.SpawnSpeed;

		GameShape shape = shapeController.CreateShape(newShapeType, spawner, speed, spawnCurrentSpeed);

		spawner.CurrentShapes++;

		shape.RegisterOnStateChanged(CallShapeStateChanged);
		
		if(onShapeCreated != null) {
			onShapeCreated(shape);
		}

		if(repeatSpawn) {
			Invoke(SpawnShapeFunction, Delay);
		}
    }
    
	protected void CallShapeStateChanged(GameShape shape) {
		if(shape.HasFinished) {
			shape.Spawner.CurrentShapes--;
		}
		onShapeStateChanged(shape);
	}
    protected virtual void SetShapeStateChanged()
    {
		if (spawnType == SpawnType.FINISH)
        {
			onShapeStateChanged = FinishShapeStateChanged;
        }
		else if (spawnType == SpawnType.TIME)
        {
			onShapeStateChanged = TimeShapeStateChanged;
        }

    }

	public void SetSpawnersConfig(GameConfig gameConfig)
    {
		SetSpeedConfig(gameConfig);

		afterExplodeDelay = gameConfig.afterExplodeDelay;
		afterFinishDelay = gameConfig.afterFinishDelay;

		SpawnerConfig[] spawnersConfig = gameConfig.spawners;
		spawners = new GameSpawner[spawnersConfig.Length];

		for (int index = spawnersConfig.Length-1; index >= 0; index --) {
			SpawnerConfig config = spawnersConfig[index];
			spawners[index] = new GameSpawner(config.shapeTypes, config.position);
		}

		if(onSpawnersUpdated != null) {
			onSpawnersUpdated(spawners);
        }

		StartSpawners();
    }
	protected virtual void StartSpawners() {
        if (spawnType == SpawnType.FINISH)
        {
            Invoke(SpawnShapeFunction, startDelay);
        }
		else if(spawnType == SpawnType.TIME) {
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
		repeatSpawn = true;
		Invoke(SpawnShapeFunction, toStartDelay);
	}



    //strategy shape state changed, by spawn type
    protected void TimeShapeStateChanged(GameShape shape)
	{
		if (shape.State == GameShapeState.EXPLODING)
        {
            CancelInvoke(SpawnShapeFunction);

            StartShapeSpawn(afterExplodeDelay);
        }

	}
	protected void FinishShapeStateChanged(GameShape shape) {
        if (shape.State == GameShapeState.EXPLODING)
        {
            Invoke(SpawnShapeFunction, afterExplodeDelay);
        }
        else if (shape.State == GameShapeState.FINISHED)
        {
            Invoke(SpawnShapeFunction, afterFinishDelay);
        }
	}
	protected void SetSpeedConfig(GameConfig config) {
		SpeedObject shapeObjSpeed = new SpeedObject(config.shapeSpeed.startSpeed, config.shapeSpeed.rate, config.shapeSpeed.endSpeed);
		SpeedObject spawnObjSpeed = new SpeedObject(config.spawnSpeed.startSpeed, config.spawnSpeed.rate, config.spawnSpeed.endSpeed);
		float spawnDistanceThreshold = config.distanceBeforeSpawningNext;

		speedConfigController.SetConfig(shapeObjSpeed, spawnObjSpeed, spawnDistanceThreshold);
	}
}
