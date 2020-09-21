using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private LayerMask PlayerLayerMask = -1;

    private NavMeshAgent _enemyNavmeshAgent;
    private bool _isTargetFind = false;
    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        _enemyNavmeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTargetFind)
            FollowTarget();
        else
            FindTarget();
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3.0f, PlayerLayerMask);
        _isTargetFind = (colliders.Length != 0);
        if (_isTargetFind)
        {
            _target = colliders[0].transform.gameObject;
        }
    }

    private void FollowTarget()
    {
        _enemyNavmeshAgent.destination = _target.transform.position;
        if (Vector3.Distance(transform.position, _target.transform.position) < 5.0f)
            _isTargetFind = false;
    }
}
