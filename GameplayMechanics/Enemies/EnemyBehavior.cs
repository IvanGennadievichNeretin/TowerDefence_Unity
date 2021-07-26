using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehavior : MonoBehaviour
{
    private EnemyStats stats;
    public TextMeshProUGUI damageText;
    public GameObject hpBarRemaining;
    public Canvas damageTextCanvas;
    public GameObject Waypoints;
    private List<Vector3> path;
    private int currentPathNode;

    void Start()
    {
        if (stats == null)
        {
            stats = new EnemyStats(1, 0, new Vector3(0, 0, 0));
        }
        if (this.tag == ConstValues.Tags.PROTOTYPE_TAG)
        {
            this.gameObject.SetActive(false);
        }
        getPath();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = GoThroughPath(this.gameObject.transform.position, path, stats.speed);        
        if (stats.hp <= 0)
        {
            EnemyDied();
        }
    }

    public void HitEnemy(float damage)
    {
        stats.hp -= damage;
        outputDamage(damage);
    }

    public void SetStats(EnemyStats stats)
    {
        this.stats = stats;
    }

    private void outputDamage(float damage)
    {                
        TextMeshProUGUI captionCreated = Instantiate(damageText);       
        captionCreated.tag = ConstValues.Tags.DAMAGE_CAPTION_TAG;
        captionCreated.text = damage + "";
        captionCreated.gameObject.SetActive(true);
        captionCreated.transform.SetParent(damageTextCanvas.transform);
        captionCreated.SendMessage("Appear", this.transform.position);
        hpBarRemaining.SetActive(true);
        hpBarRemaining.SendMessage("SetCurrentHp", stats.hp / stats.maxHp);
    }

    private void EnemyDied()
    {
        Destroy(this.gameObject);
    }

    private void getPath()
    {
        path = new List<Vector3>();
        for (int i = 0; i < Waypoints.transform.childCount; i++)
        {
            path.Add(Waypoints.transform.GetChild(i).position);
        }
        currentPathNode = 0;
    }

    private Vector3 GoThroughPath(Vector3 oldPosition, List<Vector3> path, float speed)
    {
        if ((path.Count <= currentPathNode) || (speed <= 0))
        {
            return oldPosition;
        }
        float DistanceCutted;
        stats.direction = path[currentPathNode] - oldPosition;
        
        if (GeometryMath.ItIsNullVector(stats.direction))
        {
            currentPathNode++;
            return GoThroughPath(oldPosition, path, speed);
        }

        Vector3 newPosition = GeometryMath.goToPosition(
                oldPosition,
                GeometryMath.normilizeVector(stats.direction),
                path[currentPathNode],
                speed,
                out DistanceCutted);
        if (DistanceCutted > 0)
        {           
            currentPathNode++;
            newPosition = GoThroughPath(newPosition, path, DistanceCutted);
        }

        return newPosition;
    }

   
}
