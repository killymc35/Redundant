using System.Drawing;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridHandler : MonoBehaviour
{
    #region Dependencies
    public LayerMask gridLayerMask;
    [SerializeField] GameObject homeBlock;
    [SerializeField] GameObject validIndicator;
    #endregion
    #region Public Fields
    public Vector2Int gridSize;
    public GameObject[,] gridBlocks;
    #endregion
    #region Private Variables

    private GameObject blockPrefab;
    private GameObject phantomBlock;
    private PhantomBlockController phantomController;
    
    private Camera mainCamera;
    private Grid grid;

    private Ray ray;
    private RaycastHit hit;
    #endregion

    void Awake()
    {
        mainCamera = Camera.main;
        blockPrefab = null;
        gridBlocks = new GameObject[gridSize.x, gridSize.y];
    }

    void Start()
    {
        grid = GetComponentInChildren<Grid>();
        FirstGrid();
    }

    void Update()
    {
        if (blockPrefab != null)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
            if (Physics.Raycast(ray, out hit, 100f, gridLayerMask))
            {
                if (!phantomBlock.activeSelf) phantomBlock.SetActive(true);
                phantomBlock.transform.position = hit.point;

                PhantomBlockController phantomController = phantomBlock.GetComponent<PhantomBlockController>();
                Vector2Int coordinate = (Vector2Int)grid.WorldToCell(hit.point);
                CheckValidity(coordinate);
                if (Input.GetMouseButtonDown(0))
                {
                    if (phantomController.hasValidPlacement)
                    {
                        PlaceBlock(blockPrefab, coordinate);
                        Destroy(phantomBlock);
                        blockPrefab = null;
                        phantomBlock = null;
                    }
                }
            }
            else if (phantomBlock.activeSelf) phantomBlock.SetActive(false);
        }
    }

    public void SetBlockPrefab(GameObject prefab)
    {
        blockPrefab = prefab;
        PrepareBlock();
    }

    private void PrepareBlock()
    {
        if (phantomBlock) Destroy(phantomBlock);

        phantomBlock = Instantiate(blockPrefab, parent: this.transform);
        phantomBlock.SetActive(false);

        phantomController = phantomBlock.GetComponent<PhantomBlockController>();
        phantomController.SetValidity(true);
    }

    private bool CheckValidity(Vector2Int coordinate)
    {
        Vector2Int arrayCoord = CoordToArrayIndex(coordinate);
        if (CheckForAdjacentBlocks(arrayCoord) && gridBlocks[arrayCoord.x, arrayCoord.y] == null)
        {
            phantomController.SetValidity(true);
            return true;
        }
        else 
        {
            phantomController.SetValidity(false);
            return false;
        }
    }

    private bool CheckForAdjacentBlocks(Vector2Int arrayCoord)
    {
        if (arrayCoord.x + 1 < gridSize.x) if (gridBlocks[arrayCoord.x + 1, arrayCoord.y] != null) return true;
        if (arrayCoord.x - 1 >= 0) if (gridBlocks[arrayCoord.x - 1, arrayCoord.y] != null) return true;
        if (arrayCoord.y + 1 < gridSize.y) if (gridBlocks[arrayCoord.x, arrayCoord.y + 1] != null) return true;
        if (arrayCoord.y - 1 >= 0) if (gridBlocks[arrayCoord.x, arrayCoord.y - 1] != null) return true;
        return false;
    }

    public void PlaceBlock(GameObject blockPrefab, Vector2Int coordinate)
    {
        Vector3Int position = Vector3Int.zero;
        position.x = coordinate.x;
        position.y = coordinate.y;
        Vector2Int arrayCoord = CoordToArrayIndex(coordinate);
        if (gridBlocks[arrayCoord.x, arrayCoord.y] != null)
        {
            Destroy(gridBlocks[arrayCoord.x, arrayCoord.y]);
            gridBlocks[arrayCoord.x, arrayCoord.y] = null;
        }
        gridBlocks[arrayCoord.x, arrayCoord.y] = Instantiate(blockPrefab, grid.GetCellCenterWorld(position), Quaternion.identity, parent: this.transform);
        validPlaceholders(position);
    }

    private void validPlaceholders (Vector3Int position)
    {
        Vector2Int arrayCoord = CoordToArrayIndex((Vector2Int)position);
        if (arrayCoord.x + 1 < gridSize.x) if (gridBlocks[arrayCoord.x + 1, arrayCoord.y] == null) gridBlocks[arrayCoord.x, arrayCoord.y] = Instantiate(validIndicator, grid.GetCellCenterWorld(position + Vector3Int.right), Quaternion.identity, parent: this.transform);
        if (arrayCoord.x - 1 >= 0) if (gridBlocks[arrayCoord.x - 1, arrayCoord.y] == null) gridBlocks[arrayCoord.x, arrayCoord.y] = Instantiate(validIndicator, grid.GetCellCenterWorld(position + Vector3Int.left), Quaternion.identity, parent: this.transform);
        if (arrayCoord.y + 1 < gridSize.y) if (gridBlocks[arrayCoord.x, arrayCoord.y + 1] == null) gridBlocks[arrayCoord.x, arrayCoord.y] = Instantiate(validIndicator, grid.GetCellCenterWorld(position + Vector3Int.up), Quaternion.identity, parent: this.transform);
        if (arrayCoord.y - 1 >= 0) if (gridBlocks[arrayCoord.x, arrayCoord.y - 1] == null) gridBlocks[arrayCoord.x, arrayCoord.y] = Instantiate(validIndicator, grid.GetCellCenterWorld(position + Vector3Int.down), Quaternion.identity, parent: this.transform);
    }

    private void FirstGrid()
    {
        PlaceBlock(homeBlock, Vector2Int.zero);
    }

    private Vector2Int CoordToArrayIndex(Vector2Int coordinate)
    {
        Vector2Int index = coordinate;
        index += gridSize / 2;
        return index;
    }
}
