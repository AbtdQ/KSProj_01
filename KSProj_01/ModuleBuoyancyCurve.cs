using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KSProj_01
{
    class ModuleBuoyancyCurve : PartModule
    {
        [KSPField(guiName = "Buoyancy", guiActive = true, guiActiveEditor = true, isPersistant = true)]
        [UI_ProgressBar(minValue = 0f, maxValue = 0f, scene = UI_Scene.All)]
        public float currentBuoyancyDisplay;

        public FloatCurve maxtempBuoyancyCurve = new FloatCurve();

        [KSPField(isPersistant = true)]
        public double currentMaxTemp = float.MinValue;

        public void FixedUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                if (part.temperature > currentMaxTemp)
                {
                    currentMaxTemp = part.temperature;

                    currentBuoyancyDisplay = (float)Math.Round(maxtempBuoyancyCurve.Evaluate((float)currentMaxTemp), 2);
                    part.buoyancy = currentBuoyancyDisplay;
                }
            }
        }

        public override void OnLoad(ConfigNode node)
        {
            base.OnLoad(node);

            if (node.HasNode("maxTempBuoyancyCurve"))
            {
                Debug.Log("[BuoyancyCurve]Loading Curve");
                maxtempBuoyancyCurve.Load(node.GetNode("maxTempBuoyancyCurve"));
            }
            else
            {
                Debug.Log("[BuoyancyCurve]maxTempBuoyancyCurve is missing, creating default");
                maxtempBuoyancyCurve.Add(0f, 1f, 0f, 0f);
                maxtempBuoyancyCurve.Add(300f, 1f, 0f, 0f);
                maxtempBuoyancyCurve.Add(800f, 0.5f, 0f, 0f);
                maxtempBuoyancyCurve.Add(1600f, 0f, 0f, 0f);
            }
            Debug.Log("[BuoyancyCurve]UI init");
            float tmaxValue, tminValue;
            maxtempBuoyancyCurve.FindMinMaxValue(out tminValue, out tmaxValue);
            ((UI_ProgressBar)(Fields["currentBuoyancyDisplay"].uiControlEditor)).minValue = tminValue;
            ((UI_ProgressBar)(Fields["currentBuoyancyDisplay"].uiControlEditor)).maxValue = tmaxValue;
            ((UI_ProgressBar)(Fields["currentBuoyancyDisplay"].uiControlFlight)).minValue = tminValue;
            ((UI_ProgressBar)(Fields["currentBuoyancyDisplay"].uiControlFlight)).maxValue = tmaxValue;
            currentBuoyancyDisplay = tmaxValue;
        }
    }
}
