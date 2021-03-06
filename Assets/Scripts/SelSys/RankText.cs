using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RankText : MonoBehaviour
{
    public Sprite[] rankspr;
    public GameObject rk;
    RectTransform rect;
    GameObject panel;
    RankPanel rp;
    Image rend;
    int idx;
    public int onlinerank;
    public TextMeshProUGUI playertxt;
    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI combotxt;
    public TextMeshProUGUI datetxt;
    public TextMeshProUGUI sunwitxt;
    public TextMeshProUGUI acctxt;
    // Start is called before the first frame update
    public void Awake()
    {
        rect = GetComponent<RectTransform>();
        rend = rk.GetComponent<Image>();
    }
    private void Update()
    {
        if (idx != -1)
            rect.anchoredPosition = new Vector2(-0.1f, 46.46f - idx * 31f + rp.ypos);
        else
            sunwitxt.text = onlinerank.ToString();
    }
    public void SetText(int id, string pname, int score,float acc, int state, int maxcombo, string date, GameObject p)
    {
        idx = id;
        playertxt.text = pname;
        scoretxt.text = score.ToString("0000000");
        combotxt.text = $"{maxcombo} combo";
        datetxt.text = date;
        acctxt.text = string.Format("{0:p}", acc);
        if (id != -1)
        {
            sunwitxt.text = $"#{id + 1}";
        }
        panel = p;
        rp = p.GetComponent<RankPanel>();

        if (acc >= 0.95f)
        {
            if (state == 1)
            {
                rend.sprite = rankspr[0];
            }
            else
            {
                rend.sprite = rankspr[1];
            }
        }
        else if (acc >= 0.9f)
        {
            rend.sprite = rankspr[2];
        }
        else if (acc >= 0.8f)
        {
            rend.sprite = rankspr[3];
        }
        else if (acc >= 0.6f)
        {
            rend.sprite = rankspr[4];
        }
        else
        {
            rend.sprite = rankspr[5];
        }
    }
    //233.3, 46.2
}
