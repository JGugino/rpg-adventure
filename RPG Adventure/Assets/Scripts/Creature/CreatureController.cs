using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour {

    public Creature creature;

    private CreatureBehavior behavior;

    private Rigidbody creatureRigidbody;

    private bool posPicked = false;

    private Vector3 currentPosition, newPosition;

	void Start () {
        behavior = creature.creatureBehavior;

        creatureRigidbody = GetComponent<Rigidbody>();

        currentPosition = transform.position;
    }
	
	void LateUpdate () {

        currentPosition = transform.position;

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

        if ((Random.value >= 0.9) && (newPosition != Vector3.zero) && (posPicked))
        {
            Debug.Log("Wandering...");
            creatureWander();
            return;
        }
        else if(Random.value < 0.9)
        {
            Debug.Log("Waiting...");
            StartCoroutine(creatureWait());
            return;
        }
    }

    private void aggressiveBehavior()
    {

    }

    private void neutralBehavior()
    {

    }

    private void creatureWander()
    {
        if ((currentPosition != newPosition))
        {
            creatureRigidbody.MovePosition(Vector3.Lerp(currentPosition, newPosition, creature.creatureSpeed * Time.deltaTime));
            return;
        }

    }

    private IEnumerator creatureWait()
    {
        yield return new WaitForSeconds(10);

        if (!posPicked)
        {
            newPosition = new Vector3(transform.position.x + creature.creatureRange, transform.position.y, transform.position.z + creature.creatureRange);
            posPicked = true;
            Debug.Log("New Positon: " + newPosition);
        }else if (posPicked)
        {
            posPicked = false;
            yield return new WaitForSeconds(5);
        }
    }
}
