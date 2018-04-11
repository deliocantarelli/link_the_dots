using UnityEngine;
using System.Collections;

//This class is a helper to make easier when porting to other platforms

public enum InputStage {
    Start,
    Move,
    Finish
}

public class InputClass
{
    public int Id { get; private set; }
    public Vector3 Position { get; private set; }
    public InputStage Stage { get; private set; }
    public InputClass(int id, Vector3 pos, InputStage inputStage) {
        this.Id = id;
        Position = pos;
        Stage = inputStage;
    }
}
