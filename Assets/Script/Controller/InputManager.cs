using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
#if !UNITY_STANDALONE
	const int maxTouches = 5;
#endif

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool condition = false;
#if UNITY_STANDALONE
        condition = Input.GetMouseButtonDown(0);
        Vector3[] positions = {Input.mousePosition};
#else
        //it could be optimized to run only one loop, but this one bellow will run at most 10 times(touches)
        //so I don't think that's a problem as the first is a small code block and the heavy code will run at most 5
        //times either way
		int touchCount = Input.touchCount;
		int arraySize = Mathf.Max(touchCount, maxTouches);
        condition = touchCount > 0;
        Vector3[] positions = new Vector3[arraySize];
        for(int index = 0; index < touchCount; index ++) {
            Touch touch = Input.touches[index];
            int id = touch.fingerId;
            if(id >= maxTouches) {
                //we will get only the first 5
                continue;
            }
            positions[id] = touch.position;
        }
#endif
  //      for(int index = 0; index < touchCount && index < maxTouches; index ++) {
		//	Touch touch = Input.touches[index];
		//	Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
		//	Debug.Log("is touching");
		//}
	}
}
