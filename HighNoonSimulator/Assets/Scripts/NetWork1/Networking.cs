using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Networking : MonoBehaviourPunCallbacks
{
    //New Shit
    #region Unity Methods
    [Header("IU Panels")]
    public GameObject CreateRoomPanel;
    public GameObject RoomList;
    [Header("Login UI Panel")]
    public InputField playerNameInput;

    [Header("RoomListing Prefab")]
    public GameObject RoomListingPrefab;

    [Header("RoomListing Spawn")]
    public GameObject RoomListSpawn;

    [Header("Lobby Room")]
    public GameObject startGame;
    public Text RoomInfoText;
    public GameObject PlayerListingPrefab;
    public GameObject PlayerListingContent;
    //For Room Lisitngs in Menu UI
    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> RoomListGO;
    private Dictionary<int, GameObject> PlayerListGOs;
    private void Start()
    {
        cachedRoomList = new Dictionary<string, RoomInfo>();
        RoomListGO = new Dictionary<string, GameObject>();

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //Old Shit
    [Header("Connection Status")]
    public Text connectionStatusText;
    public void OnConnectedToPhotonServer()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    private void Update()
    {
        connectionStatusText.text = "Connect Status: " +  PhotonNetwork.NetworkClientState;
        Debug.Log("Connection Status: " + PhotonNetwork.NetworkClientState);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " has been created");
        
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " has joined" + PhotonNetwork.CurrentRoom.Name);
        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGame.SetActive(true);

        }
        else
        {
            startGame.SetActive(false);
        }
        RoomInfoText.text = "Room Name " + PhotonNetwork.CurrentRoom.Name + " "
            + "Players/Max.players: " + PhotonNetwork.CurrentRoom.PlayerCount + " /"
            + PhotonNetwork.CurrentRoom.MaxPlayers;

        if(PlayerListGOs == null)
        {
            PlayerListGOs = new Dictionary<int, GameObject>();
        }
        

        /// PlayerName 
        foreach( Player player in PhotonNetwork.PlayerList)
        {
            GameObject PlayerLisitngGO = Instantiate(PlayerListingPrefab);
            PlayerLisitngGO.transform.SetParent(PlayerListingContent.transform);
            PlayerLisitngGO.transform.localScale = Vector3.one;

            PlayerLisitngGO.transform.Find("PlayerNameText").GetComponent<Text>().text = player.NickName;

            if(player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                PlayerLisitngGO.transform.Find("PlayerIndicator").gameObject.SetActive(true);
            }
            else
            {
                PlayerLisitngGO.transform.Find("PlayerIndicator").gameObject.SetActive(false);
            }

            PlayerListGOs.Add(player.ActorNumber, PlayerLisitngGO);

        } 


    }


    #endregion




    #region UI Callbacks
    public void onLoginClicked()
    {
        string PlayerName = playerNameInput.text;
        if(!string.IsNullOrEmpty(PlayerName))
        {
            PhotonNetwork.LocalPlayer.NickName = PlayerName;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("PlayerName Invalid");
        }
    }
    public void onShowRoomClicked()
    {
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
    public void OnBackClicked()
    {
        
            playerNameInput.text = "";
            PhotonNetwork.Disconnect();
            
      
             
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Player is Disconnected");
    }
    #endregion

    #region Photon Callbacks
    public override void OnConnected()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //ClearRoomListings();
        foreach(RoomInfo room in roomList)
        {
            
            if(!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if(cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList.Remove(room.Name);
                }
            }
            else
            {
                //update cached room List
                if(cachedRoomList.ContainsKey(room.Name))
                {
                    cachedRoomList[room.Name] = room;
                }
                else
                {
                    cachedRoomList.Add(room.Name, room);
                    
                }
            }
           
        }

        foreach(RoomInfo room in cachedRoomList.Values)
        {
            GameObject roomListEntryGO = Instantiate(RoomListingPrefab);
            roomListEntryGO.transform.SetParent(RoomListSpawn.transform);
            roomListEntryGO.transform.localScale = Vector3.one;

            roomListEntryGO .transform.Find("RoomNameText").GetComponent<Text>().text = room.Name;
            roomListEntryGO.transform.Find("RoomPlayersText").GetComponent<Text>().text = room.PlayerCount + "/" + room.MaxPlayers;
            roomListEntryGO.transform.Find("JoinRoomButton").GetComponent<Button>().onClick.AddListener(()=>OnJoinRoomButtonClicked(room.Name));
            roomListEntryGO.transform.Find("JoinRoomButton").GetComponent<Button>().onClick.AddListener(() => OnJoinRoomButtonClicked(room.Name));

            RoomListGO.Add(room.Name, RoomListingPrefab);
        }
    }

    public void OnStartGame()
    {
        if(PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Main");
    }
    #endregion

    #region Private Methods
    void OnJoinRoomButtonClicked(string _roomName)
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }

        PhotonNetwork.JoinRoom(_roomName);
        CreateRoomPanel.SetActive(true);
        RoomList.SetActive(false);
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject PlayerLisitngGO = Instantiate(PlayerListingPrefab);
        PlayerLisitngGO.transform.SetParent(PlayerListingContent.transform);
        PlayerLisitngGO.transform.localScale = Vector3.one;

        PlayerLisitngGO.transform.Find("PlayerNameText").GetComponent<Text>().text = newPlayer.NickName;

        if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            PlayerLisitngGO.transform.Find("PlayerIndicator").gameObject.SetActive(true);
        }
        else
        {
            PlayerLisitngGO.transform.Find("PlayerIndicator").gameObject.SetActive(false);
        }

        PlayerListGOs.Add(newPlayer.ActorNumber, PlayerLisitngGO);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomInfoText.text = "Room Name " + PhotonNetwork.CurrentRoom.Name + " "
         + "Players/Max.players: " + PhotonNetwork.CurrentRoom.PlayerCount + " /"
         + PhotonNetwork.CurrentRoom.MaxPlayers;
        Destroy(PlayerListGOs[otherPlayer.ActorNumber].gameObject);
        PlayerListGOs.Remove(otherPlayer.ActorNumber);
        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGame.SetActive(true);
        }
    }
    public override void OnLeftRoom()
    {

    }



    void ClearRoomListings()
    {
        foreach (var RoomListing in RoomListGO.Values)
        {
            Destroy(RoomListing);
        }
        RoomListGO.Clear();
    }
    #endregion
}
