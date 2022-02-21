using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        //Temp
        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();
        Dictionary<int, Stat> dic = Managers.Data.StatDict;
    }


    public override void Clear()
    {

    }
}
