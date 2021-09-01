using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid 
{
    public int width, height;
    public float cellSize;
    public Vector3 offset;

    private int[,] gridArray;
    public MyGrid(int w, int h,float cs,Vector3 o)
    {
        width = w;
        height = h;
        cellSize = cs;
        offset = o;

        gridArray = new int[width, height];
        Debug.Log("Grid Size: " + width + "," + height + " ");

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(0); j++)
            {
                Debug.Log(i +"," + j);
                Debug.DrawLine(WorldPosition(i, j), WorldPosition(i, j + 1), Color.red, 100f);
                Debug.DrawLine(WorldPosition(i, j), WorldPosition(i + 1 , j), Color.red, 100f);
            }

            Debug.DrawLine(WorldPosition(0, height), WorldPosition(width,height), Color.red, 100f);
            Debug.DrawLine(WorldPosition(width, 0), WorldPosition(width,height ), Color.red, 100f);
        }
    }

    Vector3 WorldPosition(int x, int z)
    {
        return new Vector3(x,0,z) + offset * cellSize;
    }
}
