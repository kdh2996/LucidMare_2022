using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

// Dialougue 함수
// Dialogue.cs에 Dialogue 함수 정의되어 있음. 이후 데이터 파싱 하는 과정에서 이 함수 지우고 Dialogue.cs 정의 함수 사용
public class Diaglogue
{
    [TextArea]
    public string contexts;
    public string name;
    public Sprite cg;
}
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private SpriteRenderer spriteStandingCG;
    [SerializeField] private SpriteRenderer spriteDialogueBox;
    [SerializeField] private Text txtDialogue;
    [SerializeField] private Text txtName;

    private List<string> listSentences;
    private bool isDialouge = false;
    private int linecount = 0;
    private int contextcount = 0;

    [SerializeField] private Diaglogue[] dialogue;

    //[Header("텍스트 출력 딜레이")]
    //[SerializeField] float textDelay;

    //사건이 발생할 때 onOff를 true로 만들어서 대화창이 나타나도록 설정
    // count : 문자배열을 셀 때 사용
    public void showDialogue()
    {
        SettingUI(true);

        linecount = 0;
        contextcount = 0;
        isDialouge = true;
        //StartCoroutine(TypeWriter());

        nextDialogue();
    }


    // 대화가 진행되도록 함 
    private void nextDialogue()
    {
        txtDialogue.text = dialogue[linecount].contexts;
        txtName.text = dialogue[linecount].name;
        spriteStandingCG.sprite = dialogue[linecount].cg;
        contextcount++;
        linecount++;
    }

    /*IEnumerator TypeWriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogue[linecount].contexts;
        txtDialogue.text = t_ReplaceText;

        for (int i = 0; i < listSentences[linecount].Length; i++) {
            txtDialogue.text += listSentences[linecount][i];
            yield return new WaitForSeconds(textDelay);
        }
    }*/

    // flag를 받아 Sprite, dialougebar, text를 활성/비활성화 시킴
    private void SettingUI(bool flag)
    {
        spriteDialogueBox.gameObject.SetActive(flag);
        spriteStandingCG.gameObject.SetActive(flag);
        txtDialogue.gameObject.SetActive(flag);
        txtName.gameObject.SetActive(flag);
        isDialouge = flag;
    }

    void Update()
    {
        if (isDialouge) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) //space바 or enter키를 눌러 대화 넘김
            {
                if (linecount < dialogue.Length) // 지정한 대화보다 count가 많아지면 자동 종료
                    nextDialogue();
                else
                    SettingUI(false);
            }
        }
    }
}