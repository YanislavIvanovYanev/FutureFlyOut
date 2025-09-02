using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [HideInInspector] public bool pushed;
    private bool boosting;
    private float moveSpeed = movePower;
    private const float movePower = 3f, runPower = 1.5f, turnSpeed = 4f, turnStopSpeed = turnSpeed * 1.5f, xMult = 1.5f;
    private Vector2 smoothedDir = Vector2.zero;
    private Vector2 MoveI => World.Input.move.Normalized;

#region MonoBehaviour
    private void Awake() => transform.position = new(-8f, 0f);

    private void Start()
    {
        World.Input.shoot.OnDown += Shoot;
        World.Input.boost.OnDown += Boost;
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

    // private void Update()
    // {
    //     animator.SetFloat("MoveX", moveX);
    //     animator.SetFloat("MoveY", World.Input.move.Y);
    //     animator.SetBool("Boost", boost);
    // }
#endregion

#region Actions
    private void Shoot(bool value)
    {
        //magic
    }

    private void Boost(bool value)
    {
        boosting = value;
        moveSpeed = boosting ? movePower * runPower : movePower;
    }
#endregion
}
