using UnityEngine;

public class TrackingControler : AbstractProjectile
{
    public override void Set(Attack atk)
    {
        SetAbstract(atk);
        hp = maxHp;
        rb.linearVelocity = Quaternion.AngleAxis(atk.angleMod, Vector3.forward) * Dir * atk.speed;
        rb.angularVelocity = atk.speed * 100f;
        animator.Play("Tracking");
    }
}
