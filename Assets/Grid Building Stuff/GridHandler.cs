using UnityEngine;

public class GridHandler : MonoBehaviour
{
    #region Dependencies
    [SerializeField] Grid grid;
    [SerializeField] GameObject homeBlock;
    #endregion
    #region Public Fields
    #endregion
    #region Private Variables
    enum BlockType : byte { Empty, Home }
    #endregion
    #region Start
    void Start()
    {
        CreateBlock(BlockType.Home, Vector2Int.zero);
        //CreateBlock(BlockType.Home, new Vector2Int(1,2));
    }
    #endregion
    #region Create Block
    private void CreateBlock(BlockType blockType, Vector2Int position)
    {
        Vector3Int blockPosition = Vector3Int.zero;
        blockPosition.x = position.x;
        blockPosition.y = position.y;
        Instantiate(homeBlock, grid.GetCellCenterWorld(blockPosition), Quaternion.identity, parent: this.transform);
    }
    #endregion
}
