using UnityEngine;
public class GamePipeEnd
{
	public GameShapeType Type { get; private set; }
	public Vector3 Position { get; private set; }

	public GamePipeEnd(GameShapeType type, Vector3 position) {
		this.Type = type;
		this.Position = position;
	}
}
