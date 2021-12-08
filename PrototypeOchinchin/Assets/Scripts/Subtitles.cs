using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Subtitles : MonoBehaviour
{

    public GameObject Triger;
    public GameObject textbox;
    UIObject Show;


    void Start()
    {
        textbox.GetComponent<Text>().text = "";
        Show = Triger.GetComponent<UIObject>();
        Show.Shows += ShowSubs;
    }


    IEnumerator TheSequence()
    {

        yield return new WaitForSeconds(0);
        

        if (Show.UIShow == true)
        {
            yield return new WaitForSeconds(2);
            textbox.GetComponent<Text>().text = "- ���, ��� �����������?";
            yield return new WaitForSeconds(3);
            textbox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(2);
            textbox.GetComponent<Text>().text = "- ���, ��� ������";
        }
    }
    public void ShowSubs()
    {
        StartCoroutine(TheSequence());
    }
}
