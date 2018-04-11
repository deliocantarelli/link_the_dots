using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputHandler : MonoBehaviour
{
	private class RegisteredObject {
        public GameObject GameObject { get; private set; }
        public MonoBehaviour Behaviour { get; private set; }
        public RegisteredObject(GameObject obj, MonoBehaviour monoBehaviour) {
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
            if(input.Stage == InputStage.Start){
                GameObject target = FindTarget(input.Position);
                //TODO: handle inputs on target found
            }
        }
	}

    public void RegisterObject(GameObject gameObject, MonoBehaviour behaviour) {
        RegisteredObject registered = new RegisteredObject(gameObject, behaviour);
        registeredObjects.Add(registered);
    }

    private GameObject FindTarget(Vector3 position) {
        //TODO: find the object under input on registereds
        foreach(RegisteredObject registered in registeredObjects) {
            RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), Vector2.zero);
            if (hitInfo) {
                return hitInfo.transform.gameObject;
            }
        }
        //TODO: handle correctly when not finding objects
        throw new System.Exception("No game object found");
    }

    private void CallOnInputStart() {
        
    }

    private void CallOnInputMove() {
        
    }

    private void CallOnInputFinish() {
        
    }
}
