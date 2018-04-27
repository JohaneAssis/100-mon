using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InkScript : MonoBehaviour
{
    [SerializeField]
    private TextAsset textAsset;
    private Story officeStory;
    private bool storyNeeded;

    public Text actualTextBox;
    public Button button;
    public GameObject textBoxPrefab;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private float elementPadding;

    void Awake()
    {
        officeStory = new Story(textAsset.text);
        storyNeeded = true;
    }

    void Update()
    {
        if (storyNeeded == true)
        {
            RemoveChildren();

            float offset = 0;
            while (officeStory.canContinue)
            {

                GameObject obj = Instantiate<GameObject>(textBoxPrefab);
                Text storyText = obj.GetComponent<Text>();
                storyText.text = officeStory.ContinueMaximally();
                storyText.transform.SetParent(canvas.transform, false);
                storyText.transform.Translate(new Vector2(0, offset));
                offset -= (storyText.fontSize + elementPadding);
            }

            if (officeStory.currentChoices.Count > 0)
            {
                for (int j = 0; j < officeStory.currentChoices.Count; ++j)
                {
                    Button choice = Instantiate(button) as Button;
                    choice.transform.SetParent(canvas.transform, false);
                    choice.transform.Translate(new Vector2(0, offset));

                    Text choiceText = choice.GetComponentInChildren<Text>();
                    choiceText.text = officeStory.currentChoices[j].text;

                    HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();

                    int choiceId = j;
                    choice.onClick.AddListener(delegate { ChoiceSelected(choiceId); });

                    offset -= (choiceText.fontSize + layoutGroup.padding.top + layoutGroup.padding.bottom + elementPadding);
                }
            }

            storyNeeded = false;
        }
    }

    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    public void ChoiceSelected(int id)
    {
        officeStory.ChooseChoiceIndex(id);
        storyNeeded = true;
    }

}
