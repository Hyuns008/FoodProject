using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameManager gameManager;
    private QuestManager questManager;

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

        questManager = QuestManager.Instance;
    }

    private void Update()
    {
        playerInteraction();
        followCam();
        playerMove();
    }

    private void OnTrigger(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.Z) && collider.gameObject.layer == LayerMask.NameToLayer("Npc"))
        {

        }

        if (Input.GetKeyDown(KeyCode.X) && collider.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            Item itemSc = collider.gameObject.GetComponent<Item>();
            inventory.SetItem(itemSc.GetItemIndex(), itemSc.GetItemType(), itemSc.gameObject);
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
        moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized * moveSpeed;

        rigid.velocity = moveVec;
    }
}
