using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyList;
    BasicProjectileStats stats;

    void Start()
    {
        if (this.tag == ConstValues.Tags.PROTOTYPE_TAG)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stats == null) return;
        checkCollisions();
    }

    public void CreateProjectile(BasicProjectileStats stats)
    {
        this.stats = stats;
    }

    public void destroyProjectile()
    {
        if (this.tag != ConstValues.Tags.PROTOTYPE_TAG)
        {
            Destroy(this.gameObject);
        }
    }

    private void checkCollisions()
    {
        List<GameObject> enemiesFound;
        for (int i = 0; i < enemyList.transform.childCount; i++)
        {
            GameObject enemyFound = enemyList.transform.GetChild(i).gameObject;
            if (GeometryMath.itIsCollision(this.transform.position, enemyFound.transform.position, stats.hitbox.x, stats.hitbox.y, stats.hitbox.z))
            {
                enemyFound.SendMessage("HitEnemy", stats.damage);                
            }
        }
        destroyProjectile();
    }
}
