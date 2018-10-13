using UnityEngine;

[System.Serializable]
public class GameConfigSerializable
{
	public int[] test;
	public SpawnerConfigSerializable[] spawners;
	public PipeEndConfigSerializable[] pipeEnds;
	public SpeedConfigSerializable shapeSpeed;
	public SpeedConfigSerializable spawnSpeed;
	public float afterExplodeDelay = 0.5f;
	public float afterFinishDelay = 0.5f;
	public float distanceBeforeSpawningNext = 0.33f;

	public GameConfig ToGameConfig() {
		SpawnerConfig[] unserializedSpawners = new SpawnerConfig[spawners.Length];
		PipeEndConfig[] unserializedPipeEnds = new PipeEndConfig[pipeEnds.Length];

		for (int index = spawners.Length-1; index >= 0; index--)
        {
			unserializedSpawners[index] = spawners[index].Unserialize();
        }
		for (int index = pipeEnds.Length-1; index >= 0; index--)
        {
			unserializedPipeEnds[index] = pipeEnds[index].Unserialize();
        }


		return new GameConfig
		{
			spawners = unserializedSpawners,
			pipeEnds = unserializedPipeEnds,
			shapeSpeed = shapeSpeed.Unserialize(),
			spawnSpeed = spawnSpeed.Unserialize(),
			afterFinishDelay = afterFinishDelay,
			afterExplodeDelay = afterExplodeDelay,
			distanceBeforeSpawningNext = distanceBeforeSpawningNext
		};
	}
}