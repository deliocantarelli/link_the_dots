using System;
using System.Collections;
using UnityEngine;

public enum GameSpeedType
{
    MAX_TIME,
    ENDLESS
}

public class SpeedObject
{
    private bool configSet = false;
    private float startSpeed;
    private float endSpeed;
    private float increaseRate;
	private GameSpeedType speedMode;

	public SpeedObject(float _startSpeed, float _increaseRate, float _endSpeed = -1)
    {
        startSpeed = _startSpeed;
        CurrentSpeed = _startSpeed;

        if (Mathf.Approximately(-1.0F, _endSpeed))
        {
			speedMode = GameSpeedType.ENDLESS;
        }
        else
        {
			speedMode = GameSpeedType.MAX_TIME;
            endSpeed = _endSpeed;
        }


        //increase rate is how much speed every second
        increaseRate = _increaseRate;
    }


    public void Update()
    {
		if (speedMode == GameSpeedType.ENDLESS)
        {
            CurrentSpeed = CurrentSpeed + increaseRate * Time.deltaTime;
        }
        else
        {
			float sign = Mathf.Sign(increaseRate);
			if(sign < 0) {
				CurrentSpeed = Mathf.Max(CurrentSpeed + increaseRate * Time.deltaTime, endSpeed);
			} else {
				CurrentSpeed = Mathf.Min(CurrentSpeed + increaseRate * Time.deltaTime, endSpeed);
            }

        }
    }

    public float CurrentSpeed { get; private set; }
}
