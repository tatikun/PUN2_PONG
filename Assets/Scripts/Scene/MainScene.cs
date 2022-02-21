using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにしている
public class MainScene : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーに接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功したときに呼ばれるコールバック
    public override void OnConnectedToMaster(){
        // "Room"という名前のルームに参加する(ルームが存在しなければ作成して参加する)
        PhotonNetwork.JoinOrCreateRoom("Room",new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功したときに呼ばれるコールバック
    public override void OnJoinedRoom(){
        var position = new Vector3(Random.Range(-3f,3f), Random.Range(-3f,3f));
        PhotonNetwork.Instantiate("Player",position,Quaternion.identity);
    }
}
