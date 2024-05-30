using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNode : MonoBehaviour
{
    [Header("��� ����")]
    [SerializeField, Tooltip("��� ��ȣ")] private int nodeNumber;

    private Image nodeColor;

    private void Awake()
    {
        nodeColor = GetComponent<Image>();
    }

    /// <summary>
    /// Ű �ڵ带 ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_number"></param>
    /// <returns></returns>
    public KeyCode GetKeyCode(int _number)
    {
        switch (_number)
        {
            case 0:
                nodeColor.color = Color.red;
                nodeNumber = _number;
                return KeyCode.UpArrow;
            case 1:
                nodeColor.color = Color.blue;
                nodeNumber = _number;
                return KeyCode.DownArrow;
            case 2:
                nodeColor.color = Color.yellow;
                nodeNumber = _number;
                return KeyCode.LeftArrow;
            case 3:
                nodeColor.color = Color.green;
                nodeNumber = _number;
                return KeyCode.RightArrow;
        }

        return KeyCode.None;
    }
}
