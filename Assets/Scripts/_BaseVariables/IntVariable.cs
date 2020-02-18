using UnityEngine;

namespace BaseVariables
{
    [CreateAssetMenu(menuName = "BaseVariables/IntVariable")]
    public class IntVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string developerDescription = "";
#endif
        public int value;

        public void SetValue(int val)
        {
            value = val;
        }

        public void SetValue(IntVariable val)
        {
            value = val.value;
        }

        public void ApplyChange(int change)
        {
            value += change;
        }

        public void ApplyChange(IntVariable change)
        {
            value += change.value;
        }

        public void AddOne()
        {
            value++;
        }

        public void SubtractOne()
        {
            value--;
        }
    }
}