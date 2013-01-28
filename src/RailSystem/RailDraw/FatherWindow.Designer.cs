using System.Drawing;
using System.Windows.Forms;

namespace RailDraw
{
    partial class FatherWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChooseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.operationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScale = new System.Windows.Forms.ToolStripMenuItem();
            this.menupercentone = new System.Windows.Forms.ToolStripMenuItem();
            this.menupercenttwo = new System.Windows.Forms.ToolStripMenuItem();
            this.menupercentthree = new System.Windows.Forms.ToolStripMenuItem();
            this.menupercentfour = new System.Windows.Forms.ToolStripMenuItem();
            this.programRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.new_btn = new System.Windows.Forms.ToolStripButton();
            this.open = new System.Windows.Forms.ToolStripButton();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cut = new System.Windows.Forms.ToolStripButton();
            this.copy = new System.Windows.Forms.ToolStripButton();
            this.paste = new System.Windows.Forms.ToolStripButton();
            this.delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.enlarge = new System.Windows.Forms.ToolStripButton();
            this.shorten = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.counter_clw = new System.Windows.Forms.ToolStripButton();
            this.clw = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.drawCanvas = new System.Windows.Forms.ToolStripButton();
            this.mouse = new System.Windows.Forms.ToolStripButton();
            this.mirror = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.addtext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.importPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.operationToolStripMenuItem,
            this.menuView,
            this.menuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(808, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.menuSave,
            this.menuSaveAs,
            this.menuClose});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(39, 21);
            this.menuFile.Text = "File";
            // 
            // menuNew
            // 
            this.menuNew.Name = "menuNew";
            this.menuNew.Size = new System.Drawing.Size(120, 22);
            this.menuNew.Text = "New";
            this.menuNew.Click += new System.EventHandler(this.new_btn_Click);
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(120, 22);
            this.menuOpen.Text = "Open";
            this.menuOpen.Click += new System.EventHandler(this.open_Click);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(120, 22);
            this.menuSave.Text = "Save";
            this.menuSave.Click += new System.EventHandler(this.save_Click);
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(120, 22);
            this.menuSaveAs.Text = "Save as";
            this.menuSaveAs.Click += new System.EventHandler(this.menuSaveAs_Click);
            // 
            // menuClose
            // 
            this.menuClose.Name = "menuClose";
            this.menuClose.Size = new System.Drawing.Size(120, 22);
            this.menuClose.Text = "Close";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCut,
            this.menuCopy,
            this.menuPaste,
            this.menuDelete,
            this.menuChooseAll});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(42, 21);
            this.menuEdit.Text = "Edit";
            // 
            // menuCut
            // 
            this.menuCut.Enabled = false;
            this.menuCut.Name = "menuCut";
            this.menuCut.Size = new System.Drawing.Size(113, 22);
            this.menuCut.Text = "Cut";
            this.menuCut.Click += new System.EventHandler(this.cut_Click);
            // 
            // menuCopy
            // 
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(113, 22);
            this.menuCopy.Text = "Copy";
            this.menuCopy.Click += new System.EventHandler(this.copy_Click);
            // 
            // menuPaste
            // 
            this.menuPaste.Name = "menuPaste";
            this.menuPaste.Size = new System.Drawing.Size(113, 22);
            this.menuPaste.Text = "Paste";
            this.menuPaste.Click += new System.EventHandler(this.paste_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(113, 22);
            this.menuDelete.Text = "Delete";
            this.menuDelete.Click += new System.EventHandler(this.delete_Click);
            // 
            // menuChooseAll
            // 
            this.menuChooseAll.Name = "menuChooseAll";
            this.menuChooseAll.Size = new System.Drawing.Size(113, 22);
            this.menuChooseAll.Text = "All";
            this.menuChooseAll.Click += new System.EventHandler(this.menuChooseAll_Click);
            // 
            // operationToolStripMenuItem
            // 
            this.operationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importDotToolStripMenuItem,
            this.importPictureToolStripMenuItem});
            this.operationToolStripMenuItem.Name = "operationToolStripMenuItem";
            this.operationToolStripMenuItem.Size = new System.Drawing.Size(79, 21);
            this.operationToolStripMenuItem.Text = "Operation";
            // 
            // importDotToolStripMenuItem
            // 
            this.importDotToolStripMenuItem.Name = "importDotToolStripMenuItem";
            this.importDotToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.importDotToolStripMenuItem.Text = "Import Dot";
            this.importDotToolStripMenuItem.Click += new System.EventHandler(this.importDotToolStripMenuItem_Click);
            // 
            // menuView
            // 
            this.menuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuScale,
            this.programRegionToolStripMenuItem,
            this.propertyPageToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.workRegionToolStripMenuItem,
            this.drawToolStripMenuItem});
            this.menuView.Name = "menuView";
            this.menuView.Size = new System.Drawing.Size(47, 21);
            this.menuView.Text = "View";
            // 
            // menuScale
            // 
            this.menuScale.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menupercentone,
            this.menupercenttwo,
            this.menupercentthree,
            this.menupercentfour});
            this.menuScale.Name = "menuScale";
            this.menuScale.Size = new System.Drawing.Size(168, 22);
            this.menuScale.Text = "Scale";
            // 
            // menupercentone
            // 
            this.menupercentone.Name = "menupercentone";
            this.menupercentone.Size = new System.Drawing.Size(108, 22);
            this.menupercentone.Text = "100%";
            this.menupercentone.Click += new System.EventHandler(this.menupercentone_Click);
            // 
            // menupercenttwo
            // 
            this.menupercenttwo.Name = "menupercenttwo";
            this.menupercenttwo.Size = new System.Drawing.Size(108, 22);
            this.menupercenttwo.Text = "200%";
            this.menupercenttwo.Click += new System.EventHandler(this.menupercentone_Click);
            // 
            // menupercentthree
            // 
            this.menupercentthree.Name = "menupercentthree";
            this.menupercentthree.Size = new System.Drawing.Size(108, 22);
            this.menupercentthree.Text = "300%";
            this.menupercentthree.Click += new System.EventHandler(this.menupercentone_Click);
            // 
            // menupercentfour
            // 
            this.menupercentfour.Name = "menupercentfour";
            this.menupercentfour.Size = new System.Drawing.Size(108, 22);
            this.menupercentfour.Text = "400%";
            this.menupercentfour.Click += new System.EventHandler(this.menupercentone_Click);
            // 
            // programRegionToolStripMenuItem
            // 
            this.programRegionToolStripMenuItem.Name = "programRegionToolStripMenuItem";
            this.programRegionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.programRegionToolStripMenuItem.Text = "ProgramRegion";
            this.programRegionToolStripMenuItem.Click += new System.EventHandler(this.programRegionToolStripMenuItem_Click);
            // 
            // propertyPageToolStripMenuItem
            // 
            this.propertyPageToolStripMenuItem.Name = "propertyPageToolStripMenuItem";
            this.propertyPageToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.propertyPageToolStripMenuItem.Text = "PropertyPage";
            this.propertyPageToolStripMenuItem.Click += new System.EventHandler(this.propertyPageToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.toolsToolStripMenuItem.Text = "Tools";
            this.toolsToolStripMenuItem.Click += new System.EventHandler(this.toolsToolStripMenuItem_Click);
            // 
            // workRegionToolStripMenuItem
            // 
            this.workRegionToolStripMenuItem.Name = "workRegionToolStripMenuItem";
            this.workRegionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.workRegionToolStripMenuItem.Text = "WorkRegion";
            this.workRegionToolStripMenuItem.Click += new System.EventHandler(this.workRegionToolStripMenuItem_Click);
            // 
            // drawToolStripMenuItem
            // 
            this.drawToolStripMenuItem.Name = "drawToolStripMenuItem";
            this.drawToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.drawToolStripMenuItem.Text = "Draw";
            this.drawToolStripMenuItem.Click += new System.EventHandler(this.drawToolStripMenuItem_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(47, 21);
            this.menuHelp.Text = "Help";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.new_btn,
            this.open,
            this.save,
            this.toolStripSeparator1,
            this.cut,
            this.copy,
            this.paste,
            this.delete,
            this.toolStripSeparator2,
            this.enlarge,
            this.shorten,
            this.toolStripSeparator3,
            this.counter_clw,
            this.clw,
            this.toolStripSeparator4,
            this.drawCanvas,
            this.mouse,
            this.mirror,
            this.toolStripSeparator5,
            this.addtext,
            this.toolStripSeparator6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // new_btn
            // 
            this.new_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.new_btn.Image = global::RailDraw.Properties.Resources.newfile;
            this.new_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.new_btn.Name = "new_btn";
            this.new_btn.Size = new System.Drawing.Size(23, 22);
            this.new_btn.Text = "new";
            this.new_btn.Click += new System.EventHandler(this.new_btn_Click);
            // 
            // open
            // 
            this.open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.open.Image = global::RailDraw.Properties.Resources.open;
            this.open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(23, 22);
            this.open.Text = "open";
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // save
            // 
            this.save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.save.Image = global::RailDraw.Properties.Resources.Save;
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(23, 22);
            this.save.Text = "save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cut
            // 
            this.cut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cut.Image = global::RailDraw.Properties.Resources.cut;
            this.cut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cut.Name = "cut";
            this.cut.Size = new System.Drawing.Size(23, 22);
            this.cut.Text = "cut";
            this.cut.Click += new System.EventHandler(this.cut_Click);
            // 
            // copy
            // 
            this.copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copy.Image = global::RailDraw.Properties.Resources.Copy;
            this.copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(23, 22);
            this.copy.Text = "copy";
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // paste
            // 
            this.paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.paste.Image = global::RailDraw.Properties.Resources.Paste;
            this.paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.paste.Name = "paste";
            this.paste.Size = new System.Drawing.Size(23, 22);
            this.paste.Text = "paste";
            this.paste.Click += new System.EventHandler(this.paste_Click);
            // 
            // delete
            // 
            this.delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delete.Image = global::RailDraw.Properties.Resources.delete;
            this.delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(23, 22);
            this.delete.Text = "delete";
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // enlarge
            // 
            this.enlarge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.enlarge.Image = global::RailDraw.Properties.Resources.enlarge;
            this.enlarge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.enlarge.Name = "enlarge";
            this.enlarge.Size = new System.Drawing.Size(23, 22);
            this.enlarge.Text = "enlarge";
            this.enlarge.Click += new System.EventHandler(this.enlarge_Click);
            // 
            // shorten
            // 
            this.shorten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.shorten.Image = global::RailDraw.Properties.Resources.shrink;
            this.shorten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.shorten.Name = "shorten";
            this.shorten.Size = new System.Drawing.Size(23, 22);
            this.shorten.Text = "shorten";
            this.shorten.Click += new System.EventHandler(this.shorten_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // counter_clw
            // 
            this.counter_clw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.counter_clw.Image = global::RailDraw.Properties.Resources.counter_clw;
            this.counter_clw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.counter_clw.Name = "counter_clw";
            this.counter_clw.Size = new System.Drawing.Size(23, 22);
            this.counter_clw.Text = "counter_clw";
            this.counter_clw.Click += new System.EventHandler(this.counter_clw_Click);
            // 
            // clw
            // 
            this.clw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clw.Image = global::RailDraw.Properties.Resources.clw;
            this.clw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clw.Name = "clw";
            this.clw.Size = new System.Drawing.Size(23, 22);
            this.clw.Text = "clw";
            this.clw.Click += new System.EventHandler(this.clw_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // drawCanvas
            // 
            this.drawCanvas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawCanvas.Image = global::RailDraw.Properties.Resources.drap;
            this.drawCanvas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawCanvas.Name = "drawCanvas";
            this.drawCanvas.Size = new System.Drawing.Size(23, 22);
            this.drawCanvas.Text = "drap";
            this.drawCanvas.Click += new System.EventHandler(this.drap_Click);
            // 
            // mouse
            // 
            this.mouse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mouse.Image = global::RailDraw.Properties.Resources.arrow;
            this.mouse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mouse.Name = "mouse";
            this.mouse.Size = new System.Drawing.Size(23, 22);
            this.mouse.Text = "mouse";
            this.mouse.ToolTipText = "mouse";
            this.mouse.Click += new System.EventHandler(this.mouse_Click);
            // 
            // mirror
            // 
            this.mirror.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mirror.Image = global::RailDraw.Properties.Resources.mirror;
            this.mirror.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mirror.Name = "mirror";
            this.mirror.Size = new System.Drawing.Size(23, 22);
            this.mirror.Text = "mirror";
            this.mirror.Click += new System.EventHandler(this.mirror_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // addtext
            // 
            this.addtext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addtext.Image = global::RailDraw.Properties.Resources.text;
            this.addtext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addtext.Name = "addtext";
            this.addtext.Size = new System.Drawing.Size(23, 22);
            this.addtext.Text = "addtext";
            this.addtext.Click += new System.EventHandler(this.addtext_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Enabled = false;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "revocation";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Enabled = false;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "regain";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 435);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(808, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
            this.dockPanel1.DockBottomPortion = 0.2D;
            this.dockPanel1.DockLeftPortion = 0.18D;
            this.dockPanel1.DockRightPortion = 0.18D;
            this.dockPanel1.DockTopPortion = 0.2D;
            this.dockPanel1.Location = new System.Drawing.Point(0, 50);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.RightToLeftLayout = true;
            this.dockPanel1.Size = new System.Drawing.Size(808, 385);
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("微软雅黑", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("微软雅黑", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            this.dockPanel1.Skin = dockPanelSkin1;
            this.dockPanel1.TabIndex = 8;
            // 
            // importPictureToolStripMenuItem
            // 
            this.importPictureToolStripMenuItem.Name = "importPictureToolStripMenuItem";
            this.importPictureToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.importPictureToolStripMenuItem.Text = "Import Picture";
            this.importPictureToolStripMenuItem.Click += new System.EventHandler(this.importPictureToolStripMenuItem_Click);
            // 
            // FatherWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 457);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "FatherWindow";
            this.Text = "Rails Map Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FatherWindow_FormClosing);
            this.Load += new System.EventHandler(this.FatherWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton open;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cut;
        private System.Windows.Forms.ToolStripButton copy;
        private System.Windows.Forms.ToolStripButton paste;
        private System.Windows.Forms.ToolStripButton delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton enlarge;
        private System.Windows.Forms.ToolStripButton shorten;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private ToolStripButton counter_clw;
        private ToolStripButton clw;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton drawCanvas;
        private ToolStripButton mouse;
        private ToolStripButton new_btn;
        private ToolStripButton mirror;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton addtext;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuFile;
        private ToolStripMenuItem menuNew;
        private ToolStripMenuItem menuOpen;
        private ToolStripMenuItem menuSave;
        private ToolStripMenuItem menuSaveAs;
        private ToolStripMenuItem menuClose;
        private ToolStripMenuItem menuEdit;
        private ToolStripMenuItem menuCut;
        private ToolStripMenuItem menuCopy;
        private ToolStripMenuItem menuPaste;
        private ToolStripMenuItem menuDelete;
        private ToolStripMenuItem menuChooseAll;
        private ToolStripMenuItem menuView;
        private ToolStripMenuItem menuHelp;
        private ToolStripMenuItem menuScale;
        private ToolStripMenuItem menupercentone;
        private ToolStripMenuItem menupercenttwo;
        private ToolStripMenuItem menupercentthree;
        private ToolStripMenuItem menupercentfour;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem programRegionToolStripMenuItem;
        private ToolStripMenuItem propertyPageToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem workRegionToolStripMenuItem;
        public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private ToolStripMenuItem drawToolStripMenuItem;
        private ToolStripMenuItem operationToolStripMenuItem;
        private ToolStripMenuItem importDotToolStripMenuItem;
        private ToolStripMenuItem importPictureToolStripMenuItem;
    }
}

