using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NoteCard : MonoBehaviour
{
    public GameObject cardObj;
    public GameObject stamp;
    public GameObject emptyText;
    public GameObject flipButton;
    public GameObject cardForward;
    public GameObject cardBack;

    [SerializeField] Text titleText;
    [SerializeField] Text forwardContentsText;
    [SerializeField] Text backContentsText;
    [SerializeField] Image iconImage;

    [SerializeField] string titleString;
    [TextArea]
    [SerializeField] string forwardContentsString;
    [TextArea]
    [SerializeField] string backContentsString;
    [SerializeField] Sprite iconSprite;

    void Start()
    {
        titleText.text = titleString;
        forwardContentsText.text = forwardContentsString;
        backContentsText.text = backContentsString;
        iconImage.sprite = iconSprite;

        SetImageSize();
    }

    /// <summary>
    /// 카드 내 공간에 맞게 이미지 크기 재조정
    /// </summary>
    void SetImageSize()
    {
        Vector2 imageSize = iconImage.rectTransform.sizeDelta;

        iconImage.SetNativeSize();
        Vector2 iconSize = iconImage.rectTransform.sizeDelta;
        float sizeRate = iconSize.x / iconSize.y;

        if (iconSize.x > imageSize.x)
        {
            iconSize.x = imageSize.x;
            iconSize.y = iconSize.x / sizeRate;
        }

        if (iconSize.y > imageSize.y)
        {
            iconSize.y = imageSize.y;
            iconSize.x = iconSize.y * sizeRate;
        }

        iconImage.rectTransform.sizeDelta = iconSize;
    }
}
