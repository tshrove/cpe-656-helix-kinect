using System.Collections.Generic;
using System.IO;

namespace GestureRecorder.Data
{
    public class GestureFolderReader
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="path"></param>
        public static List<Gesture> Read(string path)
        {
            List<Gesture> gestures = new List<Gesture>();
            string folderPath = path;
            string[] filePaths = Directory.GetFiles(path, "*.iava");
            foreach (string file in filePaths)
            {
                gestures.Add(Gesture.Load(file));
            }
            return gestures;
        }
        #endregion
    }
}
