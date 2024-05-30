using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureController : MonoBehaviour
{
    private PictureManager pictureManager;

    [Header("�׸� ����")]
    [SerializeField, Tooltip("���� ���������� �ð�")] private float nextTime;
    private float timer;
    private List<int> nextNumber = new List<int>();
    private int nextIndex;

    private void Start()
    {
        pictureManager = PictureManager.Instance;

        timer = nextTime;
    }

    private void Update()
    {
        nextPicture();
        timerCheck();
    }

    private void timerCheck()
    {
        if (nextNumber.Count >= 4)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
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
        }
        else if (nextNumber.Count != 0)
        {
            if (nextNumberCheck(randomNumber) == false)
            {
                nextNumber.Add(randomNumber);
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
        for (int i = 0; i < 4; i++)
        {
            if (nextNumber[i] == _number)
            {
                return true;
            }
        }

        return false;
    }
}
