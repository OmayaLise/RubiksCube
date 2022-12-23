using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Camera cam;
    public float zoomSpeed;
    public Vector2 ClampViewValues;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            transform.position = transform.position + (transform.forward * zoomSpeed) * Time.deltaTime;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            transform.position = transform.position + (-transform.forward * zoomSpeed) * Time.deltaTime;

        if (transform.position.z > ClampViewValues.x)
            transform.position = new Vector3(0f, 1f, ClampViewValues.x);
        else if(transform.position.z < ClampViewValues.y)
            transform.position = new Vector3(0f, 1f, ClampViewValues.y);
    }
}
