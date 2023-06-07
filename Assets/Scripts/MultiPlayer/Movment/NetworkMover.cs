using Fusion;
using UnityEngine;

// From Fusion tutorial: https://doc.photonengine.com/fusion/current/tutorials/shared-mode-basics/3-movement-and-camera
public class NetworkMover : NetworkBehaviour
{
    [SerializeField] Vector2 speed = new Vector2(0, -4);

    public override void FixedUpdateNetwork()
    {
        transform.position += new Vector3(speed.x, speed.y) * Runner.DeltaTime;
    }
}

