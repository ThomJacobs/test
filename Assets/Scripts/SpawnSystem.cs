using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    [System.Serializable]
    public struct SpawnSettings
    {
        //Attributes:
        [SerializeField] private float m_minimum;
        [SerializeField] private float m_maximum;
        [Range(MINIMUM_INCREASE, MAXIMUM_INCREASE), SerializeField] private float m_rateOfIncrease;
        private const float MAXIMUM_INCREASE = 1.0f;
        private const float MINIMUM_INCREASE = 0.0f;

        //Properties:
        public float RandomRange
        {
            get
            {
                //Calculate current random value.
                float value = Random.Range(m_minimum, m_maximum);

                //Reduce range between minimum and maximum floating point numbers.
                if ((m_maximum - m_maximum * RateOfIncrease) <= m_minimum) return value;
                m_maximum -= (m_maximum * RateOfIncrease);

                return value;
            }
        }

        public float RateOfIncrease => Mathf.Clamp(m_rateOfIncrease, MINIMUM_INCREASE, MAXIMUM_INCREASE);
        
        //Methods:
        public float Clamp(float p_value)
        {
            if (p_value <= m_minimum) return m_minimum;
            return p_value > m_maximum ? m_maximum : m_minimum;
        }
    }

    public sealed class SpawnSystem : MonoBehaviour
    {
        //Attributes:
        public TextGenerator m_text_generator;
        public SpawnSettings m_spawn_rate;
        public List<Rect> m_bounds;
        public Enemy m_target;

        //Methods:
        private void Awake()
        {
            if (m_text_generator == null || m_bounds.Count <= 0) { Destroy(this); return; }

            //Begin spawning...
            StartCoroutine(BeginSpawn(m_spawn_rate.RandomRange));
        }

        private Vector2 GetRandomPosition()
        {
            Rect boundary = m_bounds[Random.Range(0, m_bounds.Count)];
            float xPos = Random.Range(transform.position.x + boundary.position.x - (boundary.width/2), transform.position.x + boundary.position.x + (boundary.width/2));
            float yPos = Random.Range(transform.position.y + boundary.position.y - (boundary.height/2), transform.position.y + boundary.position.y + (boundary.height/2));
            return new Vector2(xPos, yPos);
        }

        private IEnumerator BeginSpawn(float p_realtime_delay)
        {
            yield return new WaitForSecondsRealtime(p_realtime_delay);

            //Spawn game object.
            Enemy test = Instantiate(m_target);
            test.transform.position = GetRandomPosition();
            test.SetText(m_text_generator.GetRandom);

            //Start next spawn delay.
            StartCoroutine(BeginSpawn(m_spawn_rate.RandomRange));
        }

        #region DEVELOPMENT_ONLY
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
           
            for(int i = 0; i < m_bounds.Count; i++)
            {
                Gizmos.DrawWireCube( (Vector2) transform.position + m_bounds[i].position, m_bounds[i].size);
            }
        }
#endif
        #endregion
    }
}