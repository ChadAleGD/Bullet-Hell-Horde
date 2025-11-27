using System.Collections;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }




    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        _boxCollider.enabled = false;
        _spriteRenderer.enabled = false;
        CurrencyBank.AddCoins(3);

        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(gameObject);
    }

}
