using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Helper
{
    public static class MousePositionHelper
    {
        public static Vector3 GetWorldPositionFromMousePosition(float yPosition = 0) 
        {
            var MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            var piso = Physics.RaycastAll(MouseRay).FirstOrDefault(r=>r.collider.tag == Tags.Horizon);
            if (piso.collider != null)
            {
                Vector3 targetPosition = new Vector3(piso.point.x, yPosition, piso.point.z);
                return targetPosition;
            }
            return Vector3.zero;
        }
    }
}
