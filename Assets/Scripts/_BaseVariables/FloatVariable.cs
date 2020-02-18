using UnityEngine;

namespace BaseVariables
{
    [CreateAssetMenu(menuName = "BaseVariables/FloatVariable")]
    public class FloatVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string developerDescription = "";
#endif
        public float value;

        //constructor with given float value for use with conversion
        public FloatVariable(float newValue)
        {
            value = newValue;
        }
        
        public void SetValue(float newValue)
        {
            value = newValue;
        }

        public void SetValue(FloatVariable newValue)
        {
            value = newValue.value;
        }

        public void ApplyChange(float change)
        {
            value += change;
        }

        public void ApplyChange(FloatVariable change)
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
        
        public override string ToString()
        {
            return value.ToString();
        }

        /*public static implicit operator FloatVariable(float v)
        {
            return new FloatVariable(v);
        }*/

        public static implicit operator float(FloatVariable variable)
        {
            return variable.value;
        }
    }
}