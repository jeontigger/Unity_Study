using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UI_Button : UI_Popup
{
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

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);


    }
    public void OnButtonClicked(PointerEventData data)
    {
        Debug.Log("Button Clicked!");

        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }
}
