using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("������ ����")]
    [SerializeField] private int itemIndex;

    public int ItemIndex()
    {
        return itemIndex;
    }
}
