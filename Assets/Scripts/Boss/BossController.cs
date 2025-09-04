using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [HideInInspector] public int phase = 1;
    private bool avoiding;
    private float moveSpeed = firstPhaseMS, moveDir = 1f, slow, slowOnHit = .15f, lastSlowOnHit;
    private const float avoidanceSensitivity = 1f, borderMax = 3.5f, borderMin = 2.5f, movePower = 2.5f, turnSpeed = 6f,
     pushTime = .1f, pushForce = -15f, firstPhaseMS = 1f, secondPhaseMS = 2.25f, thirdPhaseMS = 3f,
     initialAttackDelay = 1f;
    private Vector2 smoothedDir = Vector2.zero;

    private static readonly WaitForSeconds avoidingTime = new(.8f), slowTime = new(1f);

    public float MyY => transform.position.y;
    private float PlayerY => World.Player.transform.position.y;

    public void StartBoss()
    {
        InvokeRepeating(nameof(MoveDirChange), 0f, .1f);
        World.BossBar.StartBoss();
        World.BossAttacks.StartCoroutine(World.BossAttacks.Attack(initialAttackDelay));
        enabled = true;
    }

    private void FixedUpdate()
    {
        float moveY = movePower * moveDir;
        smoothedDir = Vector2.Lerp(smoothedDir, Vector2.up * moveY, turnSpeed * Time.fixedDeltaTime);
        rb.linearVelocity = (moveSpeed - slow) * smoothedDir;
    }

    private void MoveDirChange()
    {
        float myY = MyY;
        float absMyY = Mathf.Abs(myY);
        float oppositeDir = myY < 0 ? 1f : -1f;

        if(absMyY > borderMax) moveDir = oppositeDir;
        if(absMyY > borderMin || avoiding)
            return;

        if(Mathf.Abs(myY - PlayerY) < avoidanceSensitivity)
        {
            moveDir = oppositeDir;
            avoiding = true;
            StartCoroutine(StopAvoiding());
        }
        else moveDir = MathUtil.RandomSign();
    }

    private IEnumerator StopAvoiding()
    {
        yield return avoidingTime;
        avoiding = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.CompareTag("Player"))
            return;
        var playerCon = col.GetComponent<PlayerController>();
        playerCon.pushed = true;
        col.attachedRigidbody.linearVelocity = new(pushForce, 0f);
        World.Lives.LoseLife();
        World.BossBar.DamageBoss(10);
        StartCoroutine(PushPlayerBack(playerCon));
    }

    private IEnumerator PushPlayerBack(PlayerController playerCon)
    {
        yield return new WaitForSeconds(pushTime);
        playerCon.pushed = false;
    }

    public void SwitchPhase(bool third)
    {
        LogUtil.Log($"Switching to {(third ? "third" : "second")} phase");
        phase = third ? 3 : 2;
        moveSpeed = third ? thirdPhaseMS : secondPhaseMS;
        slowOnHit *= 2f;
        BossBar.scoreMultOnDamage *= 2;
    }

    public void HitBoss(int amount)
    {
        World.BossBar.DamageBoss(amount);
        slow += slowOnHit;
        lastSlowOnHit = slowOnHit;
        StartCoroutine(RemoveSlow());
    }

    private IEnumerator RemoveSlow()
    {
        yield return slowTime;
        slow -= lastSlowOnHit;
    }
}
