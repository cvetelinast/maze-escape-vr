using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

    [SerializeField] private int seed = 1;
    [SerializeField] private GameObject Floor = null;
    [SerializeField] private GameObject Wall = null;
    [SerializeField] private GameObject Pillar = null;
    [SerializeField] private int Rows = 5;
    [SerializeField] private int Columns = 5;
    [SerializeField] private float CellWidth = 5;
    [SerializeField] private float CellHeight = 5;
    [SerializeField] private bool AddGaps = true;
    [SerializeField] private GameObject GoalPrefab = null;
    [SerializeField] private GameObject FinishPrefab = null;

    [SerializeField] private ColorsGenerator colorsGenerator;

    private BasicMazeGenerator mMazeGenerator = null;

    private List<GameObject> walls = new List<GameObject>();
    private List<GameObject> pillars = new List<GameObject>();
    private List<GameObject> floors = new List<GameObject>();

    private GameObject tmp;

    void Start()
    {
        mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);

        mMazeGenerator.GenerateMaze();

        InstantiateGameObjectsInMaze();

        colorsGenerator.CalculateBaseColor();

        GPUInstancingWalls();
        GPUInstancingFloors();
        GPUInstancingPillars();
    }

    private void InstantiateGameObjectsInMaze()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);

                InstantiateFloor(new Vector3(x, 0, z), Quaternion.identity);

                if (cell.WallRight)
                {
                    InstantiateWall(new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0));// right
                }
                if (cell.WallFront)
                {
                    InstantiateWall(new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0));// front
                }
                if (cell.WallLeft)
                {
                    InstantiateWall(new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0));// left
                }
                if (cell.WallBack)
                {
                    InstantiateWall(new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0));// back
                }
                if (cell.IsGoal && GoalPrefab != null)
                {
                    tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0));
                    tmp.transform.parent = transform;
                }
            }
        }
        if (Pillar != null)
        {
            for (int row = 0; row < Rows + 1; row++)
            {
                for (int column = 0; column < Columns + 1; column++)
                {
                    float x = column * (CellWidth + (AddGaps ? .2f : 0));
                    float z = row * (CellHeight + (AddGaps ? .2f : 0));

                    InstantiatePillar(new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2));
                }
            }
        }

        int lastCellRow = Rows;
        int lastCellColumn = Columns;

        float xLast = lastCellRow * (CellWidth + (AddGaps ? .2f : 0));
        float zLast = lastCellColumn * (CellHeight + (AddGaps ? .2f : 0));

        GameObject finishGO = Instantiate(FinishPrefab, new Vector3(xLast - CellWidth, 0, zLast - CellHeight), Quaternion.identity) as GameObject;
        finishGO.transform.parent = transform;
    }

    private void InstantiateFloor(Vector3 position, Quaternion orientation)
    {
        tmp = Instantiate(Floor, position, orientation);
        floors.Add(tmp);
        tmp.transform.parent = transform;
    }

    private void InstantiateWall(Vector3 position, Quaternion orientation)
    {
        tmp = Instantiate(Wall, position, orientation);
        walls.Add(tmp);
        tmp.transform.parent = transform;
    }

    private void InstantiatePillar(Vector3 position)
    {
        tmp = Instantiate(Pillar, position, Quaternion.identity);
        pillars.Add(tmp);
        tmp.transform.parent = transform;
    }

    private void GPUInstancingWalls()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        foreach (GameObject obj in walls)
        {
            Color color = colorsGenerator.GetNextColorForColorScheme();
            props.SetColor("_BaseColor", color);

            renderer = obj.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
    }

    private void GPUInstancingFloors()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        foreach (GameObject obj in floors)
        {
            Color color = colorsGenerator.GetFloorColor();
            props.SetColor("_BaseColor", color);

            renderer = obj.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
    }

    private void GPUInstancingPillars()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        foreach (GameObject obj in pillars)
        {
            Color color = colorsGenerator.GetNextColorForColorScheme();
            props.SetColor("_BaseColor", color);

            renderer = obj.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
    }
}
