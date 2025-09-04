using UnityEngine;

public class TrackingControler : AbstractProjectile
{
    private float maxAngularSpeed;

    private Attack atk;

    private Vector2 MyDir => ((Vector2)World.Player.transform.position - rb.position).normalized;

    public override void Set(Attack _atk)
    {
        atk = _atk;
        SetAbstract(atk);
        hp = maxHp;
        rb.linearVelocity = Quaternion.AngleAxis(atk.angleMod, Vector3.forward) * Dir * atk.speed;
        maxAngularSpeed = maxAngularSpeed = atk.maxSpeed * 2 / 3;
    }

    private void FixedUpdate()
    {
        var dir = MyDir;
        rb.angularVelocity = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * (maxAngularSpeed - atk.speed) * 50f;
    }
}
