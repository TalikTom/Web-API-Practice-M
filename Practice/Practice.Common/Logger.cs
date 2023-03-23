using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Practice.Common
{
    public static class Logger
    {
        public static void createTxtFSSW(string line)
        {
           
            // Set a variable to the Documents path.
            string docPath = $"C:\\Users\\student\\Documents\\Luka";


            if (!Directory.Exists(docPath))
            {
                Directory.CreateDirectory(docPath);
            }

            string fileLocation = Path.Combine(docPath, "WriteLines3.txt");


            if (!File.Exists(fileLocation))
            {
                FileStream stream = File.Create(fileLocation);

                stream.Flush();
                stream.Close();

            }



            using (FileStream stream = new FileStream(fileLocation, FileMode.Open))
            {
                StreamWriter writer = new StreamWriter(stream);
              
                
                writer.WriteLine(line);
                
                writer.Close();

            }
        }
    }
}