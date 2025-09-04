using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator animator;

    protected const float maxHp = 2f, maxLaserWallHp = 10f;
    protected float hp;

    protected Vector2 Dir => (World.Player.laserRenderer.transform.position - World.Boss.transform.position).normalized;

    protected void SetAbstract(Attack atk)
    {
        hp = (int)atk.type * maxHp;
    }

    public void DamageProj(float dmg)
    {
        hp -= dmg;
        if(hp <= 0) Destroy(gameObject);
    }

    public abstract void Set(Attack atk = null);

    private bool IsOnPlayerLayer(GameObject go) => go.layer.Equals(LayerMask.NameToLayer("Player"));
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(IsOnPlayerLayer(col.collider.gameObject) && !IsOnPlayerLayer(gameObject)) World.Lives.LoseLife();
        Destroy(gameObject);
    }
}
