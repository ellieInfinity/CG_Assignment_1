using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScreenCameraShader : MonoBehaviour
{
//public Shader awesomeShader = null;
    public Material m_renderMaterial1;
    public Material m_renderMaterial2;
    public Material m_renderMaterial3;
    public Material m_renderMaterial4;
    public Material m_renderMaterial;
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_renderMaterial);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            m_renderMaterial = m_renderMaterial1;
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            m_renderMaterial = m_renderMaterial2;
        }
        else if (Input.GetKey(KeyCode.Keypad3))
        {
            m_renderMaterial = m_renderMaterial3;
        }
        else if (Input.GetKey(KeyCode.Keypad4))
        {
            m_renderMaterial = m_renderMaterial4;
        }
    }
}