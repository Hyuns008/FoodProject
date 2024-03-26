using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private QuestManager questManager;

    [SerializeField] private int npcIndex;
    [SerializeField] private List<int> questIndex;

    private void Start()
    {
        questManager = QuestManager.Instance;
    }

    /// <summary>
    /// ����Ʈ�� ������ �� �ְ� �ϴ� �Լ�
    /// </summary>
    public void NpcQuestChapter1()
    {
        for (int i = 0; i < questIndex.Count; i++)
        {
            questManager.QuestAccept(npcIndex, questIndex[i]);
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
}
