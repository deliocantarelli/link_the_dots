using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class InputHandler : MonoBehaviour
{
	private class RegisteredObject {
        public GameObject GameObject { get; private set; }
        public IInputScript Behaviour { get; private set; }
        public RegisteredObject(GameObject obj, IInputScript monoBehaviour) {
            GameObject = obj;
            Behaviour = monoBehaviour;
        }
	}
	private List<RegisteredObject> registeredObjects; 
	private IInputManager inputManager;


#if !UNITY_STANDALONE
    const int MaxTouches = 5;
#endif

    // Use this for initialization
    void Start()
    {
        registeredObjects = new List<RegisteredObject>();
        //we could easily make a factory here, but I don't think that a new file overhead is worth
#if !UNITY_STANDALONE
        inputManager = new TouchInputManager(MaxTouches);
#else
        inputManager = new MouseInputManager();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        InputClass[] inputs = inputManager.GetInputs();
        if(inputs.Length == 0) {
            return;
        }


        foreach(InputClass input in inputs) {
            switch(input.Stage) {
                case InputStage.Start:
					RegisteredObject registered = FindRegisteredByInput(input.Position);
					registered.Behaviour.OnTouchStart(input.Position);
                    break;
                //TODO: we have to save the current behavior so we can know what is happening with the scene
                //we will save the object and the input id, so we will call this object even if not directly below input
                //we also have to disable multi-touch on the same object!
                case InputStage.Move:
                    break;
                case InputStage.Finish:
                    break;
            }
        }
	}

    public void RegisterObject(GameObject gameObject, IInputScript behaviour)
    {
        RegisteredObject registered = new RegisteredObject(gameObject, behaviour);
        registeredObjects.Add(registered);
    }
    private RegisteredObject FindRegisteredByInput(Vector3 position)
    {
        return FindRegistered(FindTarget(position));
    }
    private RegisteredObject FindRegistered(GameObject obj) 
    {
        foreach(RegisteredObject registered in registeredObjects) {
            if(obj == registered.GameObject) {
                return registered;
            }
        }
        return null;
    }
    private GameObject FindTarget(Vector3 position) 
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = position
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        GameObject result;
        if(results.Count > 0) {
            result = results[0].gameObject;
        } else {
            result = null;
        }
        return result;
    }
}
