using System;

namespace BaseVariables
{
    [Serializable]
    public class FloatReference
    {
        public bool isLocal = true;
        public bool isStatic = true;
        public float localValue;
        public FloatVariable Variable;

        public FloatReference()
        { }

        public FloatReference(float value)
        {
            isLocal = true;
            isStatic = true;
            localValue = value;
        }

        public float Value
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

        public static implicit operator float(FloatReference reference)
        {
            return reference.Value;
        }

        public override string ToString()
        {
            return isLocal ? localValue.ToString() : Variable.value.ToString();
        }
    }
}
