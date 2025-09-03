using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float moveSpeed = firstPhaseMS, moveDir = 1f;
    private const float avoidanceSensitivity = 1f, borderMax = 4f, borderMin = 3f, movePower = 2f, turnSpeed = 6f,
     pushTime = .1f, pushForce = -15f, firstPhaseMS = 1f, secondPhaseMS = 2.5f, thirdPhaseMS = 4f;
    private Vector2 smoothedDir = Vector2.zero;

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
        rb.linearVelocity = moveSpeed * smoothedDir;
    }

    private void MoveDirChange()
    {
        float myY = MyY;
        float absMyY = Mathf.Abs(myY);
        float diff = myY - PlayerY;
        if(absMyY > borderMax) moveDir = myY < 0 ? 1f : -1f;
        if(absMyY > borderMin || Mathf.Abs(diff) < avoidanceSensitivity)
            return;
        //if(Mathf.Abs(diff) < avoidanceSensitivity) moveDir = diff < 0 ? -1f : 1f;
        /*else*/ moveDir *= Random.value < 0.5f ? -1f : 1f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.CompareTag("Player"))
            return;
        var playerCon = col.GetComponent<PlayerController>();
        playerCon.pushed = true;
        col.attachedRigidbody.linearVelocity = new(pushForce, 0f);
        World.Lives.LoseLife();
        World.BossBar.Damage(10f);
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
        moveSpeed = third ? thirdPhaseMS : secondPhaseMS;
    }
}
