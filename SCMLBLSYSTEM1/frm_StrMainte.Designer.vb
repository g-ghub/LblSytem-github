<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_StrMainte
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_StrMainte))
        Me.BtnPanelD1 = New System.Windows.Forms.Button()
        Me.BtnPanelD3 = New System.Windows.Forms.Button()
        Me.BtnPanelD2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PnlInput1 = New System.Windows.Forms.Panel()
        Me.BtnInput1 = New System.Windows.Forms.Button()
        Me.BtnBac1 = New System.Windows.Forms.Button()
        Me.PnlUpdate1 = New System.Windows.Forms.Panel()
        Me.BtnUpdate1 = New System.Windows.Forms.Button()
        Me.PnlDelete1 = New System.Windows.Forms.Panel()
        Me.BtnDelete1 = New System.Windows.Forms.Button()
        Me.DtgDelete1 = New System.Windows.Forms.DataGridView()
        Me.DtgDeleteClm1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm7 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CmbTok1 = New System.Windows.Forms.ComboBox()
        Me.CmbCen1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtgUpdate1 = New SCMLBLSYSTEM.DtaGriEnterKeyRClass()
        Me.DtgUpdateClm1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DtgUpdateClm3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgInput1 = New SCMLBLSYSTEM.DtaGriEnterKeyRClass()
        Me.DtgInputClm1 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DtgInputClm2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgInputClm3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgInputClm4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgInputClm5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnlInput1.SuspendLayout()
        Me.PnlUpdate1.SuspendLayout()
        Me.PnlDelete1.SuspendLayout()
        CType(Me.DtgDelete1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtgUpdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtgInput1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnPanelD1
        '
        Me.BtnPanelD1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPanelD1.Location = New System.Drawing.Point(16, 145)
        Me.BtnPanelD1.Name = "BtnPanelD1"
        Me.BtnPanelD1.Size = New System.Drawing.Size(97, 65)
        Me.BtnPanelD1.TabIndex = 5
        Me.BtnPanelD1.Text = "店舗登録"
        Me.BtnPanelD1.UseVisualStyleBackColor = True
        '
        'BtnPanelD3
        '
        Me.BtnPanelD3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPanelD3.Location = New System.Drawing.Point(16, 287)
        Me.BtnPanelD3.Name = "BtnPanelD3"
        Me.BtnPanelD3.Size = New System.Drawing.Size(97, 65)
        Me.BtnPanelD3.TabIndex = 7
        Me.BtnPanelD3.Text = "店舗削除"
        Me.BtnPanelD3.UseVisualStyleBackColor = True
        '
        'BtnPanelD2
        '
        Me.BtnPanelD2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPanelD2.Location = New System.Drawing.Point(16, 216)
        Me.BtnPanelD2.Name = "BtnPanelD2"
        Me.BtnPanelD2.Size = New System.Drawing.Size(97, 65)
        Me.BtnPanelD2.TabIndex = 6
        Me.BtnPanelD2.Text = "店舗変更"
        Me.BtnPanelD2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "得意先"
        '
        'PnlInput1
        '
        Me.PnlInput1.Controls.Add(Me.DtgInput1)
        Me.PnlInput1.Controls.Add(Me.BtnInput1)
        Me.PnlInput1.Location = New System.Drawing.Point(169, 26)
        Me.PnlInput1.Name = "PnlInput1"
        Me.PnlInput1.Size = New System.Drawing.Size(584, 502)
        Me.PnlInput1.TabIndex = 1
        '
        'BtnInput1
        '
        Me.BtnInput1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnInput1.Image = CType(resources.GetObject("BtnInput1.Image"), System.Drawing.Image)
        Me.BtnInput1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnInput1.Location = New System.Drawing.Point(238, 439)
        Me.BtnInput1.Name = "BtnInput1"
        Me.BtnInput1.Size = New System.Drawing.Size(120, 60)
        Me.BtnInput1.TabIndex = 2
        Me.BtnInput1.Text = "      登録する"
        Me.BtnInput1.UseVisualStyleBackColor = True
        '
        'BtnBac1
        '
        Me.BtnBac1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBac1.Image = CType(resources.GetObject("BtnBac1.Image"), System.Drawing.Image)
        Me.BtnBac1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBac1.Location = New System.Drawing.Point(16, 394)
        Me.BtnBac1.Name = "BtnBac1"
        Me.BtnBac1.Size = New System.Drawing.Size(97, 48)
        Me.BtnBac1.TabIndex = 8
        Me.BtnBac1.Text = "    戻る"
        Me.BtnBac1.UseVisualStyleBackColor = True
        '
        'PnlUpdate1
        '
        Me.PnlUpdate1.Controls.Add(Me.DtgUpdate1)
        Me.PnlUpdate1.Controls.Add(Me.BtnUpdate1)
        Me.PnlUpdate1.Location = New System.Drawing.Point(169, 26)
        Me.PnlUpdate1.Name = "PnlUpdate1"
        Me.PnlUpdate1.Size = New System.Drawing.Size(584, 502)
        Me.PnlUpdate1.TabIndex = 1
        '
        'BtnUpdate1
        '
        Me.BtnUpdate1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnUpdate1.Image = CType(resources.GetObject("BtnUpdate1.Image"), System.Drawing.Image)
        Me.BtnUpdate1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnUpdate1.Location = New System.Drawing.Point(238, 439)
        Me.BtnUpdate1.Name = "BtnUpdate1"
        Me.BtnUpdate1.Size = New System.Drawing.Size(120, 60)
        Me.BtnUpdate1.TabIndex = 2
        Me.BtnUpdate1.Text = "      変更する"
        Me.BtnUpdate1.UseVisualStyleBackColor = True
        '
        'PnlDelete1
        '
        Me.PnlDelete1.Controls.Add(Me.BtnDelete1)
        Me.PnlDelete1.Controls.Add(Me.DtgDelete1)
        Me.PnlDelete1.Location = New System.Drawing.Point(169, 26)
        Me.PnlDelete1.Name = "PnlDelete1"
        Me.PnlDelete1.Size = New System.Drawing.Size(584, 502)
        Me.PnlDelete1.TabIndex = 1
        '
        'BtnDelete1
        '
        Me.BtnDelete1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnDelete1.Image = CType(resources.GetObject("BtnDelete1.Image"), System.Drawing.Image)
        Me.BtnDelete1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDelete1.Location = New System.Drawing.Point(238, 439)
        Me.BtnDelete1.Name = "BtnDelete1"
        Me.BtnDelete1.Size = New System.Drawing.Size(120, 60)
        Me.BtnDelete1.TabIndex = 2
        Me.BtnDelete1.Text = "      削除する"
        Me.BtnDelete1.UseVisualStyleBackColor = True
        '
        'DtgDelete1
        '
        Me.DtgDelete1.AllowUserToDeleteRows = False
        Me.DtgDelete1.AllowUserToResizeColumns = False
        Me.DtgDelete1.AllowUserToResizeRows = False
        Me.DtgDelete1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgDelete1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DtgDeleteClm1, Me.DtgDeleteClm2, Me.DtgDeleteClm3, Me.DtgDeleteClm4, Me.DtgDeleteClm5, Me.DtgDeleteClm6, Me.DtgDeleteClm7})
        Me.DtgDelete1.Location = New System.Drawing.Point(3, 3)
        Me.DtgDelete1.Name = "DtgDelete1"
        Me.DtgDelete1.RowTemplate.Height = 21
        Me.DtgDelete1.Size = New System.Drawing.Size(581, 413)
        Me.DtgDelete1.TabIndex = 1
        '
        'DtgDeleteClm1
        '
        Me.DtgDeleteClm1.HeaderText = "NO"
        Me.DtgDeleteClm1.Name = "DtgDeleteClm1"
        Me.DtgDeleteClm1.ReadOnly = True
        Me.DtgDeleteClm1.Visible = False
        Me.DtgDeleteClm1.Width = 130
        '
        'DtgDeleteClm2
        '
        Me.DtgDeleteClm2.HeaderText = "センター名      （業態名）"
        Me.DtgDeleteClm2.Name = "DtgDeleteClm2"
        Me.DtgDeleteClm2.ReadOnly = True
        Me.DtgDeleteClm2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgDeleteClm3
        '
        Me.DtgDeleteClm3.HeaderText = "横持ちセンター"
        Me.DtgDeleteClm3.MaxInputLength = 4
        Me.DtgDeleteClm3.Name = "DtgDeleteClm3"
        Me.DtgDeleteClm3.ReadOnly = True
        Me.DtgDeleteClm3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgDeleteClm4
        '
        Me.DtgDeleteClm4.HeaderText = "県名　　　（地区名）"
        Me.DtgDeleteClm4.MaxInputLength = 4
        Me.DtgDeleteClm4.Name = "DtgDeleteClm4"
        Me.DtgDeleteClm4.ReadOnly = True
        Me.DtgDeleteClm4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgDeleteClm4.Width = 80
        '
        'DtgDeleteClm5
        '
        Me.DtgDeleteClm5.HeaderText = "店番"
        Me.DtgDeleteClm5.MaxInputLength = 4
        Me.DtgDeleteClm5.Name = "DtgDeleteClm5"
        Me.DtgDeleteClm5.ReadOnly = True
        Me.DtgDeleteClm5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgDeleteClm5.Width = 60
        '
        'DtgDeleteClm6
        '
        Me.DtgDeleteClm6.HeaderText = "店舗名"
        Me.DtgDeleteClm6.MaxInputLength = 8
        Me.DtgDeleteClm6.Name = "DtgDeleteClm6"
        Me.DtgDeleteClm6.ReadOnly = True
        Me.DtgDeleteClm6.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgDeleteClm7
        '
        Me.DtgDeleteClm7.HeaderText = "削除"
        Me.DtgDeleteClm7.Name = "DtgDeleteClm7"
        Me.DtgDeleteClm7.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgDeleteClm7.TrueValue = "true"
        Me.DtgDeleteClm7.Width = 60
        '
        'CmbTok1
        '
        Me.CmbTok1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbTok1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbTok1.FormattingEnabled = True
        Me.CmbTok1.Location = New System.Drawing.Point(15, 48)
        Me.CmbTok1.Name = "CmbTok1"
        Me.CmbTok1.Size = New System.Drawing.Size(146, 24)
        Me.CmbTok1.TabIndex = 3
        '
        'CmbCen1
        '
        Me.CmbCen1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbCen1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbCen1.FormattingEnabled = True
        Me.CmbCen1.Location = New System.Drawing.Point(16, 98)
        Me.CmbCen1.Name = "CmbCen1"
        Me.CmbCen1.Size = New System.Drawing.Size(145, 24)
        Me.CmbCen1.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 16)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "センター名"
        '
        'DtgUpdate1
        '
        Me.DtgUpdate1.AllowUserToDeleteRows = False
        Me.DtgUpdate1.AllowUserToResizeColumns = False
        Me.DtgUpdate1.AllowUserToResizeRows = False
        Me.DtgUpdate1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgUpdate1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DtgUpdateClm1, Me.DtgUpdateClm2, Me.DtgUpdateClm3, Me.DtgUpdateClm4, Me.DtgUpdateClm5, Me.DtgUpdateClm6})
        Me.DtgUpdate1.Location = New System.Drawing.Point(3, 3)
        Me.DtgUpdate1.Name = "DtgUpdate1"
        Me.DtgUpdate1.RowTemplate.Height = 21
        Me.DtgUpdate1.Size = New System.Drawing.Size(581, 413)
        Me.DtgUpdate1.TabIndex = 1
        '
        'DtgUpdateClm1
        '
        Me.DtgUpdateClm1.HeaderText = "No"
        Me.DtgUpdateClm1.Name = "DtgUpdateClm1"
        Me.DtgUpdateClm1.Visible = False
        '
        'DtgUpdateClm2
        '
        Me.DtgUpdateClm2.HeaderText = "センター名"
        Me.DtgUpdateClm2.Name = "DtgUpdateClm2"
        Me.DtgUpdateClm2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgUpdateClm2.Width = 130
        '
        'DtgUpdateClm3
        '
        Me.DtgUpdateClm3.HeaderText = "横持ちセンター"
        Me.DtgUpdateClm3.MaxInputLength = 4
        Me.DtgUpdateClm3.Name = "DtgUpdateClm3"
        Me.DtgUpdateClm3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgUpdateClm4
        '
        Me.DtgUpdateClm4.HeaderText = "県名　　　（地区名）"
        Me.DtgUpdateClm4.MaxInputLength = 4
        Me.DtgUpdateClm4.Name = "DtgUpdateClm4"
        Me.DtgUpdateClm4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgUpdateClm4.Width = 80
        '
        'DtgUpdateClm5
        '
        Me.DtgUpdateClm5.HeaderText = "店番"
        Me.DtgUpdateClm5.MaxInputLength = 4
        Me.DtgUpdateClm5.Name = "DtgUpdateClm5"
        Me.DtgUpdateClm5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgUpdateClm5.Width = 60
        '
        'DtgUpdateClm6
        '
        Me.DtgUpdateClm6.HeaderText = "店舗名"
        Me.DtgUpdateClm6.MaxInputLength = 15
        Me.DtgUpdateClm6.Name = "DtgUpdateClm6"
        Me.DtgUpdateClm6.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgInput1
        '
        Me.DtgInput1.AllowDrop = True
        Me.DtgInput1.AllowUserToResizeColumns = False
        Me.DtgInput1.AllowUserToResizeRows = False
        Me.DtgInput1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DtgInput1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DtgInputClm1, Me.DtgInputClm2, Me.DtgInputClm3, Me.DtgInputClm4, Me.DtgInputClm5})
        Me.DtgInput1.Location = New System.Drawing.Point(3, 3)
        Me.DtgInput1.Name = "DtgInput1"
        Me.DtgInput1.RowTemplate.Height = 21
        Me.DtgInput1.Size = New System.Drawing.Size(581, 413)
        Me.DtgInput1.TabIndex = 1
        '
        'DtgInputClm1
        '
        Me.DtgInputClm1.HeaderText = "センター名"
        Me.DtgInputClm1.MaxDropDownItems = 15
        Me.DtgInputClm1.Name = "DtgInputClm1"
        Me.DtgInputClm1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgInputClm1.Width = 130
        '
        'DtgInputClm2
        '
        Me.DtgInputClm2.HeaderText = "横持ちセンター"
        Me.DtgInputClm2.MaxInputLength = 4
        Me.DtgInputClm2.Name = "DtgInputClm2"
        Me.DtgInputClm2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgInputClm3
        '
        Me.DtgInputClm3.HeaderText = "県名　　　（地区名）"
        Me.DtgInputClm3.MaxInputLength = 4
        Me.DtgInputClm3.Name = "DtgInputClm3"
        Me.DtgInputClm3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgInputClm3.Width = 80
        '
        'DtgInputClm4
        '
        Me.DtgInputClm4.HeaderText = "店番"
        Me.DtgInputClm4.MaxInputLength = 4
        Me.DtgInputClm4.Name = "DtgInputClm4"
        Me.DtgInputClm4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgInputClm4.Width = 60
        '
        'DtgInputClm5
        '
        Me.DtgInputClm5.HeaderText = "店舗名"
        Me.DtgInputClm5.MaxInputLength = 15
        Me.DtgInputClm5.Name = "DtgInputClm5"
        Me.DtgInputClm5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'frm_StrMainte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(771, 562)
        Me.Controls.Add(Me.CmbCen1)
        Me.Controls.Add(Me.CmbTok1)
        Me.Controls.Add(Me.BtnBac1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnPanelD1)
        Me.Controls.Add(Me.BtnPanelD3)
        Me.Controls.Add(Me.BtnPanelD2)
        Me.Controls.Add(Me.PnlDelete1)
        Me.Controls.Add(Me.PnlInput1)
        Me.Controls.Add(Me.PnlUpdate1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(787, 600)
        Me.MinimumSize = New System.Drawing.Size(787, 600)
        Me.Name = "frm_StrMainte"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "店舗管理画面"
        Me.PnlInput1.ResumeLayout(False)
        Me.PnlUpdate1.ResumeLayout(False)
        Me.PnlDelete1.ResumeLayout(False)
        CType(Me.DtgDelete1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtgUpdate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtgInput1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnPanelD1 As System.Windows.Forms.Button
    Friend WithEvents BtnPanelD3 As System.Windows.Forms.Button
    Friend WithEvents BtnPanelD2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PnlInput1 As System.Windows.Forms.Panel
    Friend WithEvents BtnBac1 As System.Windows.Forms.Button
    Friend WithEvents BtnInput1 As System.Windows.Forms.Button
    Friend WithEvents PnlUpdate1 As System.Windows.Forms.Panel
    Friend WithEvents BtnUpdate1 As System.Windows.Forms.Button
    Friend WithEvents PnlDelete1 As System.Windows.Forms.Panel
    Friend WithEvents BtnDelete1 As System.Windows.Forms.Button
    Friend WithEvents DtgDelete1 As System.Windows.Forms.DataGridView
    Friend WithEvents CmbTok1 As System.Windows.Forms.ComboBox
    Friend WithEvents CmbCen1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DtgInput1 As SCMLBLSYSTEM.DtaGriEnterKeyRClass
    Friend WithEvents DtgUpdate1 As SCMLBLSYSTEM.DtaGriEnterKeyRClass
    Friend WithEvents DtgDeleteClm1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm7 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DtgUpdateClm1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm2 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DtgUpdateClm3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgInputClm1 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DtgInputClm2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgInputClm3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgInputClm4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgInputClm5 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
