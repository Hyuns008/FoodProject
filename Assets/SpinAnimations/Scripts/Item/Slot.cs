using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    private ItemManager itemManager;

    [Header("���� ����")]
    [SerializeField, Tooltip("���� ������ �ִ� ������")] private int itemIndex;
    [SerializeField, Tooltip("���� ������ �ִ� ������ ����")] private int slotQuantity;
    private bool maxItem = false;
    private int _itemQuantity;

    [SerializeField, Tooltip("�������� ������ �̹���")] private Image itemImag;
    [SerializeField, Tooltip("������ ������ ǥ���� �ؽ�Ʈ")] private TMP_Text itemQuantityText;
    private bool setImage = false;

    private void Start()
    {
        inventory = Inventory.Instance;

        itemManager = ItemManager.Instance;

        if (itemIndex == 0)
        {
            itemQuantityText.text = "";
        }

        Color color = itemImag.color;
        color.a = 0;
        itemImag.color = color;
    }

    private void Update()
    {
        slotCheck();
        changeSprite();
    }

    /// <summary>
    /// ������ üũ�ϰ� �����ϴ� �Լ�
    /// </summary>
    private void slotCheck()
    {
        if (slotQuantity == 20)
        {
            if (maxItem == true)
            {
                return;
            }

            maxItem = true;
        }
        else if (slotQuantity < 20)
        {
            if (maxItem == false)
            {
                return;
            }

            maxItem = false;
        }

        if (itemIndex == 0)
        {
            if (itemImag.sprite == null)
            {
                return;
            }

            slotQuantity = 0;
            itemImag.sprite = null;
            Color color = itemImag.color;
            color.a = 0;
            itemImag.color = color;
            itemQuantityText.text = "";
        }

        if (slotQuantity <= 0)
        {
            if (itemImag.sprite == null)
            {
                return;
            }

            slotQuantity = 0;
            itemImag.sprite = null;
            Color color = itemImag.color;
            color.a = 0;
            itemImag.color = color;
            itemQuantityText.text = "";
        }
    }

    private void changeSprite()
    {
        if (inventory.InventoryObj().activeSelf == true && setImage == false)
        {
            if (itemIndex == 0 || slotQuantity == 0)
            {
                itemImag.sprite = null;
                Color color = itemImag.color;
                color.a = 0;
                itemImag.color = color;
                return;
            }

            itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            setImage = true;
        }
        else if (inventory.InventoryObj().activeSelf == false && setImage == true)
        {
            setImage = false;
        }
    }

    /// <summary>
    /// ���Կ� �������� �־��� �Լ�
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemObj"></param>
    public void SetSlot(int _itemIndex)
    {
        if (maxItem == true)
        {
            return;
        }

        if (itemIndex == 0)
        {
            itemIndex = _itemIndex;
            slotQuantity = 1;
            if (inventory.InventoryObj().activeSelf == true)
            {
                itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            }
            itemQuantityText.text = slotQuantity.ToString();
        }
        else
        {
            if (inventory.InventoryObj().activeSelf == true)
            {
                itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            }
            ++slotQuantity;
            itemQuantityText.text = slotQuantity.ToString();
        }
    }

    /// <summary>
    /// ���Կ� �������� �־��� �Լ�
    /// </summary>
    /// <param name="_itemIndex"></param>
    /// <param name="_itemObj"></param>
    public void SetSlot(int _itemIndex, GameObject _itemObj)
    {
        if (maxItem == true)
        {
            return;
        }

        if (itemIndex == 0)
        {
            itemIndex = _itemIndex;
            slotQuantity = 1;
            if (inventory.InventoryObj().activeSelf == true)
            {
                itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            }
            itemQuantityText.text = slotQuantity.ToString();
            Destroy(_itemObj);
        }
        else
        {
            if (inventory.InventoryObj().activeSelf == true)
            {
                itemImag.sprite = itemManager.GetItemSprite(itemIndex);
            }
            ++slotQuantity;
            itemQuantityText.text = slotQuantity.ToString();
            Destroy(_itemObj);
        }
    }

    /// <summary>
    /// ���Կ� �ִ� �������� ������ �Լ�
    /// </summary>
    /// <returns></returns>
    public int GetItemIndex()
    {
        return itemIndex;
    }

    /// <summary>
    /// ���Կ� �ִ� �������� ������ ������ �Լ�
    /// </summary>
    /// <returns></returns>
    public int GetSlotQuantity()
    {
        return slotQuantity;
    }

    /// <summary>
    /// ����Ʈ�� �ʿ��� �������� ����� ���� �Լ�
    /// </summary>
    /// <param name="_slotQuantity"></param>
    /// <returns></returns>
    public int QuestItem(int _slotQuantity)
    {
        for (int i = 0; i < _slotQuantity; i++)
        {
            slotQuantity -= 1;
            itemQuantityText.text = slotQuantity.ToString();

            ++_itemQuantity;

            if (slotQuantity <= 0)
            {
                itemIndex = 0;
                slotQuantity = 0;
                itemQuantityText.text = "";

                return _slotQuantity -= _itemQuantity;
            }
        }

        return 0;
    }

    /// <summary>
    /// �����ߴ� �����͸� �޾ƿ� �Լ�
    /// </summary>
    public void SetSlotData(int _itemIndex, int _slotQuantity)
    {
        itemIndex = _itemIndex;
        slotQuantity = _slotQuantity;

        itemQuantityText.text = slotQuantity.ToString();
    }
}
