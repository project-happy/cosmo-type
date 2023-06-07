using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
    //, IPlayerLeft
{

    [SerializeField] float offsetSize = 3f;

    //public void PlayerLeft(PlayerRef player)
    //{
    //    if (!HasStateAuthority) return;
    //    Runner.Despawn(Object);
    //}

    public override void Spawned()
    {  // Use instead of Start/Awake for NetworkObjects
        /* Move to a random location around the current location */
        float offset_x = Random.Range(-offsetSize, offsetSize);
        float offset_y = Random.Range(-offsetSize, offsetSize);
        transform.position += new Vector3(offset_x, offset_y, 0);
    }
}