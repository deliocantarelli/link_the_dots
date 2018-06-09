using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
	public GameEndPipeController endPipeController;
	public GameShapeSpawnerController spawnerController;
	public GamePlumbingController plumbingController;

	private void Start()
	{
		spawnerController.SetSpawnersConfig();
		endPipeController.SetEndPipeConfig();
		plumbingController.StartPipes(spawnerController.GetSpawners(), endPipeController.GetPipeEnds());
	}

	private void OnShapeCreated(GameShape shape) {
		Debug.Log("Shape created successfully");
	}


}
