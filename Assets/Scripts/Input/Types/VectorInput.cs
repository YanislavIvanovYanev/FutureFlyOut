using UnityEngine;
using UnityEngine.InputSystem;

public class VectorInput : Input<Vector2>
{
    public float X => raw.x;
    public float Y => raw.y;
    public Vector2 last;
    public Vector2 Normalized => raw.normalized;
    public Vector3 Value3 => raw;

    public VectorInput(InputAction _action) : base(_action) { }
}
