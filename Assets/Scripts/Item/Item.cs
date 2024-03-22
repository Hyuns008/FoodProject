using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Ingredient,

    }

    [Header("������ ����")]
    [SerializeField] private ItemType itemType;
    [SerializeField] private int itemIndex;

    /// <summary>
    /// ������ Ÿ���� �Ѱ��ֱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public ItemType GetItemType()
    {
        return itemType;
    }

    /// <summary>
    /// ������ �ε����� �Ѱ��ֱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public int GetItemIndex()
    {
        return itemIndex;
    }
}
