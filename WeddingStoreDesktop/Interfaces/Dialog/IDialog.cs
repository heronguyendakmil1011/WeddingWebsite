﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeddingStoreDesktop.Interfaces.Dialog
{
    public interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        Window Owner { get; set; }
        void Close();
        bool? ShowDialog();
    }
}
