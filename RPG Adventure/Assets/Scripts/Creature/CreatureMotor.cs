using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CreatureController))]
public class CreatureMotor : MonoBehaviour {

    private CreatureController creatureController;

    private NavMeshAgent creatureAgent;

    private Vector3 newPosition;

    private bool posPicked = false;

    [SerializeField, Tooltip("The wait time between moves")]
    private float waitTime = 5, paincWaitTime = 1;

    private float paincSpeed;

    void Start () {
        creatureController = GetComponent<CreatureController>();

        creatureAgent = GetComponent<NavMeshAgent>();

        creatureAgent.speed = creatureController.creature.creatureSpeed;

        paincSpeed = creatureController.creature.creatureSpeed * 3;
    }
	
    public void moveToPoint(Vector3 _position)
    {
        StopAllCoroutines();
        creatureAgent.SetDestination(_position);
    }

    public void startWander()
    {
        if ((Random.value >= 0.6))
        {
            //Makes sure creature isnt to close
            if (creatureAgent.remainingDistance <= creatureAgent.stoppingDistance)
            {
                //Makes sure path is complete and it position is at its previous destination
                if (creatureAgent.pathStatus == NavMeshPathStatus.PathComplete && transform.position == creatureAgent.destination)
                {
                    //Starts wander coroutine
                    StartCoroutine(creatureWander());

                    //Sets posPicked back to false
                    if (posPicked)
                    {
                        posPicked = false;
                    }
                }
            }
            return;
        }

        //Checks if creature should wait
        if (Random.value < 0.6)
        {
            StartCoroutine(creatureWait());
        }
    }

    #region Creature AI

    #region Creature Panic
    public IEnumerator creaturePanic()
    {
        float creatureRange = Random.Range(creatureController.creature.creatureRangeMin, creatureController.creature.creatureRangeMax);

        if (Random.value <= 0.2)
        {
            newPosition = new Vector3(creatureAgent.transform.position.x + creatureRange, creatureAgent.transform.position.y, creatureAgent.transform.position.z + creatureRange);
        }

        if (Random.value >= 0.3 && Random.value <= 0.5)
        {
            newPosition = new Vector3(creatureAgent.transform.position.x - creatureRange, creatureAgent.transform.position.y, creatureAgent.transform.position.z - creatureRange);
        }

        if (Random.value >= 0.6 && Random.value <= 0.8)
        {
            newPosition = new Vector3(creatureAgent.transform.position.x - creatureRange, creatureAgent.transform.position.y, creatureAgent.transform.position.z + creatureRange);
        }

        if (Random.value >= 0.9)
        {
            newPosition = new Vector3(creatureAgent.transform.position.x + creatureRange, creatureAgent.transform.position.y, creatureAgent.transform.position.z - creatureRange);
        }

        if (newPosition != Vector3.zero)
        {
            creatureAgent.speed = paincSpeed;

            creatureAgent.SetDestination(newPosition);

            Debug.Log("Panicing...");
        }

        yield return new WaitForSeconds(paincWaitTime);
    }
    #endregion

    #region Creature Wander
    public IEnumerator creatureWander()
    {
        creatureAgent.speed = creatureController.creature.creatureSpeed;

        if (creatureAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            yield return new WaitForSeconds(waitTime);

            float creatureRange = Random.Range(creatureController.creature.creatureRangeMin, creatureController.creature.creatureRangeMax);

            if (!posPicked)
            {
                if (Random.value <= 0.2)
                {
                    if (!posPicked)
                    {
                        newPosition = new Vector3(creatureAgent.transform.position.x + creatureRange, creatureAgent.transform.position.y, creatureAgent.transform.position.z + creatureRange);

                        if (!posPicked)
                        {
                            posPicked = true;
                        }
                    }

                    yield return new WaitForSeconds(waitTime / 2);
                }

                if (Random.value >= 0.3 && Random.value <= 0.5)
                {
                    if (!posPicked)
                    {
                        newPosition = new Vector3(creatureAgent.transform.position.x - creatureRange, creatureAgent.transform.position.y, creatureAgent.transform.position.z - creatureRange);

                        if (!posPicked)
                        {
                            posPicked = true;
                        }
                    }

                    yield return new WaitForSeconds(waitTime / 2);
                }

                if (Random.value >= 0.6 && Random.value <= 0.8)
                {
                    if (!posPicked)
                    {
                        newPosition = new Vector3(creatureAgent.transform.position.x - creatureRange, creatureAgent.transform.position.y, creatureAgent.transform.position.z + creatureRange);

                        if (!posPicked)
                        {
                            posPicked = true;
                        }
                    }

                    yield return new WaitForSeconds(waitTime / 2);
                }

                if (Random.value >= 0.9)
                {
                    if (!posPicked)
                    {
                        newPosition = new Vector3(creatureAgent.transform.position.x + creatureRange, creatureAgent.transform.position.y, creatureAgent.transform.position.z - creatureRange);

                        if (!posPicked)
                        {
                            posPicked = true;
                        }
                    }

                    yield return new WaitForSeconds(waitTime / 2);
                }
            }
        }

        if (newPosition != Vector3.zero && !creatureAgent.pathPending && creatureAgent.pathStatus != NavMeshPathStatus.PathInvalid && creatureAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            creatureAgent.SetDestination(newPosition);

            yield return new WaitForSeconds(waitTime);
        }
    }

    #endregion

    #region Creature Wait
    public IEnumerator creatureWait()
    {
        //Makes creature wait for the waitTime
        yield return new WaitForSeconds(waitTime);
    }
    #endregion

    #endregion

    public NavMeshAgent getCreatureAgent()
    {
        return creatureAgent;
    }
}
