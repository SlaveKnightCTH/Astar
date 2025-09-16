using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar 
{
    //open存放未检测
    //close存放以及检测
    public static PriorityQueue closedList, openList;//open未检测  close已检测

    //计算H距离:从当前节点到终点的距离
    private static float HeuristEstimateCost(Node curNode, Node goalNode)
    {
        Vector3 vecCost = curNode.position - goalNode.position;
        return vecCost.magnitude; 
    }

    public static ArrayList FindPath(Node start, Node goal)
    {
        openList = new PriorityQueue();
        closedList = new PriorityQueue();
        openList.Push(start);
        start.nodeTotalCost = 0;
        start.estimatedCost = HeuristEstimateCost(start, goal);
        Node node = null;

        while (openList.Length!=0)
        {
            node = openList.First();

            if (node.position == goal.position)
            {
                return CalculatPath(node);
            }

            ArrayList neighbours = new ArrayList();

            GridManager.instance.GetNeighbours(node, neighbours);
            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = (Node)neighbours[i];

                if (!closedList.Contains(neighbourNode))
                {
                    //G
                    float cost = HeuristEstimateCost(node, neighbourNode);
                    float totalCost=node.nodeTotalCost + cost;
                    //
                    float neighbourNodeEstCost = HeuristEstimateCost(neighbourNode, goal);
                    
                    neighbourNode.nodeTotalCost = totalCost;
                    neighbourNode.parent = node;
                    neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Push(neighbourNode);
                    }
                }
            }

            closedList.Push(node);
            openList.Remove(node);
        }

        if (node.position != goal.position)
        {
            Debug.LogError("Goal Not Found");
            return null;
        }

        return CalculatPath(node);
    }

    private static ArrayList CalculatPath(Node node)
    {
        ArrayList list = new ArrayList();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }
}
