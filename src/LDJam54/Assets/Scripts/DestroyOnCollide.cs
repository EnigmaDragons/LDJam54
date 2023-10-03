using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            var player = other.gameObject;
            var spawnPoint = GameObject.FindWithTag("Respawn");
            if (WorkdayState.IsWorkdayStarted && !WorkdayState.IsWorkdayEnded)
                spawnPoint = GameObject.FindWithTag("RespawnWork");
            var rb = player.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            player.transform.position = spawnPoint.transform.position;
            player.transform.rotation = spawnPoint.transform.rotation;
            FirstPersonInteractionStatus.IsEnabled = true;
            rb.isKinematic = false;
        }
        else
            Destroy(other.gameObject);
    }
}