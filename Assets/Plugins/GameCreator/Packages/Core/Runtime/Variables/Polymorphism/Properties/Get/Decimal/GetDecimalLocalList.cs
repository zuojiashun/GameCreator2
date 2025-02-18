﻿using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Variables
{
    [Title("Local List Variable")]
    [Category("Variables/Local List Variable")]
    
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal)]
    [Description("Returns the decimal value of a Local List Variable")]

    [Serializable] [HideLabelsInEditor]
    public class GetDecimalLocalList : PropertyTypeGetDecimal
    {
        [SerializeField]
        protected FieldGetLocalList m_Variable = new FieldGetLocalList(ValueNumber.TYPE_ID);

        public override double Get(Args args) => this.m_Variable.Get<double>();
        public override double Get(GameObject gameObject) => this.m_Variable.Get<double>();

        public override string String => this.m_Variable.ToString();
    }
}