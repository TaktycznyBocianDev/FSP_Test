using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMeleeExploadingEnemy : EnemyAbstr
{
    public float flySpeed;
    public float explosionDamage;
    public float explosionRange;

    //I assume that movement is for ground, and fly for air
    protected void Fly(float flySpeed)
    {
        //fly
    }

    protected void Kamikadze()
    {
        //fly to Player and explode
    }
}
