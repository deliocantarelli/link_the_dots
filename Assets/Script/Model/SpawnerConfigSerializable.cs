using UnityEngine;

[System.Serializable]
public class SpawnerConfigSerializable
{
	public float[] position;
	public string[] shapeTypes;

	public SpawnerConfig Unserialize()
    {
		GameShapeType[] types = new GameShapeType[shapeTypes.Length];

		for (int index = shapeTypes.Length-1; index >= 0; index --) {
			types[index] = shapeTypes[index].ToGameShapeType();
		}

		return new SpawnerConfig
        {
			shapeTypes = types,
            position = new Vector3(position[0], position[1], position[2])
        };
    }
}