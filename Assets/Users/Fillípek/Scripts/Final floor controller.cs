using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class FinalFloorController : MonoBehaviour
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

    private bool started = false;

    public Animator animator;
    public GameObject Dialogue_box;
    [SerializeField] private Switcher Switcher;
    
    public int line_count;

    public float clocker;
    public bool clockbool;

    public bool skipper = false;

    void Start()
    {

        textComponent.text = string.Empty;
        UpdatePortrait();
        StartDialogue();
    }

    void Update()
    {
        if (clockbool)
        {
            clocker += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && started == false || Input.GetMouseButtonDown(0) && started == false || Input.GetKeyDown(KeyCode.KeypadEnter) && started == false)
        {
            NextLine();
            Debug.Log(line_count);
        }

        if (Input.GetKeyDown(KeyCode.Space) && line_count == 4 && started == true && clockbool == false || Input.GetMouseButtonDown(0) && line_count == 4 && started == true && clockbool == false || Input.GetKeyDown(KeyCode.KeypadEnter) && line_count == 4 && started == true && clockbool == false)
        {
            NextLine();
            Dialogue_box.SetActive(false);
            clockbool = true;
            Debug.Log(line_count + "!");
        }

        if (Input.GetKeyDown(KeyCode.Space) && line_count == 7 || Input.GetMouseButtonDown(0) && line_count == 7 || Input.GetKeyDown(KeyCode.KeypadEnter) && line_count == 7)
        {
            
            Debug.Log(line_count + "?");
        }

        if (Input.GetKeyDown(KeyCode.Space) && line_count == 9 || Input.GetMouseButtonDown(0) && line_count == 9 || Input.GetKeyDown(KeyCode.KeypadEnter) && line_count == 9)
        {
            Switcher.SwitchToScene("MainMenu");
            Debug.Log("Returning to main menu");
        }

        if (line_count == 4 && started == false && skipper == false)
        {
            animator.SetBool("Start", true);
            Debug.Log("Line count is 4");
            started = true;
        }

        if (clocker >= 10)
        {
            Dialogue_box.SetActive(true);
            animator.SetBool("Continue", true);
            animator.SetBool("Start", false);
            Debug.Log("Time's up!");
            started = false;
            clocker = 0;
            clockbool = false;
            skipper = true;
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

    System.Collections.IEnumerator PlayAndWait(string anim)
    {
        animator.Play(anim);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Debug.Log("Konec animace");
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
