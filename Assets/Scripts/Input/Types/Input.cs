using UnityEngine.InputSystem;

public class Input<T> where T : struct
{
    protected T raw;
    protected T wasRaw;

    public InputAction action;

    public System.Action<T> OnChange = delegate{};

    protected virtual void UpdateInput()
    {
        wasRaw = raw;
        raw = action.ReadValue<T>();
        if(!raw.Equals(wasRaw)) OnChange(raw);
    }

    protected virtual void OnDisable()
    {
        World.Input.OnUpdate -= UpdateInput;
        World.Input.OnDisableInput -= OnDisable;
        action.Disable();
    }

    public Input(InputAction _action)
    {
        action = _action;
        action.Enable();
        World.Input.OnUpdate += UpdateInput;
        World.Input.OnDisableInput += OnDisable;
    }
}
