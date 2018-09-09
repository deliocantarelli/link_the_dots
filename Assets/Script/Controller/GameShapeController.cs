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
                    Pairs.RemoveAt(i);
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

	private ShapePipePair GetShapePipePair(GameShape shape) {
		foreach (ShapePipePair pair in Pairs) {
			if(pair.Shape == shape) {
				return pair;
			}
		}
		Debug.Log("shape not found!");
		return null;
	}

    public void OnShapeFinishedSpawning(GameShape shape)
    {
		UpdateState(shape, GameShapeState.MOVING);
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

}
