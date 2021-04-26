using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.transform.position = transform.position;
    }
}
