using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSelection : MonoBehaviour
{
    public Transform Face;
    public bool IsFaceSet;
    public bool IsNormalSet;
    public Vector3 FaceNormal;
    public Vector3 CubePosition;

    private MoveFace cubeSelection;
    private ChooseCubeDimension dimensionScript;

    // Start is called before the first frame update
    void Start()
    {
        Face = transform.Find("Face");
        cubeSelection = GetComponent<MoveFace>();
        dimensionScript = GetComponent<ChooseCubeDimension>();
        IsFaceSet = false;
        IsNormalSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cubeSelection.rotationInfo.gameObjectClicked || !cubeSelection.rotationInfo.gameObjectReleased || IsFaceSet)
            return;

        ChooseCubeAndNormal(cubeSelection.rotationInfo.normalHit, cubeSelection.rotationInfo.gameObjectClicked.transform.position, cubeSelection.rotationInfo.gameObjectReleased.transform.position);
        if(IsNormalSet)
            SelectFace();
    }

    public void ChooseCubeAndNormal(Vector3 p_cubeHitNormal, Vector3 p_cubeClickedPosition, Vector3 p_cubeReleasedPosition)
    {
        FaceNormal = Vector3.Cross(p_cubeHitNormal, p_cubeReleasedPosition - p_cubeClickedPosition).normalized;

        float dotNormalUp = Vector3.Dot(FaceNormal, transform.up),
              dotNormalRight = Vector3.Dot(FaceNormal, transform.right),
              dotNormalForward = Vector3.Dot(FaceNormal, transform.forward);

        bool isNormalUpPerpendicular = Mathf.Abs(dotNormalUp) < 0.1,
             isNormalRightPerpendicular = Mathf.Abs(dotNormalRight) < 0.1,
             isNormalForwardPerpendicular = Mathf.Abs(dotNormalForward) < 0.1;

        if (!((isNormalUpPerpendicular && isNormalRightPerpendicular)
            || (isNormalUpPerpendicular && isNormalForwardPerpendicular)
            || (isNormalRightPerpendicular && isNormalForwardPerpendicular)))
        {
            cubeSelection.ResetData();
            return;
        }

        FaceNormal = dotNormalUp > 0.1 ? transform.up
                   : dotNormalUp < -0.1 ? -transform.up
                   : dotNormalRight > 0.1 ? transform.right
                   : dotNormalRight < -0.1 ? -transform.right
                   : dotNormalForward > 0.1 ? transform.forward
                   : -transform.forward;

        CubePosition = p_cubeClickedPosition;
        IsNormalSet = true;
    }

    public void SelectFace()
    {
        Plane facePlane = new Plane(FaceNormal, CubePosition);
        for (int i = 0; i < dimensionScript.cubes.Count; ++i)
        {
            float distanceToPoint = facePlane.GetDistanceToPoint(dimensionScript.cubes[i].transform.position);
            if (Mathf.Abs(distanceToPoint) < 0.1)
                dimensionScript.cubes[i].transform.parent = Face;
        }
        IsFaceSet = true;
    }

    public void DeselectFace()
    {
        if (Face.childCount < 1)
            return;
        int childCount = Face.childCount;
        for (int i = 0; i < childCount; ++i)
            Face.GetChild(0).transform.SetParent(transform, true);
        IsFaceSet = false;
        IsNormalSet = false;
        cubeSelection.ResetData();
    }
}
