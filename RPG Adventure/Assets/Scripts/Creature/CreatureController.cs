using UnityEngine;

[RequireComponent(typeof(CreatureMotor))]
public class CreatureController : MonoBehaviour {

    public Creature creature;

    private CreatureBehavior behavior;

    private CreatureMotor creatureMotor;

    private int creatureHealth;

    private bool isChasing = false, isDead = false;

    void Start () {
        creatureMotor = GetComponent<CreatureMotor>();

        behavior = creature.creatureBehavior;

        creatureHealth = creature.creatureHealth;
    }
	
	void LateUpdate () {
        if (!PlayerManager.instance.playerObject.GetComponent<PlayerController>().getControllingCreature())
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

        if (distance <= creature.triggerRange)
        {
            StopCoroutine(creatureMotor.creatureWander());
            creatureMotor.getCreatureAgent().SetDestination(PlayerManager.instance.playerObject.transform.position);
            transform.LookAt(PlayerManager.instance.playerObject.transform.position);
        }
        else
        {
            creatureMotor.startWander();
            return;
        }
    }

    private void neutralBehavior()
    {
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.playerObject.transform.position);

        if (distance > creature.triggerRange)
        {
            isChasing = false;
        }

        if (distance < creature.creatureAttackRange && isChasing)
        {
            PlayerManager.instance.playerObject.GetComponent<PlayerController>().takeDamage(creature.attackDamage);
        }

        if (isChasing)
        {
            StopCoroutine(creatureMotor.creatureWander());
            creatureMotor.getCreatureAgent().SetDestination(PlayerManager.instance.playerObject.transform.position);
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
        switch (creature.creatureBehavior)
        {
            case CreatureBehavior.Passive:
                if (hitBy.name == "Player")
                {
                    StopCoroutine(creatureMotor.creatureWander());
                    creatureHealth -= Damage;

                    Debug.Log("Creature Health: " + creatureHealth);

                    if (creatureHealth <= 0 && !isDead)
                    {
                        killCreature();
                    }

                    if (!isDead)
                    {
                        StartCoroutine(creatureMotor.creaturePanic());
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

                    StartCoroutine(creatureMotor.creaturePanic());
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
        }
    }
}
