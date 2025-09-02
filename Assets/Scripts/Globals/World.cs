using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private InputManager input;

    public static World inst;
    public static InputManager Input => inst.input;
}
