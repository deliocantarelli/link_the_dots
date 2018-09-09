using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameShapeSpawnerView : EventTrigger
{
	public GameSpawner Spawner { get; private set; }
	public GamePlumbingDragView gamePlumbingDragView;
	public GamePlumbingController gamePlumbingController;

	public static GameShapeSpawnerView CreateSpawnerView(GameObject spawnerPrefab, GameSpawner spawner, GameObject parent, GamePlumbingDragView gamePlumbingDragView, GamePlumbingController gamePlumbingController)
	{
        GameObject spawnerObj = Instantiate(spawnerPrefab, spawner.SpawnPosition, Quaternion.identity);
		spawnerObj.transform.parent = parent.transform;
		GameShapeSpawnerView component = spawnerObj.AddComponent<GameShapeSpawnerView>();
		component.InitSpawner(gamePlumbingDragView, gamePlumbingController, spawner);
		return component;
    }
	private void InitSpawner(GamePlumbingDragView plumbingDragView, GamePlumbingController plumbingController, GameSpawner spawner) {
		gamePlumbingDragView = plumbingDragView;
		gamePlumbingController = plumbingController;
		Spawner = spawner;
	}

    //public override void OnBeginDrag(PointerEventData eventData)
    //{
    //    base.OnBeginDrag(eventData);

    //    LastTouchIndex = gamePlumbingController.GetCurrentTouchIndex();
    //}

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
		gamePlumbingDragView.UpdatePipeDragView(eventData, Spawner.SpawnPosition);
    }
    
    public override void OnEndDrag(PointerEventData eventData)
    {
        GamePipeEndView endView = gamePlumbingDragView.FinishPipeDrag(eventData);
		if(endView != null) {
			gamePlumbingController.SetPipeFromSpawner(Spawner, endView.PipeEnd);
        }

    }
}
