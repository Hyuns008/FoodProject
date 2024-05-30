using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CookingMusicManager : MonoBehaviour
{
    public static CookingMusicManager Instance;

    [Header("��ŷ ���� �Ŵ���")]
    [SerializeField, Tooltip("ĵ����")] private Canvas canvas;
    [SerializeField, Tooltip("��� ������")] private GameObject nodePrefab;

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

    public GameObject GetNodeObject()
    {
        return nodePrefab;
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }
}
