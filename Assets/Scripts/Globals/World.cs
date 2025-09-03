using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private PlayerController player;
    [SerializeField] private LivesController lives;
    [SerializeField] private BossController boss;
    [SerializeField] private BossBar bossBar;

    public static World inst;
    public static InputManager Input => inst.input;
    public static PlayerController Player => inst.player;
    public static LivesController Lives => inst.lives;
    public static BossController Boss => inst.boss;
    public static BossBar BossBar => inst.bossBar;

    private void Awake() => inst = this;
}
