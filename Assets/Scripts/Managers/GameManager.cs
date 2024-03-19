using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
    [SerializeField, Tooltip("���� ��ư")] private List<Button> settingButton;
    [SerializeField, Tooltip("���� â")] private GameObject setting;
    [SerializeField, Tooltip("���� ���� ��ư")] private List<Button> gameExitButton;
    [SerializeField, Tooltip("���� ���� â")] private GameObject gameExit;
    [Space]
    [SerializeField, Tooltip("�ػ� ������ ���� ��Ӵٿ�")] private TMP_Dropdown dropdown;
    [SerializeField, Tooltip("â��� ������ ���� ���")] private Toggle toggle;
    [SerializeField, Tooltip("�������")] private Slider bgm;
    [SerializeField, Tooltip("ȿ����")] private Slider fxs;

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
        setting.SetActive(false);

        string saveScreenData = PlayerPrefs.GetString(saveOptionValue);
        saveOption = JsonUtility.FromJson<SaveOption>(saveScreenData);
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
            string setScene = JsonUtility.ToJson(saveScene);
            PlayerPrefs.SetString(saveSceneName, setScene);
            SceneManager.LoadSceneAsync("MainScene");
        });

        mainBackButton[2].onClick.AddListener(() =>
        {
            mainBackChoice.SetActive(false);
        });

        settingButton[0].onClick.AddListener(() => 
        {
            setting.SetActive(true);
        });

        settingButton[1].onClick.AddListener(() =>
        {
            dropdownScreenSize();

            Screen.SetResolution(saveOption.widthSize, saveOption.heightSize, saveOption.windowOn);
            saveOption.dropdownValue = dropdown.value;
            saveOption.windowOn = toggle.isOn;
            saveOption.bgmValue = bgm.value;
            saveOption.fxsValue = fxs.value;

            string getScreenSize = JsonUtility.ToJson(saveOption);
            PlayerPrefs.SetString(saveOptionValue, getScreenSize);

            string saveScreenData = PlayerPrefs.GetString(saveOptionValue);
            saveOption = JsonUtility.FromJson<SaveOption>(saveScreenData);
            setSaveOptionData(saveOption);
        });

        settingButton[2].onClick.AddListener(() =>
        {
            setting.SetActive(false);
        });

        gameExitButton[0].onClick.AddListener(() => 
        {
            gameExit.SetActive(true);
        });

        gameExitButton[1].onClick.AddListener(() => 
        {
            saveScene.sceneName = curSceneName;
            string setScene = JsonUtility.ToJson(saveScene);
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
    }

    private void Update()
    {
        optionOnOff();
    }

    /// <summary>
    /// ������ ���� ��Ű�� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool GetGamePause()
    {
        return gamePause;
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
}
