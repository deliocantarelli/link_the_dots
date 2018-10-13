using UnityEngine;

[System.Serializable]
public class PipeEndConfigSerializable
{
	public string shapeType;
	public float[] position;

	public PipeEndConfig Unserialize() {
		return new PipeEndConfig {
			shapeType = shapeType.ToGameShapeType(),
			position = new Vector3(position[0], position[1], position[2])
		};
	}
}
