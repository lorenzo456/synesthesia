using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLight : MonoBehaviour
{
    public string tone;
    public bool active;
    public float lightIncrement;
    public Color color;
    Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        myLight.intensity = 10;
    }
    public void Activate(float time)
    {
        StartCoroutine("ActivateTone", time);
        // Debug.Log("activated  " + time);
    }
    IEnumerator ActivateTone(float timeActive)
    {
        active = true;
        if (myLight.intensity < 10) myLight.intensity = 10;
        yield return new WaitForSeconds(timeActive);
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {

            myLight.intensity = myLight.intensity + lightIncrement * Time.deltaTime;
        }
        else
        {
            myLight.intensity = 0;
        }
    }
}
