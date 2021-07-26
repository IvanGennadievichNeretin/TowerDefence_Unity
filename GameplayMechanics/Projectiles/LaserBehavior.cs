using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : Projectile
{
    public Light lightInTheStart;
    public Light lightInTheEnd;
    public GameObject higherPoint;
    public GameObject lowerPoint;
    public Terrain activeTerrain;
    private float startingRangeStart;
    private float startingRangeEnd;

    private int frameLife = -2;
    private int cooldown;
    private Vector3 startingScale;

    override protected void Start()
    {
        if (this.tag == ConstValues.Tags.PROTOTYPE_TAG)
        {
            this.gameObject.SetActive(false);
            return;
        }
        if (frameLife == -2)
        {
            frameLife = -1;
        }      
        cooldown = 0;
        startingScale = this.transform.localScale;
        startingRangeStart = lightInTheStart.range;
        startingRangeEnd = lightInTheEnd.range;
        this.transform.localScale = new Vector3(0, this.transform.localScale.y, 0);
    }

    // Update is called once per frame
    override protected void Update()
    {
        changeStartingAndEndingSize(frameLife, projectileStats.frameLife / 3);
        if (cooldown <= 0)
        {
            if (HitEnemies())
            {
                //cooldown = stats.ticksFrameCooldown;                
            }            
        }
        else
        {
            cooldown--;
        }
        if (frameLife > 0)
        {
            frameLife--;
        }    
        if (frameLife == 0)
        {
            projectileStats.sender.SendMessage("LaserGone");
            DestroyProjectile();
        }
    }

    override public void CreateProjectile(BasicProjectileStats stats)
    {
        this.projectileStats = stats;
        frameLife = stats.frameLife;        
        cooldown = 0;
    }
    /*
    override protected bool HitEnemies()
    {
        bool enemyHitted = false;
        for (int i = 0; i < enemyList.transform.childCount; i++)
        {
            GameObject enemyFound = enemyList.transform.GetChild(i).gameObject;
            if (CheckCollision(enemyFound.transform.position, projectileStats.hitbox))
            {
                enemyFound.SendMessage("HitEnemy", projectileStats.damage);
                enemyHitted = true;               
            }
        }        
        return enemyHitted;
    }
    */

    /*
    override protected bool CheckCollision(Vector3 pt2, Vector3 diff)
    {        
        pt2 -= this.transform.position;
        if (Mathf.Abs(GeometryMath.DistanceFromPointToLine(pt2, projectileStats.direction)) < diff.x)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }
    */

    private void changeStartingAndEndingSize(int frameLife, int framesOfExtinction)
    {
        float scaleXZ = calculateScaleFromFramelife(frameLife, projectileStats.frameLife, framesOfExtinction);
        this.transform.localScale = new Vector3(scaleXZ * startingScale.x, this.transform.localScale.y, scaleXZ * startingScale.z);
        lightInTheStart.range = startingRangeStart * scaleXZ;
        lightInTheEnd.range = startingRangeEnd * scaleXZ;
        moveLightSourceToTerrainSurface(lightInTheEnd);
    }

    private float calculateScaleFromFramelife(int frameLife, int maxFrameLife, int framesOfExtinctionLaser)
    {
        
        if (framesOfExtinctionLaser == 0)
        {
            framesOfExtinctionLaser = 1;
        }

        float scale;
        if (frameLife < maxFrameLife / 2)
        {
            scale = System.Convert.ToSingle(frameLife) / System.Convert.ToSingle(framesOfExtinctionLaser);
        }
        else
        {
            scale = System.Convert.ToSingle(-(frameLife - maxFrameLife)) / System.Convert.ToSingle(framesOfExtinctionLaser);
        }

        if (scale < 0)
        {
            scale = 0;
        }
        if (scale > 1)
        {
            scale = 1;
        }
        return scale;
    }

    private void moveLightSourceToTerrainSurface(Light source)
    {
        float yHigh = Mathf.Abs(higherPoint.transform.position.y);
        float yLow = Mathf.Abs(lowerPoint.transform.position.y);
        float sum = yHigh + yLow;
        float koef;
        if (sum != 0)
        {
            koef = 2 * (yHigh / sum) - 1;
        }
        else
        {
            koef = -1;
        }
        source.transform.localPosition = new Vector3(0, koef - 0.1f, 0);
    }
}
