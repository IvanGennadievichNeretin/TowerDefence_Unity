using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBehavior : MonoBehaviour
{
    public Camera lookAtCamera;
    private int frameLife = 60;
    private int framesLeft;
    private Vector3 lastParentPosition;

    void Start()
    {
        framesLeft = frameLife;       
    }

    void Update()
    {
        if (this.tag == ConstValues.Tags.PROTOTYPE_TAG)
        {
            this.gameObject.SetActive(false);
            return;
        }

        if (framesLeft > 0)
        {
            framesLeft--;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Appear(Vector3 startingPosition)
    {
        this.transform.position = lookAtCamera.WorldToScreenPoint(startingPosition);
        framesLeft = frameLife;
    }
}
