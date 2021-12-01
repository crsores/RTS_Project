using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    public float camspeed;
    public float scrolspeed;
    public float miny = 20f;
    public float maxy = 120f;

    public float BorderThickness = 10f;


    public Vector2 limit;


    void Start()
    {

    }


    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - BorderThickness)
        {
            pos.z += camspeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= BorderThickness)
        {
            pos.z -= camspeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - BorderThickness)
        {
            pos.x += camspeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= BorderThickness)
        {
            pos.x -= camspeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrolspeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -limit.x, limit.x);
        pos.y = Mathf.Clamp(pos.y, miny, maxy);
        pos.z = Mathf.Clamp(pos.z, -limit.y, limit.y);

        transform.position = pos;
    }
}
