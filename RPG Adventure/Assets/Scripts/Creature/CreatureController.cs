using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureController : MonoBehaviour {

    public Creature creature;

    private CreatureBehavior behavior;

    private NavMeshAgent creatureAgent { get; set; }

    private Vector3 newPosition;

    private bool posPicked = false, isChasing = false;

    private GameObject playerObject;

    [SerializeField, Tooltip("The wait time between moves")]
    private float waitTime = 5, paincWaitTime = 1;

    private float paincSpeed;

    private int creatureHealth;

    private void Awake()
    {
        playerObject = GameObject.Find("Player");
    }

    void Start () {
        behavior = creature.creatureBehavior;

        creatureAgent = GetComponent<NavMeshAgent>();

        creatureAgent.speed = creature.creatureSpeed;

        paincSpeed = creature.creatureSpeed * 3;
    }
	
	void LateUpdate () {

        switch (behavior)
        {
            case CreatureBehavior.Passive:
                passiveBehavior();
                break;

            case CreatureBehavior.Aggressive:
                aggressiveBehavior();
                break;

            case CreatureBehavior.Neutral:
                neutralBehavior();
                break;

            default:
                Debug.Log("Behavior Unknown! " + behavior);
                break;
        }

	}

    private void passiveBehavior()
    {
        startWander();
    }

    private void aggressiveBehavior()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);

        if (distance <= creature.triggerRange)
        {
            StopCoroutine(creatureWander());
            isChasing = true;
            creatureAgent.SetDestination(playerObject.transform.position);
        }
        else
        {
            startWander();
        }
    }

    private void neutralBehavior()
    {
        float distance = Vector3.Distance(transform.position, playerObject.transform.position);

        if (isChasing)
        {
            StopCoroutine(creatureWander());
            creatureAgent.SetDestination(playerObject.transform.position);
        }
        else if (!isChasing)
        {
            startWander();
        }



        if (distance > creature.triggerRange)
        {
            isChasing = false;
        }
    }

    private void startWander()
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

    public void takeDamage(Transform hitBy, int Damage)
    {
        switch (creature.creatureBehavior)
        {
            case CreatureBehavior.Passive:
                if (hitBy.name == "Player")
                {
                    StopCoroutine(creatureWander());
                    creature.creatureHealth -= Damage;
                    StartCoroutine(creaturePanic());
                }
                else
                {
                    creature.creatureHealth -= Damage;
                }
                break;

            case CreatureBehavior.Aggressive:
                creature.creatureHealth -= Damage;
                break;

            case CreatureBehavior.Neutral:
                if (hitBy.name == "Player")
                {
                    creature.creatureHealth -= Damage;
                    if (!isChasing)
                    {
                        isChasing = true;
                    }
                }
                else
                {
                    creature.creatureHealth -= Damage;
                    StartCoroutine(creaturePanic());
                }
                break;
        }
    }

    #region Creature Panic
    private IEnumerator creaturePanic()
    {
      float creatureRange = Random.Range(creature.creatureRangeMin, creature.creatureRangeMax);

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
    private IEnumerator creatureWander()
    {
        creatureAgent.speed = creature.creatureSpeed;

        if (creatureAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            yield return new WaitForSeconds(waitTime);

            float creatureRange = Random.Range(creature.creatureRangeMin, creature.creatureRangeMax);

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

    private IEnumerator creatureWait()
    {
        //Makes creature wait for the waitTime
        yield return new WaitForSeconds(waitTime);
    }
}
