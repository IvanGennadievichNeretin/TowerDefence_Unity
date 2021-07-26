using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicProjectileStats
{
    [Min(0f)] public float speed;
    [Min(0f)] public float damage;
    public int targetPierces;
    public Vector3 hitbox;
    public int frameLife;
    [Tooltip("Enemy types supported")]
    public GameObject[] enemyLists;
    [HideInInspector] public float maxDistance;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public GameObject sender;

    public static int INFINITE_MAX_DISTANCE = -1;
    public static int INFINITE_TARGETS_PIERCES = -1;
    public static int INFINITE_FRAME_LIFE = -1;
    public static float DEFAULT_PROJECTILE_COLLISION_DISTANCE = 2;

    public BasicProjectileStats(float damage, float speed, Vector3 direction, float maxDistance, int targetPierces, Vector3 hitbox, GameObject sender, int frameLife, GameObject[] enemyLists)
    {
        this.speed = speed;
        this.direction = direction;
        this.damage = damage;
        this.maxDistance = maxDistance;
        this.targetPierces = targetPierces;
        this.hitbox = hitbox;
        this.sender = sender;
        this.frameLife = frameLife;
        this.enemyLists = enemyLists;
    }

    public BasicProjectileStats SetMaxDistance(float maxDistance)
    {
        this.maxDistance = maxDistance;
        return this;
    }
    public BasicProjectileStats SetDirection(Vector3 direction)
    {
        this.direction = GeometryMath.normilizeVector(direction);
        return this;
    }
    public BasicProjectileStats SetSender(GameObject sender)
    {
        this.sender = sender;
        return this;
    }

    public static Vector3 GetDefaultHitBox()
    {
        return new Vector3(DEFAULT_PROJECTILE_COLLISION_DISTANCE, DEFAULT_PROJECTILE_COLLISION_DISTANCE, DEFAULT_PROJECTILE_COLLISION_DISTANCE);
    }

    virtual public BasicProjectileStats Clone()
    {
        return (BasicProjectileStats)this.MemberwiseClone();
    }
}
