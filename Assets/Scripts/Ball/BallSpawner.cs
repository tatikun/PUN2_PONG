using Photon.Pun;
using UnityEngine;


public class BallSpawner : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private Ball m_ball;

  [SerializeField]
  private float m_respawnTime; // 再生成時間
  private float m_elapsedRespawnTime; // 再生成までの経過時間
  private bool m_setRespawnTimer; // 再生成するかのフラグ

  private void Start()
  {
    m_setRespawnTimer = false;
  }
  private void Update()
  {
    if (m_setRespawnTimer)
    {
      m_elapsedRespawnTime += Time.deltaTime;
      if (m_elapsedRespawnTime >= m_respawnTime)
      {
        SpawnBall();
        m_elapsedRespawnTime = 0;
        m_setRespawnTimer = false;
      }
    }

  }

  // ボールの生成
  public void SpawnBall()
  {
    if (PhotonNetwork.IsMasterClient)
      PhotonNetwork.Instantiate("Ball", Vector3.zero, Quaternion.identity);
  }

  // ボールの再生成フラグをオンにする
  public void RespwanBall()
  {
    m_setRespawnTimer = true;
  }
}