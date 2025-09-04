using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    protected const float maxHp = 1f;
    protected float hp;

    protected Vector2 Dir => (World.Player.transform.position - World.Boss.transform.position).normalized;

    protected void SetAbstract(Attack atk)
    {
        hp = maxHp * (int)atk.type;
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
