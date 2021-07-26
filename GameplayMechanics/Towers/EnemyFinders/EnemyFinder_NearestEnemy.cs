using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFinder_NearestEnemy : EnemyFinder
{
    public override GameObject FindEnemy(float range)
    {
        List<Enemy> enemiesComponents = ExtractComponenentsFromAllLists<Enemy>(enemyLists, false);
        GameObject enemyFound = null;
        float minDistance = range;
        for (int i = 0; i < enemiesComponents.Count; i++)
        {
            float distanceWithEnemy = DistanceMeter.CalculateLogicalDistance(startingPoint.transform.position, enemiesComponents[i].transform.position, rangeShape);
            if (distanceWithEnemy <= minDistance)
            {
                enemyFound = enemiesComponents[i].gameObject;
                minDistance = distanceWithEnemy;
            }
        }
        return enemyFound;
    }

    public override bool TryFindEnemy(out GameObject enemyFound, float range)
    {
        enemyFound = FindEnemy(range);
        if (enemyFound == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
