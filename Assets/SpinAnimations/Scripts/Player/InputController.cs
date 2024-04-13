using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class InputController : MonoBehaviour
{
    private GameManager gameManager;
    private NpcChatManager npcChatManager;
    private QuestManager questManager;
    private TIuBookManager tIuBookManager;
    private PlayerPosManager playerPosManager;

    private Inventory inventory;

    private Rigidbody2D rigid;
    private Vector3 moveVec;

    private Camera mainCam;
    private SkeletonAnimation skeletonAnim;

    [Header("�÷��̾��� �̵�����")]
    [SerializeField] private float moveSpeed;

    [Header("�÷��̾� ��ȣ�ۿ� ����")]
    [SerializeField] private CircleCollider2D interactionArea;

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

        inventory = Inventory.Instance;

        transform.position = playerPosManager.GetPlayerPos();

        skeletonAnim = GetComponent<SkeletonAnimation>();
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
        //mainCam.transform.position = transform.position + new Vector3(0f, 0f, mainCam.transform.position.z);
    }

    /// <summary>
    /// �÷��̾ �����̰� ���۽����ִ� �Լ�
    /// </summary>
    private void playerMove()
    {
        gameManager.SetSavePos(transform.position.x, transform.position.y);

        if (questManager.PlayerMoveStop() == true || npcChatManager.GetPlayerMoveStop() == true || gameManager.GetPlayerMoveStop() == true)
        {
            skeletonAnim.timeScale = 0;
            rigid.velocity = Vector3.zero;
            return;
        }

        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * moveSpeed;

        rigid.velocity = moveVec;

        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            float scale = transform.localScale.x;
            if (Input.GetAxisRaw("Horizontal") < 0f && scale > 0f)
            {
                scale *= -1;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0f && scale < 0f)
            {
                scale *= -1;
            }
            transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z); ;
            skeletonAnim.timeScale = 1;
        }
        else
        {
            skeletonAnim.timeScale = 0;
        }
    }
}
