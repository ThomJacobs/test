using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NamelessProgrammer
{
    [CreateAssetMenu(fileName = "Text_Generator", menuName = "Game_Jam/Text_Generator", order = 0)]
    public sealed class TextGenerator : ScriptableObject
    {
        //Attributes:
        [SerializeField] private List<string> m_words = new List<string>();

        //Properties:
        public string GetRandom
        {
            get
            {
                //Randomly select new word.
                int selected = Random.Range(default, m_words.Count - 1);
                string word = m_words[selected];

                //Place selected work at the back of the array where it will not be selected during the next call.
                m_words[selected] = m_words[m_words.Count - 1];
                m_words[m_words.Count - 1] = word;

                return word;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return m_words.Count <= 0;
            }
        }
    }
}