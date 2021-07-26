using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaivor : MonoBehaviour
{
    public GameObject enemyList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkCollisions();
    }

    private void checkCollisions()
    {
        for (int i = 0; i < enemyList.transform.childCount; i++)
        {
            GameObject enemyFound = enemyList.transform.GetChild(i).gameObject;
            if (GeometryMath.itIsCollision(this.transform.position, enemyFound.transform.position, ConstValues.PROJECTILE_COLLISION_DISTANCE))
            {
                enemyFound.SendMessage("EnemyDied");
                //какая-то логика
            }
        }
    }
}
