using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        agent.destination = target.transform.position;
    }

}
