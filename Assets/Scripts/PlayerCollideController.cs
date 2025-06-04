using UnityEngine;

public class PlayerCollideController : MonoBehaviour {

    public event System.Action<GameObject> OnPlayerCollideWithFinishLine;

    public event System.Action<GameObject> OnPlayerCollideWithCoin;

    void OnTriggerEnter(Collider hit)
    {
        GameObject hitObject = hit.gameObject;
        Debug.Log("Player collided with: " + hitObject.name + " with tag: " + hitObject.tag);

        if (hitObject.tag == Constants.FINISH_TAG)
        {
            Debug.Log("Player collided with finish line: " + hitObject.name);
            OnPlayerCollideWithFinishLine?.Invoke(hitObject);
        }

        if (hitObject.tag == Constants.COIN_TAG)
        {
            Debug.Log("Player collided with coin: " + hitObject.name);
            OnPlayerCollideWithCoin?.Invoke(hitObject);
        }
    }
}
