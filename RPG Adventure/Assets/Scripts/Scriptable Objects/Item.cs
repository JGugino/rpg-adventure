using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {

    public string itemName;

    public int value, range = 10;

    public Sprite icon;

    public ItemType type;

    public SpecialAbility specialAbility;

    public int damage, defense;

    public bool stackable;
}
public enum ItemType{
    None, Head, Chest, Legs, Weapon, Heal, KeyItem
};
public enum SpecialAbility
{
    None, ExtraDamage, FireDamage, ShockDamage, forceField
};
