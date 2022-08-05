using Sirenix.OdinInspector;
using UnityEngine;


public enum State
{
    None,
    Open,
    Create,
    ReCreate,
    Import,
    Export,
    Settings,
}

public enum Property
{
    Editor,
    Settings,
    Brush,
    Grid,
    Button,
    CellList

}

public enum EditorMode
{
    None,
    Model,
    Equipment,
    Property,
    Settings,
}

[System.Flags]
public enum LabelMode
{
    None,
    Grid,
    Coordinate,
    HexCoordinate,
}


[System.Flags]
public enum UpdateType
{
    None,
    ReCreate,
    Brush,
    Grid,
}


public enum BrushType
{
    None = 0,
    Terrain = 1 << 1,
    Elevation = 1 << 2,
    River = 1 << 3,
    Road = 1 << 4,
    Water = 1 << 5,
    Feature = 1 << 6,
    Special = 1 << 9,
}

