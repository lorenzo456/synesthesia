using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Note = Melanchall.DryWetMidi.Interaction.Note;
using Melanchall.DryWetMidi.Devices;
using System.Threading;
using System;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    public List<MusicNote> musicNotes = new List<MusicNote>();
    public MusicNote musicPrefab;
    MidiFile file;
    TempoMap tempoMap;
    public AudioSource audioSource;
    public audiotest audiotest;
    // Start is called before the first frame update
    void Start()
    {
        //file = MidiFile.Read("Assets/Resources/wii.mid");
        //CreateMusicNotes();

        //IEnumerable<Note> notes = file.GetNotes();
        //tempoMap = file.GetTempoMap();

        //foreach (Note note in notes)
        //{
        //    MetricTimeSpan metricTime = note.TimeAs<MetricTimeSpan>(tempoMap);
        //    BarBeatTicksTimeSpan musicalTime = note.TimeAs<BarBeatTicksTimeSpan>(tempoMap);
        //    MetricTimeSpan musicalLength = note.LengthAs<MetricTimeSpan>(tempoMap);
        //    //Debug.Log("NOTE: " +note + " - METRICTIME: " + metricTime + " - MUSICALTIME: " + musicalTime + " - NOTELENGTH: " + musicalLength);
        //}
        CreateMusicNotes();
        //audiotest = GetComponent<audiotest>();
        //audiotest.ReadNotes();
        //audioSource.Play();
    }

    public void ActivateMusicNoteByName(string name, float activeTime)
    {
        foreach(MusicNote note in musicNotes)
        {
            if(note.tone.Equals(name))
            {
               note.Activate(activeTime);
            }
        }
    }
    private void GetNotesAtTime(int currentTime, int seconds = 0, int miliseconds =0)
    {
        IEnumerable<Note> notes = file.GetNotes().AtTime(new MetricTimeSpan(0, currentTime,seconds, miliseconds), tempoMap);
        foreach (Note note in notes)
        {
            MetricTimeSpan metricTime = note.TimeAs<MetricTimeSpan>(tempoMap);
            BarBeatTicksTimeSpan musicalTime = note.TimeAs<BarBeatTicksTimeSpan>(tempoMap);
            MetricTimeSpan musicalLength = note.LengthAs<MetricTimeSpan>(tempoMap);
            //Debug.Log("NOTE: " +note + " - NOTELENGTH: " + musicalLength);
        }
    }
    private void Update()
    {
        //int currentTime = (int)Mathf.Round(Time.unscaledTime * 1000);
        ////Debug.Log(Mathf.Round(currentTime));
        //int intTime = (int)Time.time;
        //int minutes = intTime / 60;
        //int seconds = intTime % 60;
        //float fraction = Time.time * 1000;
        //fraction = (fraction % 1000);
        //string timeText = String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        //Debug.Log(timeText);
        //GetNotesAtTime(minutes, seconds, (int)fraction);
    }

    public void CreateMusicNotes()
    {
        //a0-7 b0-7 c1-8 
        float offsetX = 0;

        string[] zeroToseven = { "A", "B" };
        for(int i =0; i < zeroToseven.Length; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                MusicNote temp = Instantiate(musicPrefab, new Vector3(i * 2.0F, 0, j * 2.0F), Quaternion.identity);
                temp.tone = zeroToseven[i] + j;
                temp.color = new Color(i, 0, i+1);
                musicNotes.Add(temp);
                offsetX = i * 2.0F;
            }
        }

        offsetX *= 2;
        float placement = offsetX;
        for (int i = 1; i < 9; i++)
        {
            MusicNote temp = Instantiate(musicPrefab, new Vector3(placement, 0, (i * 2) -2), Quaternion.identity);
            temp.tone = "C" + i;
            temp.color = new Color(i, i * 2, i + 1);
            musicNotes.Add(temp);
        }

        offsetX += 2;
        //d e f g 1-7
        string[] oneToSeven = { "D", "E", "F", "G" };

        for (int i = 0; i < oneToSeven.Length; i++)
        {
            for (int j = 1; j < 8; j++)
            {
                MusicNote temp = Instantiate(musicPrefab, new Vector3((i * 2.0F) + offsetX, 0, j * 2.0F), Quaternion.identity);
                temp.tone = oneToSeven[i] + j;
                temp.color = new Color(i *2, 0,  1);
                musicNotes.Add(temp);
            }
        }

        offsetX = (offsetX * 2) + 2;

        string[] oneToSix = { "A#", "C#", "D#", "F#", "G#" };

        for (int i = 0; i < oneToSix.Length; i++)
        {
            for (int j = 1; j < 8; j++)
            {
                MusicNote temp = Instantiate(musicPrefab, new Vector3((i * 2.0F) + offsetX, 0, j * 2.0F), Quaternion.identity);
                temp.tone = oneToSix[i] + j;
                temp.color = new Color(i +2,i, i + 1);
                musicNotes.Add(temp);
            }
        }
    }

}
