using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerBehavior : BasicTower
{

    public float PointerSpeed;
    public int LaserFrameLife;
    public int LaserTickCooldown;

    private GameObject LaserPointer;
    private GameObject CreatedLaser;

    override protected void Start()
    {
        currentCooldown = 0;                     
    }
    /*
    protected void FFindTarget()
    {
        if (currentTarget == null)
        {
            if (currentCooldown <= 0)
            {
                GameObject target = FindNearestEnemy();
                if (target == null)
                {
                    float outDist;
                    target = FindNearestEnemy(out outDist, this.gameObject);
                }
                currentTarget = target;
            }            
        }
        else
        {
            if (GeometryMath.distanceBetweenPoints(
                new Vector3(this.transform.position.x, 0, this.transform.position.z),
                new Vector3(currentTarget.transform.position.x, 0, currentTarget.transform.position.z)
                ) > range)
            {
                currentTarget = null;
                //Destroy(LaserPointer.gameObject);
            }
            else
            {
                if (LaserPointer == null)
                {
                    LaserPointer = new GameObject();
                    LaserPointer.transform.position = currentTarget.transform.position;
                }
                MovePointerToPoint(currentTarget, PointerSpeed);
                if (currentCooldown <= 0)
                {                    
                    OpenFire(LaserPointer);
                }
            }
        }
    }

    override protected void OpenFire(GameObject target)
    {
        Vector3 direction = target.transform.position - this.transform.position;
        ProjectileStatsBase projectileStats = new ProjectileStatsBase(
            damage,
            projectileSpeed,
            GeometryMath.normilizeVector(direction),
            range,
            targetPierces,
            projectileHitbox,
            this.gameObject,
            LaserFrameLife,
            LaserTickCooldown
            );

        if (CreatedLaser == null)
        {
            CreatedLaser = Instantiate(projectileType);
            CreatedLaser.tag = ConstValues.Tags.PROJECTILE_TAG;
            CreatedLaser.SetActive(true);
            CreatedLaser.SendMessage("CreateProjectile", projectileStats);
        }          
        focusLaser(CreatedLaser, target, direction);
        CreatedLaser.SendMessage("RefreshStats", projectileStats);
    }

    override protected GameObject FindNearestEnemy()
    {
        float distance;
        if (LaserPointer == null)
        {
            GameObject enemyFound = FindNearestEnemy(out distance, this.gameObject);
            if (enemyFound != null)
            {
                LaserPointer = new GameObject();
                LaserPointer.transform.position = enemyFound.transform.position;
            }           
            return enemyFound;
        }
        else
        {
            return FindNearestEnemy(out distance, LaserPointer);
        }       
    }
    */
    private void focusLaser(GameObject laser, GameObject target, Vector3 direction)
    {
        laser.transform.position = this.transform.position;
        laser.transform.localScale = new Vector3(laser.transform.localScale.x, range, laser.transform.localScale.z);
        laser.transform.LookAt(target.transform);
        laser.transform.eulerAngles = new Vector3(
            laser.transform.eulerAngles.x + 90,
            laser.transform.eulerAngles.y,
            laser.transform.eulerAngles.z
            );
        laser.transform.position = GeometryMath.goToPosition(
            laser.transform.position,
            GeometryMath.normilizeVector(direction),
            laser.transform.localScale.y
            );
    }

    private void MovePointerToPoint(GameObject target, float speed)
    {
        LaserPointer.transform.position = GeometryMath.goToPosition(
            LaserPointer.transform.position, 
            GeometryMath.normilizeVector(target.transform.position - LaserPointer.transform.position), 
            speed
            );
    }

    public void LaserGone()
    {
        currentCooldown = fireRateCooldown;
        Destroy(LaserPointer.gameObject);
        currentTarget = null;        
    }
}
