using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace NamelessProgrammer
{
    public class Enemy : MonoBehaviour
    {
        //Attributes:
        public float m_speed;
        public float m_radius;
        private PlayerController m_target;
        [SerializeField] private TMPro.TextMeshPro m_textbox;
        private const float TOLERANCE = 0.01f, DAMAGE = 1;
        private static List<Enemy> m_enemies = new List<Enemy>();

        //Properties:
        static public List<Enemy> ActiveEnemies { get => m_enemies; }
        public string Text { get => m_textbox ? m_textbox.text : default; }
        private float ScaledRadius { get => m_radius * transform.localScale.magnitude;  }

        //Methods:
        public void SetText(string p_value)
        {
            if (!m_textbox) return;
            m_textbox.text = p_value;
        }

        private void Awake()
        {
            m_target = FindObjectOfType<PlayerController>();
            m_textbox = GetComponentInChildren<TMPro.TextMeshPro>();
            m_enemies.Add(this);
            if (m_target == null) { Destroy(gameObject); return; }
        }

        private void Update()
        {
            if (m_target == null) return;

            //Translate and Rotate.
            transform.position += (m_target.transform.position - transform.position).normalized * m_speed * Time.deltaTime;
            transform.rotation = Additional.Math2D.LookAtQuaternion(transform, m_target.transform.position);

            //Destruct?
            if (Vector2.Distance(m_target.transform.position, transform.position) > ScaledRadius) return;

            //Update healthbar.
            if (m_target.HealthBar) m_target.HealthBar.Reduce(DAMAGE);
            Destroy(this);
        }

        private void OnDestroy()
        {
            m_enemies.Remove(this);
            Destroy(gameObject);
        }

        #region DEVELOPMENT_ONLY
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, ScaledRadius);
        }
#endif
        #endregion
    }
}