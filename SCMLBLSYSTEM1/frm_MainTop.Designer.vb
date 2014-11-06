<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MainTop
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MainTop))
        Me.BtnLblProcess1 = New System.Windows.Forms.Button()
        Me.BtnMasProcess1 = New System.Windows.Forms.Button()
        Me.MainTopPnl1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnClose1 = New System.Windows.Forms.Button()
        Me.MainTopPnl2 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.BtnRemarksMas1 = New System.Windows.Forms.Button()
        Me.BtnBac1 = New System.Windows.Forms.Button()
        Me.BtnCenMas1 = New System.Windows.Forms.Button()
        Me.BtnStrMas1 = New System.Windows.Forms.Button()
        Me.MainTopPnl1.SuspendLayout()
        Me.MainTopPnl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnLblProcess1
        '
        Me.BtnLblProcess1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnLblProcess1.ForeColor = System.Drawing.Color.Purple
        Me.BtnLblProcess1.Location = New System.Drawing.Point(38, 30)
        Me.BtnLblProcess1.Name = "BtnLblProcess1"
        Me.BtnLblProcess1.Size = New System.Drawing.Size(200, 179)
        Me.BtnLblProcess1.TabIndex = 1
        Me.BtnLblProcess1.Text = "ラベル作成"
        Me.BtnLblProcess1.UseVisualStyleBackColor = True
        '
        'BtnMasProcess1
        '
        Me.BtnMasProcess1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnMasProcess1.ForeColor = System.Drawing.SystemColors.Desktop
        Me.BtnMasProcess1.Location = New System.Drawing.Point(300, 30)
        Me.BtnMasProcess1.Name = "BtnMasProcess1"
        Me.BtnMasProcess1.Size = New System.Drawing.Size(200, 179)
        Me.BtnMasProcess1.TabIndex = 3
        Me.BtnMasProcess1.Text = "出荷先情報の管理"
        Me.BtnMasProcess1.UseVisualStyleBackColor = True
        '
        'MainTopPnl1
        '
        Me.MainTopPnl1.Controls.Add(Me.Button1)
        Me.MainTopPnl1.Controls.Add(Me.BtnClose1)
        Me.MainTopPnl1.Controls.Add(Me.BtnMasProcess1)
        Me.MainTopPnl1.Controls.Add(Me.BtnLblProcess1)
        Me.MainTopPnl1.Location = New System.Drawing.Point(41, 55)
        Me.MainTopPnl1.Name = "MainTopPnl1"
        Me.MainTopPnl1.Size = New System.Drawing.Size(522, 352)
        Me.MainTopPnl1.TabIndex = 14
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Salmon
        Me.Button1.Location = New System.Drawing.Point(23, 278)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(107, 57)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "値札発行"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'BtnClose1
        '
        Me.BtnClose1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnClose1.Image = CType(resources.GetObject("BtnClose1.Image"), System.Drawing.Image)
        Me.BtnClose1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnClose1.Location = New System.Drawing.Point(195, 278)
        Me.BtnClose1.Name = "BtnClose1"
        Me.BtnClose1.Size = New System.Drawing.Size(121, 58)
        Me.BtnClose1.TabIndex = 5
        Me.BtnClose1.Text = "    終 了"
        Me.BtnClose1.UseVisualStyleBackColor = True
        '
        'MainTopPnl2
        '
        Me.MainTopPnl2.Controls.Add(Me.Button2)
        Me.MainTopPnl2.Controls.Add(Me.BtnRemarksMas1)
        Me.MainTopPnl2.Controls.Add(Me.BtnBac1)
        Me.MainTopPnl2.Controls.Add(Me.BtnCenMas1)
        Me.MainTopPnl2.Controls.Add(Me.BtnStrMas1)
        Me.MainTopPnl2.Location = New System.Drawing.Point(41, 55)
        Me.MainTopPnl2.Name = "MainTopPnl2"
        Me.MainTopPnl2.Size = New System.Drawing.Size(522, 352)
        Me.MainTopPnl2.TabIndex = 15
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Salmon
        Me.Button2.Location = New System.Drawing.Point(276, 141)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(215, 72)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "値札商品管理"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'BtnRemarksMas1
        '
        Me.BtnRemarksMas1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnRemarksMas1.ForeColor = System.Drawing.Color.Purple
        Me.BtnRemarksMas1.Location = New System.Drawing.Point(23, 141)
        Me.BtnRemarksMas1.Name = "BtnRemarksMas1"
        Me.BtnRemarksMas1.Size = New System.Drawing.Size(215, 72)
        Me.BtnRemarksMas1.TabIndex = 3
        Me.BtnRemarksMas1.Text = "部門とフロアの管理"
        Me.BtnRemarksMas1.UseVisualStyleBackColor = True
        '
        'BtnBac1
        '
        Me.BtnBac1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnBac1.Image = CType(resources.GetObject("BtnBac1.Image"), System.Drawing.Image)
        Me.BtnBac1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnBac1.Location = New System.Drawing.Point(195, 278)
        Me.BtnBac1.Name = "BtnBac1"
        Me.BtnBac1.Size = New System.Drawing.Size(121, 58)
        Me.BtnBac1.TabIndex = 5
        Me.BtnBac1.Text = "    戻る"
        Me.BtnBac1.UseVisualStyleBackColor = True
        '
        'BtnCenMas1
        '
        Me.BtnCenMas1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnCenMas1.ForeColor = System.Drawing.Color.Purple
        Me.BtnCenMas1.Location = New System.Drawing.Point(23, 30)
        Me.BtnCenMas1.Name = "BtnCenMas1"
        Me.BtnCenMas1.Size = New System.Drawing.Size(215, 72)
        Me.BtnCenMas1.TabIndex = 1
        Me.BtnCenMas1.Text = "物流センターの管理"
        Me.BtnCenMas1.UseVisualStyleBackColor = True
        '
        'BtnStrMas1
        '
        Me.BtnStrMas1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.BtnStrMas1.ForeColor = System.Drawing.Color.Purple
        Me.BtnStrMas1.Location = New System.Drawing.Point(276, 30)
        Me.BtnStrMas1.Name = "BtnStrMas1"
        Me.BtnStrMas1.Size = New System.Drawing.Size(215, 72)
        Me.BtnStrMas1.TabIndex = 2
        Me.BtnStrMas1.Text = "店舗管理"
        Me.BtnStrMas1.UseVisualStyleBackColor = True
        '
        'frm_MainTop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(608, 435)
        Me.Controls.Add(Me.MainTopPnl1)
        Me.Controls.Add(Me.MainTopPnl2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frm_MainTop"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ラベル発行システム-メイン画面"
        Me.MainTopPnl1.ResumeLayout(False)
        Me.MainTopPnl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnLblProcess1 As System.Windows.Forms.Button
    Friend WithEvents BtnMasProcess1 As System.Windows.Forms.Button
    Friend WithEvents MainTopPnl1 As System.Windows.Forms.Panel
    Friend WithEvents MainTopPnl2 As System.Windows.Forms.Panel
    Friend WithEvents BtnCenMas1 As System.Windows.Forms.Button
    Friend WithEvents BtnStrMas1 As System.Windows.Forms.Button
    Friend WithEvents BtnBac1 As System.Windows.Forms.Button
    Friend WithEvents BtnClose1 As System.Windows.Forms.Button
    Friend WithEvents BtnRemarksMas1 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
