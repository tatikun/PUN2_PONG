using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPunCallbacks
{
  private Vector3 velocity;
  private string goal1p_strname = "goal_1p";
  private string goal2p_strname = "goal_2p";
  private string goal_strtag = "Goal";
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
    photonView.RPC(nameof(DestroyBall), RpcTarget.All);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (PhotonNetwork.IsMasterClient)
    {
      if (other.tag.Equals(goal_strtag))
      {
        if (other.name.Equals(goal1p_strname))
        {
          PhotonNetwork.CurrentRoom.AddScore(1, 1);
        }
        else if (other.name.Equals(goal2p_strname))
        {
          PhotonNetwork.CurrentRoom.AddScore(2, 1);
        }
        photonView.RPC(nameof(DestroyBall), RpcTarget.All);
      }
      else if (other.tag.Equals("Player"))
      {
        velocity.Scale(new Vector3(-1, 1, 1));
      }
      else
      {
        velocity.Scale(new Vector3(1, -1, 1));
      }
    }
  }

  private Vector3 GetRandomVector()
  {
    return new Vector3(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f)).normalized;
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
}
