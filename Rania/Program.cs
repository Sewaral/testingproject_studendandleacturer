using DocumentFormat.OpenXml.Spreadsheet;
using Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania;
using System;
using System.Windows.Forms;



//namespace Yosef_Hamdan_Rania
//{
//    internal static class Program
//    {
//        [STAThread]
//        static void Main()
//        {
//            ApplicationConfiguration.Initialize();
//            Application.Run(new CreateExamForm()); // 🔥 מפעיל ישירות את יצירת מבחן
//        }
//    }
//}



//using System;
//using System.Windows.Forms;

//namespace Rania
//{
//    static class Program
//    {
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//            // 🟢 כאן מציינים את הטופס הראשי שייפתח:
//            Application.Run(new MainForm());
//        }
//    }
//}


//namespace Rania
//{
//    internal static class Program
//    {
//        /// <summary>
//        ///  The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            // To customize application configuration such as set high DPI settings or default font,
//            ApplicationConfiguration.Initialize();
//            Application.Run(new AddQuestion());
//        }
//    }
//}


namespace Yosef_Hamdan_Yakoob_Sewar_Doaa_Rania
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
//namespace Rania
//{
//    static class Program
//    {
//        /// <summary>
//        /// נקודת כניסה ראשית לתוכנית
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//            // הפעלה עם שם סטודנט לדוגמה:
//            string studentName = "Yosef1";
//            Application.Run(new Form2(studentName));
//        }
//    }
//}
//namespace Rania
//{
//    internal static class Program
//    {
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new StatsForm());
//        }
//    }
//}