using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNode : MonoBehaviour
{
    private SkeletonAnimation spineAnim;

    [Header("��� ����")]
    [SerializeField, Tooltip("��� ��ȣ")] private int nodeNumber;

    private void Start()
    {
        spineAnim = GetComponent<SkeletonAnimation>();
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
                nodeNumber = _number;
                return KeyCode.UpArrow;
            case 1:
                nodeNumber = _number;
                return KeyCode.DownArrow;
            case 2:
                nodeNumber = _number;
                return KeyCode.LeftArrow;
            case 3:
                nodeNumber = _number;
                return KeyCode.RightArrow;
        }

        return KeyCode.None;
    }

    public SkeletonAnimation SpineAnim()
    {
        return spineAnim;
    }

    public int GetNumber()
    {
        return nodeNumber;
    }
}
