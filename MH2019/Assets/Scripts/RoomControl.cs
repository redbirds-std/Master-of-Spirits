using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public BuildController BuildController;
    public RoomTypes type;

    void OnMouseDown()
    {
        BuildController.SelectRoom(this);
    }
}
