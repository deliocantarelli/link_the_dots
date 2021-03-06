﻿using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GamePipeView : EventTrigger
{   
	
	public GamePlumbingController gamePlumbingController;
	public GamePlumbingDragView gamePlumbingDragView;
    
    public GameObject pipePrefab;
	public GameObject pipeDragPrefab;

	public bool destroyed { get; private set; }

	//erase origin, correct updatePipe as it is just temporary
	private GamePipe updatedPipe;
	private Vector3 origin;
	private Vector3 last;

	private int lastDragId = -1;
	private bool isDragging = false;

	public int LastTouchIndex { get; private set; }

	private Action<GamePipeView> OnPipeViewUpdated;

    // Use this for initialization
    void Start()
    {
		destroyed = false;
		LastTouchIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
	public static GamePipeView CreatePipe(GameObject pipePrefab, GamePipe pipeDef, GameObject parent, GamePlumbingController plumbingController, GamePlumbingDragView plumbingDragView) {
		Vector3 position = pipeDef.StartPoint;
		GameObject newPipeObject = Instantiate(pipePrefab, position, Quaternion.identity);
		newPipeObject.transform.SetParent(parent.transform);
		GamePipeView component = newPipeObject.AddComponent<GamePipeView>();
		component.InitView(pipeDef, plumbingController, plumbingDragView);
		return component;
	}
	private void InitView(GamePipe pipe, GamePlumbingController plumbingController, GamePlumbingDragView plumbingDragView) {
        pipe.RegisterOnPipeUpdated(OnPipeUpdated);
		gamePlumbingDragView = plumbingDragView;
		gamePlumbingController = plumbingController;
        OnPipeUpdated(pipe);
	}
    public void RegisterOnPipeViewUpdated(Action<GamePipeView> action)
    {
        OnPipeViewUpdated += action;
    }
    private void OnPipeUpdated(GamePipe pipe) {
        updatedPipe = pipe;
        origin = pipe.StartPoint;
        last = pipe.CurrentEnd;
		UpdatePipe(origin, last);

		if(pipe.State == GamePipeState.FINISHED || pipe.State == GamePipeState.CORRECT) {
			if(isDragging) {
				gamePlumbingDragView.CancelPipeDragView(lastDragId);
			}
			destroyed = true;
            Destroy(gameObject);
		}

		if(OnPipeViewUpdated != null) {
			OnPipeViewUpdated(this);
		}
    }
	private void UpdatePipeEnd(GamePipeEnd pipeEnd) {
		UpdatePipe(origin, pipeEnd.Position);
		gamePlumbingController.UpdatePipeEnd(updatedPipe, pipeEnd);
    }

	private void UpdatePipe(Vector3 start, Vector3 position) {
		Vector3 endPos = new Vector3(position.x, position.y);
		Vector3 startPos = new Vector3(start.x, start.y);
        float size = Vector3.Distance(startPos, endPos);
        var angleDeg = Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg;
        RectTransform rectTrans = gameObject.GetComponent<RectTransform>();
        rectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

		rectTrans.SetPositionAndRotation(rectTrans.position, Quaternion.Euler(0,0,angleDeg + 90));
	}

	public GameShapeType GetTypeAttached () {
		return updatedPipe.CurrentEndType;
	}

	public override void OnBeginDrag(PointerEventData eventData)
	{
		base.OnBeginDrag(eventData);

		lastDragId = eventData.pointerId;
		isDragging = true;

        LastTouchIndex = gamePlumbingController.GetCurrentTouchIndex();
	}

	public override void OnDrag (PointerEventData eventData)
    {
		base.OnDrag(eventData);

		gamePlumbingDragView.UpdatePipeDragView(eventData, updatedPipe.StartPoint);
    }

    public override void OnEndDrag (PointerEventData eventData) {
		isDragging = false;
        
		GamePipeEndView endView = gamePlumbingDragView.FinishPipeDrag(eventData);
        if(endView != null && this != null) {
			UpdatePipeEnd(endView.PipeEnd);
        }
	}
	private void OnDestroy()
	{
		updatedPipe.RemoveOnPipeUpdated(OnPipeUpdated);
	}
}
