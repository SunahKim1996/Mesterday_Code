using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public enum InputType
{
    NickName,
    SchoolName,
}

public class InputDialog : MonoBehaviour
{
    [SerializeField] GameObject nickNameFieldParent;
    [SerializeField] GameObject schoolNameFieldParent;

    [SerializeField] InputField nickNameField;
    [SerializeField] InputField schoolNameField;

    [SerializeField] Button nickNameConfirmButton;
    [SerializeField] Button schoolNameConfirmButton;

    [SerializeField] Text errorTxt;
    Coroutine errorCor = null;

    string nickName;
    string schoolName;

    DialogTutorial dialogTutorial;

    void Start()
    {
        dialogTutorial = GetComponent<DialogTutorial>();

        nickNameField.onValueChanged.AddListener((text) => OnChangeInput(InputType.NickName, text));
        schoolNameField.onValueChanged.AddListener((text) => OnChangeInput(InputType.SchoolName, text));

        nickNameConfirmButton.onClick.AddListener(() => OnConfirmButton(InputType.NickName));
        schoolNameConfirmButton.onClick.AddListener(() => OnConfirmButton(InputType.SchoolName));
    }

    public void ToggleInputField(InputType inputType, bool state)
    {
        GameObject fieldParent = (inputType == InputType.NickName) ? nickNameFieldParent : schoolNameFieldParent;
        fieldParent.SetActive(state);
    }

    public void OnChangeInput(InputType inputType, string value)
    {
        Button confirmButton = (inputType == InputType.NickName) ? nickNameConfirmButton : schoolNameConfirmButton;

        bool isInteractable = (value == "") ? false : true;
        confirmButton.interactable = isInteractable;
    }

    public void OnConfirmButton(InputType inputType)
    {
        InputField inputField = (inputType == InputType.NickName) ? nickNameField : schoolNameField;

        if (IsValid(inputField.text))
        {
            if (inputType == InputType.NickName)
                nickName = inputField.text;
            else
            {
                schoolName = inputField.text;

                UserData.instance.SetUserDataInfo("nickName", nickName);
                UserData.instance.SetUserDataInfo("schoolName", schoolName);
            }

            ToggleInputField(inputType, false);
            dialogTutorial.StartChat();
        }
        else
        {
            if (errorCor != null)
            {
                StopCoroutine(errorCor);
                errorCor = null;
            }

            errorCor = StartCoroutine(ShowErrorMessage());
        }
    }

    bool IsValid(string value)
        => Regex.IsMatch(value, "^[^\\s]+$");

    IEnumerator ShowErrorMessage()
    {
        errorTxt.text = "공백이 없어야 합니다.";

        errorTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        errorTxt.gameObject.SetActive(false);
    }
}

