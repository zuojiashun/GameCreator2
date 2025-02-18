﻿using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.VisualScripting
{
    public abstract class BaseActions : MonoBehaviour
    {
        protected static BaseActions ExecutingInForeground;

        protected static bool IsForegroundRunning => ExecutingInForeground != null;
        
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        protected InstructionList m_Instructions = new InstructionList();

        [SerializeField]
        protected bool m_InBackground = true;

        private Args m_Args;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsRunning => this.m_Instructions.IsRunning;
        public int RunningIndex => this.m_Instructions.RunningIndex;
        
        // EVENTS: --------------------------------------------------------------------------------
        
        public event Action EventInstructionStartRunning;
        public event Action EventInstructionEndRunning;
        public event Action<int> EventInstructionRun;
        
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        protected BaseActions()
        {
            this.m_Instructions.EventRunInstruction += i => this.EventInstructionRun?.Invoke(i);
            this.m_Instructions.EventStartRunning += () => this.EventInstructionStartRunning?.Invoke();
            this.m_Instructions.EventEndRunning += () => this.EventInstructionEndRunning?.Invoke();
        }

        // ABSTRACT METHODS: ----------------------------------------------------------------------
        
        public abstract void Invoke(GameObject self = null);
        
        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected async Task ExecInstructions()
        {
            this.m_Args ??= new Args(this.gameObject);
            await this.ExecInstructions(this.m_Args);
        }

        protected async Task ExecInstructions(Args args)
        {
            if (!this.m_InBackground && IsForegroundRunning) return;

            ExecutingInForeground = this;
            await this.m_Instructions.Run(args);
            ExecutingInForeground = null;
        }

        protected void StopExecInstructions()
        {
            this.m_Instructions.Cancel();
        }

        // BEHAVIOR METHODS: ----------------------------------------------------------------------

        protected virtual void OnDisable()
        {
            this.StopExecInstructions();
        }
    }
}