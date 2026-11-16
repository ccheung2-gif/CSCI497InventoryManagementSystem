namespace WindowsFormsApp1
{
    partial class Form2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpIdentification = new System.Windows.Forms.GroupBox();
            this.lblItemId = new System.Windows.Forms.Label();
            this.txtItemId = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.grpStock = new System.Windows.Forms.GroupBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.lblStockCategory = new System.Windows.Forms.Label();
            this.cboStockCategory = new System.Windows.Forms.ComboBox();
            this.lblStockDescription = new System.Windows.Forms.Label();
            this.txtStockDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpIdentification.SuspendLayout();
            this.grpStock.SuspendLayout();
            this.SuspendLayout();
            //
            // grpIdentification
            //
            this.grpIdentification.Controls.Add(this.lblItemId);
            this.grpIdentification.Controls.Add(this.txtItemId);
            this.grpIdentification.Controls.Add(this.lblName);
            this.grpIdentification.Controls.Add(this.txtName);
            this.grpIdentification.Controls.Add(this.lblCategory);
            this.grpIdentification.Controls.Add(this.cboCategory);
            this.grpIdentification.Controls.Add(this.lblDescription);
            this.grpIdentification.Controls.Add(this.txtDescription);
            this.grpIdentification.Controls.Add(this.lblSupplier);
            this.grpIdentification.Controls.Add(this.cboSupplier);
            this.grpIdentification.Location = new System.Drawing.Point(12, 12);
            this.grpIdentification.Name = "grpIdentification";
            this.grpIdentification.Size = new System.Drawing.Size(336, 178);
            this.grpIdentification.TabIndex = 0;
            this.grpIdentification.TabStop = false;
            this.grpIdentification.Text = "Identification";
            //
            // lblItemId
            //
            this.lblItemId.AutoSize = true;
            this.lblItemId.Location = new System.Drawing.Point(12, 28);
            this.lblItemId.Name = "lblItemId";
            this.lblItemId.Size = new System.Drawing.Size(48, 15);
            this.lblItemId.TabIndex = 0;
            this.lblItemId.Text = "Item ID:";
            //
            // txtItemId
            //
            this.txtItemId.Location = new System.Drawing.Point(96, 24);
            this.txtItemId.Name = "txtItemId";
            this.txtItemId.Size = new System.Drawing.Size(225, 23);
            this.txtItemId.TabIndex = 1;
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 57);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(42, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            //
            // txtName
            //
            this.txtName.Location = new System.Drawing.Point(96, 53);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(225, 23);
            this.txtName.TabIndex = 3;
            //
            // lblCategory
            //
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(12, 86);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(58, 15);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category:";
            //
            // cboCategory
            //
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(96, 82);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(225, 23);
            this.cboCategory.TabIndex = 5;
            //
            // lblDescription
            //
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 115);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(70, 15);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Description:";
            //
            // txtDescription
            //
            this.txtDescription.Location = new System.Drawing.Point(96, 111);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(225, 23);
            this.txtDescription.TabIndex = 7;
            //
            // lblSupplier
            //
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(12, 144);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(53, 15);
            this.lblSupplier.TabIndex = 8;
            this.lblSupplier.Text = "Supplier:";
            //
            // cboSupplier
            //
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.Location = new System.Drawing.Point(96, 140);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(225, 23);
            this.cboSupplier.TabIndex = 9;
            //
            // grpStock
            //
            this.grpStock.Controls.Add(this.lblQuantity);
            this.grpStock.Controls.Add(this.txtQuantity);
            this.grpStock.Controls.Add(this.lblUnitPrice);
            this.grpStock.Controls.Add(this.txtUnitPrice);
            this.grpStock.Controls.Add(this.lblStockCategory);
            this.grpStock.Controls.Add(this.cboStockCategory);
            this.grpStock.Controls.Add(this.lblStockDescription);
            this.grpStock.Controls.Add(this.txtStockDescription);
            this.grpStock.Location = new System.Drawing.Point(12, 202);
            this.grpStock.Name = "grpStock";
            this.grpStock.Size = new System.Drawing.Size(336, 149);
            this.grpStock.TabIndex = 1;
            this.grpStock.TabStop = false;
            this.grpStock.Text = "Stock";
            //
            // lblQuantity
            //
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(12, 28);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(56, 15);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Quantity:";
            //
            // txtQuantity
            //
            this.txtQuantity.Location = new System.Drawing.Point(96, 24);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(225, 23);
            this.txtQuantity.TabIndex = 1;
            //
            // lblUnitPrice
            //
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Location = new System.Drawing.Point(12, 57);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(61, 15);
            this.lblUnitPrice.TabIndex = 2;
            this.lblUnitPrice.Text = "Unit Price:";
            //
            // txtUnitPrice
            //
            this.txtUnitPrice.Location = new System.Drawing.Point(96, 53);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(225, 23);
            this.txtUnitPrice.TabIndex = 3;
            //
            // lblStockCategory
            //
            this.lblStockCategory.AutoSize = true;
            this.lblStockCategory.Location = new System.Drawing.Point(12, 86);
            this.lblStockCategory.Name = "lblStockCategory";
            this.lblStockCategory.Size = new System.Drawing.Size(58, 15);
            this.lblStockCategory.TabIndex = 4;
            this.lblStockCategory.Text = "Category:";
            //
            // cboStockCategory
            //
            this.cboStockCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStockCategory.FormattingEnabled = true;
            this.cboStockCategory.Location = new System.Drawing.Point(96, 82);
            this.cboStockCategory.Name = "cboStockCategory";
            this.cboStockCategory.Size = new System.Drawing.Size(225, 23);
            this.cboStockCategory.TabIndex = 5;
            //
            // lblStockDescription
            //
            this.lblStockDescription.AutoSize = true;
            this.lblStockDescription.Location = new System.Drawing.Point(12, 115);
            this.lblStockDescription.Name = "lblStockDescription";
            this.lblStockDescription.Size = new System.Drawing.Size(70, 15);
            this.lblStockDescription.TabIndex = 6;
            this.lblStockDescription.Text = "Description:";
            //
            // txtStockDescription
            //
            this.txtStockDescription.Location = new System.Drawing.Point(96, 111);
            this.txtStockDescription.Name = "txtStockDescription";
            this.txtStockDescription.Size = new System.Drawing.Size(225, 23);
            this.txtStockDescription.TabIndex = 7;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(162, 363);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(258, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // Form2
            //
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(360, 403);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpStock);
            this.Controls.Add(this.grpIdentification);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add / Edit Item";
            this.grpIdentification.ResumeLayout(false);
            this.grpIdentification.PerformLayout();
            this.grpStock.ResumeLayout(false);
            this.grpStock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpIdentification;
        private System.Windows.Forms.Label lblItemId;
        private System.Windows.Forms.TextBox txtItemId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.GroupBox grpStock;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label lblUnitPrice;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.Label lblStockCategory;
        private System.Windows.Forms.ComboBox cboStockCategory;
        private System.Windows.Forms.Label lblStockDescription;
        private System.Windows.Forms.TextBox txtStockDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
