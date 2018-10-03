using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyController))]
public class EnemyMotor : MonoBehaviour {

    private EnemyController enemyController;

    private NavMeshAgent enemyAgent;

    private Vector3 newPosition;

    private bool posPicked = false;

    [SerializeField, Tooltip("The wait time between moves")]
    private float waitTime = 5;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();

        enemyAgent = GetComponent<NavMeshAgent>();
    }

    public void moveToPoint(Vector3 _position)
    {
        StopAllCoroutines();
        enemyAgent.SetDestination(_position);
    }

    public void startWander()
    {
        if ((Random.value >= 0.6))
        {
            //Makes sure enemy isnt to close
            if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
            {
                //Makes sure path is complete and it position is at its previous destination
                if (enemyAgent.pathStatus == NavMeshPathStatus.PathComplete && transform.position == enemyAgent.destination)
                {
                    //Starts wander coroutine
                    StartCoroutine(enemyWander());

                    //Sets posPicked back to false
                    if (posPicked)
                    {
                        posPicked = false;
                    }
                }
            }
            return;
        }

        //Checks if enemy should wait
        if (Random.value < 0.6)
        {
            StartCoroutine(enemyWait());
        }
    }

    public IEnumerator enemyWander()
    {
        enemyAgent.speed = enemyController.enemy.enemySpeed;

        if (enemyAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            yield return new WaitForSeconds(waitTime);

            float enemyRange = Random.Range(enemyController.enemy.enemyRangeMin, enemyController.enemy.enemyRangeMax);

            if (!posPicked)
            {
                if (Random.value <= 0.2)
                {
                    if (!posPicked)
                    {
                        newPosition = new Vector3(enemyAgent.transform.position.x + enemyRange, enemyAgent.transform.position.y, enemyAgent.transform.position.z + enemyRange);

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
                        newPosition = new Vector3(enemyAgent.transform.position.x - enemyRange, enemyAgent.transform.position.y, enemyAgent.transform.position.z - enemyRange);

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
                        newPosition = new Vector3(enemyAgent.transform.position.x - enemyRange, enemyAgent.transform.position.y, enemyAgent.transform.position.z + enemyRange);

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
                        newPosition = new Vector3(enemyAgent.transform.position.x + enemyRange, enemyAgent.transform.position.y, enemyAgent.transform.position.z - enemyRange);

                        if (!posPicked)
                        {
                            posPicked = true;
                        }
                    }

                    yield return new WaitForSeconds(waitTime / 2);
                }
            }
        }

        if (newPosition != Vector3.zero && !enemyAgent.pathPending && enemyAgent.pathStatus != NavMeshPathStatus.PathInvalid && enemyAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            enemyAgent.SetDestination(newPosition);

            yield return new WaitForSeconds(waitTime);
        }
    }

    public IEnumerator enemyWait()
    {
        //Makes creature wait for the waitTime
        yield return new WaitForSeconds(waitTime);
    }
}
