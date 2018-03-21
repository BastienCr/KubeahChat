﻿//--------------------------------------------------------------------
//  Kubeah ! Open Source Project
//  
//  Kubeah Chat
//  Just like Open Source
//--------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KChat.Methods
{
    class UpdateApplication
    {
        //Function verification update
        public static void VersionVerification(long iVersionApplication)
        {
            try
            {
                System.Net.WebClient webClientKubeah = new System.Net.WebClient();
                string sVersionWeb = webClientKubeah.DownloadString("http://kubeah.com/kchat/version.txt");//Affectation de */version.txt à sVersionWeb
                if (iVersionApplication >= Convert.ToInt64(sVersionWeb)) { }//Comparaison si nouvelle/ancienne version
                else
                {//Si ancienne version
                    string sInfoNewVersion = webClientKubeah.DownloadString("http://kubeah.com/kchat/info.txt");//Affectation de */version.txt à sInfoNewVersion
                    DialogResult dialogResultUser = MessageBox.Show(sInfoNewVersion, "A new version is available", MessageBoxButtons.YesNo);//Message box avec YES/NO
                    if (dialogResultUser == DialogResult.Yes)
                    {
                        try
                        {
                            Process.Start(".\\KubeahChat_Update.exe");
                            Application.Exit();
                        }
                        catch
                        {
                            MessageBox.Show("KubeahChat_Update.exe not found!", "", MessageBoxButtons.OK , MessageBoxIcon.Error);
                        }
                    }
                    else if (dialogResultUser == DialogResult.No) { }//Ne rien faire si NO
                }
            }
            catch { }//Pour gestion pas d'accès internet
        }
    }
}