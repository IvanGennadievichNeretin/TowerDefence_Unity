using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangeSpriteView))]
[RequireComponent(typeof(EnemyFinder))]
public class BasicTower : MonoBehaviour
{
    [Header("Tower stats")]
    [Min(0f)] public float range;
    public int fireRateCooldown;

    [Header("Projectile stats")]
    [SerializeField] private BasicProjectileStats projectileStats;

    [Header("Tower properties")]
    [Tooltip("How logical distance to target is calculated")]
    public DistanceMeter.DistanceMode rangeShape;

    [Header("Projectile shooted")]
    public GameObject projectileType;

    protected RangeSpriteView rangeSpriteViewComponent;
    protected EnemyFinder enemyFinderComponent;
    protected GameObject currentTarget;
    protected int currentCooldown;

    virtual protected void Start()
    {
        currentCooldown = 0;
        rangeSpriteViewComponent = this.GetComponent<RangeSpriteView>();
        enemyFinderComponent = this.GetComponent<EnemyFinder>();
        enemyFinderComponent.enemyLists = projectileStats.enemyLists;
        enemyFinderComponent.startingPoint = this.gameObject;
        enemyFinderComponent.rangeShape = rangeShape;
        SetRange(range);
    }

    virtual protected void Update()
    {
        if (currentTarget == null)
        {
            currentTarget = FindTarget();
        }

        if (TargetOutOfRange(currentTarget, range))
        {
            currentTarget = null;
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= 1;
        }

        if ((currentCooldown <= 0) && (currentTarget != null))
        {
            currentCooldown = fireRateCooldown;
            OpenFire(currentTarget);
        }
    }

    virtual protected GameObject FindTarget()
    {
        GameObject enemyFound;
        if (enemyFinderComponent.TryFindEnemy(out enemyFound, range))
        {
            return enemyFound;
        }
        else
        {
            return null;
        }    
    }

    virtual protected bool TargetOutOfRange(GameObject target, float range)
    {
        if (target == null)
        {
            return true;
        }
        else
        {
            return GetLogicalDistanceToTarget(this.transform, currentTarget.transform) > range;
        }     
    }

    virtual protected void OpenFire(GameObject target)
    {
        CreateProjectile(SetupProjectileStats(target));
    }

    private BasicProjectileStats SetupProjectileStats(GameObject target)
    {
        Vector3 direction = target.transform.position - this.transform.position;
        //Calculates max distance of projectile so it's never will go further than tower radius
        float projectileRange = DistanceMeter.CalculateMaxDistanceWithinShape(this.transform.position, target.transform.position, range, rangeShape);
        BasicProjectileStats newProjectileStats = projectileStats.Clone();
        newProjectileStats.SetDirection(direction).SetMaxDistance(projectileRange).SetSender(this.gameObject);
        return newProjectileStats;
    }

    virtual protected void CreateProjectile(BasicProjectileStats projectileStats)
    {
        GameObject createdProjectile = Instantiate(projectileType);
        createdProjectile.transform.position = this.transform.position;
        createdProjectile.AddComponent<Projectile>();
        createdProjectile.AddComponent<EnemyFinder_FirstInRange>();
        Projectile projectileComponent = createdProjectile.GetComponent<Projectile>();
        projectileComponent.SetStats(projectileStats);
        createdProjectile.SetActive(true);
    }

    virtual protected float GetLogicalDistanceToTarget(Transform from, Transform to)
    {
        return DistanceMeter.CalculateLogicalDistance(from.position, to.position, rangeShape);
    }

    protected void SetRange(float newRange)
    {
        range = newRange;
        if (rangeSpriteViewComponent != null)
        {
            rangeSpriteViewComponent.UpdateRangeSprite(newRange);
        }        
    }
}
