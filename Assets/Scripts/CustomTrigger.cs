using System;
using UnityEngine;


public class CustomTrigger : MonoBehaviour
{


    public event Action<Collider2D> OnTriggerEntered;
    public event Action<Collider2D> OnTriggerStayed;
    public event Action<Collider2D> OnTriggerExited;



    //------------------------------------------------------------------------------------------------//



    private void OnTriggerEnter2D(Collider2D collision) => OnTriggerEntered?.Invoke(collision);

    private void OnTriggerStay2D(Collider2D collision) => OnTriggerStayed?.Invoke(collision);

    private void OnTriggerExit2D(Collider2D collision) => OnTriggerExited?.Invoke(collision);


}

