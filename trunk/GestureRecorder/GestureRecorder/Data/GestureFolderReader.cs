using System.Collections.Generic;
using System.IO;
using tempuri.org.GestureDefinition.xsd;

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
                System.IO.FileStream stream = new System.IO.FileStream(file, System.IO.FileMode.Open);
                System.IO.StreamReader xmlReader = new System.IO.StreamReader(stream);
                gestures.Add(Gesture.Load(xmlReader));
            }
            return gestures;
        }
        #endregion
    }
}
