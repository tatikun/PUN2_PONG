using Photon.Pun;
using UnityEngine;


public class BallSpawner : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private Ball ball;

  [SerializeField]
  private float respawnTime;
  private float elapsedRespawnTime;
  private bool setRespawnTimer;

  private void Start()
  {
    setRespawnTimer = false;
  }
  private void Update()
  {
    if (setRespawnTimer)
    {
      elapsedRespawnTime += Time.deltaTime;
      if (elapsedRespawnTime >= respawnTime)
      {
        SpawnBall();
        elapsedRespawnTime = 0;
        setRespawnTimer = false;
      }
    }

  }
  public void SpawnBall()
  {
    if (PhotonNetwork.IsMasterClient)
      PhotonNetwork.Instantiate("Ball", Vector3.zero, Quaternion.identity);
  }

  public void RespwanBall()
  {
    setRespawnTimer = true;
  }
}