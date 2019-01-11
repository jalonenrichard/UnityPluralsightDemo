using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public float PatrolTime = 10f;
    public float AggroRange = 10f;
    public Transform[] Waypoints;

    private int _index;
    private float _speed, _agentSpeed;
    private Transform _player;
    private Animator _animator;
    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
            _agentSpeed = _agent.speed;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _index = Random.Range(0, Waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);
        if (Waypoints.Length > 0)
        {
            InvokeRepeating("Patrol", 0, PatrolTime);
        }
    }

    void Patrol()
    {
        _index = _index == Waypoints.Length - 1 ? 0 : _index + 1;
    }

    void Tick()
    {
        _agent.destination = Waypoints[_index].position;
        _agent.speed = _agentSpeed / 2;
        if (_player != null && Vector3.Distance(transform.position, _player.position) < AggroRange)
        {
            _agent.destination = _player.position;
            _agent.speed = _agentSpeed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}