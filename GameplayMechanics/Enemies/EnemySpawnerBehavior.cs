using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : MonoBehaviour
{
    public int cooldown;
    public int enemyCount;
    public GameObject enemyType;
    public GameObject enemyList;
    public GameObject nextWaypoint;
    private int currentCooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        currentCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if ((currentCooldown <= 0) && (enemyCount > 0))
        {
            spawnEnemy(enemyType);
            enemyCount--;
            currentCooldown = cooldown;
        }
        else
        {
            currentCooldown -= 1;
        }
    }

    private void spawnEnemy(GameObject typeOfEnemy)
    {
        GameObject createdEnemy = Instantiate(typeOfEnemy);
        createdEnemy.transform.position = this.transform.position;
        createdEnemy.tag = ConstValues.Tags.ENEMY_TAG;
        createdEnemy.transform.SetParent(enemyList.transform);        
        createdEnemy.SetActive(true);
        Vector3 directionVector = nextWaypoint.transform.position - createdEnemy.transform.position;
        createdEnemy.SendMessage("SetStats", new EnemyStats(1500, 0.05f, GeometryMath.normilizeVector(directionVector)));
    }
}
