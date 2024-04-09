using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private NpcChatManager npcChatManager;
    private QuestManager questManager;

    private Npc npc;

    [SerializeField] private int npcIndex;
    [SerializeField] private List<int> questIndex;

    private void Awake()
    {
        npc = GetComponent<Npc>();
    }

    private void Start()
    {
        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;
    }

    /// <summary>
    /// Npc ��ȭ�� ������ �� �ְ� �ϴ� �Լ�
    /// </summary>
    public void NpcTalk(bool _npcTalkCheck)
    {
        int count = questIndex.Count;
        for (int i = 0; i < count; i++)
        {
            if (questManager.GetCurQuestIndex() == questIndex[i])
            {
                questManager.QuestAccept(npcIndex, questIndex[i]);
                return;
            }
            else if (npcChatManager.GetQuestCheck() == false)
            {
                npcChatManager.SetNpc(npc);
                npcChatManager.SetNpcTalkCheck(_npcTalkCheck);
                npcChatManager.SetNpcIndex(npcIndex);
            }
        }
    }

    /// <summary>
    /// Npc �ε����� �ٸ� ��ũ��Ʈ���� ������ �� �ְ� �ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public int GetNpcIndex()
    {
        return npcIndex;
    }

    public List<int> GetQuestIndex()
    {
        return questIndex;
    }
}
