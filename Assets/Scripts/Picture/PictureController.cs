using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Space]
    [SerializeField] private GameObject gameEndObject;
    [SerializeField] private List<Button> buttons;

    private bool retry = false;
    private bool gameClear = false;

    private float textChangeTimer;
    private bool textChanageOn = false;
    private float textStartTimer;
    private bool textStartCheck = false;

    private void Start()
    {
        pictureManager = PictureManager.Instance;

        gameStartButton.onClick.AddListener(() => 
        {
            explanationWindow.SetActive(false);
            gameStart = true;
        });

        buttons[0].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
        });

        buttons[1].onClick.AddListener(() =>
        {
            fadeCheck = true;
            fadeImage.gameObject.SetActive(true);
            retry = true;
        });

        timer = 2;

        fadeTimer = 2;

        fadeCheck = true;

        fadeInOutCheck = true;

        textChangeTimer = 3;

        frameImage.sprite = frameSprites[0];

        gameClearOverText.text = "";
        gameClearOverText.gameObject.SetActive(true);
    }

    private void Update()
    {
        fadeInOut();

        if (gameStart == true && textChanageOn == false)
        {
            textChangeTimer -= Time.deltaTime;
            gameClearOverText.text = $"{(int)(textChangeTimer + 1)}";
            if (textChangeTimer <= 0)
            {
                gameClearOverText.text = $"";
                textChanageOn = true;
            }
        }
        else if (gameStart == true && textChanageOn == true)
        {
            textStartTimer += Time.deltaTime;

            if (textStartTimer < 1f)
            {
                gameClearOverText.text = $"���� ��ŸƮ!";
            }
            else
            {
                gameClearOverText.text = $"";
                gameClearOverText.gameObject.SetActive(false);
                textStartCheck = true;
            }
        }

        if (gameStart == true && textStartCheck == true)
        {
            if (pictureManager.GameClearCheck() == true && gameClearOverText.gameObject.activeSelf == false)
            {
                gameClear = true;
                gameClearOverText.text = "���� Ŭ����!";
                gameClearOverText.gameObject.SetActive(true);
                gameEndObject.SetActive(true);
                return;
            }
            else if (pictureManager.GameOverCheck() == true)
            {
                gameClearOverText.text = "���� ����!";
                gameClearOverText.gameObject.SetActive(true);
                gameEndObject.SetActive(true);
                return;
            }

            nextPicture();
            timerCheck();
        }
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
                    if (retry == true && gameClear == true)
                    {
                        string getSaveData = JsonConvert.SerializeObject(4);
                        PlayerPrefs.SetString("saveDataKey", getSaveData);
                        SceneManager.LoadSceneAsync("Picture");
                    }
                    else if (retry == true && gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Picture");
                    }
                    else if (gameClear == false)
                    {
                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                    else if (gameClear == true)
                    {
                        string getSaveData = JsonConvert.SerializeObject(4);
                        PlayerPrefs.SetString("saveDataKey", getSaveData);
                        SceneManager.LoadSceneAsync("Chapter1");
                    }
                }
            }
        }
    }
}
