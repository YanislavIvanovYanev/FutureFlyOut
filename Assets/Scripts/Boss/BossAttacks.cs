using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    [SerializeField] private Transform atkPoint;
    [SerializeField] private List<GameObject> projPrefabs;

    public IEnumerator Attack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Attack atk = new();
        StartCoroutine(Attack(atk.timeUntilNextAttack));
        
        WaitForSeconds delay = new(atk.timeUntilNextProj);

        for(int i = 0; i < atk.projCount; i++)
        {
            Instantiate(projPrefabs[(int)atk.type - 1], atkPoint.position, Quaternion.identity).GetComponent<AbstractProjectile>().Set(atk);
            yield return delay;
        }
    }
}
