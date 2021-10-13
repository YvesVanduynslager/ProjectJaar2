using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TILE03.Helper
{
    //Deze klasse helpt bij het manipuleren van files
    public static class FileHelper
    {
        //Converteren van een file naar een byte[]
        public static byte[] ConvertPDFtoByteArray(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] contents = new byte[fs.Length];
                fs.Read(contents, 0, (int) fs.Length);
                return contents;
            }
        }
        //Converteren van een byte[] naar een file
        public static void ConvertByteArrayToPDF(byte[] byteData, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(byteData, 0, byteData.Length);
            }
        }
    }
}