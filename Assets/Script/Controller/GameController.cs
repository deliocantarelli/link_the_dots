using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
	public string levelFileName = "level_test";

	public GamePipeEndController endPipeController;
	public GameShapeSpawnerController spawnerController;
	public GamePlumbingController plumbingController;
	public GameShapeController shapeController;

	private void Start()
	{
		GameConfig gameConfig = LevelLoader.LoadLevel(levelFileName);

		spawnerController.SetSpawnersConfig(gameConfig);
		endPipeController.SetEndPipeConfig(gameConfig);
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
