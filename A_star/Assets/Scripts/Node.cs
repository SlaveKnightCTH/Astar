using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Node : IComparable
{
    //F=G+H
    public float nodeTotalCost;//G:起始节点到当前节点
    public float estimatedCost;//H
    public bool bObstacle;
    public Node parent;//上一步节点
    public Vector3 position;

    public Node()
    {
        estimatedCost = 0.0f;
        nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
    }

    public Node(Vector3 pos)
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
        this.position = pos;
    }

    public void MarkAsObstacle()
    {
        this.bObstacle = true;
    }

    public int CompareTo(object obj)//
    {
        Node node = (Node)obj;

        if (this.estimatedCost < node.estimatedCost)
            return -1;

        if (this.estimatedCost > node.estimatedCost)
            return 1;

        return 0;
    }


}
