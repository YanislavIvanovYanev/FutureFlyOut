using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack
{
    public int projCount;
    ///<summary> duration for laser wall </summary>
    public float speed, angleMod, timeUntilNextProj, timeUntilNextAttack;
    public AttackType type;

    private const int minProj = 3, maxProj = 6; //max is exclusive only here
    private const int angleModMin = 5, angleModMax = 45; //Tracking: 6, 30; WallLaser: 9, 45
    public float minSpeed = 1.5f, maxSpeed = 2.5f;
    private const float minProjTime = .1f, maxProjTime = .3f;
    private const float minAtkTime = .5f, maxAtkTime = 1.5f;

    private const float phase2Mod = 1.75f, phase3Mod = 2.25f;

    private static readonly List<AttackType> lastAttacks = new();

    public Attack()
    {
        do type = (AttackType)Random.Range(1, 4);
        while(lastAttacks.Count(t => t == type) > 1);

        if(lastAttacks.Count > 2) lastAttacks.RemoveAt(0);
        lastAttacks.Add(type);

        float fType = (float)type;
        float phaseMod = World.Boss.phase switch{ 2 => phase2Mod, 3 => phase3Mod, _ => 1f };
        float modOVERtype = phaseMod / fType, typeOVERmod = fType / phaseMod;

        projCount = Mathf.RoundToInt(Random.Range(minProj * modOVERtype, maxProj * modOVERtype));
        speed = Random.Range(minSpeed * modOVERtype, maxSpeed * modOVERtype);
        angleMod = MathUtil.RandomSign(Random.Range(angleModMin * fType, angleModMax * fType));
        timeUntilNextProj = Random.Range(minProjTime * typeOVERmod, maxProjTime * typeOVERmod);
        timeUntilNextAttack = Random.Range(minAtkTime / phaseMod, maxAtkTime / phaseMod);
    }
}
