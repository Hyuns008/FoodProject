using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text prologueText;
    [SerializeField] private Image mapImage;
    private int prologueIndex = 0;
    [SerializeField] private bool mapOn = false;
    [SerializeField] private bool mapOff = false;
    [SerializeField] private float mapOnTimer;
    [SerializeField] private float mapOffTimer;

    private void Start()
    {
        mapImage.gameObject.SetActive(false);

        prologueText.text = "���õ� �����߾�...";
    }

    private void Update()
    {
        mpaImageTimer();
        prologue();
    }

    private void mpaImageTimer()
    {
        if (mapOn == true)
        {
            Color imageColor = mapImage.color;

            if (imageColor.a != 1)
            {
                mapOnTimer += Time.deltaTime / 2;

                imageColor.a = mapOnTimer;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    imageColor.a = 1;
                }

                if (imageColor.a >= 1)
                {
                    imageColor.a = 1f;
                    mapOnTimer = 0f;
                    mapOffTimer = 1f;
                    mapOff = true;
                    mapOn = false;
                }

                mapImage.color = imageColor;
            }
        }

        if (mapOff == true)
        {
            Color imageColor = mapImage.color;

            if (imageColor.a != 0)
            {
                mapOffTimer -= Time.deltaTime / 2;

                imageColor.a = mapOffTimer;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    imageColor.a = 0;
                }

                if (imageColor.a <= 0)
                {
                    imageColor.a = 0f;
                    mapOffTimer = 1;
                    mapOff = false;
                }

                mapImage.color = imageColor;
            }
        }
    }

    /// <summary>
    /// ���ѷα��� ���â�� ����ϴ� �Լ�
    /// </summary>
    private void prologue()
    {
        switch (prologueIndex)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "�� ������ ���� ����鿡�� ����Ʈ�� ����� �ְ� ������";
                    prologueIndex++;
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "������ �ʹ� ���Ƽ�";
                    prologueIndex++;
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "�� ������ ���� �𸣰ھ�";
                    prologueIndex++;
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "�ʹ� ��ƴ�...";
                    prologueIndex++;
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "����Ʈ �帲 Ÿ��?";
                    prologueIndex++;
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "����Ʈ ������ 100���̶��? ";
                    prologueIndex++;
                }
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "...!";
                    prologueIndex++;
                }
                break;
            case 7:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "���� ������ ����!";
                    prologueIndex++;
                }
                break;
            case 8:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "���⼭ ����Ʈ�� ���� ���� �ž�";
                    prologueIndex++;
                }
                break;
            case 9:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "�׷� ���ִ� ����Ʈ�� ���� �� �ְ���? ";
                    prologueIndex++;
                }
                break;
            case 10:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "����! ";
                    prologueIndex++;
                }
                break;
            case 11:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "�ϴ� ������ ���?";
                    prologueIndex++;
                }
                break;
            case 12:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "���� ����� ����? ";
                    prologueIndex++;
                }
                break;
            case 13:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    mapImage.gameObject.SetActive(true);
                    mapOn = true;
                    prologueIndex++;
                }
                break;
            case 14:
                if (Input.GetKeyDown(KeyCode.Space) && mapOn == false && mapOff == false)
                {
                    prologueText.text = "���~!!!";
                    prologueIndex++;
                }
                break;
            case 15:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "���Ⱑ �ٷ� ����Ʈ �����̱���...";
                    prologueIndex++;
                }
                break;
            case 16:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "�ʹ� �̻ڴ�~";
                    prologueIndex++;
                }
                break;
            case 17:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "�´�! ������� ������ �ϴµ� ��� ��� ����?";
                    prologueIndex++;
                }
                break;
            case 18:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    prologueText.text = "�������� ������?";
                    prologueIndex++;
                }
                break;
            case 19:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadSceneAsync("DessertVillage");
                }
                break;
        }
    }
}
