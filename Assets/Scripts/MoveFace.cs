using UnityEngine;

public struct RotationInfo
{
    public GameObject gameObjectClicked { get; set; }
    public GameObject gameObjectReleased { get; set; }
    public Vector3 normalHit { get; set; }

    public RotationInfo(GameObject p_gameObjectClicked, GameObject p_gameObjectReleased, Vector3 p_normal)
    {
        gameObjectClicked   = p_gameObjectClicked;
        gameObjectReleased  = p_gameObjectReleased;
        normalHit           = p_normal;
    }
};

public class MoveFace : MonoBehaviour
{
    public RotationInfo rotationInfo;

    private void Start()
    {
        ResetData();
    }

    // Update is called once per frame
    void Update()
    {
        SetGameObjectClicked(ref rotationInfo);
        SetGameObjectReleased(ref rotationInfo);
    }

    void SetGameObjectClicked(ref RotationInfo rotationInfo)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (rotationInfo.gameObjectClicked)
                return;

            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                rotationInfo.gameObjectClicked = raycastHit.transform.gameObject;
                rotationInfo.normalHit = raycastHit.normal;
            }
        }
    }

    void SetGameObjectReleased(ref RotationInfo rotationInfo)
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.transform.gameObject == rotationInfo.gameObjectClicked)
                {
                    rotationInfo.gameObjectClicked = null;
                    rotationInfo.gameObjectReleased = null;
                    return;
                }
                rotationInfo.gameObjectReleased = raycastHit.transform.gameObject;
            }
        }
    }

    public void ResetData()
    {
        rotationInfo.gameObjectClicked = null;
        rotationInfo.gameObjectReleased = null;
    }
}
