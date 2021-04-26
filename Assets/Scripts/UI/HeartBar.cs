using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HeartBar : MonoBehaviour
{
    private CharacterController character;

    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private GameObject emptyHeartGO;

    private List<UnityEngine.UI.Image> hearts;
    
    void Start()
    {
        hearts = new List<UnityEngine.UI.Image>();
        character = FindObjectOfType<PlayerController>();
        character.onHit += UpdateHealthBar;
        character.onHeal += UpdateHealthBar;

        InitHealthBar();
    }

    private void InitHealthBar()
    {
        
        for (int i = 0; i < character.MaxHealth; i++)
        {
            var newHeart = Instantiate(emptyHeartGO, transform).GetComponent<UnityEngine.UI.Image>();
            hearts.Add(newHeart);
            hearts[i].sprite = fullHeart;
        }

        UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
        if (hearts.Count < character.MaxHealth)
        {
            for (int i = 0; i < character.MaxHealth - hearts.Count; i++)
            {
                var newHeart = Instantiate(emptyHeartGO, transform).GetComponent<UnityEngine.UI.Image>();
                hearts.Add(newHeart);
            }
        }

        int iteratedHeart = 0;
        foreach (var heart in hearts)
        {

            if (iteratedHeart < character.Health)
            {
                heart.sprite = fullHeart;
            }
            else
            {
                heart.sprite = emptyHeart;
            }
            iteratedHeart++;

        }
    }

}
