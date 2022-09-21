using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public int goalColorInt;
    public float dispenserCoolDown;
    public bool OnCoolDown=false;
    public Color goalColor;
    private float timer=0;
    private MeshRenderer meshRenderer;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        goalColor = meshRenderer.material.color;
    }
    private void Update()
    {
        if (OnCoolDown)
        {
            timer += Time.deltaTime;
            meshRenderer.material.color = Color.Lerp(Color.white, goalColor, timer / dispenserCoolDown);
            if (timer >= dispenserCoolDown)
            {
                timer = 0;
                OnCoolDown = false;
            }
        }
    }
}
