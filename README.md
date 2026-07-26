# PhotonPUN_Rooms
Unity Photon PUN Simple Room System

DISCLAIMER:
Photon, Photon Engine, and PUN are trademarks of Exit Games GmbH. This project is an independent, unofficial set of helper scripts and is not affiliated with, endorsed by, or sponsored by Exit Games GmbH.

I do NOT own Photon or the Photon PUN system or name, this unity package and scripts 'RoomManager, NetworkSync, PlayerSpawner' all made by me.#

Credit for my work is not required but is appreciated. When using this for tutorials it's also appreciated that you use the github link rather than lock behind discord as the software should be easy for everyone but again not required just common courtesy.

Under MIT license.

HOW TO USE!

(DISCLAIMER: Photon PUN 2 package must be installed off the Unity Asset Store for this to work and you must use your own valid app ID from https://www.photonengine.com/)

- Download the PhotonPUN_Rooms.unitypackage and import it into your editor.
- Open PhotonPUN_Rooms/Prefabs/ and drag the prefab into your hierarchy and Unpack.
- Inside of that prefab is the RoomManager and the PlayerSpawner.
- Inside RoomManager you can set the 'Player Limit' to the maximum number of players per room.
- Then open PlayerSpawner and you will have a PlayerPrefab slot and spawn points.
- First off add your Player gameobject into a new folder in Assets/Resources so it's "e.g: Assets/Resources/Player.prefab".
- Then drag your Player prefab into the PlayerSpawner appropriate slot.
- Next create an empty gameobject at co-ordinates 0,0,0 and make a child empty gameobject and position it anywhere in your map and name it something you'll remember.
- Drag your child empty gameobject into PlayerSpawner's 'Spawn Points' section in the inspector.

---- From this stage you have set up the main logic and if you haven't already you now need to further set up:

- Open your Player.prefab and on the root of your player (where your movement script and charactercontroller/rigidbody is) and add a PhotonView component and the 'PlayerNetworkSync.cs' script and make sure playernetworksync is an observed component in your PhotonView.

--- Below this point it all todo with your PlayerController code:

- Add 'using Photon.Pun;' to the top of your PlayerController (script that defines player logic that you already have yourself).
- Add '[RequireComponent(typeof(PhotonView))]
public class YourPlayerController : MonoBehaviourPun' above your class
- In onEnable() add 'private void OnEnable()
{
    if (photonView.IsMine)
        inputActions.Player.Enable();

}'
- AT very top of Start() add 'if (!photonView.IsMine)
{
    if (cameraTransform != null)
        cameraTransform.gameObject.SetActive(false);

    return;
}' 
- At vert top of Update() before your logic add 'if (!photonView.IsMine)
    return;'


if anything doesn't make sense or is broking please reach out or feel free to push a fix!
