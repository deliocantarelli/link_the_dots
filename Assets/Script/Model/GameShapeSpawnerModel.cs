using System.Collections;
using UnityEngine;

public class GameShapeSpawnerModel : MonoBehaviour
{
	private readonly GameSpawner gameSpawner;

	public GameShapeSpawnerModel (GameShapeType[] types, Vector3 position) {
		this.gameSpawner = new GameSpawner(types, position);
    }

    public GameShapeType GetRandomShapeType()
    {
		int index = Random.Range(0, this.gameSpawner.spawnTypes.Length);
		return this.gameSpawner.spawnTypes[index];
    }
	public Vector3 GetSpawnStartPosition() {
		return this.gameSpawner.spawnPosition;
	}
}
