using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UI_Button : UI_Base
{
    [SerializeField]
    Text _text;
    int _score = 0;

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum Buttons
    {
        PointButton,
    }

    enum GameObjects
    {
        TestObject,
    }
    enum Images
    {
        ItemIcon,
    }

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += ((PointerEventData data) => { go.gameObject.transform.position = data.position; });



    }
    public void OnButtonClicked()
    {
        Debug.Log("Button Clicked!");

        _score++;
        _text.text = $"점수 : {_score}점";
    }
}
