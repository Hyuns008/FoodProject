using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureController : MonoBehaviour
{
    private PictureManager pictureManager;

    [Header("�׸� ����")]
    [SerializeField, Tooltip("���� ���������� �ð�")] private float nextTime;
    private float timer;
    [SerializeField] private List<int> nextNumber = new List<int>();
    private int nextIndex;
    private bool choiceStart = false;
    [SerializeField] private GameObject clearObject;

    private void Start()
    {
        pictureManager = PictureManager.Instance;

        timer = nextTime;
    }

    private void Update()
    {
        if (pictureManager.GameClearCheck() == true && clearObject.activeSelf == false)
        {
            clearObject.SetActive(true);
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
                    pictureManager.GetChoicePictureObject(nextNumber[i]).SetActive(true);
                }

                pictureManager.GetPictureObject(nextNumber[nextIndex - 1]).SetActive(false);

                choiceStart = true;
            }
            return;
        }

        if (nextNumber.Count >= 4 && choiceStart == false)
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
        if (nextNumber.Count == 4)
        {
            return;
        }

        int randomNumber = Random.Range(0, 4);

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
}
