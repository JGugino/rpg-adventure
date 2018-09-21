using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Armor")]
public class Armor : Item {

    public int defense;

    public SpecialAbility specialAbility;
}

public enum SpecialAbility{
    ExtraDamage, FireDamage, ShockDamage, forceField
};
