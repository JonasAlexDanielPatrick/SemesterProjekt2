﻿using System;
using System.Threading;
using Main;

namespace Controllers
{
    class ControllerUr
    // Skrevet af Jonas og Alex
    {

        public static string london = "";
        public static string københavn = "";

        public static void London()
        {
            while (true)
            {
                int temp = DateTime.Now.Hour - 1;
                string hour = temp.ToString("00");
                string minute = DateTime.Now.Minute.ToString("00");

                london = String.Format("London: {0}:{1}", hour, minute);

                MainWindow.instance.LondonUr = london;

                Thread.Sleep(60000);
            }
        }

        public static void København()
        {
            while (true)
            {
                string hour = DateTime.Now.Hour.ToString("00");
                string minute = DateTime.Now.Minute.ToString("00");

                københavn = String.Format("København: {0}:{1}", hour, minute);

                MainWindow.instance.KøbenhavnUr = københavn;

                Thread.Sleep(60000);
            }
        }

    }
}
