using System.Collections;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour, IPickupable
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



    private void OnTriggerEnter2D(Collider2D collision) => StartCoroutine(PickUp());



    public IEnumerator PickUp()
    {
        _boxCollider.enabled = false;
        _spriteRenderer.enabled = false;
        CurrencyBank.AddCoins(3);

        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(gameObject);
    }


}
