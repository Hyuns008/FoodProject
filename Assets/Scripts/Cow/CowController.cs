using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    private SkeletonAnimation spineAnim;

    [Header("�� ���� ���� ����")]
    [SerializeField, Tooltip("�� ��")] private List<GameObject> cowFace;
    [SerializeField] private GameObject faceCheck;
    [SerializeField, Tooltip("�ܽ���")] private GameObject hamsterObject;
    [Space]
    [SerializeField, Tooltip("����Ŭ���� �ؽ�Ʈ")] private GameObject gameClearText;
    [SerializeField, Tooltip("���ӿ��� �ؽ�Ʈ")] private GameObject gameOverText;
    [SerializeField] private MilkeGet milkeGet;
    [SerializeField] private ClearCheck check;
 
    private bool changeFace = false;
    private float changeFaceTimer;
    private float randomTime;

    private float angryCowTimer;

    private bool gameOver;

    private bool moveCheck = false;

    private void Awake()
    {
        changeFaceTimer = 3f;
        randomTime = changeFaceTimer;

        cowFace[1].SetActive(false);
        cowFace[2].SetActive(false);
    }

    private void Start()
    {
        spineAnim = hamsterObject.GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        if (check.ReturnChear() == true)
        {
            gameClearText.SetActive(true);
            return;
        }
        else if (gameOver == true)
        {
            cowFace[2].SetActive(true);
            gameOverText.SetActive(true);
            return;
        }

        changeCowFace();
        hamsterMoveCheck();
    }

    /// <summary>
    /// ���� �ð����� ���� ���� �ٲ���
    /// </summary>
    private void changeCowFace()
    {
        if (changeFace == false)
        {
            changeFaceTimer -= Time.deltaTime;

            if (changeFaceTimer <= randomTime * 0.3f)
            {
                faceCheck.SetActive(true);
            }

            if (changeFaceTimer <= 0)
            {
                changeFace = true;
                cowFace[1].SetActive(true);
                angryCowTimer = 2f;
            }
        }
        else
        {
            if (moveCheck == false && milkeGet.ReturnCheck() == true)
            {
                gameOver = true;
                return;
            }

            angryCowTimer -= Time.deltaTime;

            if (faceCheck.activeSelf == true)
            {
                faceCheck.SetActive(false);
            }

            if (angryCowTimer <= 0)
            {
                cowFace[1].SetActive(false);
                float cowFaceRandomTime = Random.Range(2.0f, 5.0f);
                randomTime = cowFaceRandomTime;
                changeFaceTimer = cowFaceRandomTime;
                changeFace = false;
            }
        }

        if (milkeGet.ReturnMilkeGet() == true && check.gameObject.activeSelf == false)
        {
            check.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// �ܽ��Ͱ� ��Ȳ�� ���� �����̰� �������
    /// </summary>
    private void hamsterMoveCheck()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            spineAnim.AnimationName = "Idle";
            moveCheck = true;
            return;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            moveCheck = false;
            return;
        }

        if (milkeGet.ReturnMilkeGet() == false && Input.GetKey(KeyCode.RightArrow))
        {
            spineAnim.AnimationName = "Walk";
            hamsterObject.transform.position += new Vector3(1f, 0f, 0f) * 1f * Time.deltaTime;
        }
        else if (milkeGet.ReturnMilkeGet() == true && Input.GetKey(KeyCode.LeftArrow))
        {
            spineAnim.AnimationName = "Walk";
            hamsterObject.transform.position += new Vector3(-1f, 0f, 0f) * 1f * Time.deltaTime;
            hamsterObject.transform.localScale = new Vector3(-0.4f, 0.4f, 1f);
        }
        else
        {
            spineAnim.AnimationName = "Idle";
        }
    }
}
