using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private PlayerController player;
    [SerializeField] private LivesController lives;
    [SerializeField] private BossController boss;
    [SerializeField] private BossBar bossBar;
    [SerializeField] private BossAttacks bossAttacks;
    [SerializeField] private ScoreController score;

    public static World inst;
    public static InputManager Input => inst.input;
    public static PlayerController Player => inst.player;
    public static LivesController Lives => inst.lives;
    public static BossController Boss => inst.boss;
    public static BossBar BossBar => inst.bossBar;
    public static BossAttacks BossAttacks => inst.bossAttacks;
    public static ScoreController Score => inst.score;

    private void Awake() => inst = this;
}
