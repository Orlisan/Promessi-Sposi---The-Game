using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class Quest
{
    public static GameObject questDetailImageUI;
    public static TextMeshProUGUI titoloQuestUI;
    public static TextMeshProUGUI dettagliQuestUI;
    public static TextMeshProUGUI stepQuestUI;
    public static TextMeshProUGUI indiceQuestUI;
    private string nomeQuest;
    private string dettagliQuest;
    private int indiceQuest;
    private int MaxSteps;
    private int CurrentStep;
    private bool hasSteps;
    private bool isTerminated;
    static bool isAlreadyQuest = false;
    public static Dictionary<string, bool> quests = new Dictionary<string, bool>();
    
    public Quest(string nomeQuest, string dettagliQuest, int indiceQuest, int MaxSteps, int CurrentStep)
    {
        this.nomeQuest = nomeQuest;
        this.dettagliQuest = dettagliQuest;
        this.indiceQuest = indiceQuest;
        this.MaxSteps = MaxSteps;
        this.CurrentStep = CurrentStep;
        hasSteps = true;
    }
    public Quest(string nomeQuest, string dettagliQuest, int indiceQuest)
    {
        this.nomeQuest = nomeQuest;
        this.dettagliQuest = dettagliQuest;
        this.indiceQuest = indiceQuest;
        hasSteps = false;
    }
    public void Start() //Start non è quello di Monobehaviour è uno start che ti fa startare la quests
    {
        if(!isAlreadyQuest) {
            isAlreadyQuest = true;
            isTerminated = false;
            quests.Add(nomeQuest, isTerminated);
            titoloQuestUI.text = nomeQuest;
            dettagliQuestUI.text = dettagliQuest;
            indiceQuestUI.text = indiceQuest.ToString();
            if(hasSteps)
            {
                stepQuestUI.text = CurrentStep.ToString() + " / " + MaxSteps.ToString();
            }
        }
        else
        {
            throw new Exception("Non puoi far partire più missioni contemporaneamente");
        }
    }

    public void UpdateProgresso(int newProgresso)
    {
        CurrentStep = newProgresso;
        stepQuestUI.text = CurrentStep.ToString() + " / " + MaxSteps.ToString();
    }

    public void AddProgresso(int newProgresso)
    {
        CurrentStep += newProgresso;
        stepQuestUI.text = CurrentStep.ToString() + " / " + MaxSteps.ToString();
    }
    
    public void Termina()
    {
        if(!isTerminated) {
            isTerminated = true;
            quests[nomeQuest] = isTerminated;
            titoloQuestUI.text = "";
            dettagliQuestUI.text = "";
            indiceQuestUI.text = "";
            if(hasSteps)
            {
                stepQuestUI.text = "";
            }
            isAlreadyQuest = false;
        }else
        {
            throw new Exception("Non puoi terminare una quest non partita");
        }
    }
   
}
