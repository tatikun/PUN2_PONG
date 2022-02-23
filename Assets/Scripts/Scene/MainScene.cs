using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにしている
public class MainScene : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private float m_playerDefaultPositionX = 7.5f; // プレイヤーの初期X座標
  private bool m_joinedRoom = false; // ルームへ参加しているかどうか
  [SerializeField]
  private BallSpawner m_ballSpawner = default; // ボール生成オブジェクト
  void Start()
  {
    m_joinedRoom = false;
    // プレイヤー自身の名前を"Player"に設定する
    PhotonNetwork.NickName = "Player";

    // PhotonServerSettingsの設定内容を使ってマスターサーバーに接続する
    PhotonNetwork.ConnectUsingSettings();
  }

  // ゲームサーバーへの接続が成功したときに呼ばれるコールバック
  public override void OnJoinedRoom()
  {
    var position = PhotonNetwork.LocalPlayer.ActorNumber == 1 ? new Vector3(m_playerDefaultPositionX, 0) : new Vector3(-m_playerDefaultPositionX, 0);
    PhotonNetwork.Instantiate("Player", position, Quaternion.identity);
    m_joinedRoom = true;
  }

  public override void OnLeftRoom()
  {
    m_joinedRoom = false;
  }

  public override void OnPlayerEnteredRoom(Player newPlayer)
  {
    if (PhotonNetwork.IsMasterClient)
    {
      m_ballSpawner.SpawnBall();
    }
  }

}
