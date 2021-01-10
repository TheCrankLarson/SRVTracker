using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valve.VR;
using System.Text.Json;
using EDTracking;

namespace SRVTracker
{
    class VRMatrixCollection
    {
    }

    class VRMatrixSetting
    {
        private MatrixDefinition _hmdMatrix;
        private float _overlayWidthInM = 0.6f;
        private string _name = "Matrix definition";

        public VRMatrixSetting(string Name, MatrixDefinition Matrix, float OverlayWidth)
        {
            _hmdMatrix = Matrix;
            _overlayWidthInM = OverlayWidth;
            _name = Name;
        }
    }
}
