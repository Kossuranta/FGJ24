using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ExampleClass : MonoBehaviour, IPointerEnterHandler// required interface when using the OnPointerEnter method.
{
    public RectTransform gameObjectToMove;

    private Vector2 targetPosition;

    private Vector2 startPosition;

    private float time;

    public float speed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        
         
        int lowerBound_x = Random.Range(-480, 480);
        int upperBound_y = Random.Range(-280, 280);

        targetPosition = new Vector2(lowerBound_x, upperBound_y);
        startPosition = gameObjectToMove.anchoredPosition;
        time = 0;

        

        
    }

    private void Update()
    {
        if (time > 1)
            return;
        time += Time.deltaTime*speed;
        gameObjectToMove.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, time);
    }



}
