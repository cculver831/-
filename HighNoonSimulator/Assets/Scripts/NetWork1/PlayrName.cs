using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PlayrName : MonoBehaviour
{
    public void setPlayerName(string Pname)
    {
        if(string.IsNullOrEmpty(Pname))
        {
            Pname = "Player";
        }

        GameManager.Instance.name = Pname;

        PhotonNetwork.NickName = GameManager.Instance.name;
        Debug.Log(PhotonNetwork.NickName +" name Saved");
    }
}
