using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("PlayerSpawner: no player prefab assigned in the inspector");
            return;
        }

        StartCoroutine(WaitForRoomThenSpawn());
    }

    private IEnumerator WaitForRoomThenSpawn()
    {
        Debug.Log("waiting for room before spawning...");

        while (!PhotonNetwork.IsConnectedAndReady || !PhotonNetwork.InRoom)
            yield return null;

        Debug.Log("room confirmed, spawning now");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("PlayerSpawner: no spawn points assigned, spawning at origin");
            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
            return;
        }

        int index = (PhotonNetwork.LocalPlayer.ActorNumber - 1) % spawnPoints.Length;
        Transform point = spawnPoints[index];

        Debug.Log("spawning local player at spawn point " + index);
        GameObject spawned = PhotonNetwork.Instantiate(playerPrefab.name, point.position, point.rotation);
        Debug.Log("instantiate returned: " + (spawned == null ? "null" : spawned.name));
    }
}