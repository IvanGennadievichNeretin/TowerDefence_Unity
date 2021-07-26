using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected BasicProjectileStats projectileStats;
    protected EnemyFinder enemyFinder;
    private float distanceDone;
    virtual protected void Start()
    {
        distanceDone = 0;
        enemyFinder = this.GetComponent<EnemyFinder>();
        enemyFinder.enemyLists = projectileStats.enemyLists;
        enemyFinder.startingPoint = this.gameObject;
        enemyFinder.rangeShape = DistanceMeter.DistanceMode.Sphere;
    }

    virtual protected void Update()
    {
        HitEnemies();
        this.gameObject.transform.position = GeometryMath.goToPosition(this.gameObject.transform.position, projectileStats.direction, projectileStats.speed);       
        distanceDone += GeometryMath.VectorLength(projectileStats.direction * projectileStats.speed);
        if (distanceDone >= projectileStats.maxDistance)
        {
            DestroyProjectile();
        }
    }

    virtual public void CreateProjectile(BasicProjectileStats stats)
    {       
        this.projectileStats = stats;
    }

    virtual public void SetStats(BasicProjectileStats projectileStats)
    {
        this.projectileStats = projectileStats;
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

    virtual protected bool HitEnemies()
    {
        GameObject enemyFound;
        if (enemyFinder.TryFindEnemy(out enemyFound, projectileStats.hitbox.x)){
            enemyFound.SendMessage("HitEnemy", projectileStats.damage);   
            if (projectileStats.targetPierces > 0)
            {
                projectileStats.targetPierces--;
            }
            if (projectileStats.targetPierces == 0)
            {
                Destroy(this.gameObject);
            }
            return true;
        }
        return false;
    }

}
