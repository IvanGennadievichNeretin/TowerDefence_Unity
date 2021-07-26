using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    // Start is called before the first frame update
    public float movingSpeed = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressed();
    }

    void KeyPressed()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = new Vector3(this.transform.position.x + 1 * movingSpeed, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = new Vector3(this.transform.position.x - 1 * movingSpeed, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1 * movingSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1 * movingSpeed);
        }
    }
}
