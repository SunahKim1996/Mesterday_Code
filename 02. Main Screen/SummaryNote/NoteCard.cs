using UnityEngine;
using UnityEngine.UI;

public class NoteCard : MonoBehaviour
{
    public GameObject stamp;
    public GameObject emptyText;
    public GameObject flipButton;
    public GameObject cardBack;

    [SerializeField] Text titleText;
    [SerializeField] Text forwardContentsText;
    [SerializeField] Text backContentsText;
    [SerializeField] RawImage iconImage;

    [SerializeField] string titleString;
    [SerializeField] string forwardContentsString;
    [SerializeField] string backContentsString;
    [SerializeField] Texture iconSprite;

    void Start()
    {
        titleText.text = titleString;
        forwardContentsText.text = forwardContentsString;
        backContentsText.text = backContentsString;
        iconImage.texture = iconSprite;
    }
}
