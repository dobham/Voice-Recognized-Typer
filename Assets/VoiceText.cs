using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using Random = System.Random;

public class VoiceText : MonoBehaviour
{
    private PhraseRecognizer recognizer;

    private StringBuilder buildString = new StringBuilder(" ", 10000000);

    private Text textObject;

    private string[] answers = new string[10];
    private double[,] values = new double[10,2];
    private int i;
    private int[] type = new int[10];
    
    public string inputText;
    void Start()
    {
        int questionType;
        Random rand = new Random();
        for (int y = 0; y < 10; y++) {
            values[y,0] = rand.Next(1,100);
            values[y,1] = rand.Next(1,100);
            questionType = rand.Next(0,4);
            if (questionType == 0) {
                answers[y] = (values[y,0]*values[y,0]).ToString();
                print(answers[y]);
                type[y] = questionType;
            }
            else if (questionType == 1) {
                answers[y] = (values[y,0]/values[y,0]).ToString();
                print(answers[y]);
                type[y] = questionType;
            }
            else if (questionType == 2) {
                answers[y] = (values[y,0]+values[y,0]).ToString();
                print(answers[y]);
                type[y] = questionType;
            }
            else if (questionType == 3) {
                answers[y] = (values[y,0]-values[y,0]).ToString();
                print(answers[y]);
                type[y] = questionType;
            }
        }
        textObject = gameObject.GetComponent<Text>();
        textObject.text = buildString.AppendFormat("{0}x{1} = {2}", values[i,0], values[i,1], Environment.NewLine).ToString();
        recognizer = new KeywordRecognizer(answers, ConfidenceLevel.Medium);
        recognizer.OnPhraseRecognized += OnPhraseRecognized;
        recognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        inputText = buildString.AppendFormat("{0} {1}", args.text, Environment.NewLine).ToString();
        textObject.text = inputText;
        i++;
        updateQuestion(i);

    }

    private void OnApplicationQuit()
    {
        recognizer.Stop();
    }

    private void updateQuestion(int i)
    {
        if (type[i] == 0) {
            textObject.text = buildString.AppendFormat("{0}x{1} = {2}", values[i,0], values[i,1], Environment.NewLine).ToString();
        }
        if (type[i] == 1) {
            textObject.text = buildString.AppendFormat("{0}/{1} = {2}", values[i,0], values[i,1], Environment.NewLine).ToString();
        }
        else if (type[i] == 2) {
            textObject.text = buildString.AppendFormat("{0}+{1} = {2}", values[i,0], values[i,1], Environment.NewLine).ToString();
        } 
        else if (type[i] == 3) {
            textObject.text = buildString.AppendFormat("{0}-{1} = {2}", values[i,0], values[i,1], Environment.NewLine).ToString();
        }
    }
}
