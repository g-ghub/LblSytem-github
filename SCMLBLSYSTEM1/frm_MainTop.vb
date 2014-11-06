Public Class frm_MainTop

    Private Sub frm_MainTop_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterScreen
        'Me.Location = New Point(50, 50)
        MainTopPnl1.Visible = True
        MainTopPnl2.Visible = False
    End Sub

    Private Sub LBLOPNBTN_Click(sender As System.Object, e As System.EventArgs) Handles BtnLblProcess1.Click
        'ラベル作成ボタン押下
        Dim f As New frm_LblPrint()
        f.ShowDialog(Me)
        '後始末
        f.Dispose()
        '画面の更新
        Me.Refresh()
    End Sub

    Private Sub MASOPNBTN_Click(sender As System.Object, e As System.EventArgs) Handles BtnMasProcess1.Click
        '出荷先情報管理ボタン押下
        MainTopPnl1.Visible = False
        MainTopPnl2.Visible = True
        Me.Text = "ラベル発行システム-出荷先情報管理画面"
    End Sub

    Private Sub CenInBtn1_Click(sender As System.Object, e As System.EventArgs) Handles BtnCenMas1.Click
        'センター追加ボタン押下
        Dim f As New frm_CenMainte()
        f.ShowDialog(Me)
        '後始末
        f.Dispose()
        '画面の更新
        Me.Refresh()
    End Sub

    Private Sub StrInBtn1_Click(sender As System.Object, e As System.EventArgs) Handles BtnStrMas1.Click
        '店舗管理ボタン押下
        Dim f As New frm_StrMainte()
        f.ShowDialog(Me)
        '後始末
        f.Dispose()
        '画面の更新
        Me.Refresh()
    End Sub

    Private Sub ReturnBtn1_Click(sender As System.Object, e As System.EventArgs) Handles BtnBac1.Click

        MainTopPnl1.Visible = True
        MainTopPnl2.Visible = False
        Me.Text = "ラベル発行システム-メイン画面"
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles BtnClose1.Click
        If MessageBox.Show("終了しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Me.Close()
        Else

        End If
    End Sub

    Private Sub BtnRemarksMas1_Click(sender As System.Object, e As System.EventArgs) Handles BtnRemarksMas1.Click
        '部門管理ボタン押下
        Dim f As New frm_RemarksMainte()
        f.ShowDialog(Me)
        '後始末
        f.Dispose()
        '画面の更新
        Me.Refresh()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        '値札商品管理ボタン押下
        Dim f As New frm_ShoNefuMainte()
        f.ShowDialog(Me)
        '後始末
        f.Dispose()
        '画面の更新
        Me.Refresh()
    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        '値札発行ボタン押下
        Dim f As New frm_ShoNefuPrint()
        f.ShowDialog(Me)
        '後始末
        f.Dispose()
        '画面の更新
        Me.Refresh()
    End Sub
End Class