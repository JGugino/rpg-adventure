using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour {

    public GameObject creaturePrefab;

    public Creature[] creatureTypes = new Creature[4];

    private float activeDistance = 50f;

    private int minDistance = -30, maxDistance = 30;

    [SerializeField, Tooltip("Time between spawns (in seconds)")]
    private float spawnDelay = 5f, currentDelay = 0;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.playerObject.transform.position);

        if (distance <= activeDistance)
        {
            spawnCreature();
        }

        if (distance > activeDistance && currentDelay != 0)
        {
            currentDelay = 0;
        }
    }

    private void spawnCreature()
    {
        currentDelay += Time.deltaTime;

        if (currentDelay >= spawnDelay)
        {
            if (GameController.instance.totalActiveCreatures < GameController.instance.maxCreatureCap)
            {
                GameObject createdCreature = Instantiate(creaturePrefab, new Vector3((transform.position.x + Random.Range(minDistance, maxDistance)), (transform.position.y + 1), (transform.position.z + Random.Range(minDistance, maxDistance))), Quaternion.identity);

                CreatureAI cAI = createdCreature.GetComponent<CreatureAI>();

                if (Random.value <= 0.2)
                {
                    cAI.creature = creatureTypes[0];

                    Debug.Log("Creature Type: " + creatureTypes[0].creatureBehavior);

                    GameController.instance.totalActiveCreatures++;

                    currentDelay = 0;

                    return;
                }

                if (Random.value > 0.2 && Random.value <= 0.5)
                {
                    cAI.creature = creatureTypes[1];

                    Debug.Log("Creature Type: " + creatureTypes[1].creatureBehavior);

                    GameController.instance.totalActiveCreatures++;

                    currentDelay = 0;

                    return;
                }

                if (Random.value > 0.5 && Random.value <= 0.7)
                {
                    cAI.creature = creatureTypes[2];

                    Debug.Log("Creature Type: " + creatureTypes[2].creatureBehavior);

                    GameController.instance.totalActiveCreatures++;

                    currentDelay = 0;

                    return;
                }

                if (Random.value > 0.7)
                {
                    cAI.creature = creatureTypes[3];

                    Debug.Log("Creature Type: " + creatureTypes[3].creatureBehavior);

                    GameController.instance.totalActiveCreatures++;

                    currentDelay = 0;

                    return;
                }
            }
        }
    }
}
