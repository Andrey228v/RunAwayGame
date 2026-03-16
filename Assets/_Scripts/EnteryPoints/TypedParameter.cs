using ECM2;
using System;

namespace Assets.Scripts.EnteryPoints
{
    internal class TypedParameter
    {
        private Type type;
        private Character character;

        public TypedParameter(Type type, Character character)
        {
            this.type = type;
            this.character = character;
        }
    }
}