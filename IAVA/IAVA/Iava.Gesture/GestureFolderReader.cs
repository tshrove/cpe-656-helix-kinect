using System.Collections.Generic;
using System.IO;

namespace Iava.Gesture {

    /// <summary>
    /// Reads and Returns all the Iava gesture files from a specified directory
    /// </summary>
    public class GestureFolderReader {

        #region Public Methods
        /// <summary>
        /// Scans a specified directory for IavaGesture files
        /// </summary>
        /// <param name="path">Filepath to the gesture directory </param>
        /// <returns>List of Gestures read from the directory</returns>
        public static List<IavaGesture> Read(string path) {
            List<IavaGesture> gestures = new List<IavaGesture>();

            // Load each gesture in the directory and add them to the list
            foreach (string file in Directory.GetFiles(path, "*.iava")) {
                gestures.Add(IavaGesture.Load(file));
            }

            // Return the list
            return gestures;
        }
        #endregion Public Methods
    }
}
