using NAudio.Midi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ripped : MonoBehaviour
{
    public MidiFile midi;

    public float ticks;
    public float offset;
    GameManager gameManager;
    Melanchall.DryWetMidi.Interaction.TempoMap tempoMap;

    // Use this for initialization
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        midi = new MidiFile("Assets/Resources/plin.mid");
        //Read the file
        //Ticks needed for timing calculations
        ticks = midi.DeltaTicksPerQuarterNote;
        Debug.Log(ticks);
        StartPlayback();
    }

    public void StartPlayback()
    {
        GetComponent<AudioSource>().Play();

        foreach (MidiEvent x in midi.Events[0])
        {
            Debug.Log(x);
        }
        foreach (MidiEvent note in midi.Events[1])
        {
            Debug.Log(note);
            //If its the start of the note event
            if (note.CommandCode == MidiCommandCode.NoteOn)
            {
                //Cast to note event and process it
                NoteOnEvent noe = (NoteOnEvent)note;
                NoteEvent(noe);
                
            }
        }
        
    }

    public void NoteEvent(NoteOnEvent noe)
    {
        //The bpm(tempo) of the track
        float bpm = 135;

        //Time until the start of the note in seconds
        float time = (60 * noe.AbsoluteTime) / (bpm * ticks);

        string noteNumber = noe.NoteName;
        
        //Start coroutine for each note at the start of the playback
        StartCoroutine(CreateAction(time, noteNumber, noe.NoteLength * 0.0001f));
    }

    IEnumerator CreateAction(float t, string noteNumber, float length)
    {
        //Wait for the start of the note
        yield return new WaitForSeconds(t);
        //The note is about to play, do stuff here
        Debug.Log("Playing note: " + noteNumber + " " + length);
        gameManager.ActivateMusicNoteByName(noteNumber, length);

    }
}
