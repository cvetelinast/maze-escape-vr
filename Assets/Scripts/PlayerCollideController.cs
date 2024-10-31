using UnityEngine;

public class PlayerCollideController : MonoBehaviour {

    public event System.Action<GameObject> OnPlayerCollideWithFinishLine;

    public event System.Action<GameObject> OnPlayerCollideWithCoin;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject hitObject = hit.gameObject;

        if (hitObject.tag == Constants.FINISH_TAG)
        {
            OnPlayerCollideWithFinishLine?.Invoke(hitObject);
        }

        if (hitObject.tag == Constants.COIN_TAG)
        {
            OnPlayerCollideWithCoin?.Invoke(hitObject);
        }
    }
}
