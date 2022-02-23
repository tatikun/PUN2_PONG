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

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.tag.Equals(goal_strtag)){
      if (PhotonNetwork.LocalPlayer.ActorNumber == 1){
          if (other.name.Equals(goal1p_strname))
          {
            //1pのゴールに入ったときは2pの点を加算する
            PhotonNetwork.LocalPlayer.AddLostPoint(1);
            DestroyBall();
            photonView.RPC(nameof(DestroyBall), RpcTarget.Others);
          }
      }else{
          if (other.name.Equals(goal2p_strname))
          {
            //2pのゴールに入ったときは1pの点を加算する
            PhotonNetwork.LocalPlayer.AddLostPoint(1);
            DestroyBall();
            photonView.RPC(nameof(DestroyBall), RpcTarget.Others);
          }
      }
    }else if (other.tag.Equals(player_strtag)){
      if(PhotonNetwork.IsMasterClient){
        velocity.Scale(new Vector3(-1, 1, 1));
      }
    }
    else if (other.tag.Equals(boarder_strtag))
    {
        velocity.Scale(new Vector3(1, -1, 1));
    }
  }

  private Vector3 GetRandomVector()
  {
    float dirX = Random.Range(-1f,1f) > 0 ? -1f: -1f;
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
    Destroy(gameObject,0f);
  }
}
