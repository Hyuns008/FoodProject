using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PictureController : MonoBehaviour
{
    private PictureManager pictureManager;

    [Header("�׸� ����")]
    [SerializeField, Tooltip("���� ���������� �ð�")] private float nextTime;
    [SerializeField] private float timer;
    [SerializeField] private List<int> nextNumber = new List<int>();
    private int nextIndex;
    private bool choiceStart = false;
    [SerializeField] private TMP_Text gameClearOverText;
    [Space]
    [SerializeField] private Image frameImage;
    [SerializeField] private List<Sprite> frameSprites;
    [Space]
    [SerializeField] private GameObject explanationWindow;
    [SerializeField] private Button gameStartButton;
    private bool gameStart = false;
    [Space]
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    [SerializeField] private bool fadeCheck = false;
    private bool fadeInOutCheck = false;

    private void Start()
    {
        pictureManager = PictureManager.Instance;

        gameStartButton.onClick.AddListener(() => 
        {
            explanationWindow.SetActive(false);
            gameStart = true;
        });

        timer = 3;

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        frameImage.sprite = frameSprites[0];
    }

    private void Update()
    {
        fadeInOut();

        if (gameStart == false && fadeCheck == false)
        {
            return;
        }

        if (pictureManager.GameClearCheck() == true && gameClearOverText.gameObject.activeSelf == false)
        {
            gameClearOverText.text = "���� Ŭ����!";
            gameClearOverText.gameObject.SetActive(true);
            return;
        }
        else if (pictureManager.GameOverCheck() == true)
        {
            gameClearOverText.text = "���� ����!";
            gameClearOverText.gameObject.SetActive(true);
            return;
        }

        nextPicture();
        timerCheck();
    }

    private void timerCheck()
    {
        if (nextIndex >= nextNumber.Count && choiceStart == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                for (int i = 0; i < nextNumber.Count; i++)
                {
                    frameImage.sprite = frameSprites[1];
                    pictureManager.GetChoicePictureObject(nextNumber[i]).SetActive(true);
                }

                pictureManager.GetPictureObject(nextNumber[nextIndex - 1]).SetActive(false);

                choiceStart = true;
            }
            return;
        }

        if (nextNumber.Count >= 3 && choiceStart == false)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (nextIndex > 0)
                {
                    pictureManager.GetPictureObject(nextNumber[nextIndex - 1]).SetActive(false);
                }

                pictureManager.GetPictureObject(nextNumber[nextIndex]).transform.SetAsLastSibling();
                pictureManager.GetPictureObject(nextNumber[nextIndex++]).SetActive(true);
                timer = nextTime;
            }
        }
    }

    /// <summary>
    /// �׸��� ��ȣ�� ���ϴ� �Լ�
    /// </summary>
    private void nextPicture()
    {
        if (nextNumber.Count == 3)
        {
            return;
        }

        int randomNumber = Random.Range(0, 3);

        if (nextNumber.Count == 0)
        {
            nextNumber.Add(randomNumber);
            pictureManager.SetPictureIndex(randomNumber);
        }
        else if (nextNumber.Count != 0)
        {
            if (nextNumberCheck(randomNumber) == false)
            {
                nextNumber.Add(randomNumber);
                pictureManager.SetPictureIndex(randomNumber);
            }
        }
    }

    /// <summary>
    /// ���� �׸��� ��ȣ�� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="_number"></param>
    /// <returns></returns>
    private bool nextNumberCheck(int _number)
    {
        for (int i = 0; i < nextNumber.Count; i++)
        {
            if (nextNumber[i] == _number)
            {
                return true;
            }
        }

        return false;
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
                    fadeTimer = 2;
                    fadeImage.gameObject.SetActive(false);
                    fadeInOutCheck = false;
                    fadeCheck = false;
                }
            }
            else if (fadeColor.a != 1 && fadeInOutCheck == false)
            {
                fadeTimer += Time.deltaTime;
                fadeColor.a = fadeTimer;
                fadeImage.color = fadeColor;

                if (fadeColor.a >= 1)
                {
                    fadeTimer = 2;
                    fadeImage.gameObject.SetActive(false);
                    fadeInOutCheck = true;
                    fadeCheck = false;
                }
            }
        }
    }
}
