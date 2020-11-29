using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote : MonoBehaviour
{
    public string tone;
    public Color color;
    public float brightness;
    public float brightnessTempo;
    public bool active;

    Renderer myRenderer;
    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }
    public void Activate(float time)
    {
        StartCoroutine("ActivateTone", time);
       // Debug.Log("activated  " + time);
    }
    IEnumerator ActivateTone(float timeActive)
    {
        active = true;
        brightness = 0;
        myRenderer.material.color = color;
        yield return new WaitForSeconds(timeActive);
        myRenderer.material.color = Color.gray;
        active = false;
    }

}
