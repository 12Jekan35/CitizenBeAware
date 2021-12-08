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
            textbox.GetComponent<Text>().text = "- Ало, это крастикрабс?";
            yield return new WaitForSeconds(3);
            textbox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(2);
            textbox.GetComponent<Text>().text = "- Нет, это Патрик";
        }
    }
    public void ShowSubs()
    {
        StartCoroutine(TheSequence());
    }
}
