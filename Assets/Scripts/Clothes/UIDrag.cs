using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ClothesManager clothesManager;

    [SerializeField] private int type;
    [SerializeField] private int index;
    [Space]
    [SerializeField] private RectTransform parentRectTrs; //�θ� ��ƮƮ������
    [SerializeField] private float posCheck;
    private Vector2 mousePos; //���콺 ������

    private float screenWidth; //��ũ���� ���� ���̸� ����ϱ� ���� ����
    private float screenHeight; //��ũ���� ���� ���̸� ����ϱ� ���� ����
    private Vector2 trsPos;
    [SerializeField] private Vector2 proportion;

    private CanvasGroup canvasGroup; //ĵ�����׷�

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        mousePos.x = transform.position.x - eventData.position.x;
        mousePos.y = transform.position.y - eventData.position.y;

        parentRectTrs.SetParent(clothesManager.GetCanvas().transform);
        parentRectTrs.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        parentRectTrs.position = eventData.position + new Vector2(0f, posCheck);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (transform.position.x >= screenWidth ||
            transform.position.x <= 0 ||
            transform.position.y >= screenHeight ||
            transform.position.y <= 0)
        {
            parentRectTrs.position = new Vector3((screenWidth * proportion.x), (screenHeight * proportion.y));
        }

        canvasGroup.blocksRaycasts = true;
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        clothesManager = ClothesManager.Instance;

        screenWidth = Screen.width;
        screenHeight = Screen.height;

        trsPos = new Vector2((screenWidth * proportion.x), (screenHeight * proportion.y));
        parentRectTrs.position = trsPos;
    }

    public RectTransform GetParentTrs()
    {
        return parentRectTrs;
    }

    public int GetTypeCheck()
    {
        return type;
    }

    public int GetIndexCheck()
    {
        return index;
    }

    public Vector2 GetTrs()
    {
        return trsPos;
    }
}