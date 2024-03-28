using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField, Tooltip("���� ������ �ִ� ������")] private int itemIndex;
    [SerializeField, Tooltip("���� ������ �ִ� ������ ����")] private int slotQuantity;
    private bool maxItem = false;
    private int _itemQuantity;

    [SerializeField, Tooltip("�������� ������ �̹���")] private Image itemImage;
    [SerializeField, Tooltip("������ ������ ǥ���� �ؽ�Ʈ")] private TMP_Text itemQuantityText;

    private void Start()
    {
        if (itemIndex == 0)
        {
            itemQuantityText.text = "";
        }
    }

    private void Update()
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
            if (slotQuantity == 0)
            {
                return;
            }

            itemImage.sprite = null;
            slotQuantity = 0;
            itemQuantityText.text = slotQuantity.ToString();
        }

        if (slotQuantity <= 0)
        {
            if (itemImage.sprite == null)
            {
                return;
            }

            slotQuantity = 0;
            itemImage.sprite = null;
            itemQuantityText.text = "";
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
            SpriteRenderer itemSpR = _itemObj.GetComponent<SpriteRenderer>();
            Sprite itemSr = itemSpR.sprite;
            itemImage.sprite = itemSr;
            itemQuantityText.text = slotQuantity.ToString();
            Destroy(_itemObj);
        }
        else
        {
            SpriteRenderer itemSpR = _itemObj.GetComponent<SpriteRenderer>();
            Sprite itemSr = itemSpR.sprite;
            itemImage.sprite = itemSr;
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
                itemImage.sprite = null;
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
