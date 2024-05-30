using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    private PuzzleGameManager puzzleGameManager;

    private RectTransform rectTrs;

    [Header("���� ���� ����")]
    [SerializeField, Tooltip("������ ����")] private int pieceIndex;
    private bool clickCheck = false;
    [SerializeField, Tooltip("������ ���ý� �׵θ�")] private GameObject frameObj;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (puzzleGameManager.GameClear() == true)
        {
            return;
        }

        if (puzzleGameManager.PieceObjCheck() == true)
        {
            if (clickCheck == false)
            {
                clickCheck = true;
                puzzleGameManager.SetPiece(gameObject);
            }

            frameObj.SetActive(clickCheck);
        }
    }

    private void Awake()
    {
        puzzleGameManager = PuzzleGameManager.Instance;

        rectTrs = GetComponent<RectTransform>();
    }

    /// <summary>
    /// ������ �������� �� �Ŵ������� �޾ƿ� �ε���
    /// </summary>
    /// <param name="_pieceIndex"></param>
    public void SetPieceIndex(int _pieceIndex)
    {
        pieceIndex = _pieceIndex;
    }

    /// <summary>
    /// ������ �ε����� ��ȯ�ϴ�  �Լ�
    /// </summary>
    /// <returns></returns>
    public int GetPieceIndex() 
    {
        return pieceIndex;
    }

    /// <summary>
    /// ������ ������ �� ��ġ�� �������� �Լ�
    /// </summary>
    /// <param name="_rectTrs"></param>
    public void SetRectTrs(Vector3 _rectTrs)
    {
        rectTrs.localPosition = _rectTrs;
    }

    /// <summary>
    /// ������ ��ġ�� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public RectTransform GetRectTrs()
    {
        return rectTrs;
    }

    /// <summary>
    /// ������ �ٲ���� Ȯ�����ִ� �Լ�
    /// </summary>
    public void ChangeTrue()
    {
        clickCheck = false;
        frameObj.SetActive(clickCheck);
    }

    /// <summary>
    /// ������ �ε����� Ȯ���ϱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public int PieceIndexCheck()
    {
        return pieceIndex;
    }
}
