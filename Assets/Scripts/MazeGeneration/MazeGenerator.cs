using System.Collections.Generic;
using UnityEngine;
using static ColorsGenerator;

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
    [SerializeField] private GameObject CoinPrefab = null;
    [SerializeField] private GameObject FinishPrefab = null;

    [SerializeField] private ColorsGenerator colorsGenerator;
    [SerializeField] private GameObject itemsContainerPrefab;

    private BasicMazeGenerator mMazeGenerator = null;

    private List<GameObject> walls = new List<GameObject>();
    private List<GameObject> pillars = new List<GameObject>();
    private List<GameObject> floors = new List<GameObject>();

    private GameObject tmp;
    public Vector3 center { get; private set; }

    public void Initialize()
    {
        int level = Preferences.GetLevel();
        seed = level;
        if (seed <= 5)
        {
            Rows = 5;
            Columns = 5;
        }
        else if (seed <= 10)
        {
            Rows = 8;
            Columns = 8;
        }
        else
        {
            Rows = 11;
            Columns = 11;
        }

        colorsGenerator.SetupBaseColor(level);
        itemsContainerPrefab.GetComponent<ItemsController>().Initialize(colorsGenerator.colorScheme);
    }

    public MazeColorScheme GetColorScheme()
    {
        return colorsGenerator.colorScheme;
    }

    public void SetupFloor(Transform floorCubeTransform)
    {
        float x = Columns * (CellWidth + (AddGaps ? .2f : 0));
        float z = Rows * (CellHeight + (AddGaps ? .2f : 0));

        floorCubeTransform.localScale = new Vector3(x, 1, z);
        center = new Vector3((x - CellWidth) / 2, 0f, (z - CellHeight) / 2);
        floorCubeTransform.position = new Vector3(center.x, -0.5f, center.z);
        GPUInstancingFloor(floorCubeTransform);
    }

    public float GetLargestDimension()
    {
        return Mathf.Max(Rows * CellHeight, Columns * CellWidth);
    }

    public void GenerateMaze()
    {
        mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns, false, seed);

        mMazeGenerator.GenerateMaze();

        InstantiateGameObjectsInMaze();

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
                if (cell.IsGoal && itemsContainerPrefab != null && (row != Rows - 1 || column != Columns - 1))
                {
                    tmp = Instantiate(itemsContainerPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0));
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
    }

    public void InitializeFinishGO()
    {
        int lastCellRow = Rows;
        int lastCellColumn = Columns;

        float xLast = lastCellRow * (CellWidth + (AddGaps ? .2f : 0));
        float zLast = lastCellColumn * (CellHeight + (AddGaps ? .2f : 0));

        Vector3 finishGOPosition = new Vector3(xLast - CellWidth, 1.0f, zLast - CellHeight);
        GameObject finishGO = Instantiate(FinishPrefab, finishGOPosition, Quaternion.identity) as GameObject;
        finishGO.transform.parent = transform;
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

    private void GPUInstancingFloor(Transform baseCubeTransform)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        Color color = colorsGenerator.GetFloorColor();
        props.SetColor(Constants.BASE_COLOR, color);

        renderer = baseCubeTransform.GetComponent<MeshRenderer>();
        renderer.SetPropertyBlock(props);
    }

    private void GPUInstancingWalls()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;

        foreach (GameObject obj in walls)
        {
            Color color = colorsGenerator.GetNextColorForColorScheme();
            props.SetColor(Constants.BASE_COLOR, color);

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
            props.SetColor(Constants.BASE_COLOR, color);

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
            props.SetColor(Constants.BASE_COLOR, color);

            renderer = obj.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
    }
}
