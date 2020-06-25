using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private bool IsWeighted;
    [SerializeField] private int NumberOfVertexs;
    [SerializeField] private int MaximumDegree;
    [SerializeField] private List<GameObject> Vertexs;
    [SerializeField] private int[,] AdjacencyList;
    [SerializeField] private int[] Degree;
    [SerializeField] private float[,] Weights;

    private void Awake()
    {
        bool isDigraph = false;
        AdjacencyList = new int[NumberOfVertexs, MaximumDegree];
        Degree = new int[NumberOfVertexs];
        Weights = new float[NumberOfVertexs, MaximumDegree];

        for(int i = 0; i < NumberOfVertexs; i++)
        {
            for(int j = 0; j < MaximumDegree; j++)
            {
                AdjacencyList[i, j] = -1;
            }
        }

        InsertEdge(1, 0, isDigraph, 0);
        InsertEdge(0, 2, isDigraph, 0);
        InsertEdge(2, 3, isDigraph, 0);
        InsertEdge(3, 4, isDigraph, 0);

        PrintGraph();
    }

    private int InsertEdge(int origin, int destiny, bool isDigraph, float weight)
    {
        if (origin < 0 || origin >= NumberOfVertexs || destiny < 0 || destiny >= NumberOfVertexs)
            return 0;

        AdjacencyList[origin, Degree[origin]] = destiny;
        if (IsWeighted)
            Weights[origin, destiny] = weight;
        Degree[origin]++;

        if (!isDigraph)
            InsertEdge(destiny, origin, true, weight);

        return 1;
    }

    private int RemoveEdges(int origin, int destiny, bool isDigraph)
    {
        if (origin < 0 || origin >= NumberOfVertexs || destiny < 0 || destiny >= NumberOfVertexs)
            return 0;

        int i = 0;
        while (i < Degree[origin] && AdjacencyList[origin, i] != destiny)
            i++;
        if (Degree[i] == origin)
            return 0;
        Degree[origin]--;
        AdjacencyList[origin, i] = AdjacencyList[origin, Degree[origin]];
        if (IsWeighted)
            Weights[origin, i] = Weights[origin, Degree[origin]];
        if (!isDigraph)
            RemoveEdges(destiny, origin, true);

        return 1;
    }

    private void PrintGraph()
    {
        for(int i = 0; i < NumberOfVertexs; i++)
        {
            string line = "";
            for(int j = 0; j < Degree[i]; j++)
            {
                if (IsWeighted)
                    line += AdjacencyList[i, j].ToString() + "(" + Weights[i, j].ToString() + "), ";
                else
                    line += AdjacencyList[i, j].ToString() + ", ";
            }
            if(line != "")
            {
                Debug.Log(i + " -> " + line);
            }
            
        }
    }

    public int[] GetAdjacencyListStartingByVertex(GameObject _vertex)
    {
        int verticeNumber = 0;
        int[] vertexAdjacencyList;

        foreach(GameObject vertex in Vertexs)
        {
            if (vertex == _vertex)
                break;
            verticeNumber++;
        }

        vertexAdjacencyList = new int[Degree[verticeNumber]];

        for(int i = 0; i < Degree[verticeNumber]; i++)
        {
            vertexAdjacencyList[i] = AdjacencyList[verticeNumber, i];
        }

        return vertexAdjacencyList;
    }

    public GameObject GetVertexStartingByYourNumber(int verticeNumber)
    {
        return Vertexs[verticeNumber];
    }

    public List<GameObject> GetVertexsList()
    {
        return Vertexs;
    }
}
