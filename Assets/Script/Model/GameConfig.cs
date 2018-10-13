[System.Serializable]
public class GameConfig
{
	public readonly SpawnerConfig[] spawners;
	public readonly PipeEndConfig[] pipeEnds;
	public readonly SpeedConfig shapeSpeed;
	public readonly SpeedConfig spawnSpeed;
	public readonly float afterExplodeDelay = 0.5f;
	public readonly float afterFinishDelay = 0.5f;
	public readonly float distanceBeforeSpawningNext = 0.33f;
}