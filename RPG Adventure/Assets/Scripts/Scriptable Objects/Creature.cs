using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]
public class Creature : ScriptableObject {

    public string creatureName;

    public int attackDamage, defenseAmount;

    public float creatureBond, creatureRange, creatureSpeed;

    public CreatureBehavior creatureBehavior;
}

public enum CreatureBehavior
{
  Passive, Aggressive, Neutral  
};
