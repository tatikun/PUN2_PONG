using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPunCallbacks
{
  private Vector3 velocity;
  private string goal1p_strname = "goal_1p";
  private string goal2p_strname = "goal_2p";
  private string goal_strtag = "Goal";

  private string boarder_strtag = "Border";
  private string player_strtag = "Player";
  [SerializeField]
  private float speed;
  private BallSpawner ballSpawner;
  private void Start()
  {
    ResetPositionAndVector();
    ballSpawner = GameObject.FindWithTag("Respawn").GetComponent<BallSpawner>();
  }

  private void Update()
  {
    if (PhotonNetwork.IsMasterClient)
    {
      transform.position += velocity * Time.deltaTime;
    }
  }

  // 画面外に移動したら削除する
  // Unityのエディター上ではシーンビューの画面を影響するので注意
  private void OnBecameInvisible()
  {
    photonView.RPC(nameof(DestroyBall), RpcTarget.AllViaServer);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.tag.Equals(goal_strtag)){
      if (!PhotonNetwork.IsMasterClient){
          if (other.name.Equals(goal1p_strname))
          {
            //1pのゴールに入ったときは2pの点を加算する
            PhotonNetwork.LocalPlayer.AddScore(1);
            PhotonNetwork.CurrentRoom.AddScore(2, 1);
            photonView.RPC(nameof(DestroyBall), RpcTarget.AllViaServer);
          }
      }else{
          if (other.name.Equals(goal2p_strname))
          {
            //2pのゴールに入ったときは1pの点を加算する
            PhotonNetwork.LocalPlayer.AddScore(1);
            PhotonNetwork.CurrentRoom.AddScore(1, 1);
            photonView.RPC(nameof(DestroyBall), RpcTarget.AllViaServer);
          }
      }
    }else if (other.tag.Equals(player_strtag)){
        photonView.RPC(nameof(BoundBall),RpcTarget.MasterClient);
    }
    else if (other.tag.Equals(boarder_strtag))
    {
        velocity.Scale(new Vector3(1, -1, 1));
    }
  }

  private Vector3 GetRandomVector()
  {
    float dirX = Random.Range(-1f,1f) > 0 ? 1f: -1f;
    float dirY = Random.Range(-1f,1f) > 0 ? 1f: -1f;
    dirY *= Random.Range(0.4f,0.7f);
    return new Vector3(dirX, dirY).normalized;
  }

  private void ResetPositionAndVector()
  {
    transform.position = Vector3.zero;
    velocity = speed * GetRandomVector();
  }

  private void OnDestroy()
  {
    ballSpawner.RespwanBall();
  }

  [PunRPC]
  private void DestroyBall()
  {
    Destroy(gameObject);
  }

  [PunRPC]
  private void BoundBall()
  {
    velocity.Scale(new Vector3(-1, 1, 1));
  }
}
