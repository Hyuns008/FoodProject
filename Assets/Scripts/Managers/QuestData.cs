using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
    private QuestManager questManager;

    private Inventory inventory;

    [Header("����Ʈ ������ ����")]
    [SerializeField, Tooltip("Npc�̸��� ��ǳ�� ������Ʈ")] private GameObject NpcNameAndChat;
    [SerializeField, Tooltip("���� ��ư ������Ʈ")] private GameObject ChoiceButtons;
    [SerializeField, Tooltip("����Ʈ ������ư")] private Button acceptButton;
    [SerializeField, Tooltip("����Ʈ ������ư")] private Button notAcceptButton;
    private TMP_Text ChatWindowText; //Npc�� ���� ǥ���� �ؽ�Ʈ
    private TMP_Text NpcNameText; //Npc�� �̸��� ǥ���� �ؽ�Ʈ
    private int chatIndex;  //���� ��ȭ�� ��������ֱ� ���� ����
    [SerializeField] private bool playerMoveStop = false; //�÷��̾��� �������� ���߰� �ϴ� ����

    private void Start()
    {
        questManager = QuestManager.Instance;

        inventory = Inventory.Instance;

        ChatWindowText = NpcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        NpcNameText = NpcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();

        acceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            NpcNameAndChat.SetActive(false);
            ChoiceButtons.SetActive(false);
            chatIndex = 100;
        });

        notAcceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            NpcNameAndChat.SetActive(false);
            ChoiceButtons.SetActive(false);
            chatIndex = 0;
        });
    }

    /// <summary>
    /// ����Ʈ é�� 1�� ����ϴ� �Լ�
    /// </summary>
    /// <param name="_npcIndex"></param>
    /// <param name="_questIndex"></param>
    public void NpcQuestChapter1(int _npcIndex, int _questIndex)
    {
        if (_npcIndex == 10 && _questIndex == 100 
            && questManager.QuestClearCheck(100) == false)
        {
            if (chatIndex == 0)
            {
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"�� �� ������";
                NpcNameText.text = $"�����";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                ChatWindowText.text = $"���� ���̴� �������� 22���� ������ ��";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(100, 22, 100, $"���� �� ���ؿ� �ž�?", $"����! ���� ���������� ����!");
            }
            else if (chatIndex == 101)
            {
                chatIndex--;
                NpcNameAndChat.SetActive(false);
            }
        }
        else if (_npcIndex == 11 && _questIndex == 101 
            && questManager.QuestClearCheck(100) == true 
            && questManager.QuestClearCheck(101) == false)
        {
            if (chatIndex == 0)
            {
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"�� �� ������";
                NpcNameText.text = $"�����";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                ChatWindowText.text = $"���� ���̴� �������� 12���� ������ ��";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(101, 12, 101, $"���� �� ���ؿ� �ž�?", $"����! ���� �������� ����!");
            }
            else if (chatIndex == 101)
            {
                chatIndex--;
                NpcNameAndChat.SetActive(false);
            }
        }
        else if (_npcIndex == 12 && _questIndex == 102
            && questManager.QuestClearCheck(101) == true
            && questManager.QuestClearCheck(102) == false)
        {
            if (chatIndex == 0)
            {
                playerMoveStop = true;
                chatIndex++;
                ChatWindowText.text = $"�� �� ������";
                NpcNameText.text = $"�����";
                NpcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                ChatWindowText.text = $"���� ���̴� �������� 6���� ������ ��";
                ChoiceButtons.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questClearIndex(102, 6, 102, $"���� �� ���ؿ� �ž�?", $"����! ���� ���������� ����!");
            }
            else if (chatIndex == 101)
            {
                playerMoveStop = false;
                chatIndex--;
                NpcNameAndChat.SetActive(false);
            }
        }
        else if (chatIndex == 0)
        {
            playerMoveStop = false;
            chatIndex = 0;
            NpcNameAndChat.SetActive(false);
        }
    }

    /// <summary>
    /// ����Ʈ �������� ��� ���̸� ����Ʈ�� Ŭ��� �ǵ��� �ϴ� �Լ�
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemQuantity"></param>
    /// <param name="_addIndex"></param>
    /// <param name="_notClearChat"></param>
    /// <param name="_clearChat"></param>
    private void questClearIndex(int _itemIndex, int _itemQuantity, int _addIndex, string _notClearChat, 
        string _clearChat)
    {
        chatIndex++;
        if (inventory.QuestItemCheck(_itemIndex, _itemQuantity) == false)
        {
            ChatWindowText.text = _notClearChat;
        }
        else
        {
            inventory.QuestItem(_itemIndex, _itemQuantity);
            ChatWindowText.text = _clearChat;
            questManager.SetQuestIndex(_addIndex);
            chatIndex = 0;
        }

        NpcNameAndChat.SetActive(true);
    }

    /// <summary>
    /// �÷��̾ ��ȭ���� �������� �����ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool PlayerMoveStop()
    {
        return playerMoveStop;
    }
}
