using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateFace : MonoBehaviour
{
    private FaceSelection faceSelection;
    private MoveFace rayInfoScript;
    private Transform face;
    private float interpolateTimeParameter;
    private Quaternion startingQuaternion;
    private Quaternion targetQuaternion;
    public bool isSlerping;

    [Range(1f, 600f)]
    public float reversedSpeed;

    // Start is called before the first frame update
    void Start()
    {
        isSlerping = false;
        face = transform.Find("Face");
        faceSelection = GetComponent<FaceSelection>();
        rayInfoScript = GetComponent<MoveFace>();
    }

    public void Update()
    {
        if(isSlerping)
        {
            face.rotation = Quaternion.Slerp(startingQuaternion, targetQuaternion, interpolateTimeParameter);
            interpolateTimeParameter += 1f / reversedSpeed;
            if(Quaternion.Angle(face.rotation, targetQuaternion) < 1f)
            {
                face.rotation = targetQuaternion;
                isSlerping = false;
                faceSelection.DeselectFace();
            }
            return;
        }
        if(faceSelection.IsFaceSet)
        {
            if (rayInfoScript.rotationInfo.gameObjectClicked && rayInfoScript.rotationInfo.gameObjectReleased)
                Rotate();
        }
    }

    public void Rotate()
    {
        startingQuaternion = face.rotation;
        targetQuaternion = Quaternion.AngleAxis(90, faceSelection.FaceNormal) * face.rotation;
        interpolateTimeParameter = 0f;
        isSlerping = true;
    }
}
