using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPunCallbacks
{
  // if文等で比較する際に使用するオブジェクト名やタグ名の文字列
  private const string STRNAME_GOAL1P = "goal_1p";
  private const string STRNAME_GOAL2P = "goal_2p";
  private const string STRTAG_GOAL = "Goal";
  private const string STRTAG_BORDER = "Border";
  private const string STRTAG_PLAYER = "Player";

  private Vector3 m_velocity; // 移動方向ベクトル
  [SerializeField]
  private float m_speed; // 移動速度
  private BallSpawner m_ballSpawner; // ボール生成用ゲームオブジェクト
  
  private void Start()
  {
    ResetPositionAndVector();
    m_ballSpawner = GameObject.FindWithTag("Respawn").GetComponent<BallSpawner>();
  }

  private void Update()
  {
    // ボールの位置の移動はマスター側で行う
    if (PhotonNetwork.IsMasterClient)
    {
      transform.position += m_velocity * Time.deltaTime;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.tag.Equals(STRTAG_GOAL)){ // ゴール判定
      if (PhotonNetwork.LocalPlayer.ActorNumber == 1){
          if (other.name.Equals(STRNAME_GOAL1P))
          {
            //1p側のゴールに入ったときは2pの点(1pの失点)を加算する
            PhotonNetwork.LocalPlayer.AddLostPoint(1);
            DestroyBall();
            photonView.RPC(nameof(DestroyBall), RpcTarget.Others);
          }
      }else{
          if (other.name.Equals(STRNAME_GOAL2P))
          {
            //2p側のゴールに入ったときは1pの点(2pの失点)を加算する
            PhotonNetwork.LocalPlayer.AddLostPoint(1);
            DestroyBall();
            photonView.RPC(nameof(DestroyBall), RpcTarget.Others);
          }
      }
    }else if (other.tag.Equals(STRTAG_PLAYER)){
      if(PhotonNetwork.IsMasterClient){ // 跳ね返る処理もマスター側で行う
        m_velocity.Scale(new Vector3(-1, 1, 1));
      }
    }
    else if (other.tag.Equals(STRTAG_BORDER))
    {
      if(PhotonNetwork.IsMasterClient){ // 跳ね返る処理もマスター側で行う
        m_velocity.Scale(new Vector3(1, -1, 1));
      }
    }
  }

  // 乱数で移動方向ベクトルを取得
  private Vector3 GetRandomVector()
  {
    float dirX = Random.Range(-1f,1f) > 0 ? -1f: -1f;
    float dirY = Random.Range(-1f,1f) > 0 ? 1f: -1f;
    dirY *= Random.Range(0.4f,0.7f); //浅すぎず深すぎない角度への調整
    return new Vector3(dirX, dirY).normalized;
  }

  // 位置、角度の初期化処理
  private void ResetPositionAndVector()
  {
    transform.position = Vector3.zero;
    m_velocity = m_speed * GetRandomVector();
  }

  private void OnDestroy()
  {
    // Destroy後、再生成を行う
    m_ballSpawner.RespwanBall();
  }

  [PunRPC]
  private void DestroyBall()
  {
    Destroy(gameObject,0f);
  }
}
