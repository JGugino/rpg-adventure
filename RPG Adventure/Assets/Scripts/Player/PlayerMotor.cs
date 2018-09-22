using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {

    private NavMeshAgent navAgent { get; set; }

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void moveToPoint(Vector3 _position)
    {
        navAgent.SetDestination(_position);
    }
}
