using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTriggerZone : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            LevelBiome biome = LevelManager.Instance.levelBiomeOrder[0];
            audioSource.clip = Resources.Load<AudioClip>("Audio/" + biome);
            audioSource.Play();
            SceneManager.LoadScene(1);
        }
    }
}
