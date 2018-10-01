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
	protected float delay = 1f;
	protected GameSpawner[] spawners;
	protected float afterExplodeDelay = 0.5f;
	protected float afterFinishDelay = 0.5f;
	protected Action<GameShape> onShapeCreated;
	protected Action<GameSpawner[]> onSpawnersUpdated;

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

	void LoadLevelConfig() {
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

    public void SetSpawnersConfig()
    {
		SetSpeedConfig();

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
		InvokeRepeating(SpawnShapeFunction, toStartDelay, delay);
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
	protected void SetSpeedConfig() {
        SpeedObject shapeObjSpeed = new SpeedObject(0.6f, 0.02f);
        SpeedObject spawnObjSpeed = new SpeedObject(0.5f, 0.02f);

		speedConfigController.SetConfig(shapeObjSpeed, spawnObjSpeed);
	}
}
