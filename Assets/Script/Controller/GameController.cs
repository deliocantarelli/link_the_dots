using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
	public GamePipeEndController endPipeController;
	public GameShapeSpawnerController spawnerController;
	public GamePlumbingController plumbingController;
	public GameShapeController shapeController;

	private void Start()
	{
		spawnerController.SetSpawnersConfig();
		endPipeController.SetEndPipeConfig();
		//plumbingController.StartPipes(spawnerController.GetSpawners(), endPipeController.GetPipeEnds());
		spawnerController.RegisterOnShapeCretedCB(OnGameShapeCreated);
	}

	private void OnShapeCreated(GameShape shape) {
		Debug.Log("Shape created successfully");
	}

	public void OnGameShapeCreated(GameShape shape) {
		
	}

	private void Update()
	{
		shapeController.MoveAllShapes(Time.deltaTime);
	}
}
