using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CreatureController))]
[RequireComponent(typeof(CreatureMotor))]
public class CreatureAI : MonoBehaviour {

    public Creature creature;

    private CreatureMotor creatureMotor;

    private NavMeshAgent creatureAgent;

    [SerializeField, Tooltip("The wait time between moves")]
    private float waitTime = 5, paincWaitTime = 1;

    [SerializeField, Tooltip("Time between attacks (in seconds)")]
    private float attackDelay = 5f, currentDelay = 0;

    private float paincSpeed;

    private Vector3 newPosition;

    public bool posPicked = false;

    private void Start()
    {
        creatureAgent = GetComponent<NavMeshAgent>();

        creatureAgent.speed = creature.creatureSpeed;

        paincSpeed = creature.creatureSpeed * 3;
    }

    #region Creature AI

    #region Creature Panic
    public IEnumerator creaturePanic()
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
    public IEnumerator creatureWander()
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

    #region Creature Wait
    public IEnumerator creatureWait()
    {
        //Makes creature wait for the waitTime
        yield return new WaitForSeconds(waitTime);
    }
    #endregion

    #region Creature Attack
    public void creatureAttack()
    {
        currentDelay += Time.deltaTime;

        if (currentDelay >= attackDelay)
        {
            PlayerManager.instance.playerObject.GetComponent<PlayerController>().takeDamage(creature.attackDamage);

            currentDelay = 0;

            Debug.Log("Attacking player for " + creature.attackDamage + " damage.");

            return;
        }
    }
    #endregion
    
    #endregion

    public NavMeshAgent getCreatureAgent()
    {
        return creatureAgent;
    }
}
