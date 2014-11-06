<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_ShoNefuPrint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_ShoNefuPrint))
        Me.BtnClear1 = New System.Windows.Forms.Button()
        Me.BtnPrint1 = New System.Windows.Forms.Button()
        Me.BtnPrev1 = New System.Windows.Forms.Button()
        Me.EndBtn1 = New System.Windows.Forms.Button()
        Me.DtgLblPri = New SCMLBLSYSTEM.DtaGriEnterKeyDClass()
        Me.DtgLblPriClm1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgLblPriClm2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgLblPriClm3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgLblPriClm4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgLblPriClm5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgLblPriClm6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DtgLblPriClm7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        CType(Me.DtgLblPri, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnClear1
        '
        Me.BtnClear1.Location = New System.Drawing.Point(666, 21)
        Me.BtnClear1.Name = "BtnClear1"
        Me.BtnClear1.Size = New System.Drawing.Size(106, 34)
        Me.BtnClear1.TabIndex = 2
        Me.BtnClear1.Text = "クリア"
        Me.BtnClear1.UseVisualStyleBackColor = True
        '
        'BtnPrint1
        '
        Me.BtnPrint1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPrint1.Image = CType(resources.GetObject("BtnPrint1.Image"), System.Drawing.Image)
        Me.BtnPrint1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnPrint1.Location = New System.Drawing.Point(827, 61)
        Me.BtnPrint1.Name = "BtnPrint1"
        Me.BtnPrint1.Size = New System.Drawing.Size(97, 65)
        Me.BtnPrint1.TabIndex = 3
        Me.BtnPrint1.Text = "    印刷"
        Me.BtnPrint1.UseVisualStyleBackColor = True
        '
        'BtnPrev1
        '
        Me.BtnPrev1.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnPrev1.Image = CType(resources.GetObject("BtnPrev1.Image"), System.Drawing.Image)
        Me.BtnPrev1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnPrev1.Location = New System.Drawing.Point(827, 150)
        Me.BtnPrev1.Name = "BtnPrev1"
        Me.BtnPrev1.Size = New System.Drawing.Size(97, 65)
        Me.BtnPrev1.TabIndex = 4
        Me.BtnPrev1.Text = "      印刷          プレビュー"
        Me.BtnPrev1.UseVisualStyleBackColor = True
        '
        'EndBtn1
        '
        Me.EndBtn1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.EndBtn1.Image = CType(resources.GetObject("EndBtn1.Image"), System.Drawing.Image)
        Me.EndBtn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EndBtn1.Location = New System.Drawing.Point(827, 443)
        Me.EndBtn1.Name = "EndBtn1"
        Me.EndBtn1.Size = New System.Drawing.Size(97, 50)
        Me.EndBtn1.TabIndex = 5
        Me.EndBtn1.Text = "    戻る"
        Me.EndBtn1.UseVisualStyleBackColor = True
        '
        'DtgLblPri
        '
        Me.DtgLblPri.AllowUserToResizeColumns = False
        Me.DtgLblPri.AllowUserToResizeRows = False
        Me.DtgLblPri.ColumnHeadersHeight = 32
        Me.DtgLblPri.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DtgLblPriClm1, Me.DtgLblPriClm2, Me.DtgLblPriClm3, Me.DtgLblPriClm4, Me.DtgLblPriClm5, Me.DtgLblPriClm6, Me.DtgLblPriClm7})
        Me.DtgLblPri.Location = New System.Drawing.Point(22, 61)
        Me.DtgLblPri.Name = "DtgLblPri"
        Me.DtgLblPri.RowTemplate.Height = 21
        Me.DtgLblPri.Size = New System.Drawing.Size(785, 431)
        Me.DtgLblPri.TabIndex = 1
        '
        'DtgLblPriClm1
        '
        Me.DtgLblPriClm1.Frozen = True
        Me.DtgLblPriClm1.HeaderText = "商品コード"
        Me.DtgLblPriClm1.MaxInputLength = 4
        Me.DtgLblPriClm1.Name = "DtgLblPriClm1"
        Me.DtgLblPriClm1.ReadOnly = True
        Me.DtgLblPriClm1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgLblPriClm1.Width = 90
        '
        'DtgLblPriClm2
        '
        Me.DtgLblPriClm2.HeaderText = "商品名"
        Me.DtgLblPriClm2.Name = "DtgLblPriClm2"
        Me.DtgLblPriClm2.ReadOnly = True
        Me.DtgLblPriClm2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DtgLblPriClm2.Width = 165
        '
        'DtgLblPriClm3
        '
        Me.DtgLblPriClm3.HeaderText = "発注単位"
        Me.DtgLblPriClm3.Name = "DtgLblPriClm3"
        Me.DtgLblPriClm3.ReadOnly = True
        Me.DtgLblPriClm3.Width = 80
        '
        'DtgLblPriClm4
        '
        Me.DtgLblPriClm4.HeaderText = "商品区分"
        Me.DtgLblPriClm4.Name = "DtgLblPriClm4"
        Me.DtgLblPriClm4.ReadOnly = True
        Me.DtgLblPriClm4.Width = 80
        '
        'DtgLblPriClm5
        '
        Me.DtgLblPriClm5.HeaderText = "取引先コード"
        Me.DtgLblPriClm5.Name = "DtgLblPriClm5"
        Me.DtgLblPriClm5.ReadOnly = True
        '
        'DtgLblPriClm6
        '
        Me.DtgLblPriClm6.HeaderText = "値段（税別）"
        Me.DtgLblPriClm6.Name = "DtgLblPriClm6"
        Me.DtgLblPriClm6.ReadOnly = True
        Me.DtgLblPriClm6.Width = 90
        '
        'DtgLblPriClm7
        '
        Me.DtgLblPriClm7.HeaderText = "発行枚数"
        Me.DtgLblPriClm7.MaxInputLength = 3
        Me.DtgLblPriClm7.Name = "DtgLblPriClm7"
        Me.DtgLblPriClm7.Width = 80
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'frm_NefuPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(949, 551)
        Me.Controls.Add(Me.BtnPrint1)
        Me.Controls.Add(Me.BtnPrev1)
        Me.Controls.Add(Me.EndBtn1)
        Me.Controls.Add(Me.BtnClear1)
        Me.Controls.Add(Me.DtgLblPri)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(955, 580)
        Me.MinimumSize = New System.Drawing.Size(955, 580)
        Me.Name = "frm_NefuPrint"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "値札発行枚数入力画面"
        CType(Me.DtgLblPri, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DtgLblPri As SCMLBLSYSTEM.DtaGriEnterKeyDClass
    Friend WithEvents BtnClear1 As System.Windows.Forms.Button
    Friend WithEvents BtnPrint1 As System.Windows.Forms.Button
    Friend WithEvents BtnPrev1 As System.Windows.Forms.Button
    Friend WithEvents EndBtn1 As System.Windows.Forms.Button
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents DtgLblPriClm1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgLblPriClm2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgLblPriClm3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgLblPriClm4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgLblPriClm5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgLblPriClm6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DtgLblPriClm7 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
