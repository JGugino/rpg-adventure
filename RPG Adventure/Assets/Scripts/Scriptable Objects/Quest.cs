using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject {

    public string questName;

    public string questDescription;

    public bool isCompleted = false;

    public QuestType questType;

    public QuestCompleteType questCompleteType;

    public Item itemToCollect;

    public Creature creatureToCollect;

    public int collectAmount;

    public Enemy enemyToKill;

    public int enemyKillAmount;


}
public enum QuestType
{
    MainQuest, SideQuest
}

public enum QuestCompleteType
{
    Item, Location, CollectItems, CollectCreature, KillEnemy, KillEnemyAmount
}
