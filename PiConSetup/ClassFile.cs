/*
 * Created by Adrien Marini - Copyright 2021
 */

using System;
using System.IO;

namespace PiConSetup
{
    /// <summary>
    /// Classe permettant d'effectuer tous type de traitements génériques sur les fichiers
    /// </summary>
    public static class ClassFile
    {
        /// <summary>
        /// Retourne le nom du lecteur selon les paramètres spécifiés
        /// </summary>
        /// <param name="volumeLabel"></param>
        /// <param name="driveFormat"></param>
        /// <returns></returns>
        public static string GetNameDriveBoot(string volumeLabel, string driveFormat = null)
        {
            try
            {
                foreach (DriveInfo dr in DriveInfo.GetDrives())
                {
                    if (dr.IsReady && dr.VolumeLabel != "" && dr.VolumeLabel == volumeLabel)
                    {
                        if (driveFormat != null)
                        {
                            if (dr.DriveFormat == driveFormat)
                            {
                                return dr.Name;
                            }
                        }
                        else
                        {
                            return dr.Name;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        /// <summary>
        /// Ecrit les lignes passées en paramètre dans le fichier type texte spécifié
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        public static void WritingInTextFile(string path, string[] lines)
        {
            try
            {
                using (StreamWriter fileText = new StreamWriter(path))
                {
                    foreach (string l in lines)
                    {
                        fileText.WriteLine(l);
                    }
                }
            }
                        catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Controle que les lignes passées en paramètres correspondent parfaitement au contenu d'un fichier type texte.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static bool ControlTextFile(string path, string[] lines)
        {
            try
            {
                using (StreamReader fileText = new StreamReader(path))
                {
                    while (!fileText.EndOfStream)
                    {
                        foreach (string l in lines)
                        {
                            if (l != fileText.ReadLine())
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
                                    catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }   
    }
}
