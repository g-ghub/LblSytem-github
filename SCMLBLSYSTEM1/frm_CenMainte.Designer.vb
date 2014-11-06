<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_CenMainte
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_CenMainte))
        Me.BtnPanelD1 = New System.Windows.Forms.Button()
        Me.BtnPanelD2 = New System.Windows.Forms.Button()
        Me.BtnPanelD3 = New System.Windows.Forms.Button()
        Me.PnlInput1 = New System.Windows.Forms.Panel()
        Me.DtgInput1 = New SCMLBLSYSTEM.DtaGriEnterKeyRClass()
        Me.DtgInputClm1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgInputClm2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgInputClm3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgInputClm4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnInput1 = New System.Windows.Forms.Button()
        Me.PnlDelete1 = New System.Windows.Forms.Panel()
        Me.BtnDelete1 = New System.Windows.Forms.Button()
        Me.DtgDelete1 = New System.Windows.Forms.DataGridView()
        Me.DtgDeleteClm1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgDeleteClm8 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.BtnBac1 = New System.Windows.Forms.Button()
        Me.PnlUpdate1 = New System.Windows.Forms.Panel()
        Me.DtgUpdate1 = New SCMLBLSYSTEM.DtaGriEnterKeyRClass()
        Me.DtgUpdateClm1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgUpdateClm6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnUpdate1 = New System.Windows.Forms.Button()
        Me.LblTop2 = New System.Windows.Forms.Label()
        Me.CmbTok1 = New System.Windows.Forms.ComboBox()
        Me.PnlInput1.SuspendLayout()
        CType(Me.DtgInput1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlDelete1.SuspendLayout()
        CType(Me.DtgDelete1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlUpdate1.SuspendLayout()
        CType(Me.DtgUpdate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnPanelD1
        '
        Me.BtnPanelD1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPanelD1.Location = New System.Drawing.Point(15, 109)
        Me.BtnPanelD1.Name = "BtnPanelD1"
        Me.BtnPanelD1.Size = New System.Drawing.Size(97, 65)
        Me.BtnPanelD1.TabIndex = 4
        Me.BtnPanelD1.Text = "センターの登録"
        Me.BtnPanelD1.UseVisualStyleBackColor = True
        '
        'BtnPanelD2
        '
        Me.BtnPanelD2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPanelD2.Location = New System.Drawing.Point(15, 180)
        Me.BtnPanelD2.Name = "BtnPanelD2"
        Me.BtnPanelD2.Size = New System.Drawing.Size(97, 65)
        Me.BtnPanelD2.TabIndex = 5
        Me.BtnPanelD2.Text = "センターの変更"
        Me.BtnPanelD2.UseVisualStyleBackColor = True
        '
        'BtnPanelD3
        '
        Me.BtnPanelD3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPanelD3.Location = New System.Drawing.Point(15, 251)
        Me.BtnPanelD3.Name = "BtnPanelD3"
        Me.BtnPanelD3.Size = New System.Drawing.Size(97, 65)
        Me.BtnPanelD3.TabIndex = 6
        Me.BtnPanelD3.Text = "センターの削除"
        Me.BtnPanelD3.UseVisualStyleBackColor = True
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
        'DtgInput1
        '
        Me.DtgInput1.AllowUserToResizeColumns = False
        Me.DtgInput1.AllowUserToResizeRows = False
        Me.DtgInput1.ColumnHeadersHeight = 30
        Me.DtgInput1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DtgInputClm1, Me.DtgInputClm2, Me.DtgInputClm3, Me.DtgInputClm4})
        Me.DtgInput1.Location = New System.Drawing.Point(3, 3)
        Me.DtgInput1.Name = "DtgInput1"
        Me.DtgInput1.RowTemplate.Height = 21
        Me.DtgInput1.Size = New System.Drawing.Size(581, 413)
        Me.DtgInput1.TabIndex = 1
        '
        'DtgInputClm1
        '
        Me.DtgInputClm1.HeaderText = "センター名"
        Me.DtgInputClm1.MaxInputLength = 10
        Me.DtgInputClm1.Name = "DtgInputClm1"
        Me.DtgInputClm1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgInputClm1.Width = 130
        '
        'DtgInputClm2
        '
        Me.DtgInputClm2.HeaderText = "備考１"
        Me.DtgInputClm2.MaxInputLength = 25
        Me.DtgInputClm2.Name = "DtgInputClm2"
        Me.DtgInputClm2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgInputClm3
        '
        Me.DtgInputClm3.HeaderText = "備考２"
        Me.DtgInputClm3.MaxInputLength = 25
        Me.DtgInputClm3.Name = "DtgInputClm3"
        Me.DtgInputClm3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgInputClm4
        '
        Me.DtgInputClm4.HeaderText = "備考３"
        Me.DtgInputClm4.MaxInputLength = 25
        Me.DtgInputClm4.Name = "DtgInputClm4"
        Me.DtgInputClm4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
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
        Me.BtnDelete1.Text = "      更新する"
        Me.BtnDelete1.UseVisualStyleBackColor = True
        '
        'DtgDelete1
        '
        Me.DtgDelete1.AllowUserToDeleteRows = False
        Me.DtgDelete1.AllowUserToResizeColumns = False
        Me.DtgDelete1.AllowUserToResizeRows = False
        Me.DtgDelete1.ColumnHeadersHeight = 30
        Me.DtgDelete1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DtgDeleteClm1, Me.DtgDeleteClm2, Me.DtgDeleteClm3, Me.DtgDeleteClm4, Me.DtgDeleteClm5, Me.DtgDeleteClm6, Me.DtgDeleteClm7, Me.DtgDeleteClm8})
        Me.DtgDelete1.Location = New System.Drawing.Point(3, 3)
        Me.DtgDelete1.Name = "DtgDelete1"
        Me.DtgDelete1.RowTemplate.Height = 21
        Me.DtgDelete1.Size = New System.Drawing.Size(581, 413)
        Me.DtgDelete1.TabIndex = 1
        '
        'DtgDeleteClm1
        '
        Me.DtgDeleteClm1.HeaderText = "センターID"
        Me.DtgDeleteClm1.Name = "DtgDeleteClm1"
        Me.DtgDeleteClm1.ReadOnly = True
        Me.DtgDeleteClm1.Visible = False
        '
        'DtgDeleteClm2
        '
        Me.DtgDeleteClm2.HeaderText = "センター名"
        Me.DtgDeleteClm2.MaxInputLength = 10
        Me.DtgDeleteClm2.Name = "DtgDeleteClm2"
        Me.DtgDeleteClm2.ReadOnly = True
        Me.DtgDeleteClm2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgDeleteClm2.Width = 130
        '
        'DtgDeleteClm3
        '
        Me.DtgDeleteClm3.HeaderText = "法人ID"
        Me.DtgDeleteClm3.Name = "DtgDeleteClm3"
        Me.DtgDeleteClm3.Visible = False
        '
        'DtgDeleteClm4
        '
        Me.DtgDeleteClm4.HeaderText = "削除フラグ"
        Me.DtgDeleteClm4.Name = "DtgDeleteClm4"
        Me.DtgDeleteClm4.Visible = False
        '
        'DtgDeleteClm5
        '
        Me.DtgDeleteClm5.HeaderText = "備考１"
        Me.DtgDeleteClm5.Name = "DtgDeleteClm5"
        Me.DtgDeleteClm5.ReadOnly = True
        Me.DtgDeleteClm5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgDeleteClm6
        '
        Me.DtgDeleteClm6.HeaderText = "備考２"
        Me.DtgDeleteClm6.Name = "DtgDeleteClm6"
        Me.DtgDeleteClm6.ReadOnly = True
        Me.DtgDeleteClm6.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgDeleteClm7
        '
        Me.DtgDeleteClm7.HeaderText = "備考３"
        Me.DtgDeleteClm7.Name = "DtgDeleteClm7"
        Me.DtgDeleteClm7.ReadOnly = True
        Me.DtgDeleteClm7.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgDeleteClm8
        '
        Me.DtgDeleteClm8.HeaderText = "削除"
        Me.DtgDeleteClm8.Name = "DtgDeleteClm8"
        Me.DtgDeleteClm8.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgDeleteClm8.Width = 60
        '
        'BtnBac1
        '
        Me.BtnBac1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBac1.Image = CType(resources.GetObject("BtnBac1.Image"), System.Drawing.Image)
        Me.BtnBac1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBac1.Location = New System.Drawing.Point(15, 358)
        Me.BtnBac1.Name = "BtnBac1"
        Me.BtnBac1.Size = New System.Drawing.Size(97, 48)
        Me.BtnBac1.TabIndex = 7
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
        'DtgUpdate1
        '
        Me.DtgUpdate1.AllowUserToDeleteRows = False
        Me.DtgUpdate1.AllowUserToResizeColumns = False
        Me.DtgUpdate1.AllowUserToResizeRows = False
        Me.DtgUpdate1.ColumnHeadersHeight = 30
        Me.DtgUpdate1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DtgUpdateClm1, Me.DtgUpdateClm2, Me.DtgUpdateClm3, Me.DtgUpdateClm4, Me.DtgUpdateClm5, Me.DtgUpdateClm6})
        Me.DtgUpdate1.Location = New System.Drawing.Point(3, 3)
        Me.DtgUpdate1.Name = "DtgUpdate1"
        Me.DtgUpdate1.RowTemplate.Height = 21
        Me.DtgUpdate1.Size = New System.Drawing.Size(581, 413)
        Me.DtgUpdate1.TabIndex = 1
        '
        'DtgUpdateClm1
        '
        Me.DtgUpdateClm1.HeaderText = "センターＩＤ"
        Me.DtgUpdateClm1.Name = "DtgUpdateClm1"
        Me.DtgUpdateClm1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
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
        Me.DtgUpdateClm3.HeaderText = "法人ＩＤ"
        Me.DtgUpdateClm3.Name = "DtgUpdateClm3"
        Me.DtgUpdateClm3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgUpdateClm3.Visible = False
        '
        'DtgUpdateClm4
        '
        Me.DtgUpdateClm4.HeaderText = "備考１"
        Me.DtgUpdateClm4.MaxInputLength = 25
        Me.DtgUpdateClm4.Name = "DtgUpdateClm4"
        Me.DtgUpdateClm4.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgUpdateClm5
        '
        Me.DtgUpdateClm5.HeaderText = "備考２"
        Me.DtgUpdateClm5.MaxInputLength = 25
        Me.DtgUpdateClm5.Name = "DtgUpdateClm5"
        Me.DtgUpdateClm5.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        '
        'DtgUpdateClm6
        '
        Me.DtgUpdateClm6.HeaderText = "備考３"
        Me.DtgUpdateClm6.MaxInputLength = 25
        Me.DtgUpdateClm6.Name = "DtgUpdateClm6"
        Me.DtgUpdateClm6.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
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
        'LblTop2
        '
        Me.LblTop2.AutoSize = True
        Me.LblTop2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTop2.Location = New System.Drawing.Point(21, 29)
        Me.LblTop2.Name = "LblTop2"
        Me.LblTop2.Size = New System.Drawing.Size(56, 16)
        Me.LblTop2.TabIndex = 24
        Me.LblTop2.Text = "得意先"
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
        'frm_CenMainte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(771, 562)
        Me.Controls.Add(Me.CmbTok1)
        Me.Controls.Add(Me.BtnBac1)
        Me.Controls.Add(Me.LblTop2)
        Me.Controls.Add(Me.BtnPanelD3)
        Me.Controls.Add(Me.BtnPanelD1)
        Me.Controls.Add(Me.BtnPanelD2)
        Me.Controls.Add(Me.PnlDelete1)
        Me.Controls.Add(Me.PnlInput1)
        Me.Controls.Add(Me.PnlUpdate1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(787, 600)
        Me.MinimumSize = New System.Drawing.Size(787, 600)
        Me.Name = "frm_CenMainte"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "物流センター管理画面"
        Me.PnlInput1.ResumeLayout(False)
        CType(Me.DtgInput1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlDelete1.ResumeLayout(False)
        CType(Me.DtgDelete1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlUpdate1.ResumeLayout(False)
        CType(Me.DtgUpdate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnPanelD1 As System.Windows.Forms.Button
    Friend WithEvents BtnPanelD2 As System.Windows.Forms.Button
    Friend WithEvents BtnPanelD3 As System.Windows.Forms.Button
    Friend WithEvents PnlInput1 As System.Windows.Forms.Panel
    Friend WithEvents BtnInput1 As System.Windows.Forms.Button
    Friend WithEvents PnlDelete1 As System.Windows.Forms.Panel
    Friend WithEvents BtnBac1 As System.Windows.Forms.Button
    Friend WithEvents BtnDelete1 As System.Windows.Forms.Button
    Friend WithEvents DtgDelete1 As System.Windows.Forms.DataGridView
    Friend WithEvents PnlUpdate1 As System.Windows.Forms.Panel
    Friend WithEvents BtnUpdate1 As System.Windows.Forms.Button
    Friend WithEvents LblTop2 As System.Windows.Forms.Label
    Friend WithEvents CmbTok1 As System.Windows.Forms.ComboBox
    Friend WithEvents DtgDeleteClm1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgDeleteClm8 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DtgInput1 As SCMLBLSYSTEM.DtaGriEnterKeyRClass
    Friend WithEvents DtgUpdate1 As SCMLBLSYSTEM.DtaGriEnterKeyRClass
    Friend WithEvents DtgInputClm1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgInputClm2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgInputClm3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgInputClm4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgUpdateClm6 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
