using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    private RoomControl _selectedRoom;

    public void SelectRoom(RoomControl room)
    {
        if (_selectedRoom == room)
            return;

        if (_selectedRoom != null)
            _selectedRoom.transform.Rotate(Vector3.forward * -90);

        room.transform.Rotate(Vector3.forward * 90);
        _selectedRoom = room;
    }

    public RoomTypes GetSelected()
    {
        if (_selectedRoom == null)
            return RoomTypes.None;

        return _selectedRoom.type;
    }
}
