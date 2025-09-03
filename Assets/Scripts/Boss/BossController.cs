using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [HideInInspector] public int phase = 1;
    private bool avoiding;
    private float moveSpeed = firstPhaseMS, moveDir = 1f, slow;
    private const float avoidanceSensitivity = 1f, borderMax = 4f, borderMin = 3f, movePower = 2.5f, turnSpeed = 6f,
     pushTime = .1f, pushForce = -15f, firstPhaseMS = 1f, secondPhaseMS = 2.25f, thirdPhaseMS = 3f, slowOnHit = .2f;
    private Vector2 smoothedDir = Vector2.zero;

    private static readonly WaitForSeconds avoidingTime = new(1f), slowTime = new(1f);

    private float MyY => transform.position.y;
    private float PlayerY => World.Player.transform.position.y;

    public void StartBoss()
    {
        InvokeRepeating(nameof(MoveDirChange), 0f, .2f);
        World.BossBar.StartBoss();
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
        float diff = myY - PlayerY;
        if(absMyY > borderMax) moveDir = myY < 0 ? 1f : -1f;
        if(absMyY > borderMin || avoiding)
            return;
        if(Mathf.Abs(diff) < avoidanceSensitivity)
        {
            moveDir = myY < 0 ? 1f : -1f;
            avoiding = true;
            StartCoroutine(StopAvoiding());
        }
        else moveDir *= Random.value < 0.5f ? -1f : 1f;
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
        World.BossBar.DamageBoss(10f);
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
    }

    public void HitBoss(float amount)
    {
        World.BossBar.DamageBoss(amount);
        slow += slowOnHit;
        StartCoroutine(RemoveSlow());
    }

    private IEnumerator RemoveSlow()
    {
        yield return slowTime;
        slow -= slowOnHit;
    }
}
