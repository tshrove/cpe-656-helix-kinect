using System.Collections.Generic;
using System.IO;
using Iava.Gesture;

namespace Iava.Gesture {
    public class GestureFolderReader {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="path"></param>
        public static List<IavaGesture> Read(string path) {
            List<IavaGesture> gestures = new List<IavaGesture>();
            string folderPath = path;
            string[] filePaths = Directory.GetFiles(path, "*.iava");
            foreach (string file in filePaths) {
                gestures.Add(IavaGesture.Load(file));
            }
            return gestures;
        }
        #endregion
    }
}
