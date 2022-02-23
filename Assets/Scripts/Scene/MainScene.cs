using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにしている
public class MainScene : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private float playerDefaultPositionX = 0f;
  private bool joinedRoom = false;
  [SerializeField]
  private BallSpawner ballSpawner = default;
  void Start()
  {
    joinedRoom = false;
    // プレイヤー自身の名前を"Player"に設定する
    PhotonNetwork.NickName = "Player";

    // PhotonServerSettingsの設定内容を使ってマスターサーバーに接続する
    PhotonNetwork.ConnectUsingSettings();
  }

  // ゲームサーバーへの接続が成功したときに呼ばれるコールバック
  public override void OnJoinedRoom()
  {


    var position = PhotonNetwork.LocalPlayer.ActorNumber == 1 ? new Vector3(playerDefaultPositionX, 0) : new Vector3(-playerDefaultPositionX, 0);
    PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
    joinedRoom = true;
  }


  public override void OnLeftRoom()
  {
    joinedRoom = false;
  }

  public override void OnPlayerEnteredRoom(Player newPlayer)
  {
    if (PhotonNetwork.IsMasterClient)
    {
      ballSpawner.SpawnBall();
    }
  }

}
