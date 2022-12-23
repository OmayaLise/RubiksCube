using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public float sensitivity;
    float rotationX;
    float rotationY;
    public Camera cam;
    private FaceSelection faceSelectionScript;

    // Start is called before the first frame update
    void Start()
    {
        faceSelectionScript = GetComponent<FaceSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (faceSelectionScript.IsFaceSet)
            return;
        if (Input.GetMouseButton(1))
        {
            rotationX = Input.GetAxis("Mouse X") * sensitivity;
            rotationY = Input.GetAxis("Mouse Y") * sensitivity;

            Vector3 right = Vector3.Cross(cam.transform.up, transform.position - cam.transform.position);
            Vector3 up = Vector3.Cross( transform.position - cam.transform.position, right);

            transform.rotation = Quaternion.AngleAxis(-rotationX, up)* transform.rotation;
            transform.rotation = Quaternion.AngleAxis(rotationY, right) * transform.rotation;

        }
    }
}
