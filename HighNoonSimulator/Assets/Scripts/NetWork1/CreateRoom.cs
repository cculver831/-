using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class CreateRoom : MonoBehaviour
{
    public InputField roomNamefield;
    public InputField maxPlayers;
    public GameObject InputFields;
   public void CreateRoomButton()
    {
        string roomName = roomNamefield.text;
        if(string.IsNullOrEmpty(roomName))
        {
            roomName = " Room " + Random.Range(1000, 1000);

        }
        //Create Room Options with maxplayers

        RoomOptions roomOpt = new RoomOptions();
        roomOpt.MaxPlayers = (byte) int.Parse(maxPlayers.text);
        PhotonNetwork.CreateRoom(roomName,roomOpt);
        InputFields.SetActive(false);
        
    }
}
