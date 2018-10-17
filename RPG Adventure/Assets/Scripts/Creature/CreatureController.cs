using UnityEngine;

[RequireComponent(typeof(CreatureMotor))]
[RequireComponent(typeof(CreatureAI))]
public class CreatureController : MonoBehaviour {

    private CreatureBehavior behavior;

    private CreatureMotor creatureMotor;

    private CreatureAI creatureAI;

    private int creatureHealth;

    private bool isChasing = false, isDead = false;

    private float activeDistance = 100f;

    void Start () {
        creatureMotor = GetComponent<CreatureMotor>();

        creatureAI = GetComponent<CreatureAI>();

        behavior = creatureAI.creature.creatureBehavior;

        creatureHealth = creatureAI.creature.creatureHealth;
    }
	
	void LateUpdate () {
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.playerObject.transform.position);

        if (!PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature() && distance <= activeDistance)
        {
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
        else
        {
            StopAllCoroutines();
        }
	}

    #region Creature Behaviors

    private void passiveBehavior()
    {
        creatureMotor.startWander();
        return;
    }

    private void aggressiveBehavior()
    {
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.playerObject.transform.position);

        if (distance > creatureAI.creature.triggerRange)
        {
            isChasing = false;
        }
        else
        {
            isChasing = true;
        }

        if (distance <= creatureAI.creature.creatureAttackRange && isChasing)
        {
            creatureAI.creatureAttack();
        }

        if (isChasing)
        {
            StopCoroutine(creatureMotor.getCreatureAI().creatureWander());
            creatureMotor.getCreatureAI().getCreatureAgent().SetDestination(PlayerManager.instance.playerObject.transform.position);
            transform.LookAt(PlayerManager.instance.playerObject.transform.position);
        }
        else if (!isChasing) 
        {
            creatureMotor.startWander();
            return;
        }
    }

    private void neutralBehavior()
    {
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.playerObject.transform.position);

        if (distance > creatureAI.creature.triggerRange)
        {
            isChasing = false;
        }

        if (distance < creatureAI.creature.creatureAttackRange && isChasing)
        {
            creatureAI.creatureAttack();
        }

        if (isChasing)
        {
            StopCoroutine(creatureMotor.getCreatureAI().creatureWander());
            creatureMotor.getCreatureAI().getCreatureAgent().SetDestination(PlayerManager.instance.playerObject.transform.position);
            transform.LookAt(PlayerManager.instance.playerObject.transform.position);
        }
        else if (!isChasing)
        {
            creatureMotor.startWander();
        }
    }

    #endregion

    public void takeDamage(Transform hitBy, int Damage)
    {
        switch (creatureAI.creature.creatureBehavior)
        {
            case CreatureBehavior.Passive:
                if (hitBy.name == "Player")
                {
                    StopCoroutine(creatureMotor.getCreatureAI().creatureWander());
                    creatureHealth -= Damage;

                    Debug.Log("Creature Health: " + creatureHealth);

                    if (creatureHealth <= 0 && !isDead)
                    {
                        killCreature();
                    }

                    if (!isDead)
                    {
                        StartCoroutine(creatureMotor.getCreatureAI().creaturePanic());
                    }
                }
                else
                {
                    creatureHealth -= Damage;

                    Debug.Log("Creature Health: " + creatureHealth);

                    if (creatureHealth <= 0)
                    {
                        killCreature();
                    }
                }
                break;

            case CreatureBehavior.Aggressive:
                creatureHealth -= Damage;

                Debug.Log("Creature Health: " + creatureHealth);

                if (creatureHealth <= 0)
                {
                    killCreature();
                }
                break;

            case CreatureBehavior.Neutral:
                if (hitBy.name == "Player")
                {
                    creatureHealth -= Damage;

                    Debug.Log("Creature Health: " + creatureHealth);

                    if (creatureHealth <= 0)
                    {
                        killCreature();
                    }

                    if (!isChasing)
                    {
                        isChasing = true;
                    }
                }
                else
                {
                    creatureHealth -= Damage;

                    Debug.Log("Creature Health: " + creatureHealth);

                    if (creatureHealth <= 0)
                    {
                        killCreature();
                    }

                    StartCoroutine(creatureMotor.getCreatureAI().creaturePanic());
                }
                break;
        }
    }

    public void killCreature()
    {
        if (creatureHealth <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);

            if (creatureAI.creature.creatureBehavior == CreatureBehavior.Passive)
            {
                GameController.instance.totalPassiveCreatures--;

                GameController.instance.totalActiveCreatures--;

                return;
            }


            if (creatureAI.creature.creatureBehavior == CreatureBehavior.Neutral)
            {
                GameController.instance.totalNeutralCreatures--;

                GameController.instance.totalActiveCreatures--;

                return;
            }

            if (creatureAI.creature.creatureBehavior == CreatureBehavior.Aggressive)
            {
                GameController.instance.totalAggressiveCreatures--;

                GameController.instance.totalActiveCreatures--;

                return;
            }
        }
    }
}
