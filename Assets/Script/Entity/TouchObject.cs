using System;
using UnityEngine;
using System.Collections;

public class TouchObject
{
	public Vector3 TouchStart { get; private set; }
	public Vector3 CurrentTouch { get; private set; }

	TouchObject(Vector3 touchStart, Vector3 currentTouch) {
		TouchStart = touchStart;
		CurrentTouch = currentTouch;
	}
}