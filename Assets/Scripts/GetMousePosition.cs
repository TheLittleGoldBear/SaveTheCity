using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class GetMousePosition : MonoBehaviour
{
    [SerializeField] private Camera m_camera;
    [SerializeField] private Transform m_image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 position = m_camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            position.z = 0.0f;
            
            m_image.position = position;
        }
    }
}
