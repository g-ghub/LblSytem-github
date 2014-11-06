<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_N01NefuPrint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_N01NefuPrint))
        Me.LblTop1 = New System.Windows.Forms.Label()
        Me.AxPsyBcLbl1 = New AxPSYBCLBLLib.AxPsyBcLbl()
        Me.LblUnder1 = New System.Windows.Forms.Label()
        Me.LblCen1 = New System.Windows.Forms.Label()
        Me.LblCen2 = New System.Windows.Forms.Label()
        Me.LblCen3 = New System.Windows.Forms.Label()
        Me.LblCen4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.AxPsyBcLbl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblTop1
        '
        Me.LblTop1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblTop1.Location = New System.Drawing.Point(28, 34)
        Me.LblTop1.Name = "LblTop1"
        Me.LblTop1.Size = New System.Drawing.Size(111, 22)
        Me.LblTop1.TabIndex = 0
        Me.LblTop1.Text = "ｽｲﾊﾝｼﾞｬｰSR-CL05P-AH 0.5L"
        Me.LblTop1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'AxPsyBcLbl1
        '
        Me.AxPsyBcLbl1.Enabled = True
        Me.AxPsyBcLbl1.Location = New System.Drawing.Point(29, 54)
        Me.AxPsyBcLbl1.Name = "AxPsyBcLbl1"
        Me.AxPsyBcLbl1.OcxState = CType(resources.GetObject("AxPsyBcLbl1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxPsyBcLbl1.Size = New System.Drawing.Size(76, 30)
        Me.AxPsyBcLbl1.TabIndex = 1
        '
        'LblUnder1
        '
        Me.LblUnder1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblUnder1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUnder1.Location = New System.Drawing.Point(27, 80)
        Me.LblUnder1.Name = "LblUnder1"
        Me.LblUnder1.Size = New System.Drawing.Size(78, 22)
        Me.LblUnder1.TabIndex = 2
        Me.LblUnder1.Text = "9,999"
        '
        'LblCen1
        '
        Me.LblCen1.Font = New System.Drawing.Font("MS UI Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCen1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblCen1.Location = New System.Drawing.Point(117, 50)
        Me.LblCen1.Name = "LblCen1"
        Me.LblCen1.Size = New System.Drawing.Size(17, 12)
        Me.LblCen1.TabIndex = 3
        Me.LblCen1.Text = "99"
        Me.LblCen1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCen2
        '
        Me.LblCen2.Font = New System.Drawing.Font("MS UI Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCen2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblCen2.Location = New System.Drawing.Point(117, 61)
        Me.LblCen2.Name = "LblCen2"
        Me.LblCen2.Size = New System.Drawing.Size(17, 12)
        Me.LblCen2.TabIndex = 4
        Me.LblCen2.Text = "99"
        Me.LblCen2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblCen3
        '
        Me.LblCen3.AutoSize = True
        Me.LblCen3.Font = New System.Drawing.Font("MS UI Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCen3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblCen3.Location = New System.Drawing.Point(111, 73)
        Me.LblCen3.Name = "LblCen3"
        Me.LblCen3.Size = New System.Drawing.Size(29, 11)
        Me.LblCen3.TabIndex = 5
        Me.LblCen3.Text = "1234"
        '
        'LblCen4
        '
        Me.LblCen4.Font = New System.Drawing.Font("MS UI Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LblCen4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblCen4.Location = New System.Drawing.Point(36, 70)
        Me.LblCen4.Name = "LblCen4"
        Me.LblCen4.Size = New System.Drawing.Size(62, 13)
        Me.LblCen4.TabIndex = 8
        Me.LblCen4.Text = "89703915"
        Me.LblCen4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(100, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 11)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "（税別）"
        '
        'frm_N01NefuPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(367, 302)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblUnder1)
        Me.Controls.Add(Me.LblCen3)
        Me.Controls.Add(Me.LblCen2)
        Me.Controls.Add(Me.LblCen4)
        Me.Controls.Add(Me.LblCen1)
        Me.Controls.Add(Me.AxPsyBcLbl1)
        Me.Controls.Add(Me.LblTop1)
        Me.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frm_N01NefuPrint"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "印刷"
        CType(Me.AxPsyBcLbl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblTop1 As System.Windows.Forms.Label
    Friend WithEvents AxPsyBcLbl1 As AxPSYBCLBLLib.AxPsyBcLbl
    Friend WithEvents LblUnder1 As System.Windows.Forms.Label
    Friend WithEvents LblCen1 As System.Windows.Forms.Label
    Friend WithEvents LblCen2 As System.Windows.Forms.Label
    Friend WithEvents LblCen3 As System.Windows.Forms.Label
    Friend WithEvents LblCen4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
