using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GridSizing : MonoBehaviour
{
    [MenuItem("CONTEXT/GridLayoutGroup/Set Child Size Stretched")]
    private static void SetFixedGridSizing()
    {
        GridLayoutGroup grid = (Selection.activeObject as GameObject)?.GetComponent<GridLayoutGroup>();
        if (grid.constraint == GridLayoutGroup.Constraint.Flexible)
        {
            print("Grid Layout Group constraint cannot be flexible");
            return;
        }
        Undo.RecordObject (grid, "Modify Fixed Sizing");
        grid.GridChildResizing();
    }
}