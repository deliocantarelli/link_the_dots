using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeginPipe : EventTrigger {
    public override void OnPointerDown(PointerEventData eventData) {
        Debug.Log("touch is working properly");
    }
}
