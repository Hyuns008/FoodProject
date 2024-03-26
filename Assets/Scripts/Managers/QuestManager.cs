using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private QuestData questData;

    [Header("����Ʈ ����")]
    [SerializeField, Tooltip("���� Ŭ������ ����Ʈ")] private List<int> questIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        questData = transform.GetChild(0).GetComponent<QuestData>();
    }

    /// <summary>
    /// ����Ʈ�� Ŭ�����ߴ��� üũ�ϱ� ���� �Լ�
    /// </summary>
    /// <param name="_questIndex"></param>
    /// <returns></returns>
    public bool QuestClearCheck(int _questIndex)
    {
        int count = questIndex.Count;
        for (int i = 0; i < count; i++)
        {
            if (questIndex[i] == _questIndex)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// ����Ʈ�� �����ߴ��� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    /// <param name="_npcIndex"></param>
    /// <param name="_questIndex"></param>
    public void QuestAccept(int _npcIndex, int _questIndex)
    {
        questData.NpcQuestChapter1(_npcIndex, _questIndex);
    }

    /// <summary>
    /// Ŭ�������� �� Ŭ������ ����Ʈ�� ��ȣ�� �־� �� �Լ�
    /// </summary>
    /// <param name="_questIndex"></param>
    public void SetQuestIndex(int _questIndex)
    {
        questIndex.Add(_questIndex);
    }

    /// <summary>
    /// �÷��̾ ��ȭ���� �� �����̰� ������ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool PlayerMoveStop()
    {
        return questData.PlayerMoveStop();
    }
}
