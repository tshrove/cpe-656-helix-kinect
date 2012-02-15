
using Iava.Input.Camera;

namespace GestureRecorder.Data {
    public class GestureSegment : tempuri.org.GestureDefinition.xsd.Gesture.SegmentLocalType {

        public GestureSegment()
            : this(null) { }
        
        public GestureSegment(SkeletonData skeleton) {
            if (skeleton == null) { return; }
        }

        public void SetTrackingJoints(params JointID[] joints) {
            foreach (JointID id in joints) {
                string temp = System.Enum.GetName(typeof(JointID), id);
            }
        }
    }
}
