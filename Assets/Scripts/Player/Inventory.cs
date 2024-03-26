using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("�κ��丮")]
    [SerializeField, Tooltip("�κ��丮 UI������Ʈ")] private GameObject inventory;
    [SerializeField, Tooltip("�κ��丮�� ���Դ� �ִ� ����")] private int maxQuantiry;
    [SerializeField, Tooltip("���� ����Ʈ")] private List<Slot> slot;
    private int questItems; //����Ʈ �����۵�
    private int qeustItemIndex; //����Ʈ �������� ��ȣ

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
        inventory.SetActive(false);
    }

    private void Update()
    {
        inventoryOnOff();
    }

    /// <summary>
    /// �κ��丮�� ���� ų �� �ְ� �ϴ� �Լ�
    /// </summary>
    private void inventoryOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            bool invenOnOff = inventory == inventory.activeSelf ? false : true;
            inventory.SetActive(invenOnOff);
        }
    }

    /// <summary>
    /// �������� �־��� �Լ�
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemType"></param>
    /// <param name="_itemObj"></param>
    public void SetItem(int _itemIndex, Item.ItemType _itemType, GameObject _itemObj)
    {
        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == _itemIndex && slotSc.GetSlotQuantity() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                return;
            }
            else if (slotSc.GetItemIndex() == 0 && slotSc.GetSlotQuantity() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                return;
            }
        }
    }

    /// <summary>
    /// ����Ʈ �������� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemQuantity"></param>
    /// <returns></returns>
    public bool QuestItemCheck(int _itemIndex , int _itemQuantity)
    {
        qeustItemIndex = _itemIndex;

        int itmeQuantity = 0;

        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == qeustItemIndex)
            {
                itmeQuantity += slotSc.GetSlotQuantity();
            }
        }

        return itmeQuantity >= _itemQuantity;
    }

    /// <summary>
    /// ����Ʈ �������� ����� ���� �Լ�
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_questItem"></param>
    public void QuestItem(int _itemIndex, int _questItem)
    {
        questItems = _questItem;

        for (int i = 0; i < slot.Count; i++)
        {
            Slot slotSc = slot[i];

            if (slotSc.GetItemIndex() == _itemIndex && slotSc.GetSlotQuantity() != 0)
            {
                if (questItems == 0)
                {
                    return;
                }

                questItems = slotSc.QuestItem(questItems);
            }
        }
    }
}
