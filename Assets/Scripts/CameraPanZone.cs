using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
public class CameraPanZone : MonoBehaviour
{

    private BoxCollider2D _bcol;

    private bool _inShop = false;



    //------------------------------------------------------------------------------------------------//



    private void Awake() => _bcol = GetComponent<BoxCollider2D>();


    private void OnEnable()
    {
        EventBus.Subscribe(EventType.OnRoundEnd, () => _bcol.isTrigger = true);
        EventBus.Subscribe(EventType.OnRoundStart, () => _bcol.isTrigger = false);
    }



    private void OnDisable()
    {
        EventBus.Unsubscribe(EventType.OnRoundEnd, () => _bcol.isTrigger = true);
        EventBus.Unsubscribe(EventType.OnRoundStart, () => _bcol.isTrigger = false);
    }



    //------------------------------------------------------------------------------------------------//



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_inShop) EventBus.Publish(EventType.OnEnterShop);
        else EventBus.Publish(EventType.OnExitShop);

        _inShop = !_inShop;
    }



}

