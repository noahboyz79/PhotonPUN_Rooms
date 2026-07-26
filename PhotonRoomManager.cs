using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private byte playerLimit = 4;

    public static event Action<string> RoomJoined;
    public static event Action ConnectionFailed;

    public string CurrentRoomCode => PhotonNetwork.CurrentRoom != null ? PhotonNetwork.CurrentRoom.Name : null;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        Debug.Log("connecting to photon...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master, looking for a room with space");
        PhotonNetwork.JoinRandomRoom(null, playerLimit);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("no open room found (" + message + "), making a new one");
        CreateNewRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("room code collision or create failed (" + message + "), trying another code");
        CreateNewRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room " + PhotonNetwork.CurrentRoom.Name + " - " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers + " players");
        RoomJoined?.Invoke(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("disconnected from photon, reason: " + cause);
        ConnectionFailed?.Invoke();
    }

    private void CreateNewRoom()
    {
        string code = GenerateRoomCode();
        Debug.Log("trying to create room " + code);

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = playerLimit,
            IsVisible = true,
            IsOpen = true
        };

        PhotonNetwork.CreateRoom(code, options);
    }

    private string GenerateRoomCode()
    {
        return UnityEngine.Random.Range(100000, 999999).ToString();
    }
}