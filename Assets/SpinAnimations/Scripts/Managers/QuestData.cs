using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
    private GameManager gameManager;
    private QuestManager questManager;
    private MiniGameClearCheck miniGame;
    private NpcChatManager npcChatManager;

    private Inventory inventory;

    [Header("����Ʈ ������ ����")]
    [SerializeField, Tooltip("Npc�̸��� ��ǳ�� ������Ʈ")] private GameObject npcNameAndChat;
    [SerializeField, Tooltip("���� ��ư ������Ʈ")] private GameObject choiceButton;
    [SerializeField, Tooltip("��ȭ ���� ��ư ������Ʈ")] private GameObject talkChoiceButton;
    [SerializeField, Tooltip("����Ʈ ������ư")] private Button acceptButton;
    [SerializeField, Tooltip("����Ʈ ������ư")] private Button notAcceptButton;
    private TMP_Text chatWindowText; //Npc�� ���� ǥ���� �ؽ�Ʈ
    private TMP_Text npcNameText; //Npc�� �̸��� ǥ���� �ؽ�Ʈ
    [SerializeField] private int chatIndex;  //���� ��ȭ�� ��������ֱ� ���� ����
    [SerializeField] private bool playerMoveStop = false; //�÷��̾��� �������� ���߰� �ϴ� ����
    [SerializeField] private Image playerImage;
    [SerializeField] private Image npcImage;
    [SerializeField] private List<Sprite> playerSprites;
    [SerializeField] private List<Sprite> npcBoySprites;
    [SerializeField] private List<Sprite> npcGirlSprites;

    private void Start()
    {
        gameManager = GameManager.Instance;

        questManager = QuestManager.Instance;

        inventory = Inventory.Instance;

        miniGame = MiniGameClearCheck.Instance;

        npcChatManager = NpcChatManager.Instance;

        chatWindowText = npcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        npcNameText = npcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();

        acceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            npcNameAndChat.SetActive(false);
            choiceButton.SetActive(false);
            npcChatManager.SetQuestCheck(false);
            chatIndex = 100;
        });

        notAcceptButton.onClick.AddListener(() =>
        {
            playerMoveStop = false;
            npcNameAndChat.SetActive(false);
            choiceButton.SetActive(false);
            npcChatManager.SetQuestCheck(false);
            chatIndex = 0;
            questManager.SetCurQuestIndex(0);
        });

        playerImage.gameObject.SetActive(false);
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
            npcNameText.text = $"�ҳ�";
            npcImage.sprite = npcBoySprites[4];

            if (chatIndex == 0)
            {
                npcImage.sprite = npcBoySprites[4];
                npcImage.gameObject.SetActive(true);
                questManager.SetCurQuestIndex(100);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $".....";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                npcImage.sprite = npcBoySprites[2];
                chatIndex++;
                chatWindowText.text = $".....";
            }
            else if (chatIndex == 2)
            {
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false);
                chatIndex++;
                chatWindowText.text = $"���� �� �־�?";
            }
            else if (chatIndex == 3)
            {
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);
                chatIndex++;
                chatWindowText.text = $".......������";
            }
            else if (chatIndex == 4)
            {
                chatIndex++;
                chatWindowText.text = $"�ϼ� ���ϰھ�...";
            }
            else if (chatIndex == 5)
            {
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false);
                chatIndex++;
                npcNameText.text = $"��Ÿ";
                chatWindowText.text = $"����?";
            }
            else if (chatIndex == 6)
            {
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);
                chatIndex++;
                chatWindowText.text = $"ȥ�ڼ��� �ʹ� ����...";
            }
            else if (chatIndex == 7)
            {
                chatIndex++;
                questMiniGame("PuzzleGame", 100, "�ʹ� �����..., �ٽ� �����ٰž�?", "...�����! ���п� ������ �ϼ��ƾ�");
                chatWindowText.text = $"������ �� �־�?";
            }
            else if (chatIndex == 100)
            {
                npcImage.sprite = npcBoySprites[0];
                npcNameAndChat.SetActive(true);
                playerMoveStop = true;
                questMiniGame("PuzzleGame", 100, "�ʹ� �����..., �ٽ� �����ٰž�?", "...�����! ���п� ������ �ϼ��ƾ�");
            }
            else if (chatIndex == 101)
            {
                npcImage.sprite = npcBoySprites[3];
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(100);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
                npcChatManager.SetQuestCheck(false);
            }
        }
        else if (_npcIndex == 11 && _questIndex == 101 
            && questManager.QuestClearCheck(100) == true 
            && questManager.QuestClearCheck(101) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"�ҳ�";
            npcImage.sprite = npcGirlSprites[6];

            if (chatIndex == 0)
            {
                npcImage.sprite = npcGirlSprites[6];
                npcImage.gameObject.SetActive(true);
                questManager.SetCurQuestIndex(101);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"��Ÿ��... ū�ϳ����....";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatIndex++;
                chatWindowText.text = $"���� ���ָӴϲ��� ġ� �ʿ��ϴٰ� �ϼ̴µ�";
            }
            else if (chatIndex == 2)
            {
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false);
                chatIndex++;
                chatWindowText.text = $"���� �����ع��Ⱦ��...";
            }
            else if (chatIndex == 3)
            {
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);
                chatIndex++;
                chatWindowText.text = $"�����ֽ� �� �����Ű���...?";
                choiceButton.SetActive(true);
            }
            else if (chatIndex == 100)
            {
                playerMoveStop = true;
                questClearIndex(100, 5, $"ġ� �����ؿ�...", $"��Ÿ��...! �����ؿ�!!!");
            }
            else if (chatIndex == 101)
            {
                playerMoveStop = false;
                chatIndex--;
                npcNameAndChat.SetActive(false);
                npcChatManager.SetQuestCheck(false);
            }
            else if (chatIndex == 201)
            {
                npcImage.sprite = npcGirlSprites[2];
                questManager.SetCurQuestIndex(0);
                playerMoveStop = false;
                chatIndex = 0;
                questManager.SetQuestIndex(101);
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
                npcChatManager.SetQuestCheck(false);
            }
        }
        else if (_npcIndex == 10 && _questIndex == 102
            && questManager.QuestClearCheck(101) == true
            && questManager.QuestClearCheck(102) == false)
        {
            talkChoiceButton.SetActive(false);
            npcNameText.text = $"�ҳ�";
            npcImage.sprite = npcBoySprites[4];

            if (chatIndex == 0)
            {
                npcImage.sprite = npcBoySprites[4];
                npcImage.gameObject.SetActive(true);
                questManager.SetCurQuestIndex(102);
                playerMoveStop = true;
                chatIndex++;
                chatWindowText.text = $"��Ÿ...";
                npcNameAndChat.SetActive(true);
            }
            else if (chatIndex == 1)
            {
                chatIndex++;
                chatWindowText.text = $"......������";
            }
            else if (chatIndex == 2)
            {
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false);
                chatIndex++;
                chatWindowText.text = $"���� ���̾�?";
            }
            else if (chatIndex == 3)
            {
                npcImage.sprite = npcBoySprites[2];
                playerImage.gameObject.SetActive(false);
                npcImage.gameObject.SetActive(true);
                chatIndex++;
                chatWindowText.text = $"���� ���̸� �ϴٰ�...";
            }
            else if (chatIndex == 4)
            {
                chatIndex++;
                chatWindowText.text = $"���� ���ָӴ� �����ǿ� ��ƹ��Ⱦ�...";
            }
            else if (chatIndex == 5)
            {
                chatIndex++;
                chatWindowText.text = $"���ָӴϲ� ȥ�� �� ����....";
            }
            else if (chatIndex == 6)
            {
                chatIndex++;
                questMiniGame("RecipeGame", 102, "ȥ���� ���� �ʾ�..., �ѹ��� �� ������ �� �־�?", "�����༭ ���� ��Ÿ...!");
                chatWindowText.text = $"������";
            }
            else if (chatIndex == 100)
            {
                npcNameAndChat.SetActive(true);
                playerMoveStop = true;
                questMiniGame("RecipeGame", 102, "ȥ���� ���� �ʾ�..., �ѹ��� �� ������ �� �־�?", "�����༭ ���� ��Ÿ...!");
            }
            else if (chatIndex == 101)
            {
                npcImage.sprite = npcBoySprites[3];
                questManager.SetCurQuestIndex(0);
                questManager.SetQuestIndex(102);
                playerMoveStop = false;
                chatIndex = 0;
                choiceButton.SetActive(false);
                npcNameAndChat.SetActive(false);
                npcChatManager.SetQuestCheck(false);
            }
        }
    }

    /// <summary>
    /// ����Ʈ�� ���� �̴ϰ����� �����Ű�� ���� �Լ�
    /// </summary>
    /// <param name="_miniGameSceneName"></param>
    /// <param name="_miniGameClearCheck"></param>
    private void questMiniGame(string _miniGameSceneName, int _miniGameClearCheck, string _talkReTryText, string _clearTalkText)
    {
        if (miniGame.GetSaveMiniCheckData(_miniGameClearCheck) == false)
        {
            questManager.SetCurQuestIndex(_miniGameClearCheck);

            acceptButton.onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync(_miniGameSceneName);
            });

            npcImage.sprite = npcBoySprites[2];

            chatWindowText.text = _talkReTryText;

            choiceButton.SetActive(true);

            npcChatManager.SetQuestCheck(false);
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

            chatWindowText.text = _clearTalkText;
            chatIndex = 101;
        }

        gameManager.SetSaveCheck(true);
    }

    /// <summary>
    /// ����Ʈ �������� ��� ���̸� ����Ʈ�� Ŭ��� �ǵ��� �ϴ� �Լ�
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemQuantity"></param>
    /// <param name="_addIndex"></param>
    /// <param name="_notClearChat"></param>
    /// <param name="_clearChat"></param>
    private void questClearIndex(int _itemIndex, int _itemQuantity, string _notClearChat, 
        string _clearChat)
    {
        chatIndex++;
        if (inventory.QuestItemCheck(_itemIndex, _itemQuantity) == false)
        {
            chatWindowText.text = _notClearChat;
        }
        else
        {
            chatIndex = 201;
            inventory.QuestItem(_itemIndex, _itemQuantity);
            chatWindowText.text = _clearChat;
        }

        npcNameAndChat.SetActive(true);

        gameManager.SetSaveCheck(true);
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
