using System.Collections;
using UnityEngine;




public class FillBar : MonoBehaviour
{


    [SerializeField] private GameObject _fillObject;


    [Header("Bar Values")]
    [SerializeField] private float _currentFillAmount;
    [SerializeField] private float _totalFillAmount;


    //------------------------------------------------------------------------------------------------//



    private void Start()
    {
        StartCoroutine(Starty());
    }




    private void OnEnable()
    {

    }


    private void OnDisable()
    {

    }




    private IEnumerator Starty()
    {
        yield return new WaitForSeconds(7f);
        UpdateBar();
    }





    //------------------------------------------------------------------------------------------------//


    private void UpdateBar()
    {




        var fillRatio = Mathf.Clamp01(_currentFillAmount / _totalFillAmount);

        LeanTween.scaleX(_fillObject, fillRatio, .2f);

    }


}

