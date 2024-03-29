using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public class InventorySlotData
    {
        public List<int> itemIndex = new List<int>();
        public List<int> itemQuantity = new List<int>();
    }

    private InventorySlotData inventorySlotData = new InventorySlotData();

    private QuestManager questManager;

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

        for (int i = 0; i < 20; i++)
        {
            inventorySlotData.itemIndex.Add(0);
            inventorySlotData.itemQuantity.Add(0);
        }
    }

    private void Start()
    {
        questManager = QuestManager.Instance;

        setInventoryData();
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
        if (Input.GetKeyDown(KeyCode.I) && questManager.PlayerMoveStop() == false)
        {
            bool invenOnOff = inventory == inventory.activeSelf ? false : true;
            inventory.SetActive(invenOnOff);
        }
    }


    /// <summary>
    /// �κ��丮�� ����� ���� �ҷ����� �Լ�
    /// </summary>
    private void setInventoryData()
    {
        if (PlayerPrefs.GetString("inventoryData") != string.Empty)
        {
            string getInventoryItem = PlayerPrefs.GetString("inventoryData");
            inventorySlotData = JsonConvert.DeserializeObject<InventorySlotData>(getInventoryItem);

            if (inventorySlotData != null && inventorySlotData.itemIndex.Count != 0
                && inventorySlotData.itemQuantity.Count != 0)
            {
                for (int i = 0; i < slot.Count; i++)
                {
                    Slot slotSc = slot[i];
                    slotSc.SetSlotData(inventorySlotData.itemIndex[i], inventorySlotData.itemQuantity[i]);
                }
            }
        }
    }

    /// <summary>
    /// ���Կ� �ִ� �����͸� �����ϱ� ���� �Լ�
    /// </summary>
    private void saveInventory()
    {
        string setInventoryItem = JsonConvert.SerializeObject(inventorySlotData);
        PlayerPrefs.SetString("inventoryData", setInventoryItem);
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
                inventorySlotData.itemIndex[i] = slotSc.GetItemIndex();
                inventorySlotData.itemQuantity[i] = slotSc.GetSlotQuantity();
                saveInventory();
                return;
            }
            else if (slotSc.GetItemIndex() == 0 && slotSc.GetSlotQuantity() < maxQuantiry)
            {
                slotSc.SetSlot(_itemIndex, _itemObj);
                inventorySlotData.itemIndex[i] = slotSc.GetItemIndex();
                inventorySlotData.itemQuantity[i] = slotSc.GetSlotQuantity();
                saveInventory();
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
    public bool QuestItemCheck(int _itemIndex, int _itemQuantity)
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

            inventorySlotData.itemIndex[i] = slotSc.GetItemIndex();
            inventorySlotData.itemQuantity[i] = slotSc.GetSlotQuantity();
            saveInventory();
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

            inventorySlotData.itemIndex[i] = slotSc.GetItemIndex();
            inventorySlotData.itemQuantity[i] = slotSc.GetSlotQuantity();
            saveInventory();
        }
    }

    /// <summary>
    /// �ٸ� ��ũ��Ʈ���� ������Ʈ�� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject InventoryObj()
    {
        return inventory;
    }
}
