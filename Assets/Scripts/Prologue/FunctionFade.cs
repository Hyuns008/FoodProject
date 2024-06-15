using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]//�� ��ũ��Ʈ�� �����ϴ� ȯ�濡 �ش� ������Ʈ�� ������ ���, ������ ���� ,��Ʈ����Ʈ
public class FunctionFade : MonoBehaviour
{
    public static FunctionFade Instance;

    Image imgFade;
    [SerializeField] float fadeTime = 1.0f;//���̵� �Ǵ� �ð� �� �����Ǵ� �ð�

    UnityAction actionFadeOut;//���̵� �ƿ� �Ǿ����� ������ ���
    UnityAction actionFadeIn;//���̵� �� �Ǿ����� ������ ���

    bool fade = true;//true�� In false�� Out;

    private void Awake()
    {
        imgFade = GetComponent<Image>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (fade && imgFade.color.a > 0)
        {
            Color color = imgFade.color;
            color.a -= Time.deltaTime / fadeTime;
            if (color.a < 0)
            {
                color.a = 0;
                invokeAction(fade);
            }
            imgFade.color = color;
        }
        else if (!fade && imgFade.color.a < 1)
        {
            Color color = imgFade.color;
            color.a += Time.deltaTime / fadeTime;
            if (color.a > 1)
            {
                color.a = 1;
                invokeAction(fade);
            }
            imgFade.color = color;
        }

        imgFade.raycastTarget = imgFade.color.a != 0;
    }

    private void invokeAction(bool _fade)
    {
        switch (_fade)
        {
            case true:
                {
                    if (actionFadeIn != null)
                    {
                        actionFadeIn.Invoke();
                        actionFadeIn = null;
                    }
                }
                break;

            case false:
                {
                    if (actionFadeOut != null)
                    {
                        actionFadeOut.Invoke();
                        actionFadeOut = null;
                    }
                }
                break;
        }
    }

    /// <summary>
    /// ���̵� ����� �����մϴ�
    /// </summary>
    /// <param name="_fade">true�� In false �� Out</param>
    /// <param name="_action">ture�Ͻ� In�� �ɶ� �����, false�϶� Out����� ����մϴ�</param>
    public void SetActive(bool _fade, UnityAction _action = null)
    {
        fade = _fade;
        switch (_fade)
        {
            case true: actionFadeIn = _action; break;
            case false: actionFadeOut = _action; break;
        }
    }
}
