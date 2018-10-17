using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour {

    public GameObject creaturePrefab;

    public Creature[] creatureTypes = new Creature[4];

    private GameObject createdCreature;

    [SerializeField, Tooltip("How close you have to get for it to start spawning")]
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
                createdCreature = Instantiate(creaturePrefab, new Vector3((transform.position.x + Random.Range(minDistance, maxDistance)), (transform.position.y + 1), (transform.position.z + Random.Range(minDistance, maxDistance))), Quaternion.identity);

                if (Random.value <= 0.2)
                {
                    selectCreatureType(0);
                }

                if (Random.value > 0.2 && Random.value <= 0.5)
                {
                    selectCreatureType(1);
                }

                if (Random.value > 0.5 && Random.value <= 0.7)
                {
                    selectCreatureType(2);
                }

                if (Random.value > 0.7)
                {
                    selectCreatureType(3);
                }
            }
        }
    }

    public void selectCreatureType(int _type)
    {
        CreatureAI cAI = createdCreature.GetComponent<CreatureAI>();

        if (cAI.creature.creatureBehavior == CreatureBehavior.Passive)
        {
            if (GameController.instance.totalPassiveCreatures < GameController.instance.maxPassiveCreatures)
            {
                cAI.creature = creatureTypes[_type];

                currentDelay = 0;

                GameController.instance.totalPassiveCreatures++;

                GameController.instance.totalActiveCreatures++;

                return;
            }
        }

        if (cAI.creature.creatureBehavior == CreatureBehavior.Neutral)
        {
            if (GameController.instance.totalNeutralCreatures < GameController.instance.maxNeutralCreatures)
            {
                cAI.creature = creatureTypes[_type];

                currentDelay = 0;

                GameController.instance.totalNeutralCreatures++;

                GameController.instance.totalActiveCreatures++;

                return;
            }
        }

        if (cAI.creature.creatureBehavior == CreatureBehavior.Aggressive)
        {
            if (GameController.instance.totalAggressiveCreatures < GameController.instance.maxAggressiveCreatures)
            {
                cAI.creature = creatureTypes[_type];

                currentDelay = 0;

                GameController.instance.totalAggressiveCreatures++;

                GameController.instance.totalActiveCreatures++;

                return;
            }
        }

        return;
    }
}
