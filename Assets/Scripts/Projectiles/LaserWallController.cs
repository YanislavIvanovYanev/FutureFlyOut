using System.Collections;
using UnityEngine;

public class LaserWallController : AbstractProjectile
{
    [SerializeField] private SpriteRenderer sRen;
    [SerializeField] private Collider2D col;

    private WaitForSeconds activateDelay = new(1f);

    public override void Set(Attack atk)
    {
        var direction = Quaternion.AngleAxis(atk.angleMod, Vector3.forward) * Dir;
        SetAbstract(atk);
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        StartCoroutine(Activate(atk.speed));
    }

    private IEnumerator Activate(float speed)
    {
        yield return activateDelay;
        col.enabled = true;
        sRen.color = Color.yellow;
        StartCoroutine(Die(speed));
    }

    private IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(this != null) Destroy(gameObject);
    }
}
