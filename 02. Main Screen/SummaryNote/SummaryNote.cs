using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SummaryNote : MonoBehaviour
{
    public static bool isNoteOn = false;
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

            NoteCard card = cardList[i - 1];
            bool isUnlocked = (isClear && starScore >= 3);
            UnlockCard(card, isUnlocked);
            card.cardBack.SetActive(false);

            if (userData.NoteChange)
                newSign.SetActive(true);
        }
    }

    void UnlockCard(NoteCard card, bool isUnlocked)
    {
        card.stamp.SetActive(isUnlocked);
        card.cardForward.SetActive(isUnlocked);
        card.flipButton.SetActive(isUnlocked);
        card.emptyText.SetActive(!isUnlocked);

        if (isUnlocked)
            card.flipButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                flipCard.OnStartFlip(card.cardObj, card.cardBack, card.cardForward);
            });
    }

    public void OnToggleNoteUI(bool state)
    {
        SoundManager.instance.PlaySFX(SoundClip.ButtonSFX, 0.4f);

        if (state)
            UserData.instance.SetUserDataInfo("NoteChange", false);
        else
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                if (!cardList[i].emptyText.activeSelf)
                {
                    cardList[i].cardBack.SetActive(false);
                    cardList[i].cardForward.SetActive(true);
                    cardList[i].cardObj.transform.rotation = Quaternion.identity;
                }
            }

            flipCard.InitNote();
        }

        noteUI.SetActive(state);
        newSign.SetActive(!state);

        isNoteOn = state;
    }
}
