﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GamePipeEndView : EventTrigger
{
	GamePipeView pipeView = null;
	public GameShapeType EndType { get; private set; }

	private GamePlumbingView gamePlumbingView;
	private bool dragEnabled = false;
	private bool attachedPipeIsUpdated = false;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InitPipeEnd() {
	}
	public static void CreatePipeEnd(GameObject pipeEndPrefab, GameObject parent, GamePipeEnd pipeDef, GamePlumbingView gamePlumbingView)
    {
        GameObject pipeEndObj = Instantiate(pipeEndPrefab, pipeDef.Position, Quaternion.identity);
        pipeEndObj.transform.SetParent(parent.transform);
        GamePipeEndView component = pipeEndObj.AddComponent<GamePipeEndView>();
		component.gamePlumbingView = gamePlumbingView;
		component.EndType = pipeDef.Type;
		component.InitPipeEnd();
    }
	public override void OnBeginDrag(PointerEventData eventData)
	{
		base.OnBeginDrag(eventData);

		SetCurrentPipeView();

		if (pipeView != null && pipeView.GetTypeAttached() == EndType)
        {
			dragEnabled = true;
			pipeView.OnBeginDrag(eventData);
        }
	}
	public override void OnDrag(PointerEventData eventData)
	{
		base.OnDrag(eventData);

		if(dragEnabled) {
			pipeView.OnDrag(eventData);
        }
	}
	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);

		if (dragEnabled)
        {
			pipeView.OnEndDrag(eventData);
        }
		dragEnabled = false;
	}
	private void SetCurrentPipeView() {
		pipeView = gamePlumbingView.GetLastTouchedWithType(EndType);
	}
}
