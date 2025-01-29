using TMPro;
using UnityEngine;

public class NotesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftPage;

    [SerializeField] private TextMeshProUGUI rightPage;


    public void SetPages(string leftPageText, string rightPageText)
    {
        leftPage.text = leftPageText;
        rightPage.text = rightPageText;
    }
}
