using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;


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


    public GameObject Dialogue_box;
    
    public int line_count;

    public float clocker;
    public bool clockbool;

    public Player Player;
    public Twin Twin;
    public GameObject player;
    public GameObject twin;
    public GameObject aplayer;
    public GameObject atwin;
    public GameObject camera;
    public float speedCache;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();         //Cannot find player again
        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = aplayer.transform.position;
        twin.SetActive(false);
        player.SetActive(false);

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

        if (Input.GetKeyDown(KeyCode.Space) && line_count == 12 || Input.GetMouseButtonDown(0) && line_count == 12 || Input.GetKeyDown(KeyCode.KeypadEnter) && line_count == 12)
        {
            twin.SetActive(true);
            player.SetActive(true);
            Destroy(atwin);
            Destroy(aplayer);
            Destroy(camera);
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            NextLine();
            Debug.Log(line_count);
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

    /*System.Collections.IEnumerator PlayAndWait(string anim)
    {
        animator.Play(anim);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Debug.Log("Konec animace");
    }
    */



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
