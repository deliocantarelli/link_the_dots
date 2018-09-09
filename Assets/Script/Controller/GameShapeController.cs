using UnityEngine;
using System.Collections;



public class GameShapeController : MonoBehaviour
{
	private ArrayList Pairs = new ArrayList();

	private class ShapePipePair
	{
		public GameShape Shape { get; set; }
		public GamePipe AttachedPipe { get; set; }
	}

	public void MoveAllShapes(float dt) {

		for (int i = Pairs.Count - 1; i >= 0; i--)
        {
			ShapePipePair pair = Pairs[i] as ShapePipePair;

			GameShape shape = pair.Shape;

			if (shape.CanMove)
            {
				float percentualTravelled = MoveShape(pair, dt);
                if (percentualTravelled >= 1)
                {
                    UpdateState(shape, GameShapeState.FINISHED);
                }
            }
        }
	}
	private float MoveShape(ShapePipePair pair, float dt) {
        GameShape shape = pair.Shape;
        GamePipe pipe = pair.AttachedPipe;

		float distanceTravelled = dt * shape.Speed;
		float percentualTravelled = shape.PercentualTraveled + distanceTravelled;
		Vector3 newPosition = pipe.GetPercentualPosition(percentualTravelled);

		shape.UpdatePosition(percentualTravelled, newPosition);

		return percentualTravelled;

	}

	public GameShape CreateShape(GameShapeType newShapeType, GameSpawner spawner, float speed)
	{
		GameShape shape = new GameShape(newShapeType, spawner, speed);

		ShapePipePair newPair = new ShapePipePair
		{
			Shape = shape
		};
		Pairs.Add(newPair);

		return shape;
	}

	public void AttachPipe(GameShape shape, GamePipe pipe)
	{
		ShapePipePair pair = GetShapePipePair(shape);
		pair.AttachedPipe = pipe;
	}
	public bool IsCorrect(GameShape shape) {
		ShapePipePair pair = GetShapePipePair(shape);
		return pair.Shape.Type == pair.AttachedPipe.CurrentEndType;
	}
	public int GetNumberOfValidShapesOnPipe(GamePipe pipe) {
        if (pipe == null)
        {
            return 0;
        }
		int valids = 0;

        foreach (ShapePipePair pair in Pairs)
        {
			if(pair.AttachedPipe == pipe) {
				if(pair.Shape.State == GameShapeState.SPAWNING || pair.Shape.State == GameShapeState.MOVING) {
					valids++;
				}
            }
        }
		return valids;
	}
	public int GetNumberOfValidShapesOnPipe(GameShape shape)
    {
		GamePipe pipe = GetShapePipePair(shape).AttachedPipe;
		return GetNumberOfValidShapesOnPipe(pipe);
    }
	public GamePipe GetPipeOfShape(GameShape shape) {
		return GetShapePipePair(shape).AttachedPipe;
	}

    public int GetNumberOfShapesOnPipe(GamePipe pipe)
    {
		if(pipe == null) {
			return 0;
		}
		int number = 0;

        foreach (ShapePipePair pair in Pairs)
        {
            if (pair.AttachedPipe == pipe)
            {
				if (pair.Shape.State != GameShapeState.FINISHED ||pair.Shape.State != GameShapeState.TO_DESTROY)
                {
                    number++;
                }
            }
        }
        return number;
	}
	public int GetNumberOfShapesOnPipe(GameShape shape)
	{
		GamePipe pipe = GetPipeOfShape(shape);
		return GetNumberOfShapesOnPipe(pipe);
	}
	private ShapePipePair GetShapePipePair(GameShape shape) {
		foreach (ShapePipePair pair in Pairs) {
			if(pair.Shape == shape) {
				return pair;
			}
		}
		Debug.Log("shape not found!");
		return null;
	}
	private ArrayList GetShapePipePair(GamePipe pipe)
    {
		ArrayList pairs = new ArrayList();
        foreach (ShapePipePair pair in Pairs)
        {
			if (pair.AttachedPipe == pipe)
            {
				pairs.Add(pair);
            }
        }
        return pairs;
    }
	private int GetPairIndex(GameShape shape) {
		for (int index = 0; index < Pairs.Count; index ++) {
			ShapePipePair pair = Pairs[index] as ShapePipePair;

			if (pair.Shape == shape) return index;
		}
		return -1;
	}

    public void OnShapeFinishedSpawning(GameShape shape)
    {
		GamePipe pipe = shape.Spawner.AttachedPipe;
		if(pipe != null) {
			AttachPipe(shape, pipe);
		}
		UpdateState(shape, GameShapeState.MOVING);
		if(pipe != null) {
			OnPipeUpdated(pipe);
		}
    }
	public void OnPipeUpdated(GamePipe pipe) {
		ArrayList pairs = GetShapePipePair(pipe);
		if(pairs.Count > 0) {
			foreach (ShapePipePair pair in pairs) {
				GameShapeType pipeEnd = pair.AttachedPipe.CurrentEndType;
				GameShapeType shapeType = pair.Shape.Type;
				if(pipeEnd == shapeType) {
					UpdateState(pair.Shape, GameShapeState.CORRECT_MOVING);
				}
            }
		}
	}
	public void CanRemoveShape(GameShape shape) {
		UpdateState(shape, GameShapeState.TO_DESTROY);
	} 





	private void UpdateState(GameShape shape, GameShapeState shapeState)
    {
		switch (shape.State)
        {
            case GameShapeState.SPAWNING:
				FromSpawning(shape, shapeState);
                break;
            case GameShapeState.MOVING:
				FromMoving(shape, shapeState);
                break;
			case GameShapeState.CORRECT_MOVING:
				FromCorrectMoving(shape, shapeState);
				break;
			case GameShapeState.EXPLODING:
				FromExploding(shape, shapeState);
				break;
			case GameShapeState.FINISHED:
				FromFinished(shape, shapeState);
				break;
            default:

                break;
        }
    }

	private void FromSpawning(GameShape shape, GameShapeState shapeState)
    {
        switch (shapeState)
        {
            case GameShapeState.MOVING:
				GamePipe pipe = GetShapePipePair(shape).AttachedPipe;

				if (pipe == null)
                {
					shape.SetState(GameShapeState.EXPLODING);
                    shape.OnShapeFinished(false);
                }
                else
                {
					shape.SetState(GameShapeState.MOVING);
                }
                break;
            default:
                break;
        }
    }
	private void FromMoving(GameShape shape, GameShapeState shapeState)
    {
		GamePipe pipe = GetShapePipePair(shape).AttachedPipe;
        switch (shapeState)
        {
            case GameShapeState.CORRECT_MOVING:
				if (pipe != null)
                {
					pipe.RemoveShape(shape);
                }
				shape.SetState(GameShapeState.CORRECT_MOVING);
                break;
			case GameShapeState.FINISHED:
                if (pipe != null)
                {
                    pipe.RemoveShape(shape);
                }
				shape.SetState(GameShapeState.FINISHED);
				shape.OnShapeFinished(false);
				break;
            default:
                break;
        }
	}
    private void FromCorrectMoving(GameShape shape, GameShapeState shapeState)
    {
        switch (shapeState)
        {
            case GameShapeState.FINISHED:
                shape.SetState(GameShapeState.FINISHED);
				shape.OnShapeFinished(true);
                break;
            default:
                break;
        }
    }
	private void FromExploding(GameShape shape, GameShapeState shapeState) {
		switch (shapeState)
		{
			case GameShapeState.TO_DESTROY:
				int shapeIndex = GetPairIndex(shape);
				shape.SetState(GameShapeState.TO_DESTROY);
                Pairs.RemoveAt(shapeIndex);
				break;
			default:
				break;
		}
	}
	private void FromFinished(GameShape shape, GameShapeState shapeState) {
		switch(shapeState) {
			case GameShapeState.TO_DESTROY:
				int shapeIndex = GetPairIndex(shape);
                shape.SetState(GameShapeState.TO_DESTROY);
				Pairs.RemoveAt(shapeIndex);
				break;
			default:
				break;
		}
	}

}
