using UnityEngine;

public class BombController : AbstractProjectile
{
    [SerializeField] private LayerMask mask;

    private const int bombDmg = 20, scoreGainOnBoss = 10, scoreGainOnProjectile = 2;
    private const float speed = 5f, radius = 15f;

    public override void Set(Attack _ = null) => rb.linearVelocity = World.Player.ShootDir * speed;

    private void OnDestroy()
    {
        int rawScoreGain = 0;
        foreach(Collider2D col in Physics2D.OverlapCircleAll(transform.position, radius, mask))
        {
            if(col.TryGetComponent(out BossController bossCon))
            {
                rawScoreGain += scoreGainOnBoss;
                bossCon.HitBoss(bombDmg);
            }
            else if(col.TryGetComponent(out AbstractProjectile projCon))
            {
                rawScoreGain += scoreGainOnProjectile;
                projCon.DamageProj(bombDmg);
            }
        }
        World.Score.Add(rawScoreGain * BossBar.scoreMultOnDamage);
        LogUtil.Log(rawScoreGain, BossBar.scoreMultOnDamage);
    }
}
