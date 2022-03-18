using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SvgUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            //Access file location
            //Loop through files one by one
            //Open SVG or convert JSON.
            //Find all class names
            //Append with file name
            //Update class section and css section
            //Deserialise and save as SVG again.

            string filepath = "C:\\Users\\Joel Pickin\\Downloads\\Red_Panda"; /*<----- ADD FILE PATH HERE*/

            DirectoryInfo d = new DirectoryInfo(filepath);

            foreach (var folder in d.GetDirectories())
            {
                var folderName = folder.Name.Replace($" ", $"_");

                foreach (var subfolder in folder.GetDirectories())
                {

                    var subFolderName = subfolder.Name.Replace($" ", $"_");

                    foreach (var file in subfolder.GetFiles())
                    {
                        var filename = Path.GetFileNameWithoutExtension(file.Name);

                        filename = filename.Replace($" ", $"_");

                        if (!string.IsNullOrEmpty(filename) && filename != "._")
                        {

                        try
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load(file.FullName);

                            XmlNodeList elemList = doc.GetElementsByTagName("style");

                            foreach (var element in elemList)
                            {
                                var styleElement = (XmlElement)element;

                                var styleElementText = styleElement.InnerText;

                                for (char letter = 'a'; letter <= 'z'; letter++)
                                {
                                    styleElementText = styleElementText.Replace($".{letter}" + "{", $".{folderName}_{subFolderName}_{filename}_{letter}" + "{");
                                        styleElementText = styleElementText.Replace($".{letter},", $".{folderName}_{subFolderName}_{filename}_{letter},");
                                        styleElementText = styleElementText.Replace($"(#{letter}", $"(#{folderName}_{subFolderName}_{filename}_{letter}");

                                        for (char twoLetter = 'a'; twoLetter <= 'z'; twoLetter++)
                                        {
                                            styleElementText = styleElementText.Replace($".{letter}{twoLetter}" + "{", $".{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}" + "{");
                                            styleElementText = styleElementText.Replace($".{letter}{twoLetter},", $".{folderName}_{subFolderName}_{filename}_{letter},");
                                            styleElementText = styleElementText.Replace($"(#{letter}{twoLetter}", $"(#{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}\"");


                                            for (char threeLetter = 'a'; threeLetter <= 'z'; threeLetter++)
                                            {
                                                styleElementText = styleElementText.Replace($".{letter}{twoLetter}{threeLetter}" + "{", $".{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}{threeLetter}" + "{");
                                                styleElementText = styleElementText.Replace($".{letter}{twoLetter}{threeLetter},", $".{folderName}_{subFolderName}_{filename}_{letter},");
                                                styleElementText = styleElementText.Replace($"(#{letter}{twoLetter}{threeLetter}", $"(#{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}{threeLetter}\"");
                                            }
                                        }
                                }

                                styleElement.InnerText = styleElementText;
                            }

                            XmlNodeList gElemList = doc.GetElementsByTagName("svg");

                            foreach (var element in gElemList)
                            {
                                var gElement = (XmlElement)element;

                                var gElementText = gElement.InnerXml;
                              
                                for (char letter = 'a'; letter <= 'z'; letter++)
                                {
                                    gElementText = gElementText.Replace($"class=\"{letter}\"", $"class=\"{folderName}_{subFolderName}_{filename}_{letter}\"");
                                    gElementText = gElementText.Replace($"id=\"{letter}\"", $"id=\"{folderName}_{subFolderName}_{filename}_{letter}\"");
                                    gElementText = gElementText.Replace($"\"#{letter}\"", $"\"#{folderName}_{subFolderName}_{filename}_{letter}\"");


                                        for (char twoLetter = 'a'; twoLetter <= 'z'; twoLetter++)
                                    {
                                        gElementText = gElementText.Replace($"class=\"{letter}{twoLetter}\"", $"class=\"{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}\"");
                                            gElementText = gElementText.Replace($"id=\"{letter}{twoLetter}\"", $"id=\"{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}\"");
                                            gElementText = gElementText.Replace($"\"#{letter}{twoLetter}\"", $"\"#{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}\"");

                                            for (char threeLetter = 'a'; threeLetter <= 'z'; threeLetter++)
                                        {
                                            gElementText = gElementText.Replace($"class=\"{letter}{twoLetter}{threeLetter}\"", $"class=\"{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}{threeLetter}\"");
                                                gElementText = gElementText.Replace($"id=\"{letter}{twoLetter}{threeLetter}\"", $"id=\"{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}{threeLetter}\"");
                                                gElementText = gElementText.Replace($"\"#{letter}{twoLetter}{threeLetter}\"", $"\"#{folderName}_{subFolderName}_{filename}_{letter}{twoLetter}{threeLetter}\"");

                                                Console.WriteLine($"{ letter}{ twoLetter}{ threeLetter}");
                                            }
                                    }
                                }

                                gElement.InnerXml = gElementText;
                            }

                            doc.Save(file.FullName);
                        }
                        finally
                        {
     
                        }
                    }
                    }
                }
            }
        }
    }
}
