using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;

    public Transform pos;

    public int gridX;
    public int gridY;

    public bool start;
    public bool end;

    public int gCost;
    public int hCost;

    public Node parent;

    public Node(bool _walkable, int _gridX, int _gridY, Transform pos_)
    {
        walkable = _walkable;
        gridX = _gridX;
        gridY = _gridY;
        pos = pos_;
    }

    public int GetX
    {
        get
        {
            return gridX;
        }
    }

    public int GetY
    {
        get
        {
            return gridY;
        }
    }

    public Vector3 GetPos(float Y,float cellcise)
    {

        float X = gridX * cellcise;
        float Z = gridY * cellcise;
            return new Vector3(gridX, Y, gridY);
        
    }

 


    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public bool ChangeNode
    {
        set
        {
            walkable = value;
        }
    }






  

}
