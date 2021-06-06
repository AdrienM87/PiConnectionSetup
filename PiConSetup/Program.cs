/*
 * Created by Adrien Marini - Copyright 2021
 */


using System;
using System.IO;

namespace PiConSetup
{
    class Program
    {
        private const string nameDriveBoot = "boot";
        private const string nameDriveFormat = "FAT32";
        private const string nameFileSSH = "ssh";
        private const string nameFileWifi = "wpa_supplicant.conf";
        private const string nameBoxWifi = "WifiBoxName";   //to replace
        private const string passBoxWifi = "Password";  //to replace
        private static string textFileWifi = "country=fr" + Environment.NewLine +
                    "update_config=1" + Environment.NewLine +
                    "ctrl_interface=/var/run/wpa_supplicant" + Environment.NewLine + Environment.NewLine +
                    "network={" + Environment.NewLine +
                    "scan_ssid=1" + Environment.NewLine +
                    "ssid=\"" + nameBoxWifi + "\"" + Environment.NewLine +
                    "psk=\"" + passBoxWifi + "\"" + Environment.NewLine +
                    "}";

        static void Main(string[] args)
        {
            string nameDrive = ClassFile.GetNameDriveBoot(nameDriveBoot, nameDriveFormat);
            if (nameDrive != null)
            {
                Console.WriteLine("- Nom du lecteur boot = " + nameDrive);
            }
            else
            {
                Console.WriteLine("- Nom du lecteur boot n'as pas pu être récupéré.");
            }

            bool resultSSH = ActivateSSH(nameDrive + nameFileSSH);
            if (resultSSH)
            {
                Console.WriteLine("- Fonction SSH activée avec succès.");
            }
            else
            {
                Console.WriteLine("- Fonction SSH n'a pas pu être activée correctement.");
            }

            bool resultWifi = ActivateWifi(nameDrive + nameFileWifi);
            if (resultWifi)
            {
                Console.WriteLine("- Fonction Wifi activée avec succès.");
            }
            else
            {
                Console.WriteLine("- Fonction Wifi n'a pas pu être activée correctement.");
            }

            Console.WriteLine("Terminé.");
            Console.ReadKey();
        }

        private static bool ActivateSSH(string path)
        {
            try
            {
                File.Create(path);
                if (File.Exists(path)) return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }

        private static bool ActivateWifi(string path)
        {
            try
            {
                string[] text = textFileWifi.Split(Environment.NewLine);

                ClassFile.WritingInTextFile(path, text);

                if (File.Exists(path))
                {
                    return ClassFile.ControlTextFile(path, text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
    }
}
