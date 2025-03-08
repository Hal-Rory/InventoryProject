using UnityEngine;
using UnityEngine.UI;

public static class GridLayoutGroupExtensions
{
    public static void GridChildResizing(this GridLayoutGroup grid)
    {
        Vector2 size = grid.cellSize;
        RectTransform trans = grid.transform as RectTransform;
        Vector2 transSize = trans.rect.size;
        float columns = Mathf.Ceil((float)trans.childCount / grid.constraintCount);
        float constraint = Mathf.Min(grid.constraintCount, trans.childCount);
        switch (grid.constraint)
        {
            case GridLayoutGroup.Constraint.FixedColumnCount:
                transSize.x -= grid.spacing.x * (constraint - 1);
                transSize.y -= grid.spacing.y * (columns - 1);
                size = new Vector2(
                    transSize.x / constraint,
                    transSize.y / columns);
                break;
            case GridLayoutGroup.Constraint.FixedRowCount:
                transSize.x -= grid.spacing.x * (columns - 1);
                transSize.y -= grid.spacing.y * (constraint - 1);
                size = new Vector2(
                    transSize.x / columns,
                    transSize.y / constraint);
                break;
        }
        if (grid.padding.left > 0)
        {
            size.x -= grid.padding.left / constraint;
        }
        if (grid.padding.top > 0)
        {
            size.y = (trans.rect.size.y - grid.padding.top) / columns;
        }
        grid.cellSize = size;
    }
}