using UnityEngine;

namespace BaseVariables
{
    [CreateAssetMenu(menuName = "BaseVariables/StringVariable")]
    public class StringVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string developerDescription = "";
#endif
        public string value;

        //constructor with given float value for use with conversion
        public StringVariable(string newValue)
        {
            value = newValue;
        }

        public void ChangeText(string newValue)
        {
            value = newValue;
        }

        public void ChangeText(StringVariable newValue)
        {
            value = newValue.value;
        }

        public void EmptyText()
        {
            value = "";
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static implicit operator string(StringVariable variable)
        {
            return variable.value;
        }
    }
}