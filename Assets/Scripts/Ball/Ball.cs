using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPunCallbacks
{
    private Vector3 velocity;
    private string goal1p_strname = "goal_1p";
    private string goal2p_strname = "goal_2p";
    private string goal_strtag = "Goal";
    private string border_strtag = "Border";

    public void Init(Vector3 origin, float angle){
        transform.position = origin;
        velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void Start()
    {
        velocity = 9f * GetRandomVector();
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient){
            transform.position += velocity;
        }
    }

    // 画面外に移動したら削除する
    // Unityのエディター上ではシーンビューの画面を影響するので注意
    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
        transform.position = Vector3.zero;
        velocity = 9f * GetRandomVector();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(PhotonNetwork.IsMasterClient){
            if(other.tag.Equals("Goal")){
                    ResetPositionAndVector();
                if(other.name == "goal_1p"){
                    PhotonNetwork.CurrentRoom.AddScore(1,1);
                }else if(other.name == "goal_2p"){
                    PhotonNetwork.CurrentRoom.AddScore(2,1);
                }
            }
            else if(other.tag.Equals("Player")){
                velocity.Scale(new Vector3(-1,1,1));
            }else{
                velocity.Scale(new Vector3(1,-1,1));
            }
        }
    }
    
    private Vector3 GetRandomVector()
    {
        return new Vector3(Random.Range(-1f,1f),Random.Range(-0.3f,0.3f)).normalized;
    }

    private void ResetPositionAndVector()
    {
        transform.position = Vector3.zero;
        velocity = 9f * GetRandomVector();
    }
}
