Imports System.Data.SQLite
Imports System.Runtime.InteropServices
Imports System
Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports System.Net

Public Class frm_ShoNefuPrint

    'ＤＢ操作絡みの宣言
    Dim sqlStatement As String = ""
    Dim sqlSelect As String = " SELECT "
    Dim sqlFrom As String = " FROM "
    Dim sqlWhere As String = " WHERE "
    Dim sqlOrderBy As String = " ORDER BY "
    Dim sqlUpdate As String = "UPDATE "
    Dim sqlSet As String = " SET "
    Dim sqlInsertInto As String = "INSERT INTO "
    Dim sqlDelete As String = "DELETE "
    Dim sqlValues As String = " VALUES "

    Dim sqlField1 As String = ""
    Dim sqlField2 As String = ""
    Dim sqlTableName As String = ""
    Dim sqlTableName2 As String = ""
    Dim sqlWhereCon As String = ""
    Dim sqlOrderByCon As String = ""
    Dim sqlSetCon As String = ""
    Dim sqlValuesCon As String = ""
    Dim sqlInsertSelectCon As String = ""

    Dim intRenewFlg As Integer


    '印刷を行う印刷領域
    Dim PDoc1 As New System.Drawing.Printing.PrintDocument
    '出力対象件数の最大値
    Dim Dmax As Integer
    '共通のカウンタ
    Dim WIdx As Integer
    Dim Fst_sw As Boolean

    '共通ワークエリア（値札用）
    Dim WrkNefu01_Data1() As String
    Dim WrkNefu01_Data2() As String
    Dim WrkNefu01_Data3() As String
    Dim WrkNefu01_Data4() As String
    Dim WrkNefu01_Data5() As String
    Dim WrkNefu01_Data6() As String

    'User32.dllの定義（キャプチャー用）
    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function PrintWindow(ByVal hwnd As IntPtr, _
            ByVal hDC As IntPtr, ByVal nFlags As Integer) As Boolean

    End Function
    'User32.dllの定義（キャプチャー用）
    <System.Runtime.InteropServices.DllImport("gdi32.dll")> _
    Private Shared Function BitBlt(ByVal hdcDest As IntPtr, _
    ByVal nXDest As Integer, ByVal nYDest As Integer, _
    ByVal nWidth As Integer, ByVal nHeight As Integer, _
    ByVal hdcSrc As IntPtr, _
    ByVal nXSrc As Integer, ByVal nYSrc As Integer, _
    ByVal dwRop As Integer) As Boolean
    End Function

    Private Sub frm_ShoNefuPrint_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '変数宣言
        Dim Cnt As Integer = 0
        Dim Cntup As Integer = 0
        Dim i As Integer = 0
        Dim Ccn As Integer = 0
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim idx As Integer = 0

        DtgLblPri.AllowUserToAddRows = False
        DtgLblPri.Rows.Clear()

        '接続文字列を設定
        Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection.Open()
        'コマンド作成
        Command = Connection.CreateCommand

        'SQL文の作成 OrderBYなし
        '初期化
        sqlStatement = ""
        sqlField1 = ""
        sqlTableName = ""
        sqlWhereCon = ""

        '商品値札マスターテーブルからデータを取得するＳＱＬ
        sqlTableName = "Tbl_Shonefu"
        sqlField1 = "ShoCD,ShoName,ShoHacyu,ShoKbn,ShoTori,ShoGaku"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName

        Command.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader = Command.ExecuteReader

        Do Until Not DataReader.Read

            DtgLblPri.Rows.Add()
            idx = DtgLblPri.Rows.Count - 1
            '商品値札マスターテーブルからデータを取得し、データグリッドへ出力
            DtgLblPri.Rows(idx).Cells(0).Value = DataReader.Item("ShoCD").ToString
            DtgLblPri.Rows(idx).Cells(1).Value = DataReader.Item("ShoName").ToString
            DtgLblPri.Rows(idx).Cells(2).Value = DataReader.Item("ShoHacyu").ToString
            DtgLblPri.Rows(idx).Cells(3).Value = DataReader.Item("ShoKbn").ToString
            DtgLblPri.Rows(idx).Cells(4).Value = DataReader.Item("ShoTori").ToString
            DtgLblPri.Rows(idx).Cells(5).Value = DataReader.Item("ShoGaku").ToString
        Loop

        'ＤＢ切断
        DataReader.Close()
        Connection.Close()

        DataReader.Dispose()
        Command.Dispose()
        Connection.Dispose()
    End Sub

    Private Sub BtnClear1_Click(sender As System.Object, e As System.EventArgs) Handles BtnClear1.Click
        Dim strCName As String = ""
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer = 0
        Dim intChkFlg As Integer = 0
        Dim i As Integer = 0
        Dim Cntup As Integer = 0

        '初期化エリア
        intChkFlg = 0

        If intRenewFlg = 1 Then

            If MessageBox.Show("印刷されていないデータが残っています。入力内容がクリアされますがよろしいですか？", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then
                intChkFlg = 1
            Else
                intChkFlg = 0
            End If

        ElseIf intRenewFlg = 0 Then
            intChkFlg = 1

        End If

        If intChkFlg = 1 Then

            intRenewFlg = 0
            DtgLblPri.Rows.Clear()
            DtgLblPri.AllowUserToAddRows = False
            DtgLblPri.Rows.Clear()

            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand

            'SQL文の作成 OrderBYなし
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""

            '商品値札マスターテーブルからデータを取得するＳＱＬ
            sqlTableName = "Tbl_Shonefu"
            sqlField1 = "ShoCD,ShoName,ShoHacyu,ShoKbn,ShoTori,ShoGaku"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                DtgLblPri.Rows.Add()
                Idx = DtgLblPri.Rows.Count - 1
                '商品値札マスターテーブルからデータを取得し、データグリッドへ出力
                DtgLblPri.Rows(Idx).Cells(0).Value = DataReader.Item("ShoCD").ToString
                DtgLblPri.Rows(Idx).Cells(1).Value = DataReader.Item("ShoName").ToString
                DtgLblPri.Rows(Idx).Cells(2).Value = DataReader.Item("ShoHacyu").ToString
                DtgLblPri.Rows(Idx).Cells(3).Value = DataReader.Item("ShoKbn").ToString
                DtgLblPri.Rows(Idx).Cells(4).Value = DataReader.Item("ShoTori").ToString
                DtgLblPri.Rows(Idx).Cells(5).Value = DataReader.Item("ShoGaku").ToString
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()
        End If
    End Sub

    Private Sub EndBtn1_Click(sender As System.Object, e As System.EventArgs) Handles EndBtn1.Click

        If intRenewFlg = 1 Then
            If MessageBox.Show("印刷されていないデータが残っています。入力内容が消えますがよろしいですか", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then

                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    '*******************データ型エラーチェック**********************
    'CellValidatingイベントハンドラ 
    Private Sub DtgLblPri_CellValidating(ByVal sender As Object, _
        ByVal e As DataGridViewCellValidatingEventArgs) _
             Handles DtgLblPri.CellValidating

        '変数宣言
        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim Errflg As Integer = 0
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "数値以外入力出来ません。再入力して下さい"
        Dim strErrorMessage2 As String = "空白は入力出来ません。再入力して下さい"

        '***ケース数の正規表現による制御(ラベルタイプD01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage1
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage1
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage1
                    e.Cancel = True
                End If
            End If
            '入力された値の桁数をチェック。１～５ケタ以外はエラー
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then

                ErrorMessage = strErrorMessage2
                e.Cancel = True
            End If
        End If
        If Not ErrorMessage = "" Then
            'エラーメッセージの表示
            MessageBox.Show(ErrorMessage, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
        End If



    End Sub


    Private Sub BtnPrev1_Click(sender As System.Object, e As System.EventArgs) Handles BtnPrev1.Click
        '＊＊＊空白カラムのチェック処理＊＊＊
        '変数宣言
        Dim intSpaceCnt As Integer = 0
        Dim intSpacecnt2 As Integer = 0
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        'エラーメッセージエリア
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "値を入力して下さい"

        Dim intErrorFlg As Integer = 0

        'データグリッドのマルチフォーカスをＯＦＦ
        Me.DtgLblPri.MultiSelect = False

        '印刷ボタン用
        '共通ワークエリアの初期化
        ReDim WrkNefu01_Data1(1)
        ReDim WrkNefu01_Data2(1)
        ReDim WrkNefu01_Data3(1)
        ReDim WrkNefu01_Data4(1)
        ReDim WrkNefu01_Data5(1)
        ReDim WrkNefu01_Data6(1)

        'データグリッドのニューメリックチェック
        For i = 0 To DtgLblPri.Rows.Count - 1

            '全ての発注日と納品日、オリコン、ケースがスペースの場合のチェック
            If DtgLblPri.Rows(i).Cells("DtgLblPriClm7").Value = "" Then

                intSpaceCnt = intSpaceCnt + 1

            End If


        Next

        '全ての発注日と納品日がスペースの場合
        If DtgLblPri.Rows.Count = intSpaceCnt Then

            intRow = 0
            intClm = 6
            ErrorMessage = strErrorMessage1

        End If

        Me.DtgLblPri.MultiSelect = True

        'プレビュー機能
        If Not ErrorMessage = "" Then
            'エラーメッセージの表示
            MessageBox.Show(ErrorMessage, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)

            If Me.DtgLblPri.Rows.Count = 0 Then

            ElseIf Me.DtgLblPri.Rows.Count >= 0 Then
                Me.DtgLblPri.MultiSelect = False

                'フォーカスをエラーのあった箇所へ移動する。一度フォーカスをＯＦＦにして、データグリッド⇒セルの順でフォーカス
                'データグリッドのマルチフォーカスをＯＦＦ
                Me.DtgLblPri.Focus()
                Me.DtgLblPri.Rows(intRow).Cells(intClm).Selected() = True
                Me.DtgLblPri.MultiSelect = True
                Me.DtgLblPri.Rows(intRow).Cells(intClm).Selected() = True
            End If

        Else
            'プレビュー用の定義
            Dim PPre1 As New PrintPreviewDialog

            '各カウンタを初期化
            Dmax = 0
            WIdx = 0

            'データの退避
            Call Data_Set()

            If Fst_sw = False Then
                'PrintPageイベントハンドラの追加
                AddHandler PDoc1.PrintPage, _
                    AddressOf PrintDocument1_PrintPage
                Fst_sw = True
            End If

            '印刷する内容をプレビュー表示する
            PPre1.Document = PDoc1
            PPre1.ShowDialog()
        End If

        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub


    '印刷データの退避
    Private Sub Data_Set()

        '変数宣言
        Dim CntMax As Integer
        Dim Cnt As Integer

        Dim Idx As Integer
        Dim Idx2 As Integer

        Idx2 = 0

        'データグリッドビューの行数分ループ
        For Idx = 0 To Me.DtgLblPri.RowCount - 1

            '***バーコードへの出力設定

            'ループ処理の条件設定
            Cnt = 1
            'ループの最大値を設定（発行枚数を設定）
            CntMax = CType(Me.DtgLblPri(6, Idx).Value, Integer)


            '発行枚数分ループ
            For Cnt = 1 To CntMax

                'ワークエリアの拡張（配列を追加）
                ReDim Preserve WrkNefu01_Data1(Idx2 + 1) '商品コード
                ReDim Preserve WrkNefu01_Data2(Idx2 + 1) '商品名
                ReDim Preserve WrkNefu01_Data3(Idx2 + 1) '発注単位
                ReDim Preserve WrkNefu01_Data4(Idx2 + 1) '商品区分
                ReDim Preserve WrkNefu01_Data5(Idx2 + 1) '取引先コード
                ReDim Preserve WrkNefu01_Data6(Idx2 + 1) '値段（税別）

                'ワークエリアへのセット
                '商品コード
                WrkNefu01_Data1(Idx2) = Me.DtgLblPri(0, Idx).Value

                '商品名
                WrkNefu01_Data2(Idx2) = Me.DtgLblPri(1, Idx).Value

                '発注単位
                WrkNefu01_Data3(Idx2) = Me.DtgLblPri(2, Idx).Value

                '商品区分
                WrkNefu01_Data4(Idx2) = Me.DtgLblPri(3, Idx).Value

                '取引先コード
                WrkNefu01_Data5(Idx2) = Me.DtgLblPri(4, Idx).Value

                '値段（税別） ３桁カンマ区切りで制御
                WrkNefu01_Data6(Idx2) = "￥" & Integer.Parse(Me.DtgLblPri(5, Idx).Value).ToString("#,#")

                Idx2 = Idx2 + 1
            Next


        Next

        '印刷対象件数をグローバル変数へセット
        Dmax = Idx2 - 1


    End Sub

    '印刷データの出力
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, _
           ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Dim Img As New Bitmap(frm_N01NefuPrint.Width, frm_N01NefuPrint.Height)
        Dim Memg As Graphics = Graphics.FromImage(Img)
        Dim dc As IntPtr = Memg.GetHdc()

        '印刷用画面の表示
        frm_N01NefuPrint.Show()

        'データの退避
        Call Data_Set()
        'データを取得する（引数はグローバルのカウンタ）
        Call Data_Set2(WIdx)

        '印刷用画面のパネルイメージを取得する
        PrintWindow(frm_N01NefuPrint.Handle, dc, 0)
        Memg.ReleaseHdc(dc)
        Memg.Dispose()

        'イメージを２７０度回転させる処理。N01Shonefuでは使用しない為、コメントアウト
        'Img.RotateFlip(RotateFlipType.Rotate270FlipNone)

        'パネルのイメージを印刷する
        e.Graphics.DrawImage(Img, 0, 0, 370, 470)
        Img.Dispose()

        '印刷用画面の消去
        frm_N01NefuPrint.Dispose()

        '印刷するページ数のチェック
        If Dmax <= WIdx Then
            '追加での印刷無し
            e.HasMorePages = False
        Else
            '追加での印刷有り
            e.HasMorePages = True
        End If

        WIdx = WIdx + 1

    End Sub

    '印刷データのセット
    Private Sub Data_Set2(ByVal Idx As Integer)

        frm_N01NefuPrint.AxPsyBcLbl1._Value = WrkNefu01_Data1(Idx) '商品コード
        frm_N01NefuPrint.LblTop1.Text = WrkNefu01_Data2(Idx) '商品名
        frm_N01NefuPrint.LblCen1.Text = WrkNefu01_Data3(Idx) '発注単位
        frm_N01NefuPrint.LblCen2.Text = WrkNefu01_Data4(Idx) '商品区分
        frm_N01NefuPrint.LblCen3.Text = WrkNefu01_Data5(Idx) '取引先コード
        frm_N01NefuPrint.LblCen4.Text = WrkNefu01_Data1(Idx) 'JANコード

        'フォントサイズの変更　カンマと￥込みで文字のサイズを調整
        If WrkNefu01_Data6(Idx).Length = 8 Then
            frm_N01NefuPrint.LblUnder1.Font = New Font(frm_N01NefuPrint.LblUnder1.Font.FontFamily, 11, frm_N01NefuPrint.LblUnder1.Font.Style)
            frm_N01NefuPrint.LblUnder1.TextAlign = ContentAlignment.TopLeft
            frm_N01NefuPrint.LblUnder1.Text = WrkNefu01_Data6(Idx)

        ElseIf WrkNefu01_Data6(Idx).Length = 7 Then
            frm_N01NefuPrint.LblUnder1.Font = New Font(frm_N01NefuPrint.LblUnder1.Font.FontFamily, 12, frm_N01NefuPrint.LblUnder1.Font.Style)
            frm_N01NefuPrint.LblUnder1.TextAlign = ContentAlignment.TopLeft
            frm_N01NefuPrint.LblUnder1.Text = WrkNefu01_Data6(Idx)

        ElseIf WrkNefu01_Data6(Idx).Length <= 6 Then
            frm_N01NefuPrint.LblUnder1.Font = New Font(frm_N01NefuPrint.LblUnder1.Font.FontFamily, 13, frm_N01NefuPrint.LblUnder1.Font.Style)
            frm_N01NefuPrint.LblUnder1.TextAlign = ContentAlignment.TopLeft
            frm_N01NefuPrint.LblUnder1.Text = WrkNefu01_Data6(Idx)

        End If

    End Sub

    Private Sub BtnPrint1_Click(sender As System.Object, e As System.EventArgs) Handles BtnPrint1.Click
        '＊＊＊空白カラムのチェック処理＊＊＊
        '変数宣言
        Dim intSpaceCnt As Integer = 0
        Dim intSpacecnt2 As Integer = 0
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        'エラーメッセージエリア
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "値を入力して下さい"

        Dim intErrorFlg As Integer = 0

        'データグリッドのマルチフォーカスをＯＦＦ
        Me.DtgLblPri.MultiSelect = False

        '印刷ボタン用
        '共通ワークエリアの初期化
        ReDim WrkNefu01_Data1(1)
        ReDim WrkNefu01_Data2(1)
        ReDim WrkNefu01_Data3(1)
        ReDim WrkNefu01_Data4(1)
        ReDim WrkNefu01_Data5(1)
        ReDim WrkNefu01_Data6(1)

        'データグリッドのニューメリックチェック
        For i = 0 To DtgLblPri.Rows.Count - 1

            '発行枚数の列が全てスペースかチェック
            If DtgLblPri.Rows(i).Cells("DtgLblPriClm7").Value = "" Then

                intSpaceCnt = intSpaceCnt + 1

            End If

        Next

        '全てスペースの場合
        If DtgLblPri.Rows.Count = intSpaceCnt Then

            intRow = 0
            intClm = 6
            ErrorMessage = strErrorMessage1

        End If

        Me.DtgLblPri.MultiSelect = True
        'プレビュー機能
        If Not ErrorMessage = "" Then
            'エラーメッセージの表示
            MessageBox.Show(ErrorMessage, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)

            If Me.DtgLblPri.Rows.Count = 0 Then

            ElseIf Me.DtgLblPri.Rows.Count >= 0 Then
                'フォーカスをエラーのあった箇所へ移動する。一度フォーカスをＯＦＦにして、データグリッド⇒セルの順でフォーカス
                'データグリッドのマルチフォーカスをＯＦＦ
                Me.DtgLblPri.MultiSelect = False
                Me.DtgLblPri.Focus()
                Me.DtgLblPri.Rows(intRow).Cells(intClm).Selected() = True
                Me.DtgLblPri.MultiSelect = True
                Me.DtgLblPri.Rows(intRow).Cells(intClm).Selected() = True
            End If
        Else
            '各カウンタを初期化
            Dmax = 0
            WIdx = 0

            'データの退避
            Call Data_Set()

            If Fst_sw = False Then
                'PrintPageイベントハンドラの追加
                AddHandler PDoc1.PrintPage, _
                    AddressOf PrintDocument1_PrintPage
                Fst_sw = True
            End If

            Dim PDlg As New PrintDialog()
            PDlg.Document = PrintDocument1
            If (PDlg.ShowDialog = DialogResult.OK) Then
                '印刷処理の実行
                PDoc1.Print()

                'PDlg.Document.Print() 'イベントを送信
            End If
           
            intRenewFlg = 0
        End If



        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub

    '******************データグリッドビューの入力項目、ＩＭＥ制御************************
    '画面の入力制御
    Private Sub DtgLblPri_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
             Handles DtgLblPri.CellEnter

        '---- 列番号を調べて制御 ------
        Select Case e.ColumnIndex
            Case 6
                'この列はIME無効(半角英数のみ)
                DtgLblPri.ImeMode = Windows.Forms.ImeMode.Disable
        End Select
    End Sub
End Class