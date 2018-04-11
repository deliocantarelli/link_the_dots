using UnityEngine;
using System.Collections;

public class TouchInputManager : IInputManager
{
    private int MaxTouches;
    public TouchInputManager(int maxTouches) {
        MaxTouches = maxTouches;
    }
    public InputClass[] GetInputs(){
        int touchCount = Input.touchCount;
        int arraySize = Mathf.Max(touchCount, MaxTouches);
        InputClass[] inputs = new InputClass[arraySize];

        for (int index = 0; index < touchCount; index++)
        {
            Touch touch = Input.touches[index];
            int id = touch.fingerId;
            if (id >= MaxTouches)
            {
                continue;
            }
            Vector3 position = touch.position;
            InputStage stage = getTouchStage(touch);
            inputs[index] = new InputClass(id, position, stage);
        }

        return inputs;
    }

    private InputStage getTouchStage(Touch touch)  {
        switch (touch.phase) {
            case TouchPhase.Began:
                return InputStage.Start;
            case TouchPhase.Moved:
                return InputStage.Move;
            case TouchPhase.Ended:
			default:
                return InputStage.Finish;

        }
    }
}
