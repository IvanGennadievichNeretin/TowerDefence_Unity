using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public float maxHp;
    public float hp;
    public float speed;
    public Vector3 direction;
    public EnemyStats(float hp, float speed, Vector3 direction)
    {
        maxHp = hp;
        this.hp = hp;
        this.speed = speed;
        this.direction = direction;
    }
}
