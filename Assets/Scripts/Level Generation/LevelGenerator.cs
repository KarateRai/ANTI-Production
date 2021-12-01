using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    [Header("Level Varibles")]
    public int levelWidth;
    public int levelheight;
    public int cellSize;
    public int tileSize;
    public int maxNumberOfObsitcles;
    public int maxNumberOfTries;
    public int maxPathLength;
    public bool forbidDiagonals;

    [Header("Objective Varibles")]
    public int numberOfObjectivesNodes;
    public int pathObjectiveConvergence;

    [Header("AI Spawn Varibles")]
    public int numberOfAiSpawnNodes;
    public int maxAIPathLength;
    public int pathAiDivergence;

    [Header("Node Varibles")]
    public int numberOfNodes;
    public int pathConvergence;
    public int pathDivergence;

    [Header("Prefabs")]
    public GameObject aiSpawnCellPrefab;
    public GameObject objectiveCellPrefab;
    public GameObject nodeCellPrefab;
    public GameObject[] pathCellsPrefab;
    public GameObject floorCellPrefab;
    public GameObject nonWalkablefloorCellPrefab;
    public GameObject mapborder;

    Cell[,] level;
    Cell objectiveNode;
    Cell AispawnpointCell;
    List<Cell> CellList = new List<Cell>();
    List<Cell> nonPossibleCellsList = new List<Cell>();
    List<Cell> nodeList = new List<Cell>();
    List<Cell> nodeToNodeList = new List<Cell>();
    List<Cell> destinationList = new List<Cell>();

    List<Cell> objectiveNodeList = new List<Cell>();
    List<Cell> AiSpawnNodeList = new List<Cell>();
    List<Cell> AiPathCells = new List<Cell>();
    List<List<Cell>> listOfPaths = new List<List<Cell>>();
    List<GameObject> listofReturnNodes = new List<GameObject>();

    List<GameObject> GeneratedLevel;
    List<GameObject> GeneratedNodes;


    private int nPathFaliure;
    private bool levelFailure;
    private int nFails;

    int temp;

    public List<GameObject> GenerateNewLevel(Dictionary<string, int> levelInformation)
    {
        numberOfObjectivesNodes = levelInformation["Objectives"];
        numberOfAiSpawnNodes = levelInformation["Ai-spawner"];
        numberOfNodes = levelInformation["Connections"];

        levelFailure = false;

        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        while (true)
        {
            if (GeneratedLevel != null)
            {
                if (GeneratedLevel.Count != 0)
                {
                    for (int i = 0; i < GeneratedLevel.Count; i++)
                    {
                        Destroy(GeneratedLevel[i].gameObject);
                    }
                }
                ClearLists();
                GeneratedLevel.Clear();
            }
            if (stopWatch.ElapsedMilliseconds >= 15000)
            {
                Debug.Log("TIMED OUT LEVEL GENERATION");
                ClearLists();
                //break;
                return GenerateNewLevel(levelInformation);
            }
            if (nFails >= 50)
            {
                Debug.Log("MAX NUMBER OF TRIES LEVEL GENERATION");
                //break;
                return GenerateNewLevel(levelInformation);
            }
            GenerateCells();
            ForbidBorderCells();
            GenerateObjectiveNode();
            GenerateAISpawnNode();
            GenerateNodes();
            GenerateObsticle();
            if (forbidDiagonals == true)
            {
                ForbidDiagonalCells();
            }
            nodeToNodeList.AddRange(nodeList);
            destinationList.AddRange(nodeList);
            destinationList.AddRange(objectiveNodeList);
            ConnectCells();
            DesignatePath();
            SetDestinations();
            if (Validate() == false)
            {
                ClearLists();
                nFails++;
            }
            else if (Validate() == true)
            {
                stopWatch.Stop();
                //Debug.Log("Time to generate: " + stopWatch.ElapsedMilliseconds);
                //Debug.Log("Level Sucess");
                nFails = 0;
                listofReturnNodes = BuildLevel();
                break;
                //SetDestinations();
            }
        }
        if (listofReturnNodes.Count == 0 || listofReturnNodes == null)
        {
            Debug.Log("SHIT'S FUCKED IN LEVEL GENERATION");
        }
        return listofReturnNodes;
    }

    private void SetDestinations()
    {
        List<Cell> allNodes = new List<Cell>();

        allNodes.AddRange(nodeList);
        allNodes.AddRange(AiSpawnNodeList);
        allNodes.AddRange(objectiveNodeList);

        foreach (var item in allNodes)
        {
            List<Cell> temporaryDestionations = new List<Cell>();
            List<Cell> temporaryConnections = new List<Cell>();

            temporaryDestionations.Clear();
            temporaryConnections.Clear();

            temporaryDestionations.AddRange(item.cellDestinations);
            temporaryConnections.AddRange(item.cellConnections);

            item.cellDestinations.Clear();
            item.cellConnections.Clear();


            for (int i = 0; i < temporaryConnections.Count; i++)
            {
                if (temporaryConnections[i].nodeType == "Node")
                {
                    item.AIDestinations.Add(temporaryConnections[i]);
                }
            }

            for (int i = 0; i < temporaryDestionations.Count; i++)
            {
                if (temporaryDestionations[i].nodeType == "Objective" || temporaryDestionations[i].nodeType == "Node")
                {
                    item.AIDestinations.Add(temporaryDestionations[i]);
                }
            }
        }
    }

    private void ClearLists()
    {
        level = null;
        CellList.Clear();
        nonPossibleCellsList.Clear();
        nodeList.Clear();
        nodeToNodeList.Clear();
        destinationList.Clear();
        objectiveNodeList.Clear();
        AiSpawnNodeList.Clear();
        AiPathCells.Clear();
    }
    private bool Validate()
    {
        foreach (var item in AiSpawnNodeList)
        {
            if (item.numberofDestinations == 0)
            {
                //Debug.Log("Level Faliure");
                return false;

            }
        }
        foreach (var item in nodeList)
        {

            if (item.numberofDestinations == 0 || item.numberofConnections == 0)
            {
                Debug.Log("Level Faliure");
                return false;

            }
        }
        foreach (var item in objectiveNodeList)
        {
            if (item.numberofConnections == 0)
            {
                Debug.Log("Level Faliure");
                return false;
            }
        }
        return true;
    }

    //Forbids cells adjecent to the nodes inorder to prevent paths adjecent to nodes.
    private void ForbidBorderCells()
    {
        List<Cell> cellsToRemoveFromCellList = new List<Cell>();
        foreach (var item in CellList)
        {
            if (item.yPosition == 0 || item.yPosition == levelheight - 1)
            {
                cellsToRemoveFromCellList.Add(item);
            }
            if (item.xPosition == 0 ||item.xPosition == levelWidth - 1)
            {
                cellsToRemoveFromCellList.Add(item);
            }
        }
        foreach (var item in cellsToRemoveFromCellList)
        {
            if (CellList.Contains(item))
            {
                CellList.Remove(item);
            }
        }
    }

    //Generates cells that paths cannot go over.
    private void GenerateObsticle()
    {
        for (int i = 0; i < maxNumberOfObsitcles; i++)
        {
            if (CellList.Count > 0)
            {
                Cell newObsticle = new Cell(0, 0, "Obsticle");
                newObsticle = CellList[Random.Range(0, CellList.Count)];
                newObsticle.isWalkable = false;
                level[newObsticle.xPosition, newObsticle.xPosition].nodeType = "Obsticle";
            }
        }

    }

    //Generates the level by creating all the cells in the level.
    void GenerateCells()
    {
        level = new Cell[levelWidth, levelheight];

        Cell emptyCell;

        for (int i = 0; i < levelheight; i++)
        {
            for (int ii = 0; ii < levelWidth; ii++)
            {
                emptyCell = new Cell(ii, i, "empty");
                emptyCell.isWalkable = true;
                emptyCell.assigned = false;
                level[ii, i] = emptyCell;
                CellList.Add(emptyCell);
            }
        }
    }
    void GenerateObjectiveNode()
    {
        for (int i = 0; i < numberOfObjectivesNodes; i++)
        {
            if (CellList.Count <= 0)
            {
                Debug.LogWarning("No possible Value");
            }
            Cell objectNode = new Cell(0, 0, "Objective");

            Cell setNode = CellList[Random.Range(0, CellList.Count)];

            //Sets the value of the node and position.
            objectNode.xPosition = setNode.xPosition;
            objectNode.yPosition = setNode.yPosition;
            objectNode.nodeType = "Objective";
            objectNode.isWalkable = false;
            objectNode.maxNumberofConnections = pathObjectiveConvergence;
            level[objectNode.xPosition, objectNode.yPosition] = objectNode;
            level[objectNode.xPosition, objectNode.yPosition].assigned = true;
            CellList.Remove(setNode);
            CellList = GetAllPossibleCells(objectNode);
            objectiveNodeList.Add(objectNode);
        }
    }

    void GenerateAISpawnNode()
    {
        for (int i = 0; i < numberOfAiSpawnNodes; i++)
        {
            if (CellList.Count <= 0)
            {
                Debug.LogWarning("No possible Value");
            }
            Cell aiNode = new Cell(0, 0, "Ai-Spawnpoint");

            Cell setNode = CellList[Random.Range(0, CellList.Count)];

            aiNode.xPosition = setNode.xPosition;
            aiNode.yPosition = setNode.yPosition;
            aiNode.nodeType = "Ai-Spawnpoint";
            aiNode.isWalkable = false;
            aiNode.maxNumberofDestinations = pathAiDivergence;
            level[aiNode.xPosition, aiNode.yPosition] = aiNode;
            level[aiNode.xPosition, aiNode.yPosition].assigned = true;
            CellList.Remove(setNode);
            CellList = GetAllPossibleCells(aiNode);
            AiSpawnNodeList.Add(aiNode);
        }
    }


    void GenerateNodes()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            if (CellList.Count <= 0)
            {
                Debug.LogWarning("No possible Value");
            }
            Cell newNode = new Cell(0, 0, "Node");

            Cell setNode = CellList[Random.Range(0, CellList.Count)];
            newNode.maxNumberofConnections = pathConvergence;
            newNode.maxNumberofDestinations = pathDivergence;
            newNode.xPosition = setNode.xPosition;
            newNode.yPosition = setNode.yPosition;
            newNode.isWalkable = false;
            level[newNode.xPosition, newNode.yPosition] = newNode;
            level[newNode.xPosition, newNode.yPosition].assigned = true;

            CellList.Remove(setNode);
            CellList = GetAllPossibleCells(newNode);
            nodeList.Add(newNode);
            //Debug.Log(newNode.xPosition + "," + newNode.yPosition);
        }
    }


    List<Cell> GetAllPossibleCells(Cell inCell)
    {
        //Gets all Cells adjecent to the inCell
        for (int dx = (inCell.xPosition > 0 ? -1 : 0); dx <= (inCell.xPosition < levelWidth - 1 ? 1 : 0); ++dx)
        {
            for (int dy = (inCell.yPosition > 0 ? -1 : 0); dy <= (inCell.yPosition < levelheight - 1 ? 1 : 0); ++dy)
            {
                if (dx != 0 || dy != 0)
                {
                    nonPossibleCellsList.Add(level[inCell.xPosition + dx, inCell.yPosition + dy]);
                }
            }
        }
        //Removes all the adjecent cells to the in Cell
        for (int i = 0; i < nonPossibleCellsList.Count; i++)
        {
            if (CellList.Contains(nonPossibleCellsList[i]))
            {
                CellList.Remove(nonPossibleCellsList[i]);
            }
        }

        return CellList;
    }

    //Destination finder for the cells.
    void FindDestinationNode(Cell inCell)
    {
        int tries = 0;
        List<Cell> unCheckedDestinationList = new List<Cell>();
        List<Cell> CheckeddestinationList = new List<Cell>();

        unCheckedDestinationList.Clear();
        CheckeddestinationList.Clear();
        unCheckedDestinationList.AddRange(destinationList);
        unCheckedDestinationList.Remove(inCell);

        if (inCell.cellConnections != null)
        {
            if (inCell.cellConnections.Count != 0)
            {
                if (inCell.cellConnections.Count == 1)
                {
                    unCheckedDestinationList.Remove(inCell.cellConnections[0]);
                }
                else
                {
                    for (int i = 0; i < inCell.cellConnections.Count; i++)
                    {
                        unCheckedDestinationList.Remove(inCell.cellConnections[i]);
                    }
                }
            }
        }


        //looks for destinations for as long as there are any destinations to check
        while (unCheckedDestinationList.Count > 0)
        {
            tries++;
            if (inCell.nodeType == "Ai-Spawnpoint")
            {
                if (unCheckedDestinationList.Count == 1)
                {
                    inCell.currentCellDestination = unCheckedDestinationList[0];
                    inCell.numberofDestinations++;
                }
                else if (nodeToNodeList.Count != 0)
                {
                    if (nodeToNodeList.Count > 1)
                    {
                        inCell.currentCellDestination = nodeToNodeList[Random.Range(0, nodeToNodeList.Count)];
                        inCell.numberofDestinations++;
                    }
                    else
                    {
                        inCell.currentCellDestination = nodeToNodeList[0];
                        inCell.numberofDestinations++;
                    }
                    
                }
            }

            else if (inCell.nodeType == "Node")
            {
                if (unCheckedDestinationList.Count > 1)
                {
                    inCell.currentCellDestination = unCheckedDestinationList[Random.Range(0, unCheckedDestinationList.Count)];
                    inCell.numberofDestinations++;
                }
                else
                {
                    inCell.currentCellDestination = unCheckedDestinationList[0];
                    inCell.numberofDestinations++;
                }
            }

            if (inCell.currentCellDestination != null)
            {
                bool pathFaliure = PathCalculator(inCell, inCell.currentCellDestination);
                if (pathFaliure == true)
                {
                    //Resets values incase of failure
                    inCell.currentCellDestination = null;
                    inCell.numberofDestinations--;

                    //Removes destionation from possible destination and adds it to a list of checked destinations
                    unCheckedDestinationList.Remove(inCell.currentCellDestination);
                    CheckeddestinationList.Add(inCell.currentCellDestination);
                }
                else
                {
                    //Sets values when success
                    inCell.cellDestinations.Add(inCell.currentCellDestination);
                    inCell.currentCellDestination.cellConnections.Add(inCell);
                    inCell.currentCellDestination.OriginCell = inCell;
                    inCell.currentCellDestination.numberofConnections++;

                    //removes destination from list to prevent multiple paths from the same node to the same destination
                    unCheckedDestinationList.Remove(inCell.currentCellDestination);
                    CheckeddestinationList.Add(inCell.currentCellDestination);

                    //if max number of allowed connections the destination gets removed from the list.
                    if (inCell.currentCellDestination.numberofConnections >= inCell.currentCellDestination.maxNumberofConnections)
                    {
                        destinationList.Remove(inCell.currentCellDestination);
                        if (nodeToNodeList.Contains(inCell.currentCellDestination))
                        {
                            nodeToNodeList.Remove(inCell.currentCellDestination);
                        }
                    }
                }
            }
            if (unCheckedDestinationList.Count <= 0 || inCell.numberofDestinations >= inCell.maxNumberofDestinations || tries == maxNumberOfTries)
            {
                break;
            }
        }

        if (inCell.cellDestinations.Count != 0)
        {
            for (int i = 0; i < inCell.cellDestinations.Count; i++)
            {
                if (inCell.cellDestinations[i].nodeType != "Objective")
                {
                    FindDestinationNode(inCell.cellDestinations[i]);
                }
            }
        }
    }


    bool PathCalculator(Cell inputCell, Cell destinationCell)
    {
        List<Cell> ListOfPath = new List<Cell>();

        ListOfPath = FindPath(inputCell, destinationCell);
        if (ListOfPath == null || ListOfPath.Count <= 0)
        {
            //Debug.Log("Path finding Failed");
            nPathFaliure++;
            return true;
        }
        for (int ii = 0; ii < ListOfPath.Count - 1; ii++)
        {
            ListOfPath[ii].isWalkable = false;
        }
        if (ListOfPath.Count >= maxPathLength)
        {
            ListOfPath.Clear();
            Debug.Log("Path To long");
            return true;
        }
        else
        {
            for (int i = 0; i < ListOfPath.Count; i++)
            {
                level[ListOfPath[i].xPosition, ListOfPath[i].yPosition].assigned = true;
            }
            ListOfPath[0].pathOriginCell = inputCell;
            listOfPaths.Add(ListOfPath);
            AiPathCells.AddRange(SetPathType(ListOfPath));
            AiPathCells.Remove(ListOfPath.Last());
        }

        ListOfPath.Clear();
        return false;
    }

    void DesignatePath()
    {
        foreach (var item in AiSpawnNodeList)
        {
            FindDestinationNode(item);
        }
    }

    List<GameObject> BuildLevel()
    {
        GeneratedLevel = new List<GameObject>();
        GeneratedNodes = new List<GameObject>();

        

        for (int i = 0; i < levelheight; i++)
        {
            for (int ii = 0; ii < levelWidth; ii++)
            {
                if (level[ii, i].assigned == false)
                {
                    GameObject cellToGenerate = Instantiate(floorCellPrefab, transform);
                    cellToGenerate.name = "FloorCell" + ii + "_" + i;
                    cellToGenerate.transform.localPosition = new Vector3((ii - 1) * cellSize, -1f, (i - 1) * cellSize);
                    GeneratedLevel.Add(cellToGenerate);
                }
                else
                {
                    GameObject cellToGenerate = Instantiate(nonWalkablefloorCellPrefab, transform);
                    cellToGenerate.name = "FloorCell" + ii + "_" + i;
                    cellToGenerate.transform.localPosition = new Vector3((ii - 1) * cellSize, 0f, (i - 1) * cellSize);
                    GeneratedLevel.Add(cellToGenerate);
                }
            }
        }

        for (int i = 0; i < AiSpawnNodeList.Count; i++)
        {
            GameObject cellToGenerate = Instantiate(aiSpawnCellPrefab, transform);
            cellToGenerate.name = "AiCell" + i;
            AiSpawnNodeList[i].name = cellToGenerate.name;
            cellToGenerate.transform.localPosition = new Vector3((AiSpawnNodeList[i].xPosition - 1) * cellSize, 1f, (AiSpawnNodeList[i].yPosition - 1) * cellSize);
            GeneratedLevel.Add(cellToGenerate);
            GeneratedNodes.Add(cellToGenerate);
        }
        for (int i = 0; i < objectiveNodeList.Count; i++)
        {
            GameObject cellToGenerate = Instantiate(objectiveCellPrefab, transform);
            cellToGenerate.name = "ObjectiveCell" + i;
            objectiveNodeList[i].name = cellToGenerate.name;
            cellToGenerate.transform.localPosition = new Vector3((objectiveNodeList[i].xPosition - 1) * cellSize, 1f, (objectiveNodeList[i].yPosition - 1) * cellSize);
            GeneratedLevel.Add(cellToGenerate);
            GeneratedNodes.Add(cellToGenerate);
        }
        for (int i = 0; i < nodeList.Count; i++)
        {
            GameObject cellToGenerate = Instantiate(nodeCellPrefab, transform);
            cellToGenerate.name = "NodeCell" + i + "xy: " +  nodeList[i].xPosition + "," + nodeList[i].yPosition;
            nodeList[i].name = cellToGenerate.name;
            cellToGenerate.transform.localPosition = new Vector3((nodeList[i].xPosition - 1) * cellSize, 1f, (nodeList[i].yPosition - 1) * cellSize);
            GeneratedLevel.Add(cellToGenerate);
            GeneratedNodes.Add(cellToGenerate);
        }
        for (int i = 0; i < AiPathCells.Count; i++)
        {
            GameObject cellToGenerate = Instantiate(AiPathCells[i].pathprefab, transform);
            cellToGenerate.name = "PathNode" + i;
            AiPathCells[i].name = cellToGenerate.name;
            cellToGenerate.transform.localPosition = new Vector3((AiPathCells[i].xPosition - 1) * cellSize, 1f, (AiPathCells[i].yPosition - 1) * cellSize);
            cellToGenerate.transform.Rotate(0, AiPathCells[i].cellRotation, 0);
            GeneratedLevel.Add(cellToGenerate);
            //GeneratedNodes.Add(cellToGenerate);
        }

        List<Cell> listOfOrigins = new List<Cell>();
        listOfOrigins.AddRange(nodeList);
        listOfOrigins.AddRange(AiSpawnNodeList);

        for (int i = 0; i < listOfOrigins.Count; i++)
        {
            for (int ii = 0; ii < listOfOrigins[i].AIDestinations.Count; ii++)
            {
                listOfOrigins[i].AIGameobjectDestinations.Add(GameObject.Find(listOfOrigins[i].AIDestinations[ii].name));
            }
        }

        

        for (int i = 0; i < GeneratedNodes.Count; i++)
        {
            for (int ii = 0; ii < listOfOrigins.Count; ii++)
            {
                if (GeneratedNodes[i].name == listOfOrigins[ii].name)
                {
                    GeneratedNodes[i].GetComponent<CellAction>().SetDestinationValues(listOfOrigins[ii].AIGameobjectDestinations);
                }
            }
        }

        return GeneratedNodes;
    }

    void ConnectCells()
    {
        for (int i = 0; i < level.GetLength(1); i++)
        {
            for (int ii = 0; ii < level.GetLength(0); ii++)
            {
                level[ii, i].ConnectCells(level[ii, i], level);
            }
        }
    }

    void ForbidDiagonalCells()
    {
        List<Cell> forbiddenCellList = new List<Cell>();
        //forbiddenCellList.AddRange(objectiveNodeList);
        //forbiddenCellList.AddRange(AiSpawnNodeList);
        forbiddenCellList.AddRange(nodeList);

        foreach (var item in forbiddenCellList)
        {
            List<Cell> forbidlist = new List<Cell>();
            forbidlist = GetAdjecentNavigatableCells(item);

            foreach (var itemToEval in forbidlist)
            {
                if (itemToEval.yPosition > item.yPosition && itemToEval.xPosition != item.xPosition)
                {
                    itemToEval.isWalkable = false;
                }
                if (itemToEval.yPosition < item.yPosition && itemToEval.xPosition != item.xPosition)
                {
                    itemToEval.isWalkable = false;
                }
            }
        }

    }

    List<Cell> SetPathType(List<Cell> inlist)
    {
        inlist.Insert(0, inlist[0].pathOriginCell);
        for (int i = 0; i < inlist.Count; i++)
        {
            if (i == inlist.Count - 1)
            {
                inlist.RemoveAt(0);
                return inlist;
            }
            if (i == 0)
            {
                continue;
            }
            else if (inlist[i + 1].xPosition > inlist[i].xPosition)
            {
                if (inlist[i - 1].yPosition > inlist[i].yPosition || inlist[i - 1].yPosition < inlist[i].yPosition)
                {
                    inlist[i].pathprefab = pathCellsPrefab[1];
                    if (inlist[i - 1].yPosition > inlist[i].yPosition)
                    {
                        inlist[i].cellRotation = 180;
                    }
                    else
                    {
                        inlist[i].cellRotation = 270;
                    }
                }
                else
                {
                    inlist[i].cellRotation = 90;
                    inlist[i].pathprefab = pathCellsPrefab[0];
                }
            }
            else if (inlist[i + 1].xPosition < inlist[i].xPosition)
            {

                if (inlist[i - 1].yPosition > inlist[i].yPosition || inlist[i - 1].yPosition < inlist[i].yPosition)
                {
                    inlist[i].pathprefab = pathCellsPrefab[1];
                    if (inlist[i - 1].yPosition > inlist[i].yPosition)
                    {
                        inlist[i].cellRotation = 90;
                    }
                    else
                    {
                        inlist[i].cellRotation = 0;
                    }
                }
                else
                {
                    inlist[i].cellRotation = 90;
                    inlist[i].pathprefab = pathCellsPrefab[0];
                }
            }
            else if (inlist[i + 1].yPosition > inlist[i].yPosition)
            {
                if (inlist[i - 1].xPosition > inlist[i].xPosition || inlist[i - 1].xPosition < inlist[i].xPosition)
                {
                    inlist[i].pathprefab = pathCellsPrefab[1];
                    if (inlist[i - 1].xPosition > inlist[i].xPosition)
                    {
                        inlist[i].cellRotation = 180;
                    }
                    else
                    {
                        inlist[i].cellRotation = 90;
                    }
                }
                else
                {
                    inlist[i].cellRotation = 0;
                    inlist[i].pathprefab = pathCellsPrefab[0];
                }
            }
            else if (inlist[i + 1].yPosition < inlist[i].yPosition)
            {
                if (inlist[i - 1].xPosition > inlist[i].xPosition || inlist[i - 1].xPosition < inlist[i].xPosition)
                {
                    inlist[i].pathprefab = pathCellsPrefab[1];
                    if (inlist[i - 1].xPosition > inlist[i].xPosition)
                    {
                        inlist[i].cellRotation = 270;
                    }
                    else
                    {
                        inlist[i].cellRotation = 0;
                    }
                }
                else
                {
                    inlist[i].cellRotation = 0;
                    inlist[i].pathprefab = pathCellsPrefab[0];
                }
            }
            else
            {
                Debug.Log("No Path Prefab Entry");
            }
        }
        inlist.RemoveAt(0);
        return inlist;
    }

    void AssignPath(List<Cell> listOfCellsToAssign)
    {
        for (int i = 0; i < listOfCellsToAssign.Count; i++)
        {
            listOfCellsToAssign[i].assigned = true;
        }
    }

    void CalculatePathway(Cell startingCell, Cell DestinationCell)
    {
        //startingCell.SetDistance(DestinationCell.xPosition, DestinationCell.yPosition);

        //List<Cell> NavigableCells = GetnavigatableCells();

    }

    List<Cell> GetAdjecentNavigatableCells(Cell currentCell)
    {
        List<Cell> NavigableCells = new List<Cell>();
        for (int dx = (currentCell.xPosition > 0 ? -1 : 0); dx <= (currentCell.xPosition < levelWidth - 1 ? 1 : 0); ++dx)
        {
            for (int dy = (currentCell.yPosition > 0 ? -1 : 0); dy <= (currentCell.yPosition < levelheight - 1 ? 1 : 0); ++dy)
            {
                if (dx != 0 || dy != 0)
                {
                    NavigableCells.Add(level[currentCell.xPosition + dx, currentCell.yPosition + dy]);
                }
            }
        }
        for (int i = 0; i < NavigableCells.Count; i++)
        {
            if (NavigableCells[i].assigned == true)
            {
                NavigableCells.Remove(NavigableCells[i]);
            }
        }

        return NavigableCells;
    }


    //A* Magic
    List<Cell> FindPath(Cell startCell, Cell goalCell)
    {
        goalCell.isWalkable = true;
        List<Cell> openList = new List<Cell>();
        HashSet<Cell> closedList = new HashSet<Cell>();

        openList.Add(startCell);

        while (openList.Count > 0)
        {
            Cell currentCell = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].myFCost <= currentCell.myFCost && openList[i].myHCost < currentCell.myHCost)
                {
                    currentCell = openList[i];
                }
            }

            openList.Remove(currentCell);
            closedList.Add(currentCell);

            if (currentCell == goalCell)
            {
                return GetFinalPath(startCell, goalCell);
            }

            foreach (Cell neighbourCell in FindTileNeighbours(currentCell))
            {
                if (!neighbourCell.isWalkable || closedList.Contains(neighbourCell))
                {
                    continue;
                }

                int moveCost = currentCell.myGCost + GetManhattanDistance(currentCell, neighbourCell);

                if (moveCost < neighbourCell.myGCost || !openList.Contains(neighbourCell))
                {
                    neighbourCell.myGCost = moveCost;
                    neighbourCell.myHCost = GetManhattanDistance(neighbourCell, goalCell);
                    neighbourCell.myParentCell = currentCell;

                    if (!openList.Contains(neighbourCell))
                    {
                        openList.Add(neighbourCell);
                    }
                }
            }
        }

        return null;
    }

    //More A* Magic
    List<Cell> GetFinalPath(Cell aStartCell, Cell aEndCell)
    {
        List<Cell> finalPath = new List<Cell>();
        Cell currentCell = aEndCell;

        while (currentCell != aStartCell)
        {
            finalPath.Add(currentCell);
            currentCell = currentCell.myParentCell;
        }

        finalPath.Reverse();
        aEndCell.isWalkable = false;
        return finalPath;
    }

    //Even More A* Magic
    int GetManhattanDistance(Cell aCellA, Cell aTileB)
    {
        int iX = Mathf.Abs(aCellA.xPosition - aTileB.xPosition);
        int iY = Mathf.Abs(aCellA.yPosition - aTileB.yPosition);

        return iX + iY;
    }

    //Even More A* Magic
    List<Cell> FindTileNeighbours(Cell aSourceTile)
    {
        List<Cell> neighbourList = new List<Cell>();
        Cell sourceCell = aSourceTile;

        if (sourceCell.yPosition > 0)
        {
            neighbourList.Add(level[sourceCell.xPosition, sourceCell.yPosition - 1]);
        }

        if (sourceCell.yPosition < level.GetLength(1) - 1)
        {
            neighbourList.Add(level[sourceCell.xPosition, sourceCell.yPosition + 1]);
        }

        if (sourceCell.xPosition > 0)
        {
            neighbourList.Add(level[sourceCell.xPosition - 1, sourceCell.yPosition]);
        }

        if (sourceCell.xPosition < level.GetLength(0) - 1)
        {
            neighbourList.Add(level[sourceCell.xPosition + 1, sourceCell.yPosition]);
        }

        return neighbourList;
    }

    List<Cell> GetCellList()
    {
        return CellList;
    }
}
