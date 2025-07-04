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
    [SerializeField] private Material gpuInstancingMaterial;

    private Wilson wilson = null;

    private List<GameObject> walls = new List<GameObject>();
    private List<GameObject> pillars = new List<GameObject>();

    private GameObject tmp;
    public Vector3 center { get; private set; }

    public bool useGPUInstancing = false;

    // used only for GPU instancing:
    private MaterialPropertyBlock wallPropertyBlock;
    private Mesh wallMesh;
    private MaterialPropertyBlock pillarPropertyBlock;
    private Mesh pillarMesh;
    private List<Matrix4x4> wallMatrices = new List<Matrix4x4>();
    private List<Matrix4x4> pillarMatrices = new List<Matrix4x4>();
    private List<Vector4> wallColors = new List<Vector4>();
    private List<Vector4> pillarColors = new List<Vector4>();
    private bool isGPUInstancingReady = false;

    private List<(int x, int y)> collectItems = new();

    public void Initialize()
    {
        int level = Preferences.GetLevel();
        int collectItemsCount;
        seed = level;
        if (seed <= 5)
        {
            Rows = 5;
            Columns = 5;
            collectItemsCount = 1;
        }
        else if (seed <= 10)
        {
            Rows = 8;
            Columns = 8;
            collectItemsCount = 2;
        }
        else
        {
            Rows = 11;
            Columns = 11;
            collectItemsCount = 3;
        }

        colorsGenerator.SetupBaseColor(level);
        itemsContainerPrefab.GetComponent<ItemsController>().Initialize(colorsGenerator.colorScheme);
        InitCollectItemsPositions(collectItemsCount);
    }

    // Initializes the positions of collectible items in the maze. The positions are randomly chosen, ensuring that they do not overlap with each other or the finish cell.
    private void InitCollectItemsPositions(int collectItemsCount)
    {
        for (int i = 0; i < collectItemsCount; i++)
        {
            int x, y;
            do
            {
                x = Random.Range(0, Columns);
                y = Random.Range(0, Rows);
            } while (collectItems.Contains((x, y)) || (x == Columns - 1 && y == Rows - 1)); // Avoid last cell
            collectItems.Add((x, y));
        }
    }

    public MazeColorScheme GetColorScheme()
    {
        return colorsGenerator.colorScheme;
    }

    public void SetupFloor(Transform floorCubeTransform)
    {
        Vector2 size = GetMazeSize();
        floorCubeTransform.localScale = new Vector3(size.x, 1, size.y);
        center = new Vector3(size.x / 2.0f, 0f, size.y / 2.0f);
        floorCubeTransform.position = new Vector3(center.x, -0.5f, center.z);
        SetupFloorColor(floorCubeTransform);
    }

    public Vector3 GetInitialPosition()
    {
        return new Vector3(CellWidth / 2.0f, 0, CellHeight / 2.0f);
    }

    public Vector2 GetMazeSize()
    {
        return new Vector2(Rows * (CellHeight + (AddGaps ? .2f : 0)), Columns * (CellWidth + (AddGaps ? .2f : 0)));
    }

    public void GenerateMaze()
    {
        wilson = new Wilson(Rows, Columns, seed);

        wilson.GenerateMaze();
        wilson.PrintPath();

        InstantiateGameObjectsInMaze();

        if (useGPUInstancing)
        {
            wallPropertyBlock = new MaterialPropertyBlock();
            wallPropertyBlock.SetVectorArray("_BaseColor", wallColors);
            wallMesh = Wall.GetComponent<MeshFilter>().sharedMesh;

            pillarPropertyBlock = new MaterialPropertyBlock();
            pillarPropertyBlock.SetVectorArray("_BaseColor", pillarColors);
            pillarMesh = Pillar.GetComponent<MeshFilter>().sharedMesh;

            isGPUInstancingReady = true;
        }
    }

    private void Update()
    {
        if (!useGPUInstancing || !isGPUInstancingReady)
            return;

        Graphics.DrawMeshInstanced(wallMesh, 0, gpuInstancingMaterial, wallMatrices.ToArray(), wallMatrices.Count, wallPropertyBlock);
        Graphics.DrawMeshInstanced(pillarMesh, 0, gpuInstancingMaterial, pillarMatrices.ToArray(), pillarMatrices.Count, pillarPropertyBlock);
    }

    private void InstantiateGameObjectsInMaze()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                Cell cell = wilson.GetCell(row, column);

                if (wilson.WallRight(cell))
                {
                    InstantiateWall(new Vector3(x + CellWidth, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 90, 0));// right
                }
                if (wilson.WallFront(cell))
                {
                    InstantiateWall(new Vector3(x + CellWidth / 2, 0, z + CellHeight) + Wall.transform.position, Quaternion.Euler(0, 0, 0));// front
                }
                if (wilson.WallLeft(cell))
                {
                    InstantiateWall(new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 270, 0));// left
                }
                if (wilson.WallBack(cell))
                {
                    InstantiateWall(new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 180, 0));// back
                }
                if (collectItems.Contains((row, column)) && itemsContainerPrefab != null && (row != Rows - 1 || column != Columns - 1))
                {
                    tmp = Instantiate(itemsContainerPrefab, new Vector3(x + CellWidth / 2, 1, z + CellHeight / 2), Quaternion.Euler(0, 0, 0));
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

                    InstantiatePillar(new Vector3(x, 0, z));
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
        Color color = colorsGenerator.GetNextColorForColorScheme();
        if (useGPUInstancing)
        {
            wallMatrices.Add(Matrix4x4.TRS(position, orientation, Vector3.one));
            wallColors.Add(color);
        }
        else
        {
            tmp = Instantiate(Wall, position, orientation);
            tmp.transform.parent = transform;
            walls.Add(tmp);
            MeshRenderer renderer = tmp.GetComponent<MeshRenderer>();
            renderer.material.color = color;
        }
    }

    private void InstantiatePillar(Vector3 position)
    {
        Color color = colorsGenerator.GetNextColorForColorScheme();
        if (useGPUInstancing)
        {
            pillarMatrices.Add(Matrix4x4.TRS(position, Quaternion.identity, Vector3.one));
            pillarColors.Add(color);
        }
        else
        {
            tmp = Instantiate(Pillar, position, Quaternion.identity);
            pillars.Add(tmp);
            tmp.transform.parent = transform;
            MeshRenderer renderer = tmp.GetComponent<MeshRenderer>();
            renderer.material.color = color;
        }
    }

    private void SetupFloorColor(Transform baseCubeTransform)
    {
        if (useGPUInstancing)
        {
            MaterialPropertyBlock props = new MaterialPropertyBlock();
            MeshRenderer renderer;

            Color color = colorsGenerator.GetFloorColor();
            props.SetColor(Constants.BASE_COLOR, color);

            renderer = baseCubeTransform.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
        else
        {
            MeshRenderer renderer;

            Color color = colorsGenerator.GetFloorColor();
            renderer = baseCubeTransform.GetComponent<MeshRenderer>();
            renderer.material.color = color;
        }
    }
}
