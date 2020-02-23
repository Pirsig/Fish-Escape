using System;

namespace BaseVariables
{
    [Serializable]
    public class StringReference
    {
        public bool isLocal = true;
        public bool isStatic = true;
        public string localValue;
        public StringVariable Variable;

        public StringReference()
        { }

        public StringReference(string value)
        {
            isLocal = true;
            isStatic = true;
            localValue = value;
        }

        public string Value
        {
            get { return isLocal ? localValue : Variable.value; }
            set
            {
                if (!isStatic)
                {
                    if (!isLocal)
                    {
                        Variable.ChangeText(value);
                    }
                    else
                    {
                        localValue = value;
                    }
                }
            }
        }

        public static implicit operator string(StringReference reference)
        {
            return reference.Value;
        }

        public override string ToString()
        {
            return isLocal ? localValue.ToString() : Variable.value.ToString();
        }
    }
}
