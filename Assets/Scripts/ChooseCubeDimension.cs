using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCubeDimension : MonoBehaviour
{
    public GameObject cube;
    [Range(2, 10)]
    public int size = 3;

    // Solve
    public bool isSolved = false;
    private Dictionary<string, Vector3> solvedCubes;
    public TextMeshProUGUI isSolvedText;

    public List<GameObject> cubes;
    private float volumeSliderGet;
    private bool toReload;
    GameObject button;
    Restart isReloaded;
    FaceSelection faceTurned;
    Randomize randomize;

    // Start is called before the first frame update
    void Start()
    {
        solvedCubes = new Dictionary<string, Vector3>();

        CreateCube();
        SetSolvedCubes();
        button = GameObject.Find("Restart");
        isReloaded = button.GetComponent<Restart>();
        faceTurned = GetComponent<FaceSelection>();
        randomize = GetComponent<Randomize>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloaded.isReloaded && !faceTurned.IsFaceSet)
        {
            volumeSliderGet = GameObject.Find("SliderSize").GetComponent<Slider>().value;
            size = (int)volumeSliderGet;
            transform.rotation = Quaternion.identity;
            CreateCube();
            SetSolvedCubes();

            StartCoroutine(randomize.MixCube());
            isReloaded.isReloaded = !isReloaded.isReloaded;
        }
        isSolved = CheckSolved();

        isSolvedText.enabled = isSolved;
    }

    private void CreateCube()
    {
        foreach (GameObject cube in cubes)
            Destroy(cube);
        cubes.Clear();
        int cubeNumber = 0;
        int min = -size / 2;
        int max = size / 2 + size % 2;
        for (int i = min; i < max; i++)
        {
            for (int j = min; j < max; j++)
            {
                for (int k = min; k < max; k++)
                {
                    if ((i != min || i != max - 1) && ((j == min || j == max - 1) || (k == min || k == max - 1)) ||
                       (j != min || j != max - 1) && ((k == min || k == max - 1) || (i == min || i == max - 1)) ||
                       (k != min || k != max - 1) && ((i == min || i == max - 1) || (j == min || j == max - 1)))
                    {
                        GameObject currCube = Instantiate(cube, new Vector3(i * 2.0f + (1 - size%2), j * 2.0f + (1 - size % 2), k * 2.0f + (1 - size % 2)), Quaternion.identity, transform);
                        currCube.name = "Cube" + cubeNumber;
                        ++cubeNumber;
                        cubes.Add(currCube);
                    }

                }
            }
        }
    }

    void SetSolvedCubes()
    {
        solvedCubes.Clear();
        foreach (GameObject cube in cubes)
            solvedCubes.Add(cube.name, cube.transform.position);
    }

    bool CheckSolved()
    {
        Quaternion rotationRubicsCube = gameObject.transform.rotation;
        gameObject.transform.rotation = Quaternion.identity;

        for(int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i].transform.position != solvedCubes[cubes[i].name])
            {
                gameObject.transform.rotation = rotationRubicsCube;
                return false;
            }
        }

        gameObject.transform.rotation = rotationRubicsCube;
        return true;
    }
}
