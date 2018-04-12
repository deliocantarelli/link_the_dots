using UnityEngine;
using System.Collections;

public interface IInputScript
{
    void OnTouchStart(Vector3 position);
    void OnTouchMove(Vector3 position);
    void OnTouchEnd(Vector3 position);
}
