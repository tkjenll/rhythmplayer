﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEnd : MonoBehaviour
{
    float TIME, _TIME;
    public Sprite dk, sp, def;
    SpriteRenderer rend;
    FileReader rdr;
    private void Awake()
    {
        var sys = GameObject.FindWithTag("NoteSys");
        rdr = sys.GetComponent<FileReader>();
        rend = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector2(transform.localScale.x * GlobalSettings.ColWidth, transform.localScale.y);
        if (GlobalSettings.isCutOff) rend.enabled = false;
    }
    private void LateUpdate()
    {
        transform.position = new Vector2(transform.position.x, (float)(FileReader.judgeoffset + (_TIME - FileReader.PlaybackChanged) * FileReader.multiply));
    }

    public void setInfo(int c, float t, GameObject obj, float nt) //콜룸, 타임, 시작노트
    {
        if (!GlobalSettings.isCutOff)
            if (c == 1 || c == 5) { rend.sprite = dk; } else if (c == 3) { rend.sprite = sp; } else { rend.sprite = def; }
        TIME = t; _TIME = nt;
        if (obj)
            transform.position = new Vector2(obj.transform.position.x, transform.position.y);
        else
        {
            InsertQueue();
        }
    }
    public void InsertQueue()
    {
        transform.position = new Vector2(0, 1000f);
        rdr.end_queue.Enqueue(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
