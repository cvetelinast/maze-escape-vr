using UnityEngine;

public class PlayerCollideController : MonoBehaviour {

    public event System.Action<GameObject> OnPlayerCollideWithFinishLine;

    public event System.Action<GameObject> OnPlayerCollideWithCoin;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject hitObject = hit.gameObject;

        if (hitObject.tag == "Finish")
        {
            OnPlayerCollideWithFinishLine?.Invoke(hitObject);
        }

        if (hitObject.tag == "Coin")
        {
            OnPlayerCollideWithCoin?.Invoke(hitObject);
        }
    }
}
