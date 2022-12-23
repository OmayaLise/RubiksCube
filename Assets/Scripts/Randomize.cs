using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Randomize : MonoBehaviour
{
    Slider slider;
    FaceSelection faceSelection;
    RotateFace rotateFace;
    ChooseCubeDimension chooseCubeDimension;

    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("SliderMix").GetComponent<Slider>();
        faceSelection = GetComponent<FaceSelection>();
        rotateFace = GetComponent<RotateFace>();
        chooseCubeDimension = GetComponent<ChooseCubeDimension>();
    }

    public IEnumerator MixCube()
    {
        for(int i = 0; i < slider.value - 1; i++)
        {
            // Select face
            faceSelection.CubePosition = GetRandomPositionCube();
            faceSelection.FaceNormal = GetRandomNormal();
            faceSelection.SelectFace();

            // Rotate face
            rotateFace.Rotate();


            yield return new WaitWhile(() => rotateFace.isSlerping);

            // DeselectFace
            faceSelection.DeselectFace();
        }
    }

    Vector3 GetRandomPositionCube()
    {
        return chooseCubeDimension.cubes[Random.Range(0, chooseCubeDimension.cubes.Count)].transform.position;
    }

    Vector3 GetRandomNormal()
    {
        int normalType = Random.Range(0, 5);
        Vector3 normal = new Vector3(1, 0, 0);
        switch(normalType)
        {
            case 0:
                normal = new Vector3(1, 0, 0);
                break;

            case 1:
                normal = new Vector3(0, 1, 0);
                break;

            case 2:
                normal = new Vector3(0, 0, 1);
                break;

            case 3:
                normal = new Vector3(-1, 0, 0);
                break;

            case 4:
                normal = new Vector3(0, -1, 0);
                break;

            case 5:
                normal = new Vector3(0, 0, -1);
                break;

            default:
                break;
        }
        return normal;
    }
}
