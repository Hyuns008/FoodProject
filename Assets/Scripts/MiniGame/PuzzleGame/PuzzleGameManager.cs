using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour
{
    public static PuzzleGameManager Instance;

    private MiniGameClearCheck miniGame;

    [Header("���� ����")]
    [SerializeField, Tooltip("���񺸵�")] private Transform puzzleBoardTrs;
    [SerializeField, Tooltip("��������")] private GameObject pieceObj;
    private bool gameStart = false; //������ �����ߴ��� Ȯ���ϴ� ����
    private List<int> pieceIndex = new List<int>(); //������ �ε����� �����ֱ� ���� ����Ʈ
    private List<GameObject> puzzleCheck = new List<GameObject>(); //������ ������ �־��ֱ� ���� ����Ʈ
    private List<GameObject> pieceObjCheck = new List<GameObject>(); //�÷��̾ ������ �ΰ��� ���� ������ �� ����Ʈ
    private int checkValue; //���� ������ 2�� �̻� �������� �� �ϰ� ���� ����
    private bool gameClear = false; //������ Ŭ���� ������ ���ִ� ����
    private List<bool> clearChecks = new List<bool>(); //Ŭ��� üũ�ϱ� ���� ����, 16���� ��� true�� �� ���� Ŭ����
    private int clearIndex; //Ŭ���� bool ����Ʈ�� Ȯ���� true�� ��ŭ �ε����� �޾ƿ��� ���� ����
    [SerializeField] private GameObject claerTextObj; //Ŭ���� ���� �� �ߴ� �ؽ�Ʈ
    [Space]
    [SerializeField] private List<Sprite> puzzleSprites;
    private float gameOverTimer;
    private bool gameOver = false;
    [SerializeField] private TMP_Text overTimeText;
    [SerializeField] private GameObject gameOverObj;
    [SerializeField] private Image fadeImage;
    private float fadeTimer;
    private bool fadeIn = false;
    private float sceneLoadTimer;

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

        for (int i = 0; i < 2; i++)
        {
            pieceObjCheck.Add(null);
        }

        for(int i = 0; i < 16; i++)
        {
            clearChecks.Add(false);
        }

        claerTextObj.SetActive(false);
        gameOverObj.SetActive(false);

        gameOverTimer = 60f;

        fadeImage.gameObject.SetActive(true);

        fadeTimer = 1f;
    }

    private void Start()
    {
        miniGame = MiniGameClearCheck.Instance;
    }

    private void Update()
    {
        if (fadeIn == false)
        {
            fadeTimer -= Time.deltaTime;

            Color imageColor = fadeImage.color;
            imageColor.a = fadeTimer;
            fadeImage.color = imageColor;

            if (imageColor.a <= 0)
            {
                fadeIn = true;
                fadeTimer = 0;
                fadeImage.gameObject.SetActive(false);
            }
            return;
        }

        if (gameClear == true)
        {
            claerTextObj.SetActive(true);
            fadeImage.gameObject.SetActive(true);

            fadeTimer += Time.deltaTime;

            Color imageColor = fadeImage.color;
            imageColor.a = fadeTimer;
            fadeImage.color = imageColor;

            if (imageColor.a >= 1)
            {
                miniGame.SetSaveMiniCheckData(100);
                SceneManager.LoadSceneAsync("DessertVillage");
            }
            return;
        }

        if (gameOver == false)
        {
            gameOverTimer -= Time.deltaTime;
            overTimeText.text = $"{gameOverTimer.ToString("F0")}";
            if (gameOverTimer <= 0)
            {
                gameOverObj.SetActive(true);
                gameOver = true;
                gameOverTimer = 60f;
            }
        }
        else
        {
            return;
        }

        if (clearIndex == 16)
        {
            gameClear = true;
        }

        puzzleCreate();
        piecePosChange();
    }

    /// <summary>
    /// ������ �����ϴ� �Լ�
    /// </summary>
    private void puzzleCreate()
    {
        if (gameStart == false)
        {
            for (int i = 0; i < 16; i++)
            {
                pieceIndex.Add(i);
            }

            randomIndex();

            Vector3 trsPos = Vector3.zero;

            for (int i = 0; i < 16; i++)
            {
                GameObject pieceOb = Instantiate(pieceObj, puzzleBoardTrs);
                Image pieceImage = pieceOb.GetComponent<Image>();
                PuzzlePiece puzzlePieceSc = pieceOb.GetComponent<PuzzlePiece>();
                pieceOb.name = $"Piece{pieceIndex[i]}";
                pieceImage.sprite = setPuzzleSprite(pieceIndex[i]);
                puzzlePieceSc.SetRectTrs(rectPos(i, trsPos));
                trsPos = rectPos(i, trsPos);
                puzzlePieceSc.SetPieceIndex(pieceIndex[i]);
                puzzleCheck.Add(pieceOb);
            }

            gameStart = true;
        }
    }

    /// <summary>
    /// ������ ���� ���� �Լ�
    /// </summary>
    private void randomIndex()
    {
        for (int i = 0; i < 16; i++)
        {
            int radom = Random.Range(1, 17);
            for (int j = 0; j < radom; j++)
            {
                int radomValue = pieceIndex[i];
                pieceIndex[i] = pieceIndex[j];
                pieceIndex[j] = radomValue;
            }
        }
    }

    /// <summary>
    /// ��ǥ�� �־��ֱ� ���� �Լ�
    /// </summary>
    /// <param name="_i"></param>
    /// <param name="_trsPos"></param>
    /// <returns></returns>
    private Vector3 rectPos(int _i, Vector3 _trsPos)
    {
        switch (_i)
        {
            case 0:
                _trsPos.x = -150f;
                _trsPos.y = 150f;
                return _trsPos;
            case 1:
            case 2:
            case 3:
                _trsPos.x += 100f;
                _trsPos.y = 150f;
                return _trsPos;
            case 4:
                _trsPos.x = -150f;
                _trsPos.y = 50f;
                return _trsPos;
            case 5:
            case 6: 
            case 7:
                _trsPos.x += 100f;
                _trsPos.y = 50f;
                return _trsPos;
            case 8:
                _trsPos.x = -150f;
                _trsPos.y = -50f;
                return _trsPos;
            case 9: 
            case 10: 
            case 11:
                _trsPos.x += 100f;
                _trsPos.y = -50f;
                return _trsPos;
            case 12:
                _trsPos.x = -150f;
                _trsPos.y = -150f;
                return _trsPos;
            case 13: 
            case 14: 
            case 15:
                _trsPos.x += 100f;
                _trsPos.y = -150f;
                return _trsPos;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// �ε����� �´� �̹����� �־��ֱ� ���� �Լ�
    /// </summary>
    /// <param name="_index"></param>
    /// <returns></returns>
    private Sprite setPuzzleSprite(int _index)
    {
        switch (_index)
        {
            case 0:
                return puzzleSprites[0];
            case 1:
                return puzzleSprites[1];
            case 2:
                return puzzleSprites[2];
            case 3:
                return puzzleSprites[3];
            case 4:
                return puzzleSprites[4];
            case 5:
                return puzzleSprites[5];
            case 6:
                return puzzleSprites[6];
            case 7:
                return puzzleSprites[7];
            case 8:
                return puzzleSprites[8];
            case 9:
                return puzzleSprites[9];
            case 10:
                return puzzleSprites[10];
            case 11:
                return puzzleSprites[11];
            case 12:
                return puzzleSprites[12];
            case 13:
                return puzzleSprites[13];
            case 14:
                return puzzleSprites[14];
            case 15:
                return puzzleSprites[15];
        }

        return null;
    }

    /// <summary>
    /// ���� ��ġ�� ���� �ٲٱ� ���� �Լ�
    /// </summary>
    private void piecePosChange()
    {
        if (checkValue == 2)
        {
            PuzzlePiece pieceScA = pieceObjCheck[0].GetComponent<PuzzlePiece>();
            PuzzlePiece pieceScB = pieceObjCheck[1].GetComponent<PuzzlePiece>();

            Vector3 piecePos = pieceScA.GetRectTrs().localPosition;
            pieceScA.SetRectTrs(pieceScB.GetRectTrs().localPosition);
            pieceScB.SetRectTrs(piecePos);
            pieceScA.ChangeTrue();
            pieceScB.ChangeTrue();

            GameObject temp = null;

            if (pieceObjCheck != null)
            {
                #region
                int count = puzzleCheck.Count;
                for (int i = 0; i < count; i++)
                {
                    if (puzzleCheck[i] == pieceObjCheck[0])
                    {
                        temp = puzzleCheck[i];

                        for (int j = 0; j < count; j++)
                        {
                            if (puzzleCheck[j] == pieceObjCheck[1])
                            {
                                puzzleCheck[i] = pieceObjCheck[1];
                                puzzleCheck[j] = temp;

                                pieceObjCheck[0] = null;
                                pieceObjCheck[1] = null;

                                checkValue = 0;

                                gameClearCheck();
                                return;
                            }
                        }
                    }
                }
                #endregion
            }
        }
    }

    /// <summary>
    /// ������ ������ �´ٸ� ������ Ŭ�������ִ� �Լ�
    /// </summary>
    private void gameClearCheck()
    {
        int count = puzzleCheck.Count;
        for (int i = 0; i < count; i++)
        {
            PuzzlePiece pieceSc = puzzleCheck[i].GetComponent<PuzzlePiece>();

            if (pieceSc.PieceIndexCheck() == i)
            {
                clearChecks[i] = true;
            }
            else
            {
                clearChecks[i] = false;
            }
        }

        clearIndex = checkBool();
    }

    /// <summary>
    /// ������ Ŭ�����ϱ� ���� Ŭ���� �ε����� ��½����ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    private int checkBool()
    {
        int clearValue = 0;
        for (int i = 0; i < clearChecks.Count; i++)
        {
            if (clearChecks[i] == true)
            {
                clearValue++;
            }
        }

        return clearValue;
    }

    /// <summary>
    /// ���� ������ �־��ִ� �Լ�
    /// </summary>
    /// <param name="_piece"></param>
    public void SetPiece(GameObject _piece)
    {
        int count = pieceObjCheck.Count;
        for (int i = 0; i < count; i++)
        {
            if (pieceObjCheck[i] == null)
            {
                pieceObjCheck[i] = _piece;
                checkValue++;
                return;
            }
        }
    }

    /// <summary>
    /// �÷��̾ ������ ������ �ΰ��� ���� �ʵ��� �����ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool PieceObjCheck()
    {
        if (checkValue != 2)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// ���� Ŭ���� bool���� �ٸ� ��ũ��Ʈ�� �����ֱ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool GameClear()
    {
        return gameClear;
    }
}
