using UnityEngine;
using UnityEngine.EventSystems;

public class GridHandler : MonoBehaviour 
{
    #region Dependencies
    [SerializeField] Grid grid;
    [SerializeField] GameObject homeBlock;
    #endregion
    #region Public Fields
    public RectTransform targetRect;
    #endregion
    #region Private Variables
    enum BlockType : byte { Empty, Home }
    #endregion
    #region Start
    void Start()
    {
        CreateBlock(BlockType.Home, Vector3Int.zero);
        //CreateBlock(BlockType.Home, new Vector3Int(1,2,0));
    }
    #endregion
    #region Update
    void Update()
    {
    }
    #endregion
    #region Create Block
    private void CreateBlock(BlockType blockType, Vector3Int position)
    {
        Instantiate(homeBlock, grid.GetCellCenterWorld(position), Quaternion.identity, parent: this.transform);
    }
    #endregion
}
