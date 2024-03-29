using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public class SaveOption
    {
        public int widthSize = 1280;
        public int heightSize = 720;
        public bool windowOn = true;
        public int dropdownValue = 3;
        public float bgmValue = 50f;
        public float fxsValue = 50f;
    }

    public class SaveScene
    {
        public string sceneName;
    }

    private SaveOption saveOption = new SaveOption();

    private SaveScene saveScene = new SaveScene();

    [Header("�۵��ϴ� ��ư")]
    [SerializeField, Tooltip("���� ���� ��ư")] private Button startButton;
    [SerializeField, Tooltip("���� �ҷ����� ��ư")] private Button loadButton;
    [SerializeField, Tooltip("���� �ɼ� ��ư")] private Button optionButton;
    [SerializeField, Tooltip("���� ���� ��ư")] private Button exitButton;
    [Space]
    [SerializeField, Tooltip("���� �ʱ�ȭ �ٽ� �����")] private GameObject resetChoiceButton;
    [SerializeField, Tooltip("���� ��¥ �ʱ�ȭ ��ư")] private Button resetButton;
    [SerializeField, Tooltip("�������� ���ư��� ��ư")] private Button resetBackButton;
    [Space]
    [SerializeField, Tooltip("���� ���� �� �ٽ� �����")] private GameObject choiceButton;
    [SerializeField, Tooltip("���� ��¥ ���� ��ư")] private Button endButton;
    [SerializeField, Tooltip("�������� ���ư��� ��ư")] private Button backButton;
    [Space]
    [SerializeField, Tooltip("���� �ɼ�â")] private GameObject option;
    [SerializeField, Tooltip("���� �ɼ� ���� ��ư")] private Button optionSave;
    [SerializeField, Tooltip("�������� ���ư��� ��ư")] private Button optionBack;
    [SerializeField, Tooltip("�ػ� ������ ���� ��Ӵٿ�")] private TMP_Dropdown dropdown;
    [SerializeField, Tooltip("â��� ������ ���� ���")] private Toggle toggle;
    [Space]
    [SerializeField, Tooltip("�������")] private Slider bgm;
    [SerializeField, Tooltip("ȿ����")] private Slider fxs;
    [Space]
    [SerializeField, Tooltip("���̵��ξƿ�")] private Image fadeInOut;
    private bool sceneChangefadeOn = false;
    [SerializeField] private float fadeTimer;
    private bool fadeOn = true;

    private string saveOptionValue = "saveOptionValue"; //��ũ�� ������ Ű ���� ���� ����

    private string saveSceneName = "saveSceneName"; //���� �����ϱ� ���� ����

    private void Awake()
    {
        if (choiceButton != null)
        {
            choiceButton.SetActive(false);
        }

        if (option != null)
        {
            option.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString(saveOptionValue) == string.Empty)
        {
            Screen.SetResolution(1280, 720, true);
            dropdown.value = 3;
            toggle.isOn = true;
            bgm.value = 75f / 100f;
            fxs.value = 75 / 100f;

            string getScreenSize = JsonConvert.SerializeObject(saveOption);
            PlayerPrefs.SetString(saveOptionValue, getScreenSize);
        }
        else
        {
            string saveScreenData = PlayerPrefs.GetString(saveOptionValue);
            saveOption = JsonConvert.DeserializeObject<SaveOption>(saveScreenData);
            setSaveOptionData(saveOption);
        }

        startButton.onClick.AddListener(() => 
        {
            if (PlayerPrefs.GetString(saveSceneName) == string.Empty)
            {
                fadeInOut.gameObject.SetActive(true);

                sceneChangefadeOn = true;
            }
            else
            {
                resetChoiceButton.SetActive(true);
            }
        });

        loadButton.onClick.AddListener(() =>
        {
            if (PlayerPrefs.GetString(saveSceneName) != string.Empty)
            {
                string loadSceneData = PlayerPrefs.GetString(saveSceneName);
                saveScene = JsonConvert.DeserializeObject<SaveScene>(loadSceneData);

                if (saveScene != null)
                {
                    fadeInOut.gameObject.SetActive(true);

                    sceneChangefadeOn = true;
                }
            }
        });

        optionButton.onClick.AddListener(() =>
        {
            option.gameObject.SetActive(true);
        });

        exitButton.onClick.AddListener(() =>
        {
            choiceButton.SetActive(true);
        });

        resetButton.onClick.AddListener(() => 
        {
            PlayerPrefs.SetString(saveSceneName, string.Empty);
            resetChoiceButton.SetActive(false);
        });

        resetBackButton.onClick.AddListener(() =>
        {
            resetChoiceButton.SetActive(false);
        });

        endButton.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        backButton.onClick.AddListener(() =>
        {
            choiceButton.SetActive(false);
        });

        optionSave.onClick.AddListener(() => 
        {
            dropdownScreenSize();

            Screen.SetResolution(saveOption.widthSize, saveOption.heightSize, saveOption.windowOn);
            saveOption.dropdownValue = dropdown.value;
            saveOption.windowOn = toggle.isOn;
            saveOption.bgmValue = bgm.value;
            saveOption.fxsValue = fxs.value;

            string getScreenSize = JsonConvert.SerializeObject(saveOption);
            PlayerPrefs.SetString(saveOptionValue, getScreenSize);

            string saveScreenData = PlayerPrefs.GetString(saveOptionValue);
            saveOption = JsonConvert.DeserializeObject<SaveOption>(saveScreenData);
            setSaveOptionData(saveOption);
        });

        optionBack.onClick.AddListener(() => 
        {
            option.gameObject.SetActive(false);
        });
    }

    private void Start()
    {
        fadeOn = true;
        Color fadeColor = fadeInOut.color;
        fadeColor.a = 1f;
        fadeInOut.color = fadeColor;
        fadeTimer = 1.0f;
        fadeInOut.gameObject.SetActive(true);
    }

    private void Update()
    {
        fadeIn();
        fadeOut();
    }

    /// <summary>
    /// ������ ������ ��Ӱ� �ϴ� �Լ�
    /// </summary>
    private void fadeIn()
    {
        if (sceneChangefadeOn == true)
        {
            if (fadeInOut.gameObject.activeSelf == false)
            {
                fadeInOut.gameObject.SetActive(true);
            }

            fadeTimer += Time.deltaTime / 2;
            Color fadeColor = fadeInOut.color;
            fadeColor.a = fadeTimer;
            fadeInOut.color = fadeColor;

            if (fadeColor.a >= 1.0f)
            {
                fadeColor.a = 1.0f;

                if (PlayerPrefs.GetString(saveSceneName) == string.Empty)
                {
                    saveScene.sceneName = "TestScene";
                    string setScene = JsonConvert.SerializeObject(saveScene);
                    PlayerPrefs.SetString(saveSceneName, setScene);

                    SceneManager.LoadSceneAsync("TestScene");
                }
                else
                {
                    string loadSceneData = PlayerPrefs.GetString(saveSceneName);
                    saveScene = JsonConvert.DeserializeObject<SaveScene>(loadSceneData);

                    SceneManager.LoadSceneAsync(saveScene.sceneName);
                }

                sceneChangefadeOn = false;
            }
        }
    }

    /// <summary>
    /// ����ȭ���� ������ ���̰� �ϴ� �Լ�
    /// </summary>
    private void fadeOut()
    {
        if (fadeOn == true)
        {
            fadeTimer -= Time.deltaTime / 3;
            Color fadeColor = fadeInOut.color;
            fadeColor.a = fadeTimer;
            fadeInOut.color = fadeColor;

            if (fadeColor.a <= 0.0f)
            {
                fadeInOut.gameObject.SetActive(false);
                fadeColor.a = 0.0f;
                fadeOn = false;
            }
        }
    }

    /// <summary>
    /// ��Ӵٿ��� �̿��Ͽ� ��ũ�� ����� �����ϴ� �Լ�
    /// </summary>
    private void dropdownScreenSize()
    {
        if (dropdown.value == 0)
        {
            saveOption.widthSize = 640;
            saveOption.heightSize = 360;
        }
        else if (dropdown.value == 1)
        {
            saveOption.widthSize = 854;
            saveOption.heightSize = 480;
        }
        else if (dropdown.value == 2)
        {
            saveOption.widthSize = 960;
            saveOption.heightSize = 540;
        }
        else if (dropdown.value == 3)
        {
            saveOption.widthSize = 1280;
            saveOption.heightSize = 720;
        }
    }

    /// <summary>
    /// ������ ��ũ�� �����͸� ������ �Ҵ�
    /// </summary>
    /// <param name="_saveScreenSize"></param>
    private void setSaveOptionData(SaveOption _saveScreenSize)
    {
        Screen.SetResolution(_saveScreenSize.widthSize, _saveScreenSize.heightSize, _saveScreenSize.windowOn);
        dropdown.value = _saveScreenSize.dropdownValue;
        toggle.isOn = _saveScreenSize.windowOn;
        bgm.value = _saveScreenSize.bgmValue;
        fxs.value = _saveScreenSize.fxsValue;
    }
}