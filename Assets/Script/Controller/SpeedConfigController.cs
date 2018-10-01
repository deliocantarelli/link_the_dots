using UnityEngine;
using System.Collections;

public class SpeedConfigController : MonoBehaviour
{
	private bool configSet = false;

	private SpeedObject _shapeSpeed;
	private SpeedObject _spawnSpeed;

	public float ShapeSpeed { get { return _shapeSpeed.CurrentSpeed; }}
	public float SpawnSpeed { get { return _spawnSpeed.CurrentSpeed; }}

	public float CurrentSpeed { get; private set; }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if(configSet) {
			_spawnSpeed.Update();
			_shapeSpeed.Update();
        }
    }

	public void SetConfig(SpeedObject shapeSpeed, SpeedObject spawnSpeed) {
		_shapeSpeed = shapeSpeed;
		_spawnSpeed = spawnSpeed;

		configSet = true;
	}
}
