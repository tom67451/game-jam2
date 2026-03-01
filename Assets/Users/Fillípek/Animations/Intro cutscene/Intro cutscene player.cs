using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class IntroCutscenePlayer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [SerializeField] private Image portraitImage;
    [SerializeField] private Image diaBox;
    public Sprite[] portraits;
    public Sprite[] dia_boxes;
    public string[] lines;
    private float textSpeed = 0.05f;
    private int index;
    private bool isTyping = false;
    private bool pictureon = false;


    public Animator animator;
    public GameObject dialogue_box;

    public int line_count;

    void Start()
    {
        textComponent.text = string.Empty;
        UpdatePortrait();
        StartDialogue();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter) && pictureon == false)
        {
            NextLine();
            Debug.Log(line_count);
        }

        if (Input.GetKeyDown(KeyCode.Space) && pictureon == true || Input.GetMouseButtonDown(0) && pictureon == true || Input.GetKeyDown(KeyCode.KeypadEnter) && pictureon == true)
        {
            Debug.Log("Hide pic");
            animator.SetBool("Show picture", false);
            animator.SetBool("Hide picture", true);
        }

        if (line_count == 21)
        {
            animator.SetBool("Show picture", true);
            dialogue_box.SetActive(false);
            pictureon = true;
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    System.Collections.IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }


    public void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
            isTyping = false;
            return;
        }

        if (index < lines.Length - 1)
        {
            index++;
            line_count++;
            UpdatePortrait();
            UpdateDiaBox();
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponent.text = string.Empty;
        }
    }


    public void UpdatePortrait()
    {
        if (portraitImage == null) return;
        if (portraits.Length > 0 && index >= 0 && index < portraits.Length)
        {
            portraitImage.sprite = portraits[index];
            portraitImage.enabled = portraits[index] != null;
        }
        else
        {
            portraitImage.sprite = null;
            portraitImage.enabled = false;
        }
    }

    public void UpdateDiaBox()
    {
        if (diaBox == null) return;
        if (dia_boxes.Length > 0 && index >= 0 && index < dia_boxes.Length)
        {
            diaBox.sprite = dia_boxes[index];
            diaBox.enabled = dia_boxes[index] != null;
        }
        else
        {
            diaBox.sprite = null;
            diaBox.enabled = false;
        }
    }
}
