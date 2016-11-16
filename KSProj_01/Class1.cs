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
        [KSPField(isPersistant = true, guiActive = true, guiActiveEditor = true, guiName = "Buoyancy")]
        [UI_FloatRange(minValue = 0f, maxValue = 0f, stepIncrement = 0.05f, affectSymCounterparts = UI_Scene.Editor, scene = UI_Scene.All)]
        public float CurrentBuoyancy;

        [KSPField(isPersistant = false)]
        public float DefaultBuoyancy = 1f;

        //[KSPEvent(guiActive = true, guiActiveEditor = false, guiName = "Add Buoyancy")]
        //public void AddBuoyancy()
        //{
        //    if (CurrentBuoyancy < DefaultBuoyancy)
        //        CurrentBuoyancy += 0.05f;
        //    CurrentBuoyancy = (float)Math.Round(CurrentBuoyancy, 2);
        //}

        //[KSPEvent(guiActive = true, guiActiveEditor = false, guiName = "Reduce Buoyancy")]
        //public void ReduceBuoyancy()
        //{
        //    if (CurrentBuoyancy > 0f)
        //        CurrentBuoyancy -= 0.05f;
        //    CurrentBuoyancy = (float)Math.Round(CurrentBuoyancy, 2);
        //}

        public void FixedUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                part.buoyancy = CurrentBuoyancy;
            }
        }

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            ((UI_FloatRange)(Fields["CurrentBuoyancy"].uiControlEditor)).maxValue = DefaultBuoyancy;
            ((UI_FloatRange)(Fields["CurrentBuoyancy"].uiControlFlight)).maxValue = DefaultBuoyancy;

            switch (state)
            {
                case StartState.Editor:
                    CurrentBuoyancy = DefaultBuoyancy;
                    break;
                case StartState.None:
                    break;
            }
        }
    }
}
