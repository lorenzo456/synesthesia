using NAudio.Midi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class audiotest : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    GameManager GameManager;
    List<MidiEvent> data = new List<MidiEvent>();
    public int iterator = 0;
    private void Start()
    {
        GameManager = GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        clip = audioSource.clip;
        //audioSource.Play();
        ReadNotes();
        iterator = 0;
    }
    float prevTime = 0;
    bool first = false;
    private void Update()
    {
        //Debug.Log("prevTime: " + Math.Round(prevTime + float.Parse("0." + data[iterator].DeltaTime), 3) + " currentTime: "+ Math.Round(audioSource.time ,3));
        //Debug.Log(float.Parse("0." + Math.Round(data[iterator].DeltaTime * 2.604f)));
        float next = float.Parse("0." + Math.Round(data[iterator].DeltaTime * 2.604f));
        Debug.Log(Math.Round(audioSource.time, 3) + "  " + Math.Round(prevTime + next, 3));
        Debug.Log(next);

        if (Math.Round(audioSource.time,3) >= Math.Round(prevTime + next, 3))
        {
            Debug.Log("ACTIVATE");
            prevTime = audioSource.time + next;

            iterator++;
            string[] array = data[iterator].ToString().Split(' ');
            GameManager.ActivateMusicNoteByName(array[4], float.Parse("0." + array[7]));
        }

    }
    public void ReadNotes()
    {
        MidiFile midi = new MidiFile("Assets/Resources/midi1.mid");
        

        foreach (MidiEvent note in midi.Events[1])
        {
            if(note.CommandCode == MidiCommandCode.NoteOn )
            {
                data.Add(note);

            }
        }
        audioSource.Play();
        //StartCoroutine(playNoteCor(data[iterator].ToString(), data[iterator].DeltaTime, data[iterator].AbsoluteTime));

        //foreach (MidiEvent note in data)
        //{
        //    if (Application.isPlaying == true) { await PlayNoteAsync(note.ToString(), note.DeltaTime); } else { Application.Quit(); }

        //}

    }

    IEnumerator playNoteCor(string noteData, float nextNoteDeltaTime, long absoluteTime)
    {
        string[] array = noteData.Split(' ');
        GameManager.ActivateMusicNoteByName(array[4], float.Parse("0." + array[7]));
        Debug.Log(noteData);

        TimeSpan ts = TimeSpan.FromMilliseconds(absoluteTime);

        Debug.Log("source " +audioSource.time + "  absolute: " + ts);
        iterator++;
        yield return new WaitForSeconds(float.Parse("0." + nextNoteDeltaTime));
        StartCoroutine(playNoteCor(data[iterator].ToString(), data[iterator].DeltaTime, data[iterator].AbsoluteTime));
    }

    private async Task PlayNoteAsync(string noteData, float nextNoteDeltaTime)
    {
        if (Application.isPlaying == true) {
            string[] array = noteData.Split(' ');
            GameManager.ActivateMusicNoteByName(array[4], float.Parse("0." + array[7]));
            Debug.Log(array[4]);
            await Task.Delay(TimeSpan.FromMilliseconds(nextNoteDeltaTime));
        } else { Application.Quit(); }

        //Debug.Log(float.Parse("0."+array[7]));        
    }
}