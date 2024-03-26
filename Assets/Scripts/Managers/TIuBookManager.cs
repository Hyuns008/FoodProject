using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TIuBookManager : MonoBehaviour
{
    public static TIuBookManager Instance;

    [Header("���� ����")]
    [SerializeField] private List<GameObject> bookObj;
    [SerializeField] private List<GameObject> pageObj;
    [SerializeField] private Button bookOpen;
    [SerializeField] private Button itemPageOpen;
    [SerializeField] private Button RecipePageOpen;
    [SerializeField] private Button ResidentPageOpen;
    [SerializeField] private Button bookClose;
    [Space]
    [SerializeField] private List<int> itemsIndex;
    [SerializeField] private List<Image> itemImages;
    private bool itemCheck = false;
    [Space]
    [SerializeField] private List<int> RecipeIndex;
    [SerializeField] private List<Image> RecipeImages;
    private bool npcCheck = false;
    [Space]
    [SerializeField] private List<int> npcIndex;
    [SerializeField] private List<Image> npcImages;

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

        #region
        bookOpen.onClick.AddListener(() => 
        {
            bookObj[1].SetActive(false);
            bookObj[2].SetActive(true);
        });

        itemPageOpen.onClick.AddListener(() => 
        {
            pageObj[0].SetActive(false);
            pageObj[1].SetActive(true);
            pageObj[2].SetActive(false);
            pageObj[3].SetActive(false);
        });

        RecipePageOpen.onClick.AddListener(() =>
        {
            pageObj[0].SetActive(false);
            pageObj[1].SetActive(false);
            pageObj[2].SetActive(true);
            pageObj[3].SetActive(false);
        });

        ResidentPageOpen.onClick.AddListener(() =>
        {
            pageObj[0].SetActive(false);
            pageObj[1].SetActive(false);
            pageObj[2].SetActive(false);
            pageObj[3].SetActive(true);
        });

        bookClose.onClick.AddListener(() =>
        {
            pageObj[0].SetActive(true);
            pageObj[1].SetActive(false);
            pageObj[2].SetActive(false);
            pageObj[3].SetActive(false);
            bookObj[0].SetActive(false);
            bookObj[1].SetActive(true);
            bookObj[2].SetActive(false);
        });
        #endregion
    }

    private void Start()
    {
        bookObj[0].SetActive(false);
        bookObj[2].SetActive(false);
    }

    private void Update()
    {
        bookOnOff();
        openItem();
        openNpc();
    }

    /// <summary>
    /// ������ ���� �� �� �ִ� �Լ�
    /// </summary>
    private void bookOnOff()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bool onOff = bookObj[0] == bookObj[0].activeSelf ? false : true;
            bookObj[0].SetActive(onOff);

            if (onOff == false)
            {
                pageObj[0].SetActive(true);
                pageObj[1].SetActive(false);
                pageObj[2].SetActive(false);
                pageObj[3].SetActive(false);
                bookObj[0].SetActive(false);
                bookObj[1].SetActive(true);
                bookObj[2].SetActive(false);
            }
        }
    }

    /// <summary>
    /// ������ �ε����� Ȯ���ϰ� �����ε�ó���� �����ϴ� �Լ�
    /// </summary>
    private void openItem()
    {
        if (itemCheck == true)
        {
            int count = itemsIndex.Count;
            for (int i = 0; i < count; i++)
            {
                switch (itemsIndex[i])
                {
                    case 100:
                        itemImages[0].color = Color.white;
                        break;
                    case 101:
                        itemImages[1].color = Color.white;
                        break;
                    case 102:
                        itemImages[2].color = Color.white;
                        break;
                }
            }
            itemCheck = false;
        }
    }

    /// <summary>
    /// Npc �ε����� Ȯ���ϰ� �����ε�ó���� �����ϴ� �Լ�
    /// </summary>
    private void openNpc()
    {
        if (npcCheck == true)
        {
            int count = npcIndex.Count;
            for (int i = 0; i < count; i++)
            {
                switch (npcIndex[i])
                {
                    case 10:
                        npcImages[0].color = Color.white;
                        break;
                }
            }
            npcCheck = false;
        }
    }

    /// <summary>
    /// ������ �ε����� Ȯ���Ͽ� ������ true ������ false ���� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_itemId"></param>
    /// <returns></returns>
    private bool SetItemsIndex(int _itemId)
    {
        int count = itemsIndex.Count;

        for (int i = 0; i < count; i++)
        {
            if (itemsIndex[i] == _itemId)
            {         
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// ������ �ε����� �޾ƿ��� �Լ�
    /// </summary>
    /// <param name="_itemId"></param>
    public void SetItemIdCheck(int _itemId)
    {
        if (SetItemsIndex(_itemId) == false)
        {
            itemsIndex.Add(_itemId);
            itemCheck = true;
        }
    }

    /// <summary>
    /// Npc �ε����� Ȯ���Ͽ� ������ true ������ false ���� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_itemId"></param>
    /// <returns></returns>
    private bool SetNpcIndex(int _itemId)
    {
        int count = npcIndex.Count;

        for (int i = 0; i < count; i++)
        {
            if (npcIndex[i] == _itemId)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Npc �ε����� �޾ƿ��� �Լ�
    /// </summary>
    /// <param name="_itemId"></param>
    public void SetNpcIdCheck(int _itemId)
    {
        if (SetNpcIndex(_itemId) == false)
        {
            npcIndex.Add(_itemId);
            npcCheck = true;
        }
    }
}