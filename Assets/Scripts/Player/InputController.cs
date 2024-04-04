using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameManager gameManager;
    private NpcChatManager npcChatManager;
    private QuestManager questManager;
    private TIuBookManager tIuBookManager;
    private PlayerPosManager playerPosManager;

    private Rigidbody2D rigid;
    private Vector3 moveVec;

    private Camera mainCam;

    [Header("�÷��̾��� �̵�����")]
    [SerializeField] private float moveSpeed;

    [Header("�÷��̾� ��ȣ�ۿ� ����")]
    [SerializeField] private CircleCollider2D interactionArea;

    [SerializeField] private Inventory inventory;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        mainCam = Camera.main;

        gameManager = GameManager.Instance;

        npcChatManager = NpcChatManager.Instance;

        questManager = QuestManager.Instance;

        tIuBookManager = TIuBookManager.Instance;

        playerPosManager = PlayerPosManager.Instance;

        transform.position = playerPosManager.GetPlayerPos();
    }

    private void Update()
    {
        playerInteraction();
        followCam();
        playerMove();
    }

    /// <summary>
    /// ��ȣ�ۿ��� ���� �ݶ��̴��� ���� �۵������ִ� �Լ�
    /// </summary>
    /// <param name="collider"></param>
    private void OnTrigger(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.Space) && collider.gameObject.layer == LayerMask.NameToLayer("Npc"))
        {
            Npc npcSc = collider.gameObject.GetComponent<Npc>();
            npcSc.NpcTalk(true);
            tIuBookManager.SetNpcIdCheck(npcSc.GetNpcIndex());
        }

        if (Input.GetKeyDown(KeyCode.Space) && collider.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Item itemSc = collider.gameObject.GetComponent<Item>();
            inventory.SetItem(itemSc.GetItemIndex(), itemSc.GetItemType(), itemSc.gameObject);
            tIuBookManager.SetItemIdCheck(itemSc.GetItemIndex());
        }
    }

    /// <summary>
    /// �÷��̾ ��ȣ�ۿ��� �� �� �ְ� �ϴ� �Լ�
    /// </summary>
    private void playerInteraction()
    {
        Collider2D npcInteractionColl = Physics2D.OverlapCircle(interactionArea.bounds.center, interactionArea.radius,
            LayerMask.GetMask("Npc"));

        Collider2D itemInteractionColl = Physics2D.OverlapCircle(interactionArea.bounds.center, interactionArea.radius,
           LayerMask.GetMask("Item"));

        if (npcInteractionColl != null)
        {
            OnTrigger(npcInteractionColl);
        }

        if (itemInteractionColl != null)
        {
            OnTrigger(itemInteractionColl);
        }
    }

    /// <summary>
    /// ī�޶� �÷��̾ ����ٴϰ� ����� �Լ�
    /// </summary>
    private void followCam()
    {
        mainCam.transform.position = transform.position + new Vector3(0f, 0f, -10f);
    }

    /// <summary>
    /// �÷��̾ �����̰� ���۽����ִ� �Լ�
    /// </summary>
    private void playerMove()
    {
        if (questManager.PlayerMoveStop() == true || npcChatManager.GetPlayerMoveStop() == true)
        {
            rigid.velocity = Vector3.zero;
            return;
        }

        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * moveSpeed;

        rigid.velocity = moveVec;
    }
}
