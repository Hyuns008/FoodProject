using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcChatData : MonoBehaviour
{
    private NpcChatManager npcChatManager;
    private QuestManager questManager;

    [Header("����Ʈ ������ ����")]
    [SerializeField, Tooltip("Npc�̸��� ��ǳ�� ������Ʈ")] private GameObject NpcNameAndChat;
    [SerializeField, Tooltip("���� ��ư ������Ʈ")] private GameObject ChoiceButtons;
    [SerializeField, Tooltip("��ȭ ��ư")] private Button talkButton;
    [SerializeField, Tooltip("����Ʈ ��ư")] private Button qeustButton;
    [SerializeField, Tooltip("��ȭ ���� ��ư")] private Button backButton;
    private TMP_Text ChatWindowText; //Npc�� ���� ǥ���� �ؽ�Ʈ
    private TMP_Text NpcNameText; //Npc�� �̸��� ǥ���� �ؽ�Ʈ
    private int chatIndex;  //���� ��ȭ�� ��������ֱ� ���� ����
    [SerializeField] private bool playerMoveStop = false; //�÷��̾��� �������� ���߰� �ϴ� ����
    [SerializeField] private Image npcImage;
    [SerializeField] private List<Sprite> npcSprites;

    private void Awake()
    {
        talkButton.onClick.AddListener(() => 
        {
            chatIndex = 100;
            if (npcChatManager.GetNpcIndex() == 10)
            {
                ChatWindowText.text = ".....";
            }
            else if (npcChatManager.GetNpcIndex() == 11)
            {
                ChatWindowText.text = "������ �ߺ�Ź�����!";
            }
            ChoiceButtons.SetActive(false);
        });

        qeustButton.onClick.AddListener(() =>
        {
            npcChatManager.SetPlayerMoveStop(false);
            chatIndex = 0;
            npcChatManager.SetQuestCheck(true);
            if (npcChatManager.GetQuestCheck() == true)
            {
                for (int i = 0; i < npcChatManager.GetNpc().GetQuestIndex().Count; i++)
                {
                    questManager.QuestAccept(npcChatManager.GetNpc().GetNpcIndex(), npcChatManager.GetNpc().GetQuestIndex()[i]);
                }
            }
        });

        backButton.onClick.AddListener(() =>
        {
            npcChatManager.SetPlayerMoveStop(false);
            chatIndex = 0;
            NpcNameAndChat.SetActive(false);
            ChoiceButtons.SetActive(false);
        });

        NpcNameAndChat.SetActive(false);
        ChoiceButtons.SetActive(false);
    }

    private void Start()
    {
        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;

        ChatWindowText = NpcNameAndChat.transform.Find("ChatWindow/ChatWindowText").GetComponent<TMP_Text>();
        NpcNameText = NpcNameAndChat.transform.Find("NpcName/NpcNameText").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        NpcChat();
    }

    /// <summary>
    /// Npc�� ��ȭ �����͸� ���� �Լ�
    /// </summary>
    /// <param name="_npcIndex"></param>
    /// <param name="_questIndex"></param>
    private void NpcChat()
    {
        if (npcChatManager.GetNpcTalkCheck() == true)
        {
            if (npcChatManager.GetNpcIndex() == 10)
            {
                NpcNameText.text = "�ҳ�";
                npcImage.sprite = npcSprites[0];
                
                if (chatIndex == 0)
                {
                    npcChatManager.SetPlayerMoveStop(true);
                    NpcNameAndChat.SetActive(true);
                    chatIndex++;
                    ChatWindowText.text = ".....";
                }
                else if (chatIndex == 1)
                {
                    ChoiceButtons.SetActive(true);
                    ChatWindowText.text = ".....";
                }
                else if (chatIndex == 100)
                {
                    chatIndex++;
                    ChatWindowText.text = ".....";
                }
                else if (chatIndex == 101)
                {
                    npcChatManager.SetPlayerMoveStop(false);
                    chatIndex = 0;
                    NpcNameAndChat.SetActive(false);
                    ChoiceButtons.SetActive(false);
                }
            }
            else if (npcChatManager.GetNpcIndex() == 11)
            {
                NpcNameText.text = "�ҳ�";
                npcImage.sprite = npcSprites[1];

                if (chatIndex == 0)
                {
                    npcChatManager.SetPlayerMoveStop(true);
                    NpcNameAndChat.SetActive(true);
                    chatIndex++;
                    ChatWindowText.text = "�ȳ��ϼ���!";
                }
                else if (chatIndex == 1)
                {
                    ChoiceButtons.SetActive(true);
                    ChatWindowText.text = "���� ���̽Ű���?";
                }
                else if (chatIndex == 100)
                {
                    chatIndex++;
                    ChatWindowText.text = "...!";
                }
                else if (chatIndex == 101)
                {
                    npcChatManager.SetPlayerMoveStop(false);
                    chatIndex = 0;
                    NpcNameAndChat.SetActive(false);
                    ChoiceButtons.SetActive(false);
                }
            }

            npcChatManager.SetNpcTalkCheck(false);
        }
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
