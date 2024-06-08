using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [Space]
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

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

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        moveCheck = true;
    }

    private void Update()
    {
        fadeInOut();

        if (fadeCheck == true)
        {
            return;
        }

        if (check.ReturnChear() == true)
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
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
                moveCheck = true;
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
        if (changeFace == true && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
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

    private void fadeInOut()
    {
        if (fadeCheck == true && fadeImage.gameObject.activeSelf == true)
        {
            Color fadeColor = fadeImage.color;

            if (fadeColor.a != 0 && fadeInOutCheck == true)
            {
                fadeTimer -= Time.deltaTime;
                fadeColor.a = fadeTimer;
                fadeImage.color = fadeColor;

                if (fadeColor.a <= 0)
                {
                    fadeTimer = 0;
                    fadeImage.gameObject.SetActive(false);
                    fadeInOutCheck = false;
                    fadeCheck = false;
                }
            }
            else if (fadeColor.a != 1 && fadeInOutCheck == false)
            {
                fadeTimer += Time.deltaTime / 2;
                fadeColor.a = fadeTimer;
                fadeImage.color = fadeColor;

                if (fadeColor.a >= 1)
                {
                    fadeTimer = 2;
                    fadeImage.gameObject.SetActive(false);
                    SceneManager.LoadSceneAsync("Chapter");
                    fadeInOutCheck = true;
                    fadeCheck = false;
                }
            }
        }
    }
}
