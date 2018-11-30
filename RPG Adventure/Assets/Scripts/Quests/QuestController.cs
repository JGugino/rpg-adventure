using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestController : MonoBehaviour {

    public static QuestController instance;

    public Quest activeQuest;

    public List<Quest> playersQuests;

    public List<Quest> completedQuests;

    public Transform targetLocation;

    public void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (activeQuest != null)
        {
            updateActiveQuestInfo(activeQuest);
        }
    }

    public void addQuest(Quest _quest, Transform _location = null)
    {
        if (!playersQuests.Contains(_quest))
        {
            if (_quest.questCompleteType == QuestCompleteType.Location)
            {
                if (_location != null)
                {
                    targetLocation = _location;
                }
            }

            playersQuests.Add(_quest);

            createQuestPrefab(_quest);
        }
        else
        {
            return;
        }
    }

    public void removeQuest(Quest _quest)
    {
        if (playersQuests.Contains(_quest))
        {
            playersQuests.Remove(_quest);

            removeQuestPrefab(_quest);
        }
        else
        {
            return;
        }
    }

    public void selectActiveQuest(Quest _quest)
    {
        if (activeQuest == null)
        {
            activeQuest = _quest;

            updateActiveQuestInfo(_quest);
        }
        else
        {
            activeQuest = null;

            activeQuest = _quest;

            updateActiveQuestInfo(_quest);
        }
    }

    public void updateActiveQuestInfo(Quest _quest)
    {
        GUIController.instance.activeQuestName.text = _quest.questName;

        switch (_quest.questCompleteType)
        {
            case QuestCompleteType.Location:
                float distance = 0;

                if (targetLocation != null)
                {
                    distance = Vector3.Distance(PlayerManager.instance.playerObject.transform.position, targetLocation.position);
                }
                else
                {
                    Debug.Log("No target location set.");
                }

                GUIController.instance.activeQuestObjective.text = "Distance to location: " + Mathf.Round(distance);
                break;
        }
    }

    private void createQuestPrefab(Quest _quest)
    {
        if (!GameObject.Find(_quest.questName))
        {
            GameObject createdPrefab = Instantiate(GUIController.instance.questPrefab, GUIController.instance.activeQuestsParent.transform);

            createdPrefab.name = _quest.questName;

            Button selectButton = createdPrefab.GetComponentInChildren<Button>();

            selectButton.onClick.AddListener(delegate { selectActiveQuest(_quest); });

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

    private void removeQuestPrefab(Quest _quest)
    {
        GameObject prefabToRemove = GameObject.Find(_quest.questName);

        if (prefabToRemove != null)
        {
            Destroy(prefabToRemove);
        }
    }
}
