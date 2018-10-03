using UnityEngine;

[RequireComponent(typeof(EnemyMotor))]
public class EnemyController : MonoBehaviour {

    public Enemy enemy;

    private EnemyMotor enemyMotor;

    private int enemyHealth;

	void Start () {
        enemyMotor = GetComponent<EnemyMotor>();

        enemyHealth = enemy.enemyHealth;
	}
	
	void Update () {
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.playerObject.transform.position);

        if (distance <= enemy.enemyTriggerRange)
        {
            transform.LookAt(PlayerManager.instance.playerObject.transform.position);

            enemyMotor.moveToPoint(PlayerManager.instance.playerObject.transform.position);
        }
        else
        {
            enemyMotor.startWander();
        }

        if (enemyHealth <= 0)
        {
            killEnemy();
        }
	}

    public void takeDamage(int _damage)
    {
        enemyHealth -= _damage;

        Debug.Log("Enemy Health: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            killEnemy();
        }

        return;
    }

    private void killEnemy()
    {
        if (enemyHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }
}
