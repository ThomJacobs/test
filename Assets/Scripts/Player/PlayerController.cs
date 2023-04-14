using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace NamelessProgrammer
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        //Attributes:
        [SerializeField] private TMPro.TMP_InputField m_input_field;
        [SerializeField] private KeyCode m_action_key;
        [SerializeField] private Healthbar m_healthbar;
        private Animator m_animator;
        private const string FIRE_KEY = "fire";
        private bool m_active = false;
        private const float RELOAD_TIME = 2.0f;

        //Properties:
        public Healthbar HealthBar { get => m_healthbar; }

        //Methods:
        private void Awake()
        {
            //Initialise components.
            m_animator = GetComponent<Animator>();

            if(!m_input_field) m_input_field = FindObjectOfType<TMPro.TMP_InputField>();
            if(!m_input_field) { Destroy(this); return;  }

            StartCoroutine(Reload(RELOAD_TIME));
        }

        private IEnumerator Reload(float p_realtime)
        {
            m_active = false;
            yield return new WaitForSecondsRealtime(p_realtime);
            m_active = true;

        }

        private void Update()
        {
            if (!m_input_field || !m_active || !Input.GetKeyDown(m_action_key)) return;

            m_animator.SetTrigger(FIRE_KEY);
            StartCoroutine(Reload(RELOAD_TIME));
            m_input_field.ActivateInputField();

            //Check each enemy for matching text values.
            for (int i = 0; i < Enemy.ActiveEnemies.Count; i++)
            {
                if (Enemy.ActiveEnemies[i].Text.ToLower() != m_input_field.text.ToLower()) continue;

                Highscore.Singleton.Increment();
                Destroy(Enemy.ActiveEnemies[i].gameObject);
            }

            //Reset input field text.
            m_input_field.text = default;
        }
    }
}