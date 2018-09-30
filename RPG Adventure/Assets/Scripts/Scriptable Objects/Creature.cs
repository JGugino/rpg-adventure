using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]
public class Creature : ScriptableObject {

    public string creatureName;

    public int attackDamage, defenseAmount, creatureHealth;

    public float creatureBond, creatureRangeMin, creatureRangeMax, creatureAttackRange, triggerRange, creatureSpeed;

    public CreatureBehavior creatureBehavior;
}

public enum CreatureBehavior
{
  Passive, Aggressive, Neutral  
};
