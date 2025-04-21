using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coin;
    public Text coinTxt;

    // Start is called before the first frame update
    void Start()
    {
        //firebase에서 유저 coin 데이터 불러오기
    }

    // Update is called once per frame
    void Update()
    {
        coinTxt.text = coin.ToString() + " / 10";
    }
}
