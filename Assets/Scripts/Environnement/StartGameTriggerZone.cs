using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTriggerZone : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private GameObject endWall;

    private void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();

        if (LevelManager.Instance.currentBiomeIndex >= LevelManager.Instance.levelBiomeOrder.Count - 1)
        {
            gameObject.SetActive(false);
            endWall.SetActive(true);
        }
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
