using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StudentMovement02 : MonoBehaviour
{
    Vector3 touchPosition_1;
    Vector3 touchPosition_2;
    Vector3 touchPosition_3;
    public Camera puzzleCam;

    public bool isStudentMove_1 = false;
    public List<GameObject> student_1 = new List<GameObject>();

    public bool isStudentMove_2 = false;
    public List<GameObject> student_2 = new List<GameObject>();

    public bool isStudentMove_3 = false;
    public List<GameObject> student_3 = new List<GameObject>();
    
    Vector3 studentPos_1;
    Vector3 studentPos_2;
    Vector3 studentPos_3;

    float randomNum_1;
    float randomNum_2;
    float randomNum_3;

    List<Vector3> student_startPos = new List<Vector3>();

    private void Start()
    {
        StartCoroutine(BtnSelectCancel());

        for (int i = 0; i < 3; i++)
        {
            student_startPos.Add(student_2[i].transform.position);
        }

        for (int i = 0; i < 3; i++)
        {
            student_startPos.Add(student_3[i].transform.position);
        }
    }

    private void Update()
    {
        if (isStudentMove_1 == true)
        {
            touchPosition_1 = Input.mousePosition;
            touchPosition_1 = puzzleCam.ScreenToWorldPoint(touchPosition_1);

            randomNum_1 = Random.Range(-1f, 1f);
            StartCoroutine(MoveStudent_1());          

            isStudentMove_1 = false;

        }
        else if (isStudentMove_2 == true)
        {

            touchPosition_2 = Input.mousePosition;
            touchPosition_2 = puzzleCam.ScreenToWorldPoint(touchPosition_2);

            randomNum_2 = Random.Range(-1f, 1f);
            StartCoroutine(MoveStudent_2());

            isStudentMove_2 = false;

        }
        else if (isStudentMove_3 == true)
        {

            touchPosition_3 = Input.mousePosition;
            touchPosition_3 = puzzleCam.ScreenToWorldPoint(touchPosition_3);

            randomNum_3 = Random.Range(-1f, 1f);
            StartCoroutine(MoveStudent_3());

            isStudentMove_3 = false;

        }
    }

    IEnumerator MoveStudent_1()
    {
        while(true)
        {
            yield return null;

            if (Vector3.Distance(student_1[0].transform.position, studentPos_1) >= 0.1f)
            {               
                studentPos_1 = new Vector3(touchPosition_1.x, student_1[0].transform.position.y, touchPosition_1.z);
                student_1[0].transform.position = Vector3.MoveTowards(student_1[0].transform.position, studentPos_1, Time.deltaTime * 5f);

                studentPos_2 = new Vector3(touchPosition_1.x + randomNum_1, student_1[1].transform.position.y, touchPosition_1.z + randomNum_1);
                student_1[1].transform.position = Vector3.MoveTowards(student_1[1].transform.position, studentPos_2, Time.deltaTime * 5f);

                studentPos_3 = new Vector3(touchPosition_1.x + randomNum_1, student_1[2].transform.position.y, touchPosition_1.z + randomNum_1);
                student_1[2].transform.position = Vector3.MoveTowards(student_1[2].transform.position, studentPos_3, Time.deltaTime * 5f);
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator MoveStudent_2()
    {
        while (true)
        {
            yield return null;

            if(isBtnCanceled == false)
            {
                if (Vector3.Distance(student_2[0].transform.position, studentPos_2) >= 0.1f)
                {
                    studentPos_1 = new Vector3(touchPosition_2.x, student_2[0].transform.position.y, touchPosition_2.z);
                    student_2[0].transform.position = Vector3.MoveTowards(student_2[0].transform.position, studentPos_1, Time.deltaTime * 5f);

                    studentPos_2 = new Vector3(touchPosition_2.x + randomNum_2, student_2[1].transform.position.y, touchPosition_2.z + randomNum_2);
                    student_2[1].transform.position = Vector3.MoveTowards(student_2[1].transform.position, studentPos_2, Time.deltaTime * 5f);

                    studentPos_3 = new Vector3(touchPosition_2.x + randomNum_2, student_2[2].transform.position.y, touchPosition_2.z + randomNum_2);
                    student_2[2].transform.position = Vector3.MoveTowards(student_2[2].transform.position, studentPos_3, Time.deltaTime * 5f);
                }
                else
                {
                    break;
                }

            }
        }
    }

    IEnumerator MoveStudent_3()
    {
        while (true)
        {
            yield return null;

            if(isBtnCanceled == false)
            {
                if(GetComponent<PuzzleButton>().howMuchButtonOn == 3)
                {
                    if (Vector3.Distance(student_3[0].transform.position, studentPos_3) >= 0.1f)
                    {
                        studentPos_1 = new Vector3(touchPosition_3.x, student_3[0].transform.position.y, touchPosition_3.z);
                        student_3[0].transform.position = Vector3.MoveTowards(student_3[0].transform.position, studentPos_1, Time.deltaTime * 5f);

                        studentPos_2 = new Vector3(touchPosition_3.x + randomNum_3, student_3[1].transform.position.y, touchPosition_3.z + randomNum_3);
                        student_3[1].transform.position = Vector3.MoveTowards(student_3[1].transform.position, studentPos_2, Time.deltaTime * 5f);

                        studentPos_3 = new Vector3(touchPosition_3.x + randomNum_3, student_3[2].transform.position.y, touchPosition_3.z + randomNum_3);
                        student_3[2].transform.position = Vector3.MoveTowards(student_3[2].transform.position, studentPos_3, Time.deltaTime * 5f);
                    }
                    else
                    {
                        break;
                    }
                }
               
            }
            
        }        
    }

    public bool isBtnCanceled = false;

    IEnumerator BtnSelectCancel()
    {
        while (true)
        {
            yield return null;

            if (isBtnCanceled == true)
            {
                Debug.Log(isBtnCanceled);

                student_2[0].transform.position = Vector3.MoveTowards(student_2[0].transform.position, student_startPos[0], Time.deltaTime * 10f);
                student_2[1].transform.position = Vector3.MoveTowards(student_2[1].transform.position, student_startPos[1], Time.deltaTime * 10f);
                student_2[2].transform.position = Vector3.MoveTowards(student_2[2].transform.position, student_startPos[2], Time.deltaTime * 10f);

                student_3[0].transform.position = Vector3.MoveTowards(student_3[0].transform.position, student_startPos[3], Time.deltaTime * 10f);
                student_3[1].transform.position = Vector3.MoveTowards(student_3[1].transform.position, student_startPos[4], Time.deltaTime * 10f);
                student_3[2].transform.position = Vector3.MoveTowards(student_3[2].transform.position, student_startPos[5], Time.deltaTime * 10f);

                if(GetComponent<PuzzleButton>().howMuchButtonOn == 2)
                {
                    isBtnCanceled = false;
                }
            }
        }
    }
}
