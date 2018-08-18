using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
	public GamePipeEndController endPipeController;
	public GameShapeSpawnerController spawnerController;
	public GamePlumbingController plumbingController;

	private void Start()
	{
		spawnerController.SetSpawnersConfig();
		endPipeController.SetEndPipeConfig();
		plumbingController.StartPipes(spawnerController.GetSpawners(), endPipeController.GetPipeEnds());
		spawnerController.RegisterOnShapeCretedCB(OnGameShapeCreated);
	}

	private void OnShapeCreated(GameShape shape) {
		Debug.Log("Shape created successfully");
	}

	public void OnGameShapeCreated(GameShape shape) {
		
	}

	private void Update()
	{
		ArrayList gameShapes = spawnerController.GetAllShapes();
        for (int i = gameShapes.Count - 1; i >= 0; i--)
        {
            GameShape shape = gameShapes[i] as GameShape;

			if(shape.State == GameShapeState.MOVING) {
				float percent = shape.UpdatePosition(Time.deltaTime);
				if (percent >= 1)
				{
					gameShapes.RemoveAt(i);
				}
            }
        }
	}
}
