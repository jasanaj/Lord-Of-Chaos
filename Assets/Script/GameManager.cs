using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    //SCORING AND MULTIPLIER//
    public int currentScore;
    public int scorePerGoodNote = 100;
    public int scorePerGreatNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThreshold;

    public Text scoreText;
    public Text multiText;

    //RESULTSCORE//
    public float totalNotes;
    public float goodHits;
    public float greatHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, goodsText, greatsTexts, perfectsText, missedTexts, rankText, finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            startPlaying = true;
            theBS.hasStarted = true;

            theMusic.Play();
        }
        else
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);

                goodsText.text = "" + goodHits;
                greatsTexts.text = greatHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missedTexts.text = "" + missedHits;

                float totalHit = goodHits + greatHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 86)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                switch (rankVal)
                {
                    case "S":
                        rankText.color = new Color32(0xDB, 0xB0, 0x2C, 0xFF); // #DBB02C
                        break;
                    case "A":
                        rankText.color = new Color32(0xDA, 0x1E, 0x1C, 0xFF); // #DA1E1C
                        break;
                    case "B":
                        rankText.color = new Color32(0x1E, 0x93, 0x30, 0xFF); // #1E9330
                        break;
                    case "C":
                        rankText.color = new Color32(0x0F, 0x81, 0x9E, 0xFF); // #0F819E
                        break;
                    case "D":
                        rankText.color = new Color32(0xA2, 0x14, 0x14, 0xFF); // #A21414
                        break;
                    default:
                        rankText.color = Color.white; // fallback warna
                        break;
                }


                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");


        if(currentMultiplier - 1 < multiplierThreshold.Length)
        {
            multiplierTracker++;

            if (multiplierThreshold[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "x" + currentMultiplier;
      
        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "" + currentScore;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
    }
    public void GreatHit()
    {
        currentScore += scorePerGreatNote * currentMultiplier;
        NoteHit();

        greatHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Miss");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "x" + currentMultiplier;

        missedHits++; 
    }
}
