using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpBarBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public GameObject owner;
    void Start()
    {
        if (owner != null)
        {
            this.transform.position = mainCamera.WorldToScreenPoint(owner.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (owner != null)
        {
            this.transform.position = mainCamera.WorldToScreenPoint(owner.transform.position);
        }       
    }
}
