using UnityEngine;

public abstract class PuzzleButton : MonoBehaviour
{
    [SerializeField] protected float slopeAnswer;
    [SerializeField] protected float yAnswer;
    public void OnSubmitButton()
    {
        // 정답일 경우 

        // 정답이 아닐 경우
        int randomInt = Random.Range(0, 2);
        string message = (randomInt == 0) ? "다시 해보자" : "문 앞에 일렬로 서야해";
        string[] dialogList = { message };
        SpeechBubbleManager.instance.StartSpeechBubbleGuide(dialogList);
    }

    public void OnPlusButton()
    {
        SoundManager.instance.PlaySFX(SoundClip.GameButtonSFX, 0.3f);
        PlusButton();
    }

    public void OnMinusButton()
    {
        SoundManager.instance.PlaySFX(SoundClip.GameButtonSFX, 0.3f);
        MinusButton();
    }

    protected abstract void PlusButton();

    protected abstract void MinusButton();
}
