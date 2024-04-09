using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public class QuestIndexData
    {
        public List<int> questIndex = new List<int>();
        public int curQuestIndex;
    }

    private QuestIndexData questIndexData = new QuestIndexData();

    private NpcChatManager npcChatManager;
    private QuestData questData;

    [Header("����Ʈ ����")]
    [SerializeField, Tooltip("���� Ŭ������ ����Ʈ")] private List<int> questIndex;
    [SerializeField] private int curQuestIndex;

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
        npcChatManager = NpcChatManager.Instance;

        questData = transform.GetChild(0).GetComponent<QuestData>();

        if (PlayerPrefs.GetString("saveQuestIndex") != string.Empty)
        {
            string getQeustIndex = PlayerPrefs.GetString("saveQuestIndex");
            //questIndexData = JsonUtility.FromJson<QuestIndexData>(getQeustIndex);
            questIndexData = JsonConvert.DeserializeObject<QuestIndexData>(getQeustIndex);
            if (questIndexData.questIndex != null && questIndexData.questIndex.Count != 0)
            {
                for (int i = 0; i < questIndexData.questIndex.Count; i++)
                {
                    questIndex.Add(questIndexData.questIndex[i]);
                }
            }

            curQuestIndex = questIndexData.curQuestIndex;

            if (curQuestIndex != 0)
            {
                npcChatManager.SetQuestCheck(true);
                questData.SetChatIndex(100);
            }
        }
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

    public List<int> GetQuestClearIndex()
    {
        return questIndex;
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
        questIndexData.questIndex.Add(_questIndex);

        //string setQuestIndex = JsonUtility.ToJson(new JosnConvert<int>(questIndexData.questIndex));
        string setQuestIndex = JsonConvert.SerializeObject(questIndexData); 
        PlayerPrefs.SetString("saveQuestIndex", setQuestIndex);
    }

    public void SetCurQuestIndex(int _curQuestIndex)
    {
        questIndexData.curQuestIndex = _curQuestIndex;
        curQuestIndex = _curQuestIndex;

        string setQuestIndex = JsonConvert.SerializeObject(questIndexData);
        PlayerPrefs.SetString("saveQuestIndex", setQuestIndex);
    }

    public int GetCurQuestIndex()
    {
        return curQuestIndex;
    }

    /// <summary>
    /// �÷��̾ ��ȭ���� �� �����̰� ������ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool PlayerMoveStop()
    {
        return questData.PlayerMoveStop();
    }

    //[System.Serializable]
    //public class JosnConvert<T>
    //{
    //    public List<T> questIndex;
    //    public JosnConvert(List<T> list) => this.questIndex = list;
    //}
}
