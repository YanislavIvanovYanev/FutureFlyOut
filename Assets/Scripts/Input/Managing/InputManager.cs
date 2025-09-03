using UnityEngine;

public class InputManager : MonoBehaviour
{
    public System.Action OnDisableInput = () => {}, OnUpdate = () => {};
    public PlayerInput inputActions;

    [HideInInspector] public floatInput shoot, boost, heal;
    [HideInInspector] public VectorInput move;

    private void Awake() => inputActions = new();

    private void OnEnable()
    {
        shoot = new(inputActions.Player.Shoot);
        boost = new(inputActions.Player.Boost);
        heal = new(inputActions.Player.Heal);
        move = new(inputActions.Player.Move);
    }
    private void OnDisable() => OnDisableInput();

    private void Update() => OnUpdate();
}
