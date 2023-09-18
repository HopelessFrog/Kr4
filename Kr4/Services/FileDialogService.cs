using Kr4.Services.Interface;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Kr4.Services
{
    public class FileDialogService : IFileDialogService
    {
        public string ShowFileSaveDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = ".xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;


            }
            else
            {
                return String.Empty;
            }
        }
    }
}
