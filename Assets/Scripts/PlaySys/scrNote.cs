﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrNote : MonoBehaviour
{
    public GameObject lnend, lntemp;
    GameObject inst;
    public Sprite dk, sp, dkln, spln, defspr;
    public bool pressed, lncreated, lnsetpressed;
    SpriteRenderer rend, rend2;
    Sprite tempspr;
    public BoxCollider2D box;
    int COLUMN;
    int TIME;
    float LNLENGTH, _TIME;
    bool ISLN;
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (ISLN)
        {
            if (!lnend)
            {
                if (!lncreated) //롱노트 끝이 로드가 되지 않았을 때 미리 롱노트를 출력
                {
                    lncreated = true; //중복생성 방지
                    inst = Instantiate(lntemp, transform.position, Quaternion.identity);
                    inst.transform.parent = gameObject.transform;
                    rend2 = inst.GetComponent<SpriteRenderer>();
                    float height = inst.transform.GetComponent<SpriteRenderer>().bounds.size.y;
                    float gap = 999f - transform.position.y;
                    rend2.sprite = tempspr;
                    inst.transform.localScale = new Vector2(1f, gap / height);
                }
            }
            else
            {
                if (lnsetpressed) lnend.GetComponent<scrNoteEnd>().isPressed = true;
                if (inst)
                {
                    inst.transform.position = transform.position;
                    float height = transform.GetComponent<SpriteRenderer>().bounds.size.y;
                    float gap = Mathf.Abs(lnend.transform.position.y - transform.position.y);
                    inst.transform.localScale = new Vector2(1f, gap / height);
                }
                if (lnend.GetComponent<scrNoteEnd>().getTime() + 200 < FileReader.Playback )
                {
                    if (!pressed) GameObject.FindWithTag("NoteSys").GetComponent<FileReader>().SetJudge(1);
                    Destroy(lnend);
                    Destroy(gameObject);
                }
            }
        }
        else if (TIME + 178.4f < FileReader.Playback)
        {
            GameObject.FindWithTag("NoteSys").GetComponent<FileReader>().SetJudge(0);
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector2(transform.position.x, (float)
            (FileReader.judgeoffset + (_TIME - FileReader.PlaybackChanged) * FileReader.multiply));
        if (transform.position.y < -20f) transform.position = new Vector2(transform.position.x, -20f);
    }

    public void SetInfo(int c, int t, bool ln, float length, float nt)
    {
        rend = GetComponent<SpriteRenderer>();
        //int cc = (int)Mathf.Round((c - 36) / 73f);
        COLUMN = c;
        switch (c)
        {
            case 1:
                rend.sprite = dk;
                tempspr = dkln;
                break;
            case 3:
                rend.sprite = sp;
                tempspr = spln;
                break;
            case 5:
                rend.sprite = dk;
                tempspr = dkln;
                break;

            default:
                tempspr = defspr;
                break;
        }
        TIME = t; ISLN = ln; LNLENGTH = length; _TIME = nt;
        transform.position = new Vector2((c - 3f) * scrSetting.ColWidth, transform.position.y);
    }
    public void setLnEnd(GameObject lne)
    {
        lnend = lne;
    }
    public float getTime()
    {
        return TIME;
    }
    public bool isLN()
    {
        return ISLN;
    }
    public float getLnLength()
    {
        return LNLENGTH;
    }
    //public void DestroyNote()
    //{
    //    if (!ISLN)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        if (box) box.enabled = false;
    //        if (lnend.transform.position.y < -15f && !pressed)
    //        {
    //            Destroy(gameObject);
    //            Destroy(lnend);
    //        }
    //    }
    //}
    public void DestroyCollider()
    {
        if (box) box.enabled = false;
    }
    public void setPressed()
    {
        lnsetpressed = true;
    }
}