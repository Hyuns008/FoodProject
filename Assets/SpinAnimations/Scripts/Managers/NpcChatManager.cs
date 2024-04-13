using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcChatManager : MonoBehaviour
{
    public static NpcChatManager Instance;

    [SerializeField] private int npcIndex;
    [SerializeField] private bool npcTalkCheck = false;
    private bool playerMoveStop = false;
    [SerializeField] private bool questCheck = false;
    private Npc npc;

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

    public void SetNpcIndex(int _npcIndex)
    {
        npcIndex = _npcIndex;
    }

    public int GetNpcIndex()
    {
        return npcIndex;
    }

    public void SetNpcTalkCheck(bool _npcTalkCheck)
    {
        npcTalkCheck = _npcTalkCheck;
    }

    public bool GetNpcTalkCheck()
    {
        return npcTalkCheck;
    }

    public void SetQuestCheck(bool _questCheck)
    {
        questCheck = _questCheck;
    }

    public bool GetQuestCheck()
    {
        return questCheck;
    }

    public void SetPlayerMoveStop(bool _playerMoveStop)
    {
        playerMoveStop = _playerMoveStop;
    }

    public void SetNpc(Npc _npc)
    {
        npc = _npc;
    }

    public Npc GetNpc()
    {
        return npc;
    }

    /// <summary>
    /// �÷��̾ ��ȭ���� �������� �����ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool GetPlayerMoveStop()
    {
        return playerMoveStop;
    }
}