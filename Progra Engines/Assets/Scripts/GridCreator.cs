using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public float cellSize = 5;
    public Vector3 offset;

    MyGrid grid;
    void Start()
    {
        grid = new MyGrid(rows, columns,cellSize,offset);
    }
}
