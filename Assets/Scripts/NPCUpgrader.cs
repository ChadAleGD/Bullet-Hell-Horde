using UnityEngine;



public class NPCUpgrader : MonoBehaviour, IInteractable
{


    [SerializeField] private GameObject _hoverObject;
    private SpriteRenderer _spriteRenderer;



    //------------------------------------------------------------------------------------------------//




    private void Start()
    {
        _spriteRenderer = _hoverObject.GetComponent<SpriteRenderer>();
    }





    //------------------------------------------------------------------------------------------------//


    public void Interact() => OpenShop();


    public void InRange() => DisplayInteract();

    public void LeftRange() => HideInteract();



    private void DisplayInteract()
    {
        _spriteRenderer.enabled = true;
    }

    private void HideInteract()
    {
        _spriteRenderer.enabled = false;
    }


    private void OpenShop()
    {
        EventBus.Publish(EventType.OnUpgradePanelOpen);
    }

}

