using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(new Vector3(10, 0, 10)); 
        }
        else
        {
         
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
            {
             
                agent.Warp(hit.position);
              
                agent.SetDestination(new Vector3(10, 0, 10)); 
            }
            else
            {
                Debug.LogError("Miner icin yak?nlarda bir NavMesh zemini bulunamadi!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
