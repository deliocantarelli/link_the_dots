using UnityEngine;

public class SpawnerConfig {
    public Vector3 position;
	public GameShapeType[] shapeTypes;
}
public class PipeEndConfig {
	public Vector3 position;
	public GameShapeType shapeType;
}
public class SpeedConfig {
    public float startSpeed;
    public float endSpeed;
    public float rate;
}

public class GameConfig
{
    public SpawnerConfig[] spawners;
    public PipeEndConfig[] pipeEnds;
    public SpeedConfig shapeSpeed;
    public SpeedConfig spawnSpeed;
    public float afterExplodeDelay = 0.5f;
    public float afterFinishDelay = 0.5f;
    public float distanceBeforeSpawningNext = 0.33f;
}
