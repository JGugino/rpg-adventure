using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestController : MonoBehaviour {

    public static QuestController instance;

    public Quest activeQuest;

    public List<Quest> playersQuests;

    public List<Quest> completedQuests;

    public void Awake()
    {
        instance = this;
    }

    public void addQuest(Quest _quest)
    {
        if (!playersQuests.Contains(_quest))
        {
            playersQuests.Add(_quest);

            createQuestPrefab(_quest);
        }
        else
        {
            return;
        }
    }

    private void createQuestPrefab(Quest _quest)
    {
        if (!GameObject.Find(_quest.questName))
        {
            GameObject createdPrefab = Instantiate(GUIController.instance.questPrefab, GUIController.instance.questParent.transform);

            createdPrefab.name = _quest.questName;

            TextMeshProUGUI[] texts = createdPrefab.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI t in texts)
            {
                if (t.name == "Quest Name Text")
                {
                    t.text = _quest.questName;
                }


                if (t.name == "Quest Description Text")
                {
                    t.text = _quest.questDescription;
                }
            }
        }
    }
}
