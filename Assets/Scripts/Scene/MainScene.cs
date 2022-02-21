using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにしている
public class MainScene : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float playerDefaultPositionX = 0f;
    private bool joinedRoom = false;
    void Start()
    {
        joinedRoom = false;
        // プレイヤー自身の名前を"Player"に設定する
        PhotonNetwork.NickName = "Player";

        // PhotonServerSettingsの設定内容を使ってマスターサーバーに接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // ゲームサーバーへの接続が成功したときに呼ばれるコールバック
    public override void OnJoinedRoom(){
        if(PhotonNetwork.CurrentRoom.PlayerCount == 2){
            PhotonNetwork.Instantiate("Ball",Vector3.zero,Quaternion.identity);
            PhotonNetwork.CurrentRoom.SetScore(1,0);
            PhotonNetwork.CurrentRoom.SetScore(2,0);
            PhotonNetwork.CurrentRoom.SendRoomProperties();
        }

        var position = PhotonNetwork.LocalPlayer.ActorNumber == 1 ? new Vector3(playerDefaultPositionX, 0) : new Vector3(-playerDefaultPositionX, 0);
        PhotonNetwork.Instantiate("Player",position,Quaternion.identity);
        joinedRoom = true;
    }

    private void LateUpdate() {
        if(joinedRoom && PhotonNetwork.IsMasterClient){
            PhotonNetwork.CurrentRoom.SendRoomProperties();
        }
    }

}
