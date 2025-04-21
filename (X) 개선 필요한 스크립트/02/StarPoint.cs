using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPoint : MonoBehaviour
{
    static StarPoint _instance;

    public static StarPoint Instance
    {
        get { return _instance; }
    }

    public GameObject starScroeParent;

    private UserDataInfo userData;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        userData = UserData.instance.userData;
        StartCoroutine(StageClearOrNot());        
    }

    IEnumerator StageClearOrNot()
    {
        while(true)
        {
            yield return null;

            if (userData.clear01)
            {
                starScroeParent.transform.GetChild(0).gameObject.SetActive(true);

                StartCoroutine(Stage1StarScore());
            }
            else
            {
                starScroeParent.transform.GetChild(0).gameObject.SetActive(false);
            }

            
            if (userData.clear02)
            {
                starScroeParent.transform.GetChild(1).gameObject.SetActive(true);

                StartCoroutine(Stage2StarScore());
            }
            else
            {
                starScroeParent.transform.GetChild(1).gameObject.SetActive(false);
            }


            if (userData.clear03)
            {
                starScroeParent.transform.GetChild(2).gameObject.SetActive(true);

                StartCoroutine(Stage3StarScore());
            }
            else
            {
                starScroeParent.transform.GetChild(2).gameObject.SetActive(false); 
            }


            if (userData.clear04)
            {
                starScroeParent.transform.GetChild(3).gameObject.SetActive(true);

                StartCoroutine(Stage4StarScore());
            }
            else
            {
                starScroeParent.transform.GetChild(3).gameObject.SetActive(false); 
            }


            if (userData.clear05)
            {
                starScroeParent.transform.GetChild(4).gameObject.SetActive(true);

                StartCoroutine(Stage5StarScore());
            }
            else
            {
                starScroeParent.transform.GetChild(4).gameObject.SetActive(false); 
            }

            if (userData.clear06)
            {
                starScroeParent.transform.GetChild(5).gameObject.SetActive(true);

                StartCoroutine(Stage6StarScore());
            }
            else
            {
                starScroeParent.transform.GetChild(5).gameObject.SetActive(false);
            }
        }
    }

    public bool isDiaryOn = false;

    IEnumerator Stage1StarScore()
    {
        while (true)
        {
            yield return null;

            if (userData.stage1_Score == 3)
            {
                GameObject.Find("stage1_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(5).gameObject.SetActive(false);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(5).gameObject.SetActive(false);
                }                
            }
            else if (userData.stage1_Score == 2)
            {
                GameObject.Find("stage1_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }                    
            }
            else if (userData.stage1_Score == 1)
            {
                GameObject.Find("stage1_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }                    
            }
            else
            {
                GameObject.Find("stage1_score").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage1_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_1").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }                    
            }
        }
    }

    IEnumerator Stage2StarScore()
    {
        while (true)
        {
            yield return null;

            if (userData.stage2_Score == 3)
            {
                GameObject.Find("stage2_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(5).gameObject.SetActive(false);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(5).gameObject.SetActive(false);
                }
            }
            else if (userData.stage2_Score == 2)
            {
                GameObject.Find("stage2_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else if (userData.stage2_Score == 1)
            {
                GameObject.Find("stage2_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else
            {
                GameObject.Find("stage2_score").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage2_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_2").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
        }
    }

    IEnumerator Stage3StarScore()
    {
        while (true)
        {
            yield return null;

            if (userData.stage3_Score == 3)
            {
                GameObject.Find("stage3_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(5).gameObject.SetActive(false);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(5).gameObject.SetActive(false);
                }
            }
            else if (userData.stage3_Score == 2)
            {
                GameObject.Find("stage3_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else if (userData.stage3_Score == 1)
            {
                GameObject.Find("stage3_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else
            {
                GameObject.Find("stage3_score").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage3_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_3").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
        }
    }

    IEnumerator Stage4StarScore()
    {
        while (true)
        {
            yield return null;

            if (userData.stage4_Score == 3)
            {
                GameObject.Find("stage4_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(5).gameObject.SetActive(false);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(5).gameObject.SetActive(false);
                }
            }
            else if (userData.stage4_Score == 2)
            {
                GameObject.Find("stage4_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else if (userData.stage4_Score == 1)
            {
                GameObject.Find("stage4_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else
            {
                GameObject.Find("stage4_score").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage4_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_4").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
        }
    }

    IEnumerator Stage5StarScore()
    {
        while (true)
        {
            yield return null;

            if (userData.stage5_Score == 3)
            {
                GameObject.Find("stage5_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(5).gameObject.SetActive(false);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(5).gameObject.SetActive(false);
                }
            }
            else if (userData.stage5_Score == 2)
            {
                GameObject.Find("stage5_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else if (userData.stage5_Score == 1)
            {
                GameObject.Find("stage5_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else
            {
                GameObject.Find("stage5_score").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage5_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_5").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
        }
    }

    IEnumerator Stage6StarScore()
    {
        while (true)
        {
            yield return null;

            if (userData.stage6_Score == 3)
            {
                GameObject.Find("stage6_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(5).gameObject.SetActive(false);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(2).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(5).gameObject.SetActive(false);
                }
            }
            else if (userData.stage6_Score == 2)
            {
                GameObject.Find("stage6_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else if (userData.stage6_Score == 1)
            {
                GameObject.Find("stage6_score").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
            else
            {
                GameObject.Find("stage6_score").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                GameObject.Find("stage6_score").gameObject.transform.GetChild(5).gameObject.SetActive(true);

                if (isDiaryOn == true)
                {
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(4).gameObject.SetActive(true);
                    GameObject.Find("Diary_Score1_6").gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
        }
    }
}
