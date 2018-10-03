using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject {

    public string enemyName;

    public int enemyAttack, enemyDefense, enemyHealth;

    public float enemySpeed, enemyTriggerRange, enemyAttackRange, enemyRangeMin, enemyRangeMax;

    public SpecialAbility enemyAbility;
}
