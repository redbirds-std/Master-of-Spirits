using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchControl : MonoBehaviour
{
    public BuildController BuildController;

    void OnMouseDown()
    {
        var selected = BuildController.GetSelected();

        if (selected == RoomTypes.None)
            return;

        GameStatus.Instance._battleRoom = selected;
        GameControl._instance.StartBattle();
    }
}
