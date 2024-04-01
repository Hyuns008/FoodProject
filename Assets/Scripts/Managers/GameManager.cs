using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Inventory inventory;

    public class SaveOption
    {
        public int widthSize;
        public int heightSize;
        public bool windowOn;
        public int dropdownValue;
        public float bgmValue;
        public float fxsValue;
    }

    public class SaveScene
    {
        public string sceneName;
    }

    private SaveOption saveOption = new SaveOption();

    private SaveScene saveScene = new SaveScene();

    [Header("���� ����")]
    [SerializeField] private bool gamePause;

    [Header("���� �� �̸�")]
    [SerializeField] private string curSceneName;

    private string saveOptionValue = "saveOptionValue"; //��ũ�� ������ Ű ���� ���� ����

    private string saveSceneName = "saveSceneName"; //���� �����ϱ� ���� ����

    [Header("���� �Ŵ������� ������ �ɼ� â")]
    [SerializeField, Tooltip("�ɼ��� ���� Ű�� ���� ������Ʈ")] private GameObject option;
    [SerializeField, Tooltip("�������� ���ư��� ��ư")] private Button gameBackButton;
    [SerializeField, Tooltip("�������� ���ư��� ��ư")] private List<Button> mainBackButton;
    [SerializeField, Tooltip("�������� ���ư��� â")] private GameObject mainBackChoice;
    [SerializeField, Tooltip("���� ��ư")] private Button settingButton;
    [SerializeField, Tooltip("���� â")] private GameObject setting;
    [SerializeField, Tooltip("���� ���� ��ư")] private List<Button> gameExitButton;
    [SerializeField, Tooltip("���� ���� â")] private GameObject gameExit;
    [Space]
    [SerializeField, Tooltip("�ػ� ������ ���� ��Ӵٿ�")] private TMP_Dropdown dropdown;
    [SerializeField, Tooltip("â��� ������ ���� ���")] private Toggle toggle;
    [SerializeField, Tooltip("�������")] private Slider bgm;
    [SerializeField, Tooltip("ȿ����")] private Slider fxs;
    [Space]
    [SerializeField, Tooltip("���̵� �� �ƿ�")] private Image fadeInOut;
    private bool fadeOn = false;
    private float fadeTimer;
    private bool fadeOff = false;
    private float fadeOutTimer;

    private float fadeTimeOut = 2.0f;
    private float checkTimer = 0.0f;

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

        option.SetActive(false);

        string saveScreenData = PlayerPrefs.GetString(saveOptionValue);
        saveOption = JsonConvert.DeserializeObject<SaveOption>(saveScreenData);
        setSaveOptionData(saveOption);

        gameBackButton.onClick.AddListener(() =>
        {
            option.SetActive(false);
        });

        mainBackButton[0].onClick.AddListener(() =>
        {
            mainBackChoice.SetActive(true);
        });

        mainBackButton[1].onClick.AddListener(() =>
        {
            saveScene.sceneName = curSceneName;
            string setScene = JsonConvert.SerializeObject(saveScene);
            PlayerPrefs.SetString(saveSceneName, setScene);
            gamePause = true;
            fadeOff = true;
        });

        mainBackButton[2].onClick.AddListener(() =>
        {
            mainBackChoice.SetActive(false);
        });

        settingButton.onClick.AddListener(() =>
        {
            string saveScreenData = PlayerPrefs.GetString(saveOptionValue);
            saveOption = JsonConvert.DeserializeObject<SaveOption>(saveScreenData);
            setSaveOptionData(saveOption);

            dropdownScreenSize();

            saveOption.dropdownValue = dropdown.value;
            saveOption.windowOn = toggle.isOn;
            saveOption.bgmValue = bgm.value;
            saveOption.fxsValue = fxs.value;
            Screen.SetResolution(saveOption.widthSize, saveOption.heightSize, saveOption.windowOn);

            string getScreenSize = JsonConvert.SerializeObject(saveOption);
            PlayerPrefs.SetString(saveOptionValue, getScreenSize);
        });

        gameExitButton[0].onClick.AddListener(() =>
        {
            gameExit.SetActive(true);
        });

        gameExitButton[1].onClick.AddListener(() =>
        {
            saveScene.sceneName = curSceneName;
            string setScene = JsonConvert.SerializeObject(saveScene);
            PlayerPrefs.SetString(saveSceneName, setScene);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        gameExitButton[2].onClick.AddListener(() =>
        {
            gameExit.SetActive(false);
        });

        fadeOn = true;
        gamePause = true;
    }

    private void Start()
    {
        inventory = Inventory.Instance;

        inventory.InventoryObj().SetActive(true);

        Color fadeColor = fadeInOut.color;
        fadeColor.a = 1f;
        fadeInOut.color = fadeColor;
        fadeTimer = 1.0f;
        fadeInOut.gameObject.SetActive(true);
    }

    private void Update()
    {
        gamePauseOnOff();

        fadeIn();
        fadeOut();

        optionOnOff();
    }

    /// <summary>
    /// ������ ���߰ų� ������ �� �ְ� �ϴ� �Լ�
    /// </summary>
    private void gamePauseOnOff()
    {
        if (gamePause == true)
        {
            if (Time.timeScale == 0.0f)
            {
                return;
            }

            Time.timeScale = 0.0f;
        }
        else
        {
            if (Time.timeScale == 1.0f)
            {
                return;
            }

            Time.timeScale = 1.0f;
        }
    }

    /// <summary>
    /// ������ ������ ���̰� �ϴ� �Լ�
    /// </summary>
    private void fadeIn()
    {
        if (fadeOn == true)
        {
            //int curFrame = (int)(1 / Time.unscaledDeltaTime);
            checkTimer += Time.unscaledDeltaTime;
            //if (curFrame >= 40 || checkTimer >= fadeTimeOut)
            //{
            if (checkTimer >= fadeTimeOut)
            {
                fadeTimer -= Time.unscaledDeltaTime / 2;
                Color fadeColor = fadeInOut.color;
                fadeColor.a = fadeTimer;
                fadeInOut.color = fadeColor;
                inventory.InventoryObj().SetActive(false);

                if (fadeColor.a <= 0.0f)
                {
                    fadeInOut.gameObject.SetActive(false);
                    fadeColor.a = 0.0f;
                    gamePause = false;
                    fadeOn = false;
                }
            }
        }
    }

    /// <summary>
    /// ����ȭ���� ������ ��Ӱ� �ϴ� �Լ�
    /// </summary>
    private void fadeOut()
    {
        if (fadeOff == true)
        {
            if (fadeInOut.gameObject.activeSelf == false)
            {
                fadeInOut.gameObject.SetActive(true);
            }

            fadeTimer += Time.unscaledDeltaTime / 2;
            Color fadeColor = fadeInOut.color;
            fadeColor.a = fadeTimer;
            fadeInOut.color = fadeColor;

            if (fadeColor.a >= 1.0f)
            {
                SceneManager.LoadSceneAsync("MainScene");
                fadeColor.a = 1.0f;
                gamePause = false;
                fadeOff = false;
            }
        }
    }

    /// <summary>
    /// �ɼ�â�� ���� �� �� �ְ� �����ִ� �Լ�
    /// </summary>
    private void optionOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool optionCheck = option == option.activeSelf ? false : true;
            option.SetActive(optionCheck);
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


    /// <summary>
    /// ������ ���� ��Ű�� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool GetGamePause()
    {
        return gamePause;
    }
}
