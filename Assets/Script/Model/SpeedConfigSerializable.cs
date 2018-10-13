[System.Serializable]
public class SpeedConfigSerializable
{
	public float startSpeed;
	public float endSpeed = float.PositiveInfinity;
	public float rate;

	public SpeedConfig Unserialize() {
		return new SpeedConfig
		{
			startSpeed = startSpeed,
			endSpeed = endSpeed,
			rate = rate
		};
	}
}
