using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SummaryNote : MonoBehaviour
{
    UserDataInfo userData;

    [SerializeField] GameObject noteUI;
    [SerializeField] GameObject newSign;

    [SerializeField] GameObject cardListOrigin;
    List<NoteCard> cardList;
    
    FlipCard flipCard;

    void Start()
    {
        userData = UserData.instance.userData;

        if (!userData.clearTutorial)
            return;

        Init();
        flipCard = GetComponent<FlipCard>();
        cardList = cardListOrigin.GetComponentsInChildren<NoteCard>().ToList();

        for (int i = 1; i <= GameData.MaxStage; i++)
        {
            string fieldName = $"clear0{i}";
            FieldInfo fieldInfo = typeof(UserDataInfo).GetField(fieldName);
            bool isClear = (bool)fieldInfo.GetValue(userData);

            fieldName = $"stage{i}_Score";
            fieldInfo = typeof(UserDataInfo).GetField(fieldName);
            int starScore = (int)fieldInfo.GetValue(userData);

            if (isClear && starScore >= 3)
            {
                NoteCard card = cardList[i - 1];
                card.stamp.SetActive(true);
                card.flipButton.SetActive(true);
                card.emptyText.SetActive(false);
                card.cardBack.SetActive(false);

                if (userData.NoteChange)
                    newSign.SetActive(true);
            }
        }
    }

    void Init()
    {
        noteUI.SetActive(false);
        newSign.SetActive(false);

        for (int i = 0; i < GameData.MaxStage; i++)
        {
            NoteCard card = cardList[i];
            card.emptyText.SetActive(true);
            emptyTextList[i].SetActive(true);
            stampList[i].SetActive(false);
            cardBackList[i].SetActive(false);

            GameObject cardBackObj = cardBackList[i];
            buttonList[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                flipCard.OnStartFlip(cardBackObj);
            });

            buttonList[i].SetActive(false);
        }
    }

    /*
    /// <summary>
    /// 카드를 앞면 혹은 뒷면으로 보이게 처리
    /// </summary>
    /// <param name="state"> state 가 true 이면 앞면, false 이면 뒷면 </param>
    void ToggleCardForward(NoteCard card, bool state)
    {
        card.stamp.SetActive(!state);        
        card.emptyText.SetActive(false);
        card.cardBack.SetActive(false);

        //card.flipButton.SetActive(true);
    }
    */

    public void OnToggleNoteUI(bool state)
    {
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);

        if (state)
            UserData.instance.SetUserDataInfo("NoteChange", false);
        else
        {
            for (int i = 0;i < cardBackList.Count; i++)
            {
                cardBackList[i].SetActive(false);
                cardBackList[i].transform.rotation = Quaternion.identity;
            }

            flipCard.InitNote();
        }            

        noteUI.SetActive(state);
        newSign.SetActive(!state);

        //PlayerTrigger.Instance.isNoteOrDiaryOn = state; //TODO
    }
}
