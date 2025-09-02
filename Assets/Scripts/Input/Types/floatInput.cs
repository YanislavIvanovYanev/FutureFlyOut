using UnityEngine.InputSystem;

public class floatInput : Input<float>
{
    public bool Value { get => raw > 0f; set => raw = value ? 1f : 0f; }
    public bool Was { get => wasRaw > 0f; set => raw = value ? 1f : 0f; }
    public bool Press => Value && !Was;
    public bool Release => !Value && Was;
    public float Raw => raw;

    public System.Action<bool> OnDown = delegate{};

    public floatInput(InputAction _action) : base(_action) => OnChange += ChangeToDown;
    
    public void ChangeToDown(float value)
    {
        if(value == 1f) OnDown.Invoke(true);
        else if(value == 0f) OnDown.Invoke(false);
    }
}
