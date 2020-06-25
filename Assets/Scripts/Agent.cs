using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private GameObject AgentGameObject;
    [SerializeField] private Graph Graph;
    [SerializeField] private float Speed;
    [SerializeField] private Vector3 Direction;
    [SerializeField] private GameObject PreviousVertex;
    [SerializeField] private GameObject ActuallyVertex;
    private Vector3 Velocity;

    void Awake()
    {
        AgentGameObject = this.gameObject;
        PreviousVertex = null;
        ActuallyVertex = SearchNearestVertex(Graph.GetVertexsList());
        Direction = getDirectionToVertex(ActuallyVertex);
    }

    void Update()
    {
        if ((transform.position.x - ActuallyVertex.transform.position.x) < 0.5f && (transform.position.x - ActuallyVertex.transform.position.x) > -0.5f && (transform.position.z - ActuallyVertex.transform.position.z) < 0.5f && (transform.position.z - ActuallyVertex.transform.position.z) > -0.5f)
        {
            GameObject actuallyVertex = null;

            foreach (int number in Graph.GetAdjacencyListStartingByVertex(ActuallyVertex))
            {
                actuallyVertex = Graph.GetVertexsList()[number];

                if(actuallyVertex != PreviousVertex)
                {
                    break;
                }
            }

            Direction = (transform.position - actuallyVertex.transform.position).normalized;
            
            PreviousVertex = ActuallyVertex;
            ActuallyVertex = actuallyVertex;
        }
        else
        {
            Direction = (transform.position - ActuallyVertex.transform.position).normalized;
            Velocity = Speed * Direction;
            AgentGameObject.GetComponent<Rigidbody>().velocity = new Vector3(Velocity.x, 0, Velocity.z);
        }
    }

    private GameObject SearchNearestVertex(List<GameObject> Vertexs)
    {
        GameObject NearestVertex = null;
        float NearestDistance = 0;
        bool First = true;

        foreach(GameObject vertex in Vertexs)
        {
            if (First)
            {
                NearestVertex = vertex;
                NearestDistance = Vector3.Distance(transform.position, vertex.transform.position);
                First = false;
            }
            else
            {
                float distanceVertex = Vector3.Distance(transform.position, vertex.transform.position);
                if (NearestDistance > distanceVertex)
                {
                    NearestVertex = vertex;
                    NearestDistance = distanceVertex;
                }
            }
        }

        return NearestVertex;
    }

    private Vector3 getDirectionToVertex(GameObject vertex)
    {
        return (transform.position - vertex.transform.position).normalized;
    } 
}
