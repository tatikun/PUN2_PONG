using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    void Update()
    {
        // 自身が生成したオブジェクトだけに移動処理を行う
        if(photonView.IsMine){
            var input = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            transform.Translate(6f * Time.deltaTime * input.normalized);
        }
    }
}
