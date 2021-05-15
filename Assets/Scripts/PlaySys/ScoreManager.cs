﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    GameObject gameover, judgeobj;
    public static int COOL, GREAT, GOOD, MISS, BAD, TOTAL, combo, maxcombo;
    public static float acc = 100f, HP = 1f, Score, judgeerror;
    public static bool isFailed;

    List<float> errorList = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        isFailed = false;
        HP = 1f;
        COOL = GREAT = GOOD = MISS = BAD = TOTAL = 0;
        acc = 1f;
        combo = maxcombo = 0;
        judgeerror = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float c = NowPlaying.NOTECOUNTS + (NowPlaying.LONGNOTECOUNTS * 2);
        float sum = (COOL / c) + ((GREAT * 2) / (3 * c)) + (GOOD / (3 * c)) + ((BAD / (6 * c)));
        Score = Mathf.Round(1000000f * sum);
        if (TOTAL != 0)
            acc = Mathf.Round(10000f * (sum / (TOTAL / c))) / 10000f;


        if (combo > maxcombo) maxcombo = combo; //최대 콤보 체크

        if (HP <= 0) //게임오버
        {
            isFailed = true;
            gameover.SetActive(true);
        }

        //패슬 평균
        if (errorList.Count > 30) errorList.RemoveAt(0);

    }
    //판정관련
    public void SetJudge(int a)
    {
        combo = 0;
        judgeobj.SetActive(true);
        if (a == 1) //롱노트 아애 안눌럿을시
            judgeobj.GetComponent<scrJudge>().setInfo(5);
        else
            judgeobj.GetComponent<scrJudge>().setInfo(4);
    }
    //패슬입력
    public void AddError(float error)
    {
        float sum = 0f;
        errorList.Add(error);
        for (int i = 0; i < errorList.Count; i++)
        {
            sum += errorList[i];
            judgeerror = sum / errorList.Count;
        }
    }
}
