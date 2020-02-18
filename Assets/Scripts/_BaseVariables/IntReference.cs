using System;

namespace BaseVariables
{
    [Serializable]
    public class IntReference
    {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntReference Variable;

        public IntReference()
        { }

        public IntReference(int val)
        {
            UseConstant = true;
            ConstantValue = val;
        }

        public int value
        {
            get { return UseConstant ? ConstantValue : Variable.value; }
        }

        public static implicit operator int(IntReference reference)
        {
            return reference.value;
        }
    }
}
