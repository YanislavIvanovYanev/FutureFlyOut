using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private PlayerController player;

    public static World inst;
    public static InputManager Input => inst.input;
    public static PlayerController Player => inst.player;

    private void Awake() => inst = this;
}
