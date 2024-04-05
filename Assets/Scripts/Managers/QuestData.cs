using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
    private NpcChatManager npcChatManager;
    private QuestManager questManager;
    private MiniGameClearCheck miniGame;

    private Inventory inventory;

    [Header("����Ʈ ������ ����")]
    [SerializeField, Tooltip("Npc�̸��� ��ǳ�� ������Ʈ")] private GameObject npcNameAndChat;
    [SerializeField, Tooltip("���� ��ư ������Ʈ")] private GameObject choiceButton;
    [SerializeField, Tooltip("��ȭ ���� ��ư ������Ʈ")] private GameObject talkChoiceButton;
    [SerializeField, Tooltip("����Ʈ ������ư")] private Button acceptButton;
    [SerializeField, Tooltip("����Ʈ ������ư")] private Button notAcceptButton;
    private TMP_Text chatWindowText; //Npc�� ���� ǥ���� �ؽ�Ʈ
    private TMP_Text npcNameText; //Npc�� �̸��� ǥ���� �ؽ�Ʈ
    private int chatIndex;  //���� ��ȭ�� ��������ֱ� ���� ����
    [SerializeField] private bool playerMoveStop = false; //�÷��̾��� �������� ���߰� �ϴ� ����
    [SerializeField] private Image npcImage;
    [SerializeField] private List<Sprite> npcSprites;

    private void Start()
    {
        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;

        inventory = Inventory.Instance;

        miniGame = MiniGameClearCheck.Instance;

        chatWindowText = npcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        npcNameText = npcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();

        acceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            npcNameAndChat.SetActive(false);
            choiceButton.SetActive(false);
            chatIndex = 100;
        });

        notAcceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            npcNameAndChat.SetActive(false);
            choiceButton.SetActive(false);
            chatIndex = 0;
            questManager.SetCurQuestIndex(0);
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
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"������";
            npcImage.sprite = npcSprites[0];

            if (chatIndex == 0)
            {
                questManager.SetCurQuestIndex(100);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"�ʹ� �������..";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatWindowText.text = $"������ ���߰� �ִµ� �� ���߰ھ��, �����ֽó���..?";
                questMiniGame("PuzzleGame", 100);
            }
            else if (chatIndex == 100)
            {
                npcNameAndChat.SetActive(true);
                playerMoveStop = true;
                questMiniGame("PuzzleGame", 100);
            }
            else if (chatIndex == 101)
            {
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(100);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
            }
        }
        else if (_npcIndex == 11 && _questIndex == 101 
            && questManager.QuestClearCheck(100) == true 
            && questManager.QuestClearCheck(101) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"�ɺ���";
            npcImage.sprite = npcSprites[1];
            talkChoiceButton.SetActive(false);

            if (chatIndex == 0)
            {
                questManager.SetCurQuestIndex(101);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"�� �� ������ �� �־�?";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatWindowText.text = $"���� �������� ������ �����;� �ϴµ� ���� ������, �����ٷ�?";
                choiceButton.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                questManager.SetCurQuestIndex(101);
                playerMoveStop = true;
                questClearIndex(100, 1, 100, $"���, �����ֱ�� ���ݾ�", $"���� ������ ������ �ʿ��ϸ� ���� �����ٰ�!");
            }
            else if (chatIndex == 101)
            {
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(101);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
            }
        }
        else if (_npcIndex == 10 && _questIndex == 102
            && questManager.QuestClearCheck(101) == true
            && questManager.QuestClearCheck(102) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"������";
            npcImage.sprite = npcSprites[0];

            if (chatIndex == 0)
            {
                questManager.SetCurQuestIndex(102);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"�����ǿ� ���ڰ� ����ֳ�..";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatWindowText.text = $"Ȥ��.., �� �� �����ֽ� �� �ֳ���..?";
                questMiniGame("RecipeGame", 102);
            }
            else if (chatIndex == 100)
            {
                npcNameAndChat.SetActive(true);
                playerMoveStop = true;
                questMiniGame("RecipeGame", 102);
            }
            else if (chatIndex == 101)
            {
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(102);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ����Ʈ�� ���� �̴ϰ����� �����Ű�� ���� �Լ�
    /// </summary>
    /// <param name="_miniGameSceneName"></param>
    /// <param name="_miniGameClearCheck"></param>
    private void questMiniGame(string _miniGameSceneName, int _miniGameClearCheck)
    {
        if (miniGame.GetSaveMiniCheckData(_miniGameClearCheck) == false)
        {
            questManager.SetCurQuestIndex(_miniGameClearCheck);

            acceptButton.onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync(_miniGameSceneName);
            });

            choiceButton.SetActive(true);
        }
        else
        {
            acceptButton.onClick.AddListener(() =>
            {
                playerMoveStop = false;
                npcNameAndChat.SetActive(false);
                choiceButton.SetActive(false);
                chatIndex = 100;
            });

            chatWindowText.text = $"�����մϴ�..!";
            chatIndex = 101;
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
            chatWindowText.text = _notClearChat;
        }
        else
        {
            inventory.QuestItem(_itemIndex, _itemQuantity);
            chatWindowText.text = _clearChat;
            questManager.SetQuestIndex(_addIndex);
            chatIndex = 101;
        }

        npcNameAndChat.SetActive(true);
    }

    /// <summary>
    /// �÷��̾ ��ȭ���� �������� �����ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool PlayerMoveStop()
    {
        return playerMoveStop;
    }

    public void SetChatIndex(int _chatIndex)
    {
        chatIndex = _chatIndex;
    }
}
