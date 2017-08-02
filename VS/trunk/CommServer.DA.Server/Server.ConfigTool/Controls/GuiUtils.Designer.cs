namespace CAS.CommServer.DA.Server.ConfigTool
{
    partial class GuiUtils
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuiUtils));
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "SimpleItem");
            this.ImageList.Images.SetKeyName(1, "Object");
            this.ImageList.Images.SetKeyName(2, "Folder");
            this.ImageList.Images.SetKeyName(3, "Area");
            this.ImageList.Images.SetKeyName(4, "Variable");
            this.ImageList.Images.SetKeyName(5, "Property");
            this.ImageList.Images.SetKeyName(6, "Method");
            this.ImageList.Images.SetKeyName(7, "ReferenceType");
            this.ImageList.Images.SetKeyName(8, "DataType");
            this.ImageList.Images.SetKeyName(9, "View");
            this.ImageList.Images.SetKeyName(10, "ExpandPlus");
            this.ImageList.Images.SetKeyName(11, "ExpandMinus");
            this.ImageList.Images.SetKeyName(12, "VariableType");
            this.ImageList.Images.SetKeyName(13, "ObjectType");
            this.ImageList.Images.SetKeyName(14, "Info");
            this.ImageList.Images.SetKeyName(15, "Server");
            this.ImageList.Images.SetKeyName(16, "ServerStopped");
            // 
            // GuiUtils
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "GuiUtils";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ImageList ImageList;

    }
}
