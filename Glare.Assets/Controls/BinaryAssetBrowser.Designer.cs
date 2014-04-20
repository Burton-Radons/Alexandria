namespace Glare.Assets.Controls {
	partial class BinaryAssetBrowser {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.splitter = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.DataOffset = new System.Windows.Forms.TextBox();
			this.DataSByte = new System.Windows.Forms.TextBox();
			this.DataByte = new System.Windows.Forms.TextBox();
			this.DataInt16 = new System.Windows.Forms.TextBox();
			this.DataUInt16 = new System.Windows.Forms.TextBox();
			this.DataInt32 = new System.Windows.Forms.TextBox();
			this.DataUInt32 = new System.Windows.Forms.TextBox();
			this.DataInt64 = new System.Windows.Forms.TextBox();
			this.DataUInt64 = new System.Windows.Forms.TextBox();
			this.DataSingle = new System.Windows.Forms.TextBox();
			this.DataDouble = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.DataInt16BE = new System.Windows.Forms.TextBox();
			this.DataUInt16BE = new System.Windows.Forms.TextBox();
			this.DataInt32BE = new System.Windows.Forms.TextBox();
			this.DataUInt32BE = new System.Windows.Forms.TextBox();
			this.DataInt64BE = new System.Windows.Forms.TextBox();
			this.DataUInt64BE = new System.Windows.Forms.TextBox();
			this.DataSingleBE = new System.Windows.Forms.TextBox();
			this.DataDoubleBE = new System.Windows.Forms.TextBox();
			this.DataUTF8 = new System.Windows.Forms.TextBox();
			this.DataUTF16 = new System.Windows.Forms.TextBox();
			this.DataUTF16BE = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
			this.splitter.Panel2.SuspendLayout();
			this.splitter.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitter
			// 
			this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitter.IsSplitterFixed = true;
			this.splitter.Location = new System.Drawing.Point(0, 0);
			this.splitter.Name = "splitter";
			// 
			// splitter.Panel2
			// 
			this.splitter.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitter.Size = new System.Drawing.Size(1091, 476);
			this.splitter.SplitterDistance = 646;
			this.splitter.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.DataUTF16BE, 2, 13);
			this.tableLayoutPanel1.Controls.Add(this.DataUTF16, 1, 13);
			this.tableLayoutPanel1.Controls.Add(this.DataUTF8, 1, 12);
			this.tableLayoutPanel1.Controls.Add(this.DataDoubleBE, 2, 11);
			this.tableLayoutPanel1.Controls.Add(this.DataSingleBE, 2, 10);
			this.tableLayoutPanel1.Controls.Add(this.DataUInt64BE, 2, 9);
			this.tableLayoutPanel1.Controls.Add(this.DataInt64BE, 2, 8);
			this.tableLayoutPanel1.Controls.Add(this.DataUInt32BE, 2, 7);
			this.tableLayoutPanel1.Controls.Add(this.DataInt32BE, 2, 6);
			this.tableLayoutPanel1.Controls.Add(this.DataUInt16BE, 2, 5);
			this.tableLayoutPanel1.Controls.Add(this.label12, 0, 12);
			this.tableLayoutPanel1.Controls.Add(this.DataDouble, 1, 11);
			this.tableLayoutPanel1.Controls.Add(this.DataSingle, 1, 10);
			this.tableLayoutPanel1.Controls.Add(this.DataUInt64, 1, 9);
			this.tableLayoutPanel1.Controls.Add(this.DataInt64, 1, 8);
			this.tableLayoutPanel1.Controls.Add(this.DataUInt32, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.DataInt32, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.DataUInt16, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.DataInt16, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.DataByte, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.DataSByte, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.label11, 0, 11);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 9);
			this.tableLayoutPanel1.Controls.Add(this.label10, 0, 10);
			this.tableLayoutPanel1.Controls.Add(this.DataOffset, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label13, 0, 13);
			this.tableLayoutPanel1.Controls.Add(this.label14, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label15, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label16, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.DataInt16BE, 2, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 15;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(441, 476);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 26);
			this.label1.TabIndex = 0;
			this.label1.Text = "Offset:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 26);
			this.label2.TabIndex = 1;
			this.label2.Text = "SByte:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(3, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 26);
			this.label3.TabIndex = 2;
			this.label3.Text = "Byte:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 91);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 26);
			this.label4.TabIndex = 3;
			this.label4.Text = "Int16:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label5.Location = new System.Drawing.Point(3, 117);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76, 26);
			this.label5.TabIndex = 4;
			this.label5.Text = "UInt16:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label6.Location = new System.Drawing.Point(3, 143);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(76, 26);
			this.label6.TabIndex = 5;
			this.label6.Text = "Int32:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label7.Location = new System.Drawing.Point(3, 169);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(76, 26);
			this.label7.TabIndex = 6;
			this.label7.Text = "UInt32:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label8.Location = new System.Drawing.Point(3, 195);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(76, 26);
			this.label8.TabIndex = 7;
			this.label8.Text = "Int64:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label9.Location = new System.Drawing.Point(3, 221);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(76, 26);
			this.label9.TabIndex = 8;
			this.label9.Text = "UInt64:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label10.Location = new System.Drawing.Point(3, 247);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(76, 26);
			this.label10.TabIndex = 9;
			this.label10.Text = "Single:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label11.Location = new System.Drawing.Point(3, 273);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(76, 26);
			this.label11.TabIndex = 10;
			this.label11.Text = "Double:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// DataOffset
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.DataOffset, 2);
			this.DataOffset.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataOffset.Location = new System.Drawing.Point(85, 16);
			this.DataOffset.Name = "DataOffset";
			this.DataOffset.ReadOnly = true;
			this.DataOffset.Size = new System.Drawing.Size(353, 20);
			this.DataOffset.TabIndex = 11;
			// 
			// DataSByte
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.DataSByte, 2);
			this.DataSByte.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataSByte.Location = new System.Drawing.Point(85, 42);
			this.DataSByte.Name = "DataSByte";
			this.DataSByte.ReadOnly = true;
			this.DataSByte.Size = new System.Drawing.Size(353, 20);
			this.DataSByte.TabIndex = 12;
			// 
			// DataByte
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.DataByte, 2);
			this.DataByte.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataByte.Location = new System.Drawing.Point(85, 68);
			this.DataByte.Name = "DataByte";
			this.DataByte.ReadOnly = true;
			this.DataByte.Size = new System.Drawing.Size(353, 20);
			this.DataByte.TabIndex = 13;
			// 
			// DataInt16
			// 
			this.DataInt16.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataInt16.Location = new System.Drawing.Point(85, 94);
			this.DataInt16.Name = "DataInt16";
			this.DataInt16.ReadOnly = true;
			this.DataInt16.Size = new System.Drawing.Size(173, 20);
			this.DataInt16.TabIndex = 14;
			// 
			// DataUInt16
			// 
			this.DataUInt16.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUInt16.Location = new System.Drawing.Point(85, 120);
			this.DataUInt16.Name = "DataUInt16";
			this.DataUInt16.ReadOnly = true;
			this.DataUInt16.Size = new System.Drawing.Size(173, 20);
			this.DataUInt16.TabIndex = 15;
			// 
			// DataInt32
			// 
			this.DataInt32.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataInt32.Location = new System.Drawing.Point(85, 146);
			this.DataInt32.Name = "DataInt32";
			this.DataInt32.ReadOnly = true;
			this.DataInt32.Size = new System.Drawing.Size(173, 20);
			this.DataInt32.TabIndex = 16;
			// 
			// DataUInt32
			// 
			this.DataUInt32.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUInt32.Location = new System.Drawing.Point(85, 172);
			this.DataUInt32.Name = "DataUInt32";
			this.DataUInt32.ReadOnly = true;
			this.DataUInt32.Size = new System.Drawing.Size(173, 20);
			this.DataUInt32.TabIndex = 17;
			// 
			// DataInt64
			// 
			this.DataInt64.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataInt64.Location = new System.Drawing.Point(85, 198);
			this.DataInt64.Name = "DataInt64";
			this.DataInt64.ReadOnly = true;
			this.DataInt64.Size = new System.Drawing.Size(173, 20);
			this.DataInt64.TabIndex = 18;
			// 
			// DataUInt64
			// 
			this.DataUInt64.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUInt64.Location = new System.Drawing.Point(85, 224);
			this.DataUInt64.Name = "DataUInt64";
			this.DataUInt64.ReadOnly = true;
			this.DataUInt64.Size = new System.Drawing.Size(173, 20);
			this.DataUInt64.TabIndex = 19;
			// 
			// DataSingle
			// 
			this.DataSingle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataSingle.Location = new System.Drawing.Point(85, 250);
			this.DataSingle.Name = "DataSingle";
			this.DataSingle.ReadOnly = true;
			this.DataSingle.Size = new System.Drawing.Size(173, 20);
			this.DataSingle.TabIndex = 20;
			// 
			// DataDouble
			// 
			this.DataDouble.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataDouble.Location = new System.Drawing.Point(85, 276);
			this.DataDouble.Name = "DataDouble";
			this.DataDouble.ReadOnly = true;
			this.DataDouble.Size = new System.Drawing.Size(173, 20);
			this.DataDouble.TabIndex = 21;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label12.Location = new System.Drawing.Point(3, 299);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(76, 26);
			this.label12.TabIndex = 22;
			this.label12.Text = "UTF-8 String:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label13.Location = new System.Drawing.Point(3, 325);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(76, 26);
			this.label13.TabIndex = 23;
			this.label13.Text = "UTF-16 String:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(3, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(76, 13);
			this.label14.TabIndex = 24;
			this.label14.Text = "Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(85, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(173, 13);
			this.label15.TabIndex = 25;
			this.label15.Text = "Little Endian";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(264, 0);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(174, 13);
			this.label16.TabIndex = 26;
			this.label16.Text = "Big Endian";
			this.label16.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// DataInt16BE
			// 
			this.DataInt16BE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataInt16BE.Location = new System.Drawing.Point(264, 94);
			this.DataInt16BE.Name = "DataInt16BE";
			this.DataInt16BE.ReadOnly = true;
			this.DataInt16BE.Size = new System.Drawing.Size(174, 20);
			this.DataInt16BE.TabIndex = 27;
			// 
			// DataUInt16BE
			// 
			this.DataUInt16BE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUInt16BE.Location = new System.Drawing.Point(264, 120);
			this.DataUInt16BE.Name = "DataUInt16BE";
			this.DataUInt16BE.ReadOnly = true;
			this.DataUInt16BE.Size = new System.Drawing.Size(174, 20);
			this.DataUInt16BE.TabIndex = 28;
			// 
			// DataInt32BE
			// 
			this.DataInt32BE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataInt32BE.Location = new System.Drawing.Point(264, 146);
			this.DataInt32BE.Name = "DataInt32BE";
			this.DataInt32BE.ReadOnly = true;
			this.DataInt32BE.Size = new System.Drawing.Size(174, 20);
			this.DataInt32BE.TabIndex = 29;
			// 
			// DataUInt32BE
			// 
			this.DataUInt32BE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUInt32BE.Location = new System.Drawing.Point(264, 172);
			this.DataUInt32BE.Name = "DataUInt32BE";
			this.DataUInt32BE.ReadOnly = true;
			this.DataUInt32BE.Size = new System.Drawing.Size(174, 20);
			this.DataUInt32BE.TabIndex = 30;
			// 
			// DataInt64BE
			// 
			this.DataInt64BE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataInt64BE.Location = new System.Drawing.Point(264, 198);
			this.DataInt64BE.Name = "DataInt64BE";
			this.DataInt64BE.ReadOnly = true;
			this.DataInt64BE.Size = new System.Drawing.Size(174, 20);
			this.DataInt64BE.TabIndex = 31;
			// 
			// DataUInt64BE
			// 
			this.DataUInt64BE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUInt64BE.Location = new System.Drawing.Point(264, 224);
			this.DataUInt64BE.Name = "DataUInt64BE";
			this.DataUInt64BE.ReadOnly = true;
			this.DataUInt64BE.Size = new System.Drawing.Size(174, 20);
			this.DataUInt64BE.TabIndex = 32;
			// 
			// DataSingleBE
			// 
			this.DataSingleBE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataSingleBE.Location = new System.Drawing.Point(264, 250);
			this.DataSingleBE.Name = "DataSingleBE";
			this.DataSingleBE.ReadOnly = true;
			this.DataSingleBE.Size = new System.Drawing.Size(174, 20);
			this.DataSingleBE.TabIndex = 33;
			// 
			// DataDoubleBE
			// 
			this.DataDoubleBE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataDoubleBE.Location = new System.Drawing.Point(264, 276);
			this.DataDoubleBE.Name = "DataDoubleBE";
			this.DataDoubleBE.ReadOnly = true;
			this.DataDoubleBE.Size = new System.Drawing.Size(174, 20);
			this.DataDoubleBE.TabIndex = 34;
			// 
			// DataUTF8
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.DataUTF8, 2);
			this.DataUTF8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUTF8.Location = new System.Drawing.Point(85, 302);
			this.DataUTF8.Name = "DataUTF8";
			this.DataUTF8.ReadOnly = true;
			this.DataUTF8.Size = new System.Drawing.Size(353, 20);
			this.DataUTF8.TabIndex = 35;
			// 
			// DataUTF16
			// 
			this.DataUTF16.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUTF16.Location = new System.Drawing.Point(85, 328);
			this.DataUTF16.Name = "DataUTF16";
			this.DataUTF16.ReadOnly = true;
			this.DataUTF16.Size = new System.Drawing.Size(173, 20);
			this.DataUTF16.TabIndex = 37;
			// 
			// DataUTF16BE
			// 
			this.DataUTF16BE.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataUTF16BE.Location = new System.Drawing.Point(264, 328);
			this.DataUTF16BE.Name = "DataUTF16BE";
			this.DataUTF16BE.ReadOnly = true;
			this.DataUTF16BE.Size = new System.Drawing.Size(174, 20);
			this.DataUTF16BE.TabIndex = 38;
			// 
			// BinaryResourceBrowser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitter);
			this.Name = "BinaryResourceBrowser";
			this.Size = new System.Drawing.Size(1091, 476);
			this.splitter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
			this.splitter.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitter;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox DataDouble;
		private System.Windows.Forms.TextBox DataSingle;
		private System.Windows.Forms.TextBox DataUInt64;
		private System.Windows.Forms.TextBox DataInt64;
		private System.Windows.Forms.TextBox DataUInt32;
		private System.Windows.Forms.TextBox DataInt32;
		private System.Windows.Forms.TextBox DataUInt16;
		private System.Windows.Forms.TextBox DataInt16;
		private System.Windows.Forms.TextBox DataByte;
		private System.Windows.Forms.TextBox DataSByte;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox DataOffset;
		private System.Windows.Forms.TextBox DataUTF16BE;
		private System.Windows.Forms.TextBox DataUTF16;
		private System.Windows.Forms.TextBox DataUTF8;
		private System.Windows.Forms.TextBox DataDoubleBE;
		private System.Windows.Forms.TextBox DataSingleBE;
		private System.Windows.Forms.TextBox DataUInt64BE;
		private System.Windows.Forms.TextBox DataInt64BE;
		private System.Windows.Forms.TextBox DataUInt32BE;
		private System.Windows.Forms.TextBox DataInt32BE;
		private System.Windows.Forms.TextBox DataUInt16BE;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox DataInt16BE;

	}
}
