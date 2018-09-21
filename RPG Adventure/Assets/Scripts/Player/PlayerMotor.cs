using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {

    public Camera mainCamera;

    private NavMeshAgent navAgent { get; set; }

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        playerMove();
	}

    private void playerMove()
    {
        if (Input.GetButtonDown("Move/Select"))
        {
            Ray ray  = mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitFromRay;

            if(Physics.Raycast(ray, out hitFromRay))
            {
                navAgent.SetDestination(hitFromRay.point);
            }
        }
    }
}
