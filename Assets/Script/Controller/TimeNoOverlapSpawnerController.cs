using UnityEngine;
using System.Collections;

public class TimeNoOverlapSpawnerController : GameShapeSpawnerController
{
	private new string CheckSpawnShapeFunction = "CheckCanSpawn";
	private float currentDelay = 0;

	private void Update()
	{
		currentDelay += Time.deltaTime;
	}

	private void Start()
	{
		SetShapeStateChanged();
        if (spawnType != SpawnType.TIME_NO_OVERLAP)
        {
            Debug.Log("this class is not supposed to use this type, please, change this component to GameShapeSpawnerController or some other!");
        }
	}
	override
	protected void SetShapeStateChanged()
    {
		onShapeStateChanged = TimeOrFinishShapeStateChanged;
    }
    private void TimeOrFinishShapeStateChanged(GameShape shape)
    {
        if (shape.State == GameShapeState.EXPLODING)
        {
			Invoke(CheckSpawnShapeFunction, afterExplodeDelay);
        }
        else if (shape.State == GameShapeState.FINISHED)
        {
			Invoke(CheckSpawnShapeFunction, afterFinishDelay);
        }
    }
	private void CheckCanSpawn() {
		if(currentDelay >= delay) {
			GameSpawner spawner = GetRandomEmptySpawner();
			if(spawner != null) {
				NoOverlapSpawn(spawner);
            }
		} else {
			foreach(GameSpawner spawner in spawners) {
				if(spawner.CurrentShapes > 0) {
					return;
				}
			}
			NoOverlapSpawn(null);
		}
	}
	protected override void StartSpawners()
    {
		Invoke(CheckSpawnShapeFunction, startDelay);
    }
	private void NoOverlapSpawn(GameSpawner spawner) {
        SpawnShape(spawner);
        currentDelay = 0;
        Invoke(CheckSpawnShapeFunction, delay);
	}
}
