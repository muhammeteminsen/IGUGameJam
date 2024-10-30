using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class SpringSway : MonoBehaviour
{
    [Header("Spring Sway Variables")]
    [SerializeField] private float swayAmount = 0.02f; 
    [SerializeField] private float swaySmoothness = 6f; 
    [SerializeField] private float returnSpeed = 4f; 
    
    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.localPosition; 
    }

    private void Update()
    {
        ApplySway();
    }

    private void ApplySway()
    {
       
        float mouseX = Input.GetAxis("Mouse X") * swayAmount;
        float mouseY = Input.GetAxis("Mouse Y") * swayAmount;
        Vector3 targetPosition = new Vector3(-mouseX, -mouseY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, _initialPosition + targetPosition, Time.deltaTime * swaySmoothness);
        if (mouseX == 0 && mouseY == 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _initialPosition, Time.deltaTime * returnSpeed);
        }
    }

   

   
}