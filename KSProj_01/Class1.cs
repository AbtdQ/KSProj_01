using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KSProj_01
{
    public class TestModule : PartModule
    {
        [KSPField(isPersistant = false, guiActive = true, guiActiveEditor = true, guiName = "Buoyancy"), UI_FloatRange(minValue = 0f, maxValue = 0f, stepIncrement = 1f, scene = UI_Scene.Editor)]
        public float TestNum;

        public void FixedUpdate()
        {
            part.buoyancy = TestNum;
        }

        public override void OnStart(StartState state)
        {
            var FloatRangeCtrl = (UI_FloatRange) Fields["TestNum"].uiControlEditor;
            FloatRangeCtrl.maxValue = 20f;
            if (state == StartState.Editor || state == StartState.None) return;
            base.OnStart(state);
        }
    }
}
