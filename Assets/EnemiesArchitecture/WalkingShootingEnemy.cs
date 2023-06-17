using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingShootingEnemy : EnemyAbstr
{
    public float safeDistance;


    protected override void Movement(float speed)
    {
        //Maintain distance to Player >= safeDistance
    }
}
