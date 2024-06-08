using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingMusicController : MonoBehaviour
{
    private CookingMusicManager musicManager;

    [Header("��ŷ ���� ����")]
    [SerializeField, Tooltip("���� �ð�")] private float musicTime;
    private float timer;
    [SerializeField, Tooltip("���")] private int playerHeart;
    [Space]
    [SerializeField] private List<GameObject> heartObject;
    [Space]
    [SerializeField] private Image timerImage;
    [SerializeField] private List<Transform> gridTrs;

    private int nextNode;

    private List<GameObject> nodeObject = new List<GameObject>();
    private List<KeyCode> nodeKey = new List<KeyCode>();

    private void Awake()
    {
        timer = musicTime;
    }

    private void Start()
    {
        musicManager = CookingMusicManager.Instance;

        nodeInstantiate();
    }

    private void Update()
    {
        gameTimer();
        nodeCheck();
    }

    private void gameTimer()
    {
        timer -= Time.deltaTime;
        timerImage.fillAmount = timer / musicTime;
    }

    /// <summary>
    /// ������ ���۵� �� ��� ������Ʈ�� �����ϰ� �ϴ� �Լ�
    /// </summary>
    private void nodeInstantiate()
    {
        for (int iNum = 0; iNum < 20; iNum++)
        {
            int randomNode = Random.Range(0, 4);

            GameObject nodeObj = Instantiate(musicManager.GetNodeObject(randomNode), getGridTrs(iNum));

            nodeObject.Add(nodeObj);

            MusicNode nodeSc = nodeObj.GetComponent<MusicNode>();
            nodeKey.Add(nodeSc.GetKeyCode(randomNode));
        }
    }

    /// <summary>
    /// ��忡 �°� ����Ű�� �������� üũ�ϴ� �Լ�
    /// </summary>
    private void nodeCheck()
    {
        if (nextNode > 19 || playerHeart == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            keyCodeCheck(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            keyCodeCheck(KeyCode.DownArrow);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            keyCodeCheck(KeyCode.LeftArrow);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            keyCodeCheck(KeyCode.RightArrow);
        }
    }

    /// <summary>
    /// �޾ƿ� Ű�ڵ� ���̶� ���ǽ� ���ϴ� �Լ�
    /// </summary>
    /// <param name="_keyCode"></param>
    private void keyCodeCheck(KeyCode _keyCode)
    {
        if (Input.GetKeyDown(nodeKey[nextNode]) == Input.GetKeyDown(_keyCode))
        {
            SkeletonGraphic spineAnim = nodeObject[nextNode].GetComponent<SkeletonGraphic>();

            nextNode++;

            if (_keyCode == KeyCode.UpArrow)
            {
                spineAnim.startingAnimation = "SUp";
            }
            else if (_keyCode == KeyCode.DownArrow)
            {
                spineAnim.startingAnimation = "SDown";
            }
            else if (_keyCode == KeyCode.LeftArrow)
            {
                spineAnim.startingAnimation = "SLeft";
            }
            else if (_keyCode == KeyCode.RightArrow)
            {
                spineAnim.startingAnimation = "SRight";
            }

            spineAnim.AnimationState.SetAnimation(0, spineAnim.startingAnimation, false);
        }
        else
        {
            SkeletonGraphic spineAnim = nodeObject[nextNode].GetComponent<SkeletonGraphic>();

            if (nodeKey[nextNode] == KeyCode.UpArrow)
            {
                spineAnim.startingAnimation = "FUp";
            }
            else if (nodeKey[nextNode] == KeyCode.DownArrow)
            {
                spineAnim.startingAnimation = "FDown";
            }
            else if (nodeKey[nextNode] == KeyCode.LeftArrow)
            {
                spineAnim.startingAnimation = "FLeft";
            }
            else if (nodeKey[nextNode] == KeyCode.RightArrow)
            {
                spineAnim.startingAnimation = "FRight";
            }

            spineAnim.AnimationState.SetAnimation(0, spineAnim.startingAnimation, false);

            heartObject[--playerHeart].SetActive(false);
        }
    }

    private Transform getGridTrs(int _iNum)
    {
        if (_iNum < 6)
        {
            return gridTrs[0];
        }
        else if (_iNum == 6)
        {
            return gridTrs[1];
        }
        else if (_iNum > 6 && _iNum < 13)
        {
            return gridTrs[2];
        }
        else if (_iNum == 13) 
        {
            return gridTrs[3];
        }
        else
        {
            return gridTrs[4];
        }
    }
}
