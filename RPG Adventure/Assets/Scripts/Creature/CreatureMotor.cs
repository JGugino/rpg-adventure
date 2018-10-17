using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CreatureAI))]
public class CreatureMotor : MonoBehaviour {

    private CreatureAI creatureAI;

    void Start () {

        creatureAI = GetComponent<CreatureAI>();
    }
	
    public void moveToPoint(Vector3 _position)
    {
        StopAllCoroutines();
        creatureAI.getCreatureAgent().SetDestination(_position);
    }

    public void startWander()
    {
        if ((Random.value >= 0.6))
        {
            //Makes sure creature isnt to close
            if (creatureAI.getCreatureAgent().remainingDistance <= creatureAI.getCreatureAgent().stoppingDistance)
            {
                //Makes sure path is complete and it position is at its previous destination
                if (creatureAI.getCreatureAgent().pathStatus == NavMeshPathStatus.PathComplete && transform.position == creatureAI.getCreatureAgent().destination)
                {
                    //Starts wander coroutine
                    StartCoroutine(creatureAI.creatureWander());

                    //Sets posPicked back to false
                    if (creatureAI.posPicked)
                    {
                        creatureAI.posPicked = false;
                    }
                }
            }
            return;
        }

        //Checks if creature should wait
        if (Random.value < 0.6)
        {
            StartCoroutine(creatureAI.creatureWait());
        }
    }

    public CreatureAI getCreatureAI()
    {
        return creatureAI;
    }
}
