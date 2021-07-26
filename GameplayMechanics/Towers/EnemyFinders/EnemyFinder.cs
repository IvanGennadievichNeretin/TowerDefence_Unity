using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyFinder : MonoBehaviour
{
    public GameObject startingPoint { get; set; }
    public GameObject[] enemyLists { get; set; }
    public DistanceMeter.DistanceMode rangeShape { get; set; }

    public void Awake()
    {
        startingPoint = this.gameObject;
        rangeShape = DistanceMeter.DistanceMode.Sphere;
        enemyLists = new GameObject[0];
    }

    public abstract bool TryFindEnemy(out GameObject enemyFound, float range);
    public abstract GameObject FindEnemy(float range);

    public List<Enemy> FindEnemiesInRange(float range)
    {
        List<Enemy> enemiesFound = new List<Enemy>();
        List<Enemy> enemiesInLists = ExtractComponenentsFromAllLists<Enemy>(enemyLists, false);
        for (int i = 0; i < enemiesInLists.Count; i++)
        {
            if (DistanceMeter.CalculateLogicalDistance(this.transform.position, enemiesInLists[i].transform.position, rangeShape) > range)
            {
                enemiesFound.Add(enemiesInLists[i]);
            }
        }
        return enemiesFound;
    }

    protected static List<T> ExtractComponenentsFromAllLists<T>(GameObject[] enemyLists, bool includeInactive)
    {
        List<T> componentsList = new List<T>();
        for (int i = 0; i < enemyLists.Length; i++)
        {
            T[] components = enemyLists[i].GetComponentsInChildren<T>(includeInactive);
            for (int j = 0; j < components.Length; j++)
            {
                componentsList.Add(components[j]);
            }
        }
        return componentsList;
    }
}
