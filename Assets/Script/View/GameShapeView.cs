using UnityEngine;
using System.Collections;


public class ShapeAnimationName
{
	private ShapeAnimationName(string value) { Value = value; }

    public string Value { get; set; }

	public static ShapeAnimationName SPAWINING_ANIMATION { get { return new ShapeAnimationName("SpawnAnimation"); } }
	public static ShapeAnimationName SPAWN_DELAY { get { return new ShapeAnimationName("SpawnDelay"); } }
}

public class GameShapeView : MonoBehaviour
{
	GameShape shape;
	GameShapeController shapeController;

	Animator animator;
    // Use this for initialization
    void Start()
	{

    }

    // Update is called once per frame
    void Update()
    {
        if(shape.State == GameShapeState.SPAWNING) {
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName(ShapeAnimationName.SPAWINING_ANIMATION.Value) && !animator.GetCurrentAnimatorStateInfo(0).IsName(ShapeAnimationName.SPAWN_DELAY.Value)) {
				shapeController.OnShapeFinishedSpawning(shape);
			}
		}
    }
	private void OnStateChanged(GameShape gameShape) {
		if(gameShape.State == GameShapeState.EXPLODING) {
			Debug.Log("exploded...");
            Invoke("BeforeDestruction", 0);
		} else if (gameShape.State == GameShapeState.FINISHED)
        {
			gameObject.transform.position = gameShape.Position;
			Invoke("BeforeDestruction", 0);
        }
	}
	private void BeforeDestruction() {
		shapeController.CanRemoveShape(shape);
		DestroySelf();
	}
	private void OnPositionUpdated(Vector3 newPosition) {
		gameObject.transform.position = newPosition;
	}
	private void DestroySelf() {

		shape.RemoveOnStateChanged(OnStateChanged);
		shape.RemoveOnPositionUpdated(OnPositionUpdated);
        Destroy(gameObject);
	}

	private void InitShapeView(GameShape gameShape, GameShapeController gameShapeController)
	{
		shapeController = gameShapeController;
		shape = gameShape;
		animator = gameObject.GetComponent<Animator>();
		gameShape.RegisterOnPositionUpdated(OnPositionUpdated);
		gameShape.RegisterOnStateChanged(OnStateChanged);
	}
	public static void CreateShape(GameObject shapePrefab, GameShape shape, GameObject parent, GameShapeController shapeController) {
		GameObject shapeObj = Instantiate(shapePrefab, shape.Position, Quaternion.identity);
		shapeObj.transform.SetParent(parent.transform);
		GameShapeView shapeView = shapeObj.AddComponent<GameShapeView>();
		shapeView.InitShapeView(shape, shapeController);
	}
}
