using System;

namespace BaseVariables
{
    [Serializable]
    public class IntReference
    {
        public bool isLocal = true;
        public bool isStatic = true;
        public int localValue;
        public IntVariable Variable;

        public IntReference()
        { }

        public IntReference(int value)
        {
            isLocal = true;
            isStatic = true;
            localValue = value;
        }

        public int Value
        {
            get { return isLocal ? localValue : Variable.value; }
            set
            {
                if (isStatic != true)
                {
                    if (isLocal != true)
                    {
                        Variable.SetValue(value);
                    }
                    else
                    {
                        localValue = value;
                    }
                }
            }
        }

        public static implicit operator int(IntReference reference)
        {
            return reference.Value;
        }

        public override string ToString()
        {
            return isLocal ? localValue.ToString() : Variable.value.ToString();
        }
    }
}
