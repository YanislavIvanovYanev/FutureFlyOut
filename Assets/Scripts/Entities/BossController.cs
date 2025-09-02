using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float moveSpeed = movePower, moveDir = 1f;
    private const float maxHp = 5000, avoidanceSensitivity = 1f, borderMax = 4f, borderMin = 3f, movePower = 2f, turnSpeed = 6f,
     pushTime = .1f, pushForce = -15f;
    private Vector2 smoothedDir = Vector2.zero;

    private float MyY => transform.position.y;
    private float PlayerY => World.Player.transform.position.y;

    private void Start() => InvokeRepeating(nameof(MoveDirChange), 0f, .2f);

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
        LogUtil.Log($"Boss Y: {myY}, Player Y: {PlayerY}, MoveDir: {moveDir}, 1st: {absMyY > borderMax}, 2nd: {Mathf.Abs(diff) < avoidanceSensitivity}, 3rd {absMyY > borderMin}");
        if(absMyY > borderMax) moveDir = myY < 0 ? 1f : -1f;
        if(absMyY > borderMin)
            return;
        if(Mathf.Abs(diff) < avoidanceSensitivity) moveDir = diff < 0 ? -1f : 1f;
        else moveDir *= Random.value < 0.5f ? -1f : 1f;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.CompareTag("Player"))
            return;
        var playerCon = col.GetComponent<PlayerController>();
        playerCon.pushed = true;
        col.attachedRigidbody.linearVelocity = new(pushForce, 0f);//-col.attachedRigidbody.linearVelocity.y);
        StartCoroutine(PushPlayerBack(playerCon));
    }

    private IEnumerator PushPlayerBack(PlayerController playerCon)
    {
        yield return new WaitForSeconds(pushTime);
        playerCon.pushed = false;
    }
}
