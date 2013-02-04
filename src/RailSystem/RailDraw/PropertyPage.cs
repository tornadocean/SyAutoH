using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace RailDraw
{
    public partial class PropertyPage : DockContent
    {
        public bool winShown = false;

        public PropertyPage()
        {
            InitializeComponent();
        }

        private void PropertyPage_Shown(object sender, EventArgs e)
        {
            winShown = true;
        }

        private void PropertyPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            winShown = false;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            FatherWindow father = (FatherWindow)this.DockPanel.Parent;
            father.objectEvent.ChangePropertyValue();
            father.workRegion.picBoxCanvas.Invalidate();
            this.propertyGrid1.Refresh();
        }
    }
}
