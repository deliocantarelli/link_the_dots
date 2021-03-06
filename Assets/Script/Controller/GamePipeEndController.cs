﻿using UnityEngine;
using System.Collections;
using System;

public class GamePipeEndController : MonoBehaviour
{
	private GamePipeEnd[] pipeEnds;
	private Action<GamePipeEnd> onPipeEndAdded;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void SetEndPipeConfig() {
		GameShapeType type = GameShapeType.CIRCLE;
        Vector3 vector3 = new Vector3(0f, -3f, 0);
		GamePipeEnd pipeEnd1 = new GamePipeEnd(type, vector3);
		vector3 = new Vector3(2f, -3f, 0);

        type = GameShapeType.SQUARE;
        GamePipeEnd pipeEnd2 = new GamePipeEnd(type, vector3);
		vector3 = new Vector3(-2f, -3f, 0);

		type = GameShapeType.TRIANGLE;
		GamePipeEnd pipeEnd3 = new GamePipeEnd(type, vector3);

		GamePipeEnd[] pipeEndArray = { pipeEnd1, pipeEnd2, pipeEnd3 };
		pipeEnds = pipeEndArray;

		if(onPipeEndAdded != null) {
			foreach(GamePipeEnd pipeEnd in pipeEndArray) {
				onPipeEndAdded(pipeEnd);
            }
        }
	}
	public GamePipeEnd[] GetPipeEnds() {
		return pipeEnds;
	}
	public void RegisterOnPipeEndAdded(Action<GamePipeEnd> action) {
		onPipeEndAdded += action;
	}
}
