using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KSProj_01
{
    class ModuleShowBuoyancy : PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = true)]
        public float BuoyancyDisplay;

        public void FixedUpdate()
        {
            BuoyancyDisplay = part.buoyancy;
        }
    }
}
