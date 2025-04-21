using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleButton : MonoBehaviour
{
    public int howMuchButtonOn = 0;

    public List<Button> buttons = new List<Button>();

    public List<GameObject> students = new List<GameObject>();
    List<string> selectedBtn = new List<string>();
    public string btnTag;

    public GameObject speechBubble;
    public Text bubbleTxt;


    private void Start()
    {
        selectedBtn.Add("0");
        selectedBtn.Add("1");
        selectedBtn.Add("2");
    }

    public void btnOnClick()
    {        
        Button btn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        btnTag = EventSystem.current.currentSelectedGameObject.GetComponent<Button>().tag;
        

        ColorBlock color = btn.colors;

        Color NoneSelected = new Color(255, 255, 255, 0.2f);
        Color selectedColor = new Color(255, 187, 0, 0.6f);

        
        if((color.normalColor != selectedColor)) 
        {
            //좌표 선택            

            if(howMuchButtonOn < 3)
            {
                howMuchButtonOn += 1;

                color.normalColor = selectedColor;
                color.selectedColor = selectedColor;

                btn.colors = color;

                if(howMuchButtonOn == 1)
                {
                    GetComponent<StudentMovement02>().isStudentMove_1 = true;
                    selectedBtn[0] = btnTag;
                }
                else if (howMuchButtonOn == 2)
                {
                    GetComponent<StudentMovement02>().isStudentMove_2 = true;
                    selectedBtn[1] = btnTag;
                }
                else if (howMuchButtonOn == 3)
                {
                    GetComponent<StudentMovement02>().isStudentMove_3 = true;
                    selectedBtn[2] = btnTag;
                }
            }
            else 
            {
                // 선택된 좌표가 이미 세개일 경우

                for (int i = 0; i < 9; i++) 
                {
                    Button allCancelBtn = GetComponent<Button>();

                    ColorBlock allCancelColor = buttons[i].colors;

                    allCancelColor.normalColor = NoneSelected;
                    allCancelColor.selectedColor = NoneSelected;

                    buttons[i].colors = allCancelColor;
                }

                howMuchButtonOn = 1;
                GetComponent<StudentMovement02>().isStudentMove_1 = true;
                GetComponent<StudentMovement02>().isBtnCanceled = true;
                selectedBtn[0] = btnTag;
                selectedBtn[1] = "1"; // 버튼 기록 초기화
                selectedBtn[2] = "2";

                color.normalColor = selectedColor;
                color.selectedColor = selectedColor;

                btn.colors = color;                
            }
        }
    }

    private void Update()
    {
        //Debug.Log(selectedBtn[0] + ", " + selectedBtn[1] + ", " + selectedBtn[2]);
        if (selectedBtn[0] == "answer" && selectedBtn[1] == "answer" && selectedBtn[2] == "answer")
        {
            GetComponent<FomulaTxt>().isAnswer = true;
        }

        else
        {
            GetComponent<FomulaTxt>().isAnswer = false;
        }
    }

    public void SubmitButton()
    {
        if(selectedBtn[0] == "answer" && selectedBtn[1] == "answer" && selectedBtn[2] == "answer")
        {           
            
            GetComponent<CameraMoving02>().isPuzzleGameStart = false;
            GetComponent<StarScore02>().isStarScoreOnTime = true;
        }
        else
        {
            StartCoroutine(SpeechBubbleOn());
        }
    }

    IEnumerator SpeechBubbleOn()
    {
        speechBubble.SetActive(true);

        int randomInt = Random.Range(0, 2);

        if(randomInt == 0)
        {
            bubbleTxt.text = "다시 해보자";
        }
        else
        {
            bubbleTxt.text = "문 앞에 일렬로 서야해";
        }

        yield return new WaitForSeconds(1.5f);

        speechBubble.SetActive(false);
    }
}
