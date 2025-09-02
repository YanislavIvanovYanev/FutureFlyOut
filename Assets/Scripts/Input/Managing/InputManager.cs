using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event System.Action OnDisableInput = () => {}, OnUpdate = () => {};
    public PlayerInput inputActions;

    [HideInInspector] public floatInput interact, run, esc, shoot;
    [HideInInspector] public VectorInput move;

    private void Awake() => inputActions = new();

    private void OnEnable()
    {
        shoot = new(inputActions.Player.Shoot);
        interact = new(inputActions.Player.Interact);
        // run = new(inputActions.Player.Run);
        // esc = new(inputActions.Player.Esc);
        // move = new(inputActions.Player.Move);
    }
    private void OnDisable() => OnDisableInput();

    private void Update() => OnUpdate();
}
