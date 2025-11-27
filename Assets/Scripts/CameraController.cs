using System.Collections;
using UnityEngine;



[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

   private Camera _camera;

   [SerializeField] private Transform _shopPosition;
   [SerializeField] private Transform _combatPosition;




   //------------------------------------------------------------------------------------------------//



   private void Awake() => _camera = GetComponent<Camera>();



   private void OnEnable()
   {
      EventBus.Subscribe(EventType.OnEnterShop, PanToShop);
      EventBus.Subscribe(EventType.OnExitShop, PanToCombat);
   }


   private void OnDisable()
   {
      EventBus.Unsubscribe(EventType.OnEnterShop, PanToShop);
      EventBus.Unsubscribe(EventType.OnExitShop, PanToCombat);
   }



   //------------------------------------------------------------------------------------------------//



   private void PanToShop() => LeanTween.move(gameObject, _shopPosition, 1f).setEase(LeanTweenType.easeInOutQuad);

   private void PanToCombat() => LeanTween.move(gameObject, _combatPosition, 1f).setEase(LeanTweenType.easeInOutQuad);


}