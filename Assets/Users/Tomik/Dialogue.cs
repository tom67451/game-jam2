using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour
{   
    public TextMeshProUGUI textComponent;
    [SerializeField] private Image portraitImage;
    public Sprite[] portraits;
    public string[] lines;
    private float textSpeed = 0.05f;
    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        UpdatePortrait();
        StartDialogue();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter));
        {
            NextLine();
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    System.Collections.IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            UpdatePortrait();
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
}
