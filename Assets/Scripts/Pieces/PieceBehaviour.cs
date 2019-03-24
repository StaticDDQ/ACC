using UnityEngine;
using UnityEngine.AI;

public class PieceBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform target;
    private bool foundTarget = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!foundTarget && other.tag.Equals("enemyPiece"))
        {
            target = other.transform;
            foundTarget = true;
        }
    }
}
