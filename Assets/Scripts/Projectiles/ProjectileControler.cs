using UnityEngine;

public class ProjectileControler : AbstractProjectile
{
    public override void Set(Attack atk)
    {
        SetAbstract(atk);
        rb.linearVelocity = Quaternion.AngleAxis(atk.angleMod, Vector3.forward) * Dir * atk.speed;
        animator.Play("Projectile");
    }
}
