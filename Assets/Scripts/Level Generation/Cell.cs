using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public int xPosition;
    public int yPosition;
    public string nodeType;
    public bool assigned;
    public int numberofConnections;
    public int numberofDestinations;
    public int maxNumberofConnections;
    public int maxNumberofDestinations;
    public int numberOfTries;
    public float cellRotation;
    public Cell pathOriginCell;
    public Cell OriginCell;
    public Cell self;
    public Cell currentCellDestination;

    private enum CellTypes { Spawner, Objective, Node, Empty };
    private CellTypes celltype;

    public GameObject pathprefab;
    public string name;

    int numberOfPaths;
    public bool isWalkable = true;
    public Cell myParentCell = null;

    public int myGCost = 0;
    public int myHCost = 0;
    public int myFCost { get { return myGCost + myHCost; } }

    public List<Cell> cellDestinations;
    public List<Cell> cellConnections;

    public List<Cell> AIDestinations;
    public List<GameObject> AIGameobjectDestinations;

    Cell connectedUpCell;
    Cell connectedRightCell;
    Cell connectedDownCell;
    Cell connectedLeftCell;



    public Cell(int setxPosition, int setyPosition, string setnodetype)
    {
        xPosition = setxPosition;
        yPosition = setyPosition;
        nodeType = setnodetype;
        cellDestinations = new List<Cell>();
        cellConnections = new List<Cell>();
        AIDestinations = new List<Cell>();
        numberofConnections = 0;
}

    public void ConnectCells(Cell cell, Cell[,] level)
    {
        List<Cell> adjecentCells = new List<Cell>();
        for (int dx = (cell.xPosition > 0 ? -1 : 0); dx <= (cell.xPosition < level.GetLength(0) - 1 ? 1 : 0); ++dx)
        {
            for (int dy = (cell.yPosition > 0 ? -1 : 0); dy <= (cell.yPosition < level.GetLength(1) - 1 ? 1 : 0); ++dy)
            {
                if (dx != 0 || dy != 0)
                {
                    adjecentCells.Add(level[cell.xPosition + dx, cell.yPosition + dy]);
                }
            }
        }
        foreach (var item in adjecentCells)
        {
            if (item.xPosition == xPosition + 1)
            {
                connectedRightCell = item;
            }
            if (item.xPosition == xPosition - 1)
            {
                connectedLeftCell = item;
            }
            if (item.xPosition == yPosition + 1)
            {
                connectedDownCell = item;
            }
            if (item.xPosition == xPosition - 1)
            {
                connectedDownCell = item;
            }
        }
    }
}
