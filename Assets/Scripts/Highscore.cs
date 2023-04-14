using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class Highscore : MonoBehaviour
{
    //Attributes:
    [SerializeField] private float m_increase;
    private TMPro.TextMeshProUGUI m_textbox;
    private float m_value;
    private static Highscore m_singleton;

    //Properties:
    public static Highscore Singleton
    {
        get
        {
            return m_singleton == null ? default : m_singleton;
        }
    }
    private float Text { set { if(m_textbox) m_textbox.text = "Highscore: " + m_value.ToString("F"); } }

    //Methods:
    public void Awake() { m_textbox = GetComponent<TMPro.TextMeshProUGUI>(); m_singleton = this; }
    public void Increment()
    {
        m_value += m_increase;

        if (!m_textbox) return;
        Text = m_value;
    }
}
