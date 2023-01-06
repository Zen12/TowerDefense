using UnityEngine;

namespace _Project.Scripts.Movables
{
    public class UnityEngineBezierPathAdapter : MonoBehaviour, IPath
    {
        [SerializeField] private BezierSolution.BezierSpline _spline;
        public Vector3 GetPositionFromTime(in float time)
        {
            return _spline.GetPoint(time);
        }
    }
}
