using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask laserTargetLayers;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer laserRenderer;

    [HideInInspector] public bool pushed;
    private bool boosting, canShoot = true;
    private float moveSpeed = movePower;
    private const float movePower = 3.3f, boostPower = 1.5f, turnSpeed = 4.5f, turnStopSpeed = turnSpeed * 1.5f, xMult = 1.5f,
     range = 200f, baseDamage = 1f;
    private Vector2 smoothedDir = Vector2.zero;
    
    private static readonly WaitForSeconds laserDuration = new(.08f);

    private Vector2 MoveI => World.Input.move.Normalized;

#region MonoBehaviour
    private void Awake() => transform.position = new(-8f, 0f);

    private void Start()
    {
        World.Input.shoot.OnDown += Shoot;
        World.Input.boost.OnDown += Boost;
        World.Boss.StartBoss();
    }

    private void FixedUpdate()
    {
        if(pushed)
            return;
        var moveI = MoveI;
        bool stop = moveI.magnitude == 0f;
        if(!stop) moveI = new(moveI.x * xMult, moveI.y);
        smoothedDir = Vector2.Lerp(smoothedDir, stop ? Vector2.zero : moveI, (stop ? turnStopSpeed : turnSpeed) * Time.fixedDeltaTime);
        rb.linearVelocity = rb.linearVelocity.magnitude < .01f && stop ? Vector2.zero : moveSpeed * smoothedDir;
    }

    private void Update()
    {
        if(!canShoot) MakeLaser(true);
        // animator.SetFloat("MoveX", moveX);
        // animator.SetFloat("MoveY", World.Input.move.Y);
        // animator.SetBool("Boost", boost);
    }
#endregion

#region Actions
    private void Shoot(bool value)
    {
        if(!value || !canShoot)
            return;
        
        var hit = MakeLaser(false);

        if(hit.collider == null)
            return;
        if(hit.collider.TryGetComponent(out BossController bossCon)) bossCon.HitBoss(baseDamage);
    }

    private RaycastHit2D MakeLaser(bool onlyUpdate)
    {
        Vector2 origin = laserRenderer.transform.position;
        Vector2 direction = laserRenderer.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, range, laserTargetLayers);
        Vector2 endPoint = hit.collider != null ? hit.point : origin + direction.normalized * range;

        if(onlyUpdate) UpdateLaser(origin, endPoint);
        else StartCoroutine(DrawLaser(origin, endPoint));

        return hit;
    }

    private IEnumerator DrawLaser(Vector2 start, Vector2 end)
    {
        UpdateLaser(start, end);
        canShoot = false;
        yield return laserDuration;
        laserRenderer.enabled = false;
        canShoot = true;
    }

    private void UpdateLaser(Vector2 start, Vector2 end)
    {
        laserRenderer.positionCount = 2;
        laserRenderer.SetPosition(0, start);
        laserRenderer.SetPosition(1, end);
        laserRenderer.enabled = true;
    }
    
    private void Boost(bool value)
    {
        boosting = value;
        moveSpeed = boosting ? movePower * boostPower : movePower;
    }
#endregion
}
