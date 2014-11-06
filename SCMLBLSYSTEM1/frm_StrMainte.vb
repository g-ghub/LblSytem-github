Imports System.Data.SQLite

Public Class frm_StrMainte
    '共通ワークエリアの宣言
    Dim Wrk_Data1() As String
    Dim Wrk_Data2(,) As String

    Dim intTokID As Integer
    Dim intRenewFlg As Integer = 0
    Dim intCbxFlg As Integer = 0

    Dim strCbxTxt As String
    Dim strCbxCenTxt As String
    Dim strCenID As String = ""
    Dim strLblTypeID As String = ""

    'ＤＢ操作絡みの宣言
    Dim sqlStatement As String = ""
    Dim sqlSelect As String = "SELECT "
    Dim sqlFrom As String = " FROM "
    Dim sqlWhere As String = " WHERE "
    Dim sqlOrderBy As String = " ORDER BY "
    Dim sqlUpdate As String = " UPDATE "
    Dim sqlSet As String = " SET "
    Dim sqlInsertInto As String = "INSERT INTO "
    Dim sqlDelete As String = "DELETE "

    Dim sqlField1 As String = ""
    Dim sqlField2 As String = ""
    Dim sqlTableName As String = ""
    Dim sqlTableName2 As String = ""
    Dim sqlWhereCon As String = ""
    Dim sqlOrderByCon As String = ""
    Dim sqlSetCon As String = ""
    Dim sqlInsertSelectCon As String = ""

    Private Sub UpdateBtn1_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD2.Click
        '店舗変更ボタン押下時イベント
        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Cnt As Integer = 0
        Dim Cntup As Integer = 0
        Dim Idx As Integer
        Dim Cbc As New DataGridViewComboBoxColumn
        Dim intChkFlg As Integer = 0

        Dim Command2 As SQLiteCommand
        Dim DataReader2 As SQLiteDataReader
        Dim Connection2 As New SQLiteConnection

        '初期化エリア
        intChkFlg = 0

        If intRenewFlg = 1 Then

            If MessageBox.Show("処理が途中です。入力内容が消えますがよろしいですか？", _
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
            Me.Text = "店舗管理－変更"
            DtgUpdate1.AllowUserToAddRows = False

            'パネルの表示＆非表示
            PnlUpdate1.Visible = True
            PnlInput1.Visible = False
            PnlDelete1.Visible = False

            'データグリッド初期化
            DtgUpdate1.Rows.Clear()

            intTokID = 0
            For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data2(0, Cntbb)
                End If
            Next Cntbb

            'ワークエリアの初期化
            ReDim Wrk_Data1(1)

            '接続文字列を設定
            Connection2.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection2.Open()
            'コマンド作成
            Command2 = Connection2.CreateCommand

            'SQL文の作成 OrderBYなし
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""
            '各ＳＱＬ文の構文設定
            If CmbCen1.Text = "全て表示" Then
                '店舗登録がある状態で、センターを削除した場合に削除済みも表示する
                sqlWhereCon = "CorpID = " & intTokID & ""

            Else 'センターを指定場合
                '削除済みセンターがコンボボックスのアイテムに追加されるのを防ぐ
                sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                              "NOT DelFlg = 1"
            End If
            sqlField1 = "CenNameD"
            sqlTableName = "Tbl_CenMas"


            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

            Command2.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader2 = Command2.ExecuteReader

            Do Until Not DataReader2.Read
                Wrk_Data1(Cnt) = DataReader2.Item("CenNameD").ToString
                Cntup = Cntup + 1
                Cnt = Cntup
                ReDim Preserve Wrk_Data1(Cnt)
            Loop
            Cntup = 0
            Cnt = 0

            'ＤＢ切断
            DataReader2.Close()
            Connection2.Close()

            DataReader2.Dispose()
            Command2.Dispose()
            Connection2.Dispose()


            'データグリッド内のコンボボックスの初期値設定
            Dim Dttbl As New DataTable
            Dttbl.Columns.Add("Display", GetType(String))
            Dttbl.Columns.Add("Value", GetType(String))
            For i = 0 To Wrk_Data1.Length - 2
                Dttbl.Rows.Add(Wrk_Data1(Cnt), Wrk_Data1(Cnt))
                Cntup = Cntup + 1
                Cnt = Cntup
            Next

            DtgUpdate1.Refresh()

            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand

            intTokID = 0
            For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data2(0, Cntbb)
                End If
            Next Cntbb

            'SQL作成 OrderBYあり　
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""
            sqlOrderByCon = ""

            'SQL作成
            If CmbCen1.Text = "全て表示" Then
                '各ＳＱＬ文の構文設定　
                sqlField1 = "*"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID "
                sqlOrderByCon = "Tbl_StrMgt.CenID,Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement
            Else
                '各ＳＱＬ文の構文設定
                sqlField1 = "*"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_CenMas.CenName = '" & CmbCen1.Text & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                sqlOrderByCon = "Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

            End If

            Cbc.DataSource = Dttbl
            Cbc.ValueMember = "Value"
            Cbc.DisplayMember = "Display"
            Cbc = CType(DtgUpdate1.Columns(1), DataGridViewComboBoxColumn)
            Cbc.DataSource = Dttbl
            Cbc.ValueMember = "Value"
            Cbc.DisplayMember = "Display"
            Cbc = CType(DtgUpdate1.Columns(1), DataGridViewComboBoxColumn)

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            'データグリッドへ出力
            Do Until Not DataReader.Read
                DtgUpdate1.Rows.Add()
                Idx = DtgUpdate1.Rows.Count - 1
                DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("StrID").ToString
                DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("CenNameD").ToString
                DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("YokoCen").ToString
                DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("KenName").ToString
                DtgUpdate1.Rows(Idx).Cells(4).Value = DataReader.Item("StrNo").ToString
                DtgUpdate1.Rows(Idx).Cells(5).Value = DataReader.Item("StrName").ToString
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If


        Me.DtgUpdate1.Focus()
    End Sub
    Private Sub DeleteBtn1_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD3.Click
        '店舗削除ボタン押下時イベント
        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader

        Dim Idx As Integer
        Dim intChkFlg As Integer = 0

        '初期化エリア
        intChkFlg = 0

        If intRenewFlg = 1 Then

            If MessageBox.Show("処理が途中です。入力内容が消えますがよろしいですか？", _
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
            Me.Text = "店舗管理－削除"
            'パネルの表示＆非表示
            PnlUpdate1.Visible = False
            PnlInput1.Visible = False
            PnlDelete1.Visible = True

            DtgDelete1.AllowUserToAddRows = False

            DtgDelete1.Rows.Clear()

            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand

            intTokID = 0
            For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data2(0, Cntbb)
                End If
            Next Cntbb

            'SQL作成 OrderBYあり　
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""
            sqlOrderByCon = ""

            'SQL作成
            If CmbCen1.Text = "全て表示" Then
                '各ＳＱＬ文の構文設定　
                sqlField1 = "*"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID "
                sqlOrderByCon = "Tbl_StrMgt.CenID,Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement
            Else
                '各ＳＱＬ文の構文設定 
                sqlField1 = "*"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_CenMas.CenName = '" & CmbCen1.Text & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                sqlOrderByCon = "Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

            End If

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader
            'データグリッドの値設定
            Do Until Not DataReader.Read
                DtgDelete1.Rows.Add()
                Idx = DtgDelete1.Rows.Count - 1
                DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("StrID").ToString
                DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("CenNameD").ToString
                DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("YokoCen").ToString
                DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("KenName").ToString
                DtgDelete1.Rows(Idx).Cells(4).Value = DataReader.Item("StrNo").ToString
                DtgDelete1.Rows(Idx).Cells(5).Value = DataReader.Item("StrName").ToString
            Loop
            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

            Me.DtgDelete1.Focus()
        End If

    End Sub
    Private Sub TourokuBtn1_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD1.Click
        '店舗登録ボタン押下時イベント
        '変数宣言
        Dim Cnt As Integer = 0
        Dim Cntup As Integer = 0
        Dim Cbc As New DataGridViewComboBoxColumn
        Dim intChkFlg As Integer = 0

        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Connection As New SQLiteConnection

        intTokID = 0
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data2(0, Cntbb)
            End If
        Next Cntbb

        '初期化エリア
        intChkFlg = 0

        If intRenewFlg = 1 Then

            If MessageBox.Show("処理が途中です。入力内容が消えますがよろしいですか？", _
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
            DtgInput1.Rows.Clear()
            DtgInput1.AllowUserToAddRows = True
            Me.Text = "店舗管理－登録"
            'パネルの表示＆非表示
            PnlUpdate1.Visible = False
            PnlInput1.Visible = True
            PnlDelete1.Visible = False

            ReDim Wrk_Data1(1)

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
            '各ＳＱＬ文の構文設定
            sqlField1 = "CenName"
            sqlTableName = "Tbl_CenMas"
            sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                          "NOT DelFlg = 1"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                Wrk_Data1(Cnt) = DataReader.Item("CenName").ToString
                Cntup = Cntup + 1
                Cnt = Cntup
                ReDim Preserve Wrk_Data1(Cnt)
            Loop
            Cntup = 0
            Cnt = 0

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

            'コンボボックスの初期値設定
            Dim Dttbl As New DataTable
            Dttbl.Columns.Add("Display", GetType(String))
            Dttbl.Columns.Add("Value", GetType(String))
            For i = 0 To Wrk_Data1.Length - 2
                Dttbl.Rows.Add(Wrk_Data1(Cnt), Wrk_Data1(Cnt))
                Cntup = Cntup + 1
                Cnt = Cntup
            Next

            'コンボボックスの初期値入力
            Cbc.DataSource = Dttbl
            Cbc.ValueMember = "Value"
            Cbc.DisplayMember = "Display"
            Cbc = CType(DtgInput1.Columns(0), DataGridViewComboBoxColumn)

            Cbc.DataSource = Dttbl
            Cbc.ValueMember = "Value"
            Cbc.DisplayMember = "Display"
            Cbc = CType(DtgInput1.Columns(0), DataGridViewComboBoxColumn)
            DtgInput1.Refresh()
            'データグリッドへフォーカスを移す
            Me.DtgInput1.Focus()
        End If

    End Sub

    Private Sub UpdateBtn2_Click(sender As System.Object, e As System.EventArgs) Handles BtnInput1.Click
        'パネルの登録ボタン押下時イベント
        '変数宣言
        Dim ErrorMessage As String = "" 'エラーメッセージ出力用変数
        Dim strErrorMessage1 As String = "空白の項目があります。値を入力して下さい"
        Dim strErrorMessage2 As String = "センター名が空白です。必ず入力して下さい"
        Dim strErrorMessage3 As String = "県名が空白です。必ず入力して下さい"
        Dim strErrorMessage4 As String = "店番が空白です。必ず入力して下さい"
        Dim strErrorMessage5 As String = "店舗名が空白です。必ず入力して下さい"
        Dim strErrorMessage6 As String = "値を入力して下さい"

        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader

        Dim intErrorFlg As Integer = 0
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        'データグリッドのマルチフォーカスをＯＦＦ
        Me.DtgInput1.MultiSelect = False

        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data2(0, Cntbb)
                strLblTypeID = Wrk_Data2(2, Cntbb)
            End If
        Next Cntbb

        'データグリッドの空白項目チェック
        For i = 0 To DtgInput1.Rows.Count - 2
            Select Case strLblTypeID
                Case "D01"
                    'センター名のチェック
                    If DtgInput1.Rows(i).Cells(0).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(0).Selected() = True
                            intRow = i
                            intClm = 0
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage2
                        End If
                    End If

                    '県名のチェック
                    If DtgInput1.Rows(i).Cells(2).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(2).Selected() = True
                            intRow = i
                            intClm = 2
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage3
                        End If
                    End If

                    '店番のチェック
                    If DtgInput1.Rows(i).Cells(3).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(3).Selected() = True
                            intRow = i
                            intClm = 3
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage4
                        End If
                    End If

                    '店舗名のチェック
                    If DtgInput1.Rows(i).Cells(4).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(4).Selected() = True
                            intRow = i
                            intClm = 4
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage5
                        End If
                    End If

                Case "G01", "A01"
                    'センター名のチェック
                    If DtgInput1.Rows(i).Cells(0).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(0).Selected() = True
                            intRow = i
                            intClm = 0
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage2
                        End If
                    End If

                    '店舗名のチェック
                    If DtgInput1.Rows(i).Cells(4).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(4).Selected() = True
                            intRow = i
                            intClm = 4
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage5
                        End If
                    End If
                Case "Y01", "M01", "M02"
                    'センター名のチェック
                    If DtgInput1.Rows(i).Cells(0).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(0).Selected() = True
                            intRow = i
                            intClm = 0
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage2
                        End If
                    End If
                    '店番のチェック
                    If DtgInput1.Rows(i).Cells(3).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(3).Selected() = True
                            intRow = i
                            intClm = 3
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage4
                        End If
                    End If

                    '店舗名のチェック
                    If DtgInput1.Rows(i).Cells(4).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgInput1.MultiSelect = True
                            Me.DtgInput1.Rows(i).Cells(4).Selected() = True
                            intRow = i
                            intClm = 4
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage5
                        End If
                    End If
            End Select

        Next

        If DtgInput1.Rows.Count = 1 Then

            ErrorMessage = strErrorMessage6
        End If

        Me.DtgInput1.MultiSelect = True

        If Not ErrorMessage = "" Then

            MessageBox.Show(ErrorMessage, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)

            'フォーカスをエラーのあった箇所へ移動する。一度フォーカスをＯＦＦにして、データグリッド⇒セルの順でフォーカス
            'データグリッドのマルチフォーカスをＯＦＦ
            Me.DtgInput1.MultiSelect = False
            Me.DtgInput1.Focus()
            Me.DtgInput1.Rows(intRow).Cells(intClm).Selected() = True
            Me.DtgInput1.MultiSelect = True
            Me.DtgInput1.Rows(intRow).Cells(intClm).Selected() = True

        Else

            '**************出荷先情報追加***************
            If MessageBox.Show("データを登録します。よろしいですか？", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then


                Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
                'オープン
                Connection.Open()
                'コマンド作成
                Command = Connection.CreateCommand
                intTokID = 0
                For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                        '二次元配列の得意先ＩＤを出力
                        intTokID = Wrk_Data2(0, Cntbb)
                    End If
                Next Cntbb

                'データグリッドの列の数だけ実行
                For i = 0 To DtgInput1.Rows.Count - 2
                    'CenIDの取得
                    'SQL文の作成 OrderBYなし
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "CenId"
                    sqlTableName = "Tbl_CenMas"
                    sqlWhereCon = "CorpID = " & intTokID & " AND " &
                                  "CenName = '" & DtgInput1.Rows(i).Cells(0).Value & "'"

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader

                    Do Until Not DataReader.Read
                        'ワークエリアへのセット
                        strCenID = DataReader.Item("CenID").ToString
                    Loop

                    DataReader.Close()
                    DataReader.Dispose()

                    sqlStatement = ""
                    sqlField1 = ""
                    sqlField2 = ""
                    sqlTableName = ""
                    sqlTableName2 = ""
                    sqlWhereCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlTableName = "Tbl_StrMgt"
                    sqlField1 = "KenName,StrNo,StrName,YokoCen,CorpID,CenID"
                    sqlTableName2 = "Tbl_CenMas"
                    sqlInsertSelectCon = "'" & DtgInput1.Rows(i).Cells(2).Value & "', " &
                                         "'" & DtgInput1.Rows(i).Cells(3).Value & "', " &
                                         "'" & DtgInput1.Rows(i).Cells(4).Value & "'," &
                                         "'" & DtgInput1.Rows(i).Cells(1).Value & "'," &
                                         "'" & intTokID & "'," &
                                         "'" & strCenID & "' "
                    sqlWhereCon = "Tbl_CenMas.CorpID = '" & intTokID & "' AND " &
                                  "Tbl_CenMas.CenName = '" & DtgInput1.Rows(i).Cells(0).Value & "'"

                    'SQL
                    sqlStatement = sqlInsertInto & sqlTableName & " (" & sqlField1 & ") " & sqlSelect & sqlInsertSelectCon &
                                   sqlFrom & sqlTableName2 & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    Command.ExecuteNonQuery()

                Next

                MessageBox.Show("登録が完了しました", _
                                "登録完了", _
                                MessageBoxButtons.OK)

                'データグリッドの値を初期化
                DtgInput1.Rows.Clear()

                'ＤＢ切断
                Connection.Close()

                Command.Dispose()
                Connection.Dispose()

                intRenewFlg = 0

            End If
        End If

        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub UpdateBtn3_Click(sender As System.Object, e As System.EventArgs) Handles BtnUpdate1.Click
        '変更ボタンを押下した場合のイベント
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader

        Dim Idx As Integer
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "空白の項目があります。値を入力して下さい"
        Dim strErrorMessage2 As String = "センター名が空白です。必ず入力して下さい"
        Dim strErrorMessage3 As String = "県名が空白です。必ず入力して下さい"
        Dim strErrorMessage4 As String = "店番が空白です。必ず入力して下さい"
        Dim strErrorMessage5 As String = "店舗名が空白です。必ず入力して下さい"
        Dim intErrorFlg As Integer = 0
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0

        'データグリッドのマルチフォーカスをＯＦＦ
        Me.DtgUpdate1.MultiSelect = False

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data2(0, Cntbb)
                strLblTypeID = Wrk_Data2(2, Cntbb)
            End If
        Next Cntbb

        'データグリッドの空白チェック
        For i = 0 To DtgUpdate1.Rows.Count - 1
            Select Case strLblTypeID
                Case "D01"
                    'センター名のチェック
                    If DtgUpdate1.Rows(i).Cells(1).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(1).Selected() = True
                            intRow = i
                            intClm = 1
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage2
                        End If
                    End If

                    '県名のチェック
                    If DtgUpdate1.Rows(i).Cells(3).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(3).Selected() = True
                            intRow = i
                            intClm = 3
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage3
                        End If
                    End If

                    '店番のチェック
                    If DtgUpdate1.Rows(i).Cells(4).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(4).Selected() = True
                            intRow = i
                            intClm = 4
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage4
                        End If
                    End If

                    '店舗名のチェック
                    If DtgUpdate1.Rows(i).Cells(5).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(5).Selected() = True
                            intRow = i
                            intClm = 5
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage5
                        End If
                    End If
                Case "G01", "A01"
                    'センター名のチェック
                    If DtgUpdate1.Rows(i).Cells(1).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(1).Selected() = True
                            intRow = i
                            intClm = 1
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage2
                        End If
                    End If

                    '店舗名のチェック
                    If DtgUpdate1.Rows(i).Cells(5).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(5).Selected() = True
                            intRow = i
                            intClm = 5
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage5
                        End If
                    End If

                Case "Y01", "M01", "M02"
                    'センター名のチェック
                    If DtgUpdate1.Rows(i).Cells(1).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(1).Selected() = True
                            intRow = i
                            intClm = 1
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage2
                        End If
                    End If

                    '店番のチェック
                    If DtgUpdate1.Rows(i).Cells(4).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(4).Selected() = True
                            intRow = i
                            intClm = 4
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage4
                        End If
                    End If

                    '店舗名のチェック
                    If DtgUpdate1.Rows(i).Cells(5).Value = "" Then

                        If intErrorFlg = 0 Then
                            Me.DtgUpdate1.MultiSelect = True
                            Me.DtgUpdate1.Rows(i).Cells(5).Selected() = True
                            intRow = i
                            intClm = 5
                            intErrorFlg = 1
                            ErrorMessage = strErrorMessage5
                        End If
                    End If

            End Select

        Next

        Me.DtgUpdate1.MultiSelect = True

        If Not ErrorMessage = "" Then
            MessageBox.Show(ErrorMessage, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
            'フォーカスをエラーのあった箇所へ移動する。一度フォーカスをＯＦＦにして、データグリッド⇒セルの順でフォーカス
            'データグリッドのマルチフォーカスをＯＦＦ
            Me.DtgUpdate1.MultiSelect = False
            Me.DtgUpdate1.Focus()
            Me.DtgUpdate1.Rows(intRow).Cells(intClm).Selected() = True
            Me.DtgUpdate1.MultiSelect = True
            Me.DtgUpdate1.Rows(intRow).Cells(intClm).Selected() = True

        Else

            ' どのボタンを選択したかを判断する
            If MessageBox.Show("変更します。よろしいですか？", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then
                '*********UPDATE***********
                '接続文字列を設定
                Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
                'オープン
                Connection.Open()
                'コマンド作成
                Command = Connection.CreateCommand


                For i = 0 To DtgUpdate1.Rows.Count - 1
                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlTableName = ""
                    sqlSetCon = ""
                    sqlWhereCon = ""
                    '各ＳＱＬ文の構文設定()
                    sqlTableName = "Tbl_StrMgt"
                    sqlSetCon = "KenName = '" & DtgUpdate1.Rows(i).Cells(3).Value & "' ," &
                                "StrNo = '" & DtgUpdate1.Rows(i).Cells(4).Value & "' ," &
                                "StrName = '" & DtgUpdate1.Rows(i).Cells(5).Value & "', " &
                                "YokoCen = '" & DtgUpdate1.Rows(i).Cells(2).Value & "', " &
                                "CenID = (select Tbl_CenMas.CenID from Tbl_CenMas where Tbl_CenMas.CenName = '" & DtgUpdate1.Rows(i).Cells(1).Value & "') "
                    sqlWhereCon = "StrID = " & DtgUpdate1.Rows(i).Cells(0).Value
                    sqlStatement = sqlUpdate & sqlTableName & sqlSet & sqlSetCon & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    Command.ExecuteNonQuery()
                Next

                DtgUpdate1.Rows.Clear()

                '*********SELECT***********
                intTokID = 0
                For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                        '二次元配列の得意先ＩＤを出力
                        intTokID = Wrk_Data2(0, Cntbb)
                    End If
                Next Cntbb

                'SQL作成 OrderBYあり　
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""

                'SQL作成
                If CmbCen1.Text = "全て表示" Then
                    '各ＳＱＬ文の構文設定　
                    sqlField1 = "*"
                    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID "
                    sqlOrderByCon = "Tbl_StrMgt.CenID,Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement
                Else
                    '各ＳＱＬ文の構文設定 
                    sqlField1 = "*"
                    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                                  "Tbl_CenMas.CenName = '" & CmbCen1.Text & "' AND " &
                                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                    sqlOrderByCon = "Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                End If

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    DtgUpdate1.Rows.Add()
                    Idx = DtgUpdate1.Rows.Count - 1
                    DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("StrID").ToString
                    DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("CenName").ToString
                    DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("YokoCen").ToString
                    DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("KenName").ToString
                    DtgUpdate1.Rows(Idx).Cells(4).Value = DataReader.Item("StrNo").ToString
                    DtgUpdate1.Rows(Idx).Cells(5).Value = DataReader.Item("StrName").ToString
                Loop

                'ＤＢ切断
                DataReader.Close()
                Connection.Close()

                DataReader.Dispose()
                Command.Dispose()
                Connection.Dispose()

                intRenewFlg = 0
                MessageBox.Show("変更が完了しました", _
                                "変更完了", _
                                MessageBoxButtons.OK)

            End If
        End If

        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DLEBtn1_Click(sender As System.Object, e As System.EventArgs) Handles BtnDelete1.Click
        '削除ボタンを押下時のイベント
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer
        Dim Cmd As Integer

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        ' どのボタンを選択したかを判断する
        If MessageBox.Show("データを削除します。よろしいですか？", _
                           "確認", _
                           MessageBoxButtons.YesNo, _
                           MessageBoxIcon.Question) = DialogResult.Yes Then
            '*********DELETE***********
            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand
            Cmd = 0

            'SQL文の作成 OrderBYなし
            '初期化
            sqlStatement = ""
            sqlTableName = ""
            sqlWhereCon = ""

            Command.CommandText = sqlStatement

            'データグリッドの削除項目でチェックされている列を処理
            For i = 0 To DtgDelete1.Rows.Count - 1
                If DtgDelete1.Rows(i).Cells(6).Value = True Then
                    '各ＳＱＬ文の構文設定
                    sqlTableName = "Tbl_StrMgt"
                    sqlWhereCon = "StrID = '" & DtgDelete1.Rows(i).Cells(0).Value & "'"

                    sqlStatement = sqlDelete & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                    Command.CommandText = sqlStatement

                    Command.ExecuteNonQuery()
                    Cmd = Cmd + 1
                End If
            Next

            If Cmd = 0 Then
                MessageBox.Show("削除する項目にチェックをして,もう一度やり直してください", _
                                "エラー", _
                                MessageBoxButtons.OK, _
                                MessageBoxIcon.Error)
            Else
                MessageBox.Show(Cmd & "件のデータを削除しました", _
                                "削除完了", _
                                MessageBoxButtons.OK)
            End If


            DtgDelete1.Rows.Clear()

            intTokID = 0
            For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data2(0, Cntbb)
                End If
            Next Cntbb

            'SQL作成 OrderBYあり　
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""
            sqlOrderByCon = ""

            'SQL作成
            If CmbCen1.Text = "全て表示" Then
                '各ＳＱＬ文の構文設定　
                sqlField1 = "*"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID "
                sqlOrderByCon = "Tbl_StrMgt.CenID,Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement
            Else
                '各ＳＱＬ文の構文設定
                sqlField1 = "*"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_CenMas.CenName = '" & CmbCen1.Text & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                sqlOrderByCon = "Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

            End If

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            'データグリッドへ表示
            Do Until Not DataReader.Read
                DtgDelete1.Rows.Add()
                Idx = DtgDelete1.Rows.Count - 1
                DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("StrID").ToString
                DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("CenNameD").ToString
                DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("YokoCen").ToString
                DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("KenName").ToString
                DtgDelete1.Rows(Idx).Cells(4).Value = DataReader.Item("StrNo").ToString
                DtgDelete1.Rows(Idx).Cells(5).Value = DataReader.Item("StrName").ToString
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()
            intRenewFlg = 0
        Else

        End If

        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DTGCAN1_Click(sender As System.Object, e As System.EventArgs) Handles BtnBac1.Click
        If intRenewFlg = 1 Then
            If MessageBox.Show("処理が途中です。入力内容が消えますがよろしいですか？", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frm_MasMainte_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterParent
        '変数宣言
        Dim Cnt As Integer = 0
        Dim Cntup As Integer = 0
        Dim i As Integer = 0
        Dim Connection2 As New SQLiteConnection
        Dim Command2 As SQLiteCommand
        Dim DataReader2 As SQLiteDataReader

        'パネルの表示＆非表示
        PnlUpdate1.Visible = True
        PnlInput1.Visible = False
        PnlDelete1.Visible = False

        '共通ワークエリアの初期化
        ReDim Wrk_Data2(2, 1)

        '接続文字列を設定
        Connection2.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection2.Open()
        'コマンド作成
        Command2 = Connection2.CreateCommand

        'SQL文の作成 OrderBYなし
        '初期化
        sqlStatement = ""
        sqlField1 = ""
        sqlTableName = ""
        sqlWhereCon = ""

        '得意先名を取得するＳＱＬ。アマゾンとニトリを除外。
        sqlField1 = "CorpName,CorpID,CorpName,LblTypeID"
        sqlTableName = "Tbl_CorpMas"
        sqlWhereCon = "Not LblTypeID = 'A01' AND " &
                      "Not LblTypeID = 'N01'"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

        Command2.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader2 = Command2.ExecuteReader

        Do Until Not DataReader2.Read

            CmbTok1.Items.Add(DataReader2.Item("CorpName").ToString)
            'ワークエリアへのセット
            Wrk_Data2(0, i) = DataReader2.Item("CorpID").ToString
            Wrk_Data2(1, i) = DataReader2.Item("CorpName").ToString
            Wrk_Data2(2, i) = DataReader2.Item("LblTypeID").ToString

            'ワークエリアの拡張（配列を追加）
            ReDim Preserve Wrk_Data2(2, Cntup + 1)
            Cntup = Cntup + 1
            i = i + 1

        Loop

        CmbTok1.Text = CmbTok1.Items(0)

        'ＤＢ切断
        'ＤＢ切断
        DataReader2.Close()
        Connection2.Close()

        DataReader2.Dispose()
        Command2.Dispose()
        Connection2.Dispose()

        intTokID = 0

        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data2(0, Cntbb)
            End If
        Next Cntbb


    End Sub

    '******************データグリッドビューの入力項目、ＩＭＥ制御************************
    '登録画面の入力制御
    Private Sub DtgInput1_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
             Handles DtgInput1.CellEnter

        '---- 列番号を調べて制御 ------
        Select Case e.ColumnIndex
            Case 1, 2, 4
                'この列は日本語入力ON
                DtgInput1.ImeMode = Windows.Forms.ImeMode.Hiragana
            Case 3
                'この列はIME無効(半角英数のみ)
                DtgInput1.ImeMode = Windows.Forms.ImeMode.Disable
        End Select
        'DtGVDFU3.BeginEdit(True)
    End Sub
    '変更画面の入力制御
    Private Sub DtgUpdate1_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
             Handles DtgUpdate1.CellEnter

        '---- 列番号を調べて制御 ------
        Select Case e.ColumnIndex
            Case 2, 3, 5
                'この列は日本語入力ON
                DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Hiragana
            Case 4
                'この列はIME無効(半角英数のみ)
                DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Disable
        End Select
        'DtGVDFU2.BeginEdit(True)
    End Sub
    '*****************END*******************************************

    '登録画面のデータグリッドビューの正規表現処理
    'CellValidatingイベントハンドラ
    '対象のセルからフォーカスが移動した際に処理を実行
    Private Sub DtgInput1_CellValidating(ByVal sender As Object, _
        ByVal e As DataGridViewCellValidatingEventArgs) _
        Handles DtgInput1.CellValidating

        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim intErrorFlg As Integer = 0
        Dim strErrorMessage1 As String = "空白は登録できません。何か文字を入力して下さい"
        Dim strErrorMessage2 As String = "店番は４桁で、必ず数字で入力して下さい"
        Dim strErrorMessage3 As String = "数値以外入力できません。再入力して下さい"
        Dim strErrorMessage4 As String = "既に登録されている店番です。再入力して下さい"
        Dim strErrorMessage5 As String = "既に登録されている店舗名です。再入力して下さい"
        Dim strErrorMessage6 As String = "既に同じ名前が入力されています。再入力して下さい"
        Dim strErrorMessage7 As String = "店番は４桁以上登録出来ません。再入力して下さい"
        Dim strErrorMessage8 As String = "店舗名は９桁以上登録出来ません。再入力して下さい"
        Dim strErrorMessage9 As String = "文章中に空白は入力できません。空白を削除して下さい。"


        '得意先ＩＤの取得
        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data2(0, Cntbb)
                strLblTypeID = Wrk_Data2(2, Cntbb)
            End If
        Next Cntbb

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If

        'データグリッドの変更フラグ
        intRenewFlg = 1

        '***店番の重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

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

            '店番が既に登録されているか確認するＳＱＬ
            sqlField1 = "*"
            sqlTableName = "Tbl_StrMgt"
            sqlWhereCon = "CorpID = " & intTokID & " AND " &
                          "StrNo = '" & e.FormattedValue.ToString() & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
            'SQL
            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                ErrorMessage = strErrorMessage4
                e.Cancel = True
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If

        '***店舗名の重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm5" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

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

            '店舗名が既に登録されているか確認するＳＱＬ
            sqlField1 = "*"
            sqlTableName = "Tbl_StrMgt"
            sqlWhereCon = "CorpID = " & intTokID & " AND " &
                          "StrName = '" & e.FormattedValue.ToString() & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
            'SQL
            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                ErrorMessage = strErrorMessage5
                e.Cancel = True
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If
        '***店番の正規表現
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            '空白チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage9
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If
            Select Case strLblTypeID
                Case "D01", "M02" 'ダイレックス用、MrMax（第２）用のラベルタイプの処理　４文字必須入力チェック
                    If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9]") Then

                        ErrorMessage = strErrorMessage2
                        e.Cancel = True
                    End If

                Case "G01", "A01", "Y01" '４文字まで数字チェック

                    If e.FormattedValue.ToString().Length = 1 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9]") Then

                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 2 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9]") Then

                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 3 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 4 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9]") Then

                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If
                    End If

                Case "M01" 'マキヤ用のラベルタイプの処理 文字数３文字チェック 
                    '入力された値が数字かチェック
                    If e.FormattedValue.ToString().Length = 1 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9]") Then

                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If
                    End If
                    If e.FormattedValue.ToString().Length = 2 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9]") Then

                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 3 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length >= 4 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\w{3,}") Then

                            ErrorMessage = strErrorMessage7
                            e.Cancel = True
                        End If
                    End If

            End Select



        End If

        '***店番の重複入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            For i = 0 To DtgInput1.Rows.Count - 2
                If e.FormattedValue.ToString() = DtgInput1.Rows(i).Cells(3).Value Then
                    If e.RowIndex = i Then

                    ElseIf Not e.RowIndex = i Then
                        intErrorFlg = intErrorFlg + 1
                    End If
                End If
                If intErrorFlg > 0 Then
                    ErrorMessage = strErrorMessage6
                    e.Cancel = True
                End If
            Next

        End If
        '***店舗名の重複入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm5" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            For i = 0 To DtgInput1.Rows.Count - 2
                If e.FormattedValue.ToString() = DtgInput1.Rows(i).Cells(4).Value Then
                    If e.RowIndex = i Then

                    ElseIf Not e.RowIndex = i Then
                        intErrorFlg = intErrorFlg + 1
                    End If
                End If
                If intErrorFlg > 0 Then
                    ErrorMessage = strErrorMessage6
                    e.Cancel = True
                End If
            Next

        End If
        '***店舗名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm5" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm5" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then

                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage9
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

            '文字数の入力チェック
            Select Case strLblTypeID
                Case "D01", "G01", "Y01", "A01", "M02" 'ダイレックス用、ルミエール用（汎用ラベル）、ヤサカ用、アマゾン用、MrMax(第二)用の処理
                    '入力された値が９文字以上かチェック
                    If e.FormattedValue.ToString().Length >= 9 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm5" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\w{8,}") Then

                            ErrorMessage = strErrorMessage8
                            e.Cancel = True
                        End If
                    End If


                Case "M01" 'マキヤ用の処理
                    'マキヤ用はＭＡＸ１５文字の為、空白チェックのみ

            End Select

        End If
        '***県名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            '空白の入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage9
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If
        '***横持センター名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            '空白の入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage9
                Else

                    ErrorMessage = strErrorMessage1
                End If

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

    Private Sub DtgUpdate1_CellValidating(ByVal sender As Object, _
       ByVal e As DataGridViewCellValidatingEventArgs) _
             Handles DtgUpdate1.CellValidating

        '変更画面のデータグリッドに対する正規表現処理
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "空白は登録できません。何か文字を入力して下さい"
        Dim strErrorMessage2 As String = "店番は４桁で、必ず数字で入力して下さい"
        Dim strErrorMessage3 As String = "削除済みセンターで登録することは出来ません"
        Dim strErrorMessage4 As String = "店番は４桁で、必ず数字で入力して下さい"
        Dim strErrorMessage5 As String = "既に登録されている店番です。再入力して下さい"
        Dim strErrorMessage6 As String = "既に登録されている店舗名です。再入力して下さい"
        Dim strErrorMessage7 As String = "店番は４桁以上登録出来ません。再入力して下さい"
        Dim strErrorMessage8 As String = "店舗名は９桁以上登録出来ません。再入力して下さい"
        Dim strErrorMessage9 As String = "文章中に空白は入力できません。空白を削除して下さい。"

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If
        intRenewFlg = 1

        '得意先ＩＤの取得
         intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data2(0, Cntbb)
                strLblTypeID = Wrk_Data2(2, Cntbb)
            End If
        Next Cntbb

        '***店番の重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

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

            '入力された店番が既に登録されているか確認するＳＱＬ
            sqlField1 = "*"
            sqlTableName = "Tbl_StrMgt"
            sqlWhereCon = "CorpID = " & intTokID & " AND " &
                          "StrNo = '" & e.FormattedValue.ToString() & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                ErrorMessage = strErrorMessage5
                e.Cancel = True
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If

        '***店舗名の重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

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

            '入力された店舗名が既に登録されているか確認するＳＱＬ
            sqlField1 = "*"
            sqlTableName = "Tbl_StrMgt"
            sqlWhereCon = "CorpID = " & intTokID & " AND " &
                          "StrName = '" & e.FormattedValue.ToString() & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
            'SQL
            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                ErrorMessage = strErrorMessage6
                e.Cancel = True
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If

        '***店番の正規表現
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            '空白チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage9
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If
            Select Case strLblTypeID
                Case "D01", "M02" 'ダイレックス用、MrMax(第２)用のラベルタイプの処理
                    If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9]") Then

                        ErrorMessage = strErrorMessage2
                        e.Cancel = True
                    End If

                Case "G01", "A01", "Y01"


                    If e.FormattedValue.ToString().Length = 1 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 2 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 3 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 4 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                Case "M01" 'マキヤ用のラベルタイプの処理
                    '入力された値が数字かチェック
                    If e.FormattedValue.ToString().Length = 1 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If
                    If e.FormattedValue.ToString().Length = 2 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 3 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length >= 4 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\w{3,}") Then

                            ErrorMessage = strErrorMessage7
                            e.Cancel = True
                        End If
                    End If

            End Select
        End If


        '***店舗名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then

                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage9
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

            '文字数の入力チェック
            Select Case strLblTypeID
                Case "D01", "G01", "Y01", "A01", "M02"  'ダイレックス用、ルミエール用（汎用ラベル）、
                    '                                   'ヤサカ用、アマゾン用、MrMax(第２)の処理
                    '入力された値が９文字以上かチェック
                    If e.FormattedValue.ToString().Length >= 9 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\w{8,}") Then

                            ErrorMessage = strErrorMessage8
                            e.Cancel = True
                        End If
                    End If


                Case "M01" 'マキヤ用の処理
                    'マキヤ用はＭＡＸ１５文字の為、空白チェックのみ

            End Select

        End If

        '***県名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm4" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm4" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage9
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If
        End If

        '***横持センター名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage9
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If
        End If

        '***センター名の制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            If e.FormattedValue.ToString() = "削除済みセンター" Then

                ErrorMessage = strErrorMessage3
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
    '削除画面のデータグリッド
    Private Sub DtGVDFU4_CellValidating(ByVal sender As Object, _
       ByVal e As DataGridViewCellValidatingEventArgs) _
       Handles DtgDelete1.CellValidating
        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If
        intRenewFlg = 1
    End Sub
    'CellValidatedイベントハンドラ 
    Private Sub DtGVDFU2_CellValidated(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) _
        Handles DtgUpdate1.CellValidated

        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        'エラーテキストを消す 
        Dgv.Rows(e.RowIndex).ErrorText = Nothing
    End Sub
    'CellValidatedイベントハンドラ 
    Private Sub DtGVDFU3_CellValidated(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) _
        Handles DtgInput1.CellValidated

        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        'エラーテキストを消す 
        Dgv.Rows(e.RowIndex).ErrorText = Nothing
    End Sub
    'CellValidatedイベントハンドラ 
    Private Sub DtGVDFU4_CellValidated(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) _
        Handles DtgDelete1.CellValidated

        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        'エラーテキストを消す 
        Dgv.Rows(e.RowIndex).ErrorText = Nothing
    End Sub

    Private Sub TokCbx1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbTok1.Enter
        If CmbTok1.Focused Then
            strCbxTxt = CmbTok1.Text
        End If
    End Sub

    Private Sub CenCbx1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbCen1.Enter
        If CmbCen1.Focused Then
            strCbxCenTxt = CmbCen1.Text
        End If
    End Sub
    Private Sub TokCbx1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbTok1.SelectedIndexChanged
        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader

        Dim Cnt As Integer = 0
        Dim Cntup As Integer = 0
        Dim intChkFlg As Integer = 0

        If intRenewFlg = 1 Then
            '一度コンボボックスの値を戻すとセレクトイベントが発生するので、２回目は流さない
            If intCbxFlg = 0 Then
                If MessageBox.Show("処理が途中です。入力内容が消えますがよろしいですか？", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then
                    intChkFlg = 1
                Else
                    intChkFlg = 0
                    intCbxFlg = 1
                    'コンボボックスの表示をもとに戻す
                    CmbTok1.Text = strCbxTxt

                End If
            ElseIf intCbxFlg = 1 Then
                intCbxFlg = 0
            End If

        ElseIf intRenewFlg = 0 Then
            intChkFlg = 1
        End If

        If intChkFlg = 1 Then
            intRenewFlg = 0

            CmbCen1.Items.Clear()

            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand

            intTokID = 0
            For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data2(0, Cntbb)
                End If
            Next Cntbb

            'SQL文の作成 OrderBYなし
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""

            ReDim Wrk_Data1(1)

            '各ＳＱＬ文の構文設定
            If CmbCen1.Text = "全て表示" Then
                '店舗登録がある状態で、センターを削除した場合に削除済みも表示する
                sqlWhereCon = "CorpID = " & intTokID & ""

            Else 'センターを指定した場合
                '削除済みセンターがコンボックスのアイテムに追加されるのを防ぐ
                sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                              "NOT DelFlg = 1"
            End If
            sqlField1 = "CenName"
            sqlTableName = "Tbl_CenMas"
            'SQL
            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            '画面左側コンボボックスの初期値設定
            CmbCen1.Items.Add("全て表示")
            Do Until Not DataReader.Read
                'コンボボックス用
                CmbCen1.Items.Add(DataReader.Item("CenName").ToString)
                'ワークデータ用
                Wrk_Data1(Cnt) = DataReader.Item("CenName").ToString
                Cntup = Cntup + 1
                Cnt = Cntup
                ReDim Preserve Wrk_Data1(Cnt)
            Loop
            CmbCen1.Text = CmbCen1.Items(0)

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()
        End If

    End Sub

    Private Sub CenCbx1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbCen1.SelectedIndexChanged
        'センター名表示のコンボボックスが選択された場合のイベント

        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer
        Dim Cnt As Integer = 0
        Dim Cntup As Integer = 0

        Dim Command2 As SQLiteCommand
        Dim DataReader2 As SQLiteDataReader
        Dim Connection2 As New SQLiteConnection

        Dim intChkFlg As Integer = 0

        intTokID = 0
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data2(0, Cntbb)
            End If
        Next Cntbb


        '接続文字列を設定
        Connection2.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection2.Open()
        'コマンド作成
        Command2 = Connection2.CreateCommand

        'SQL文の作成 OrderBYなし
        '初期化
        sqlStatement = ""
        sqlField1 = ""
        sqlTableName = ""
        sqlWhereCon = ""
        '各ＳＱＬ文の構文設定
        If CmbCen1.Text = "全て表示" Then
            '店舗登録がある状態で、センターを削除した場合に削除済みも表示する
            sqlWhereCon = "CorpID = " & intTokID & ""

        Else 'センター指定の場合
            '削除済みセンターがコンボックスのアイテムに追加されるのを防ぐ
            sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                          "NOT DelFlg = 1"
        End If
        sqlField1 = "CenNameD"
        sqlTableName = "Tbl_CenMas"


        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

        Command2.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader2 = Command2.ExecuteReader

        Do Until Not DataReader2.Read
            Wrk_Data1(Cnt) = DataReader2.Item("CenNameD").ToString
            Cntup = Cntup + 1
            Cnt = Cntup
            ReDim Preserve Wrk_Data1(Cnt)
        Loop
        Cntup = 0
        Cnt = 0

        'ＤＢ切断
        DataReader2.Close()
        Connection2.Close()

        DataReader2.Dispose()
        Command2.Dispose()
        Connection2.Dispose()



        If intRenewFlg = 1 Then
            '一度コンボボックスの値を戻すとセレクトイベントが発生するので、２回目は流さない
            If intCbxFlg = 0 Then
                If MessageBox.Show("処理が途中です。入力内容が消えますがよろしいですか？", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then
                    intChkFlg = 1
                Else
                    intChkFlg = 0
                    intCbxFlg = 1
                    'コンボボックスの表示をもとに戻す
                    CmbCen1.Text = strCbxCenTxt
                End If
            ElseIf intCbxFlg = 1 Then
                intCbxFlg = 0
            End If

        ElseIf intRenewFlg = 0 Then
            intChkFlg = 1
        End If

        If intChkFlg = 1 Then

            intRenewFlg = 0

            If PnlInput1.Visible = True Then
                Me.Text = "店舗管理－登録"
                PnlUpdate1.Visible = False
                PnlInput1.Visible = True
                PnlDelete1.Visible = False

                Dim Cbc As New DataGridViewComboBoxColumn

                ReDim Wrk_Data1(1)

                Cnt = 0

                'データグリッドの初期化
                DtgInput1.Rows.Clear()
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
                '各ＳＱＬ文の構文設定
                sqlField1 = "CenName"
                sqlTableName = "Tbl_CenMas"
                sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                              "NOT DelFlg = 1"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    Wrk_Data1(Cnt) = DataReader.Item("CenName").ToString
                    Cntup = Cntup + 1
                    Cnt = Cntup
                    ReDim Preserve Wrk_Data1(Cnt)
                Loop

                Cntup = 0
                Cnt = 0

                'ＤＢ切断
                DataReader.Close()
                Connection.Close()

                DataReader.Dispose()
                Command.Dispose()
                Connection.Dispose()

                Dim Dttbl As New DataTable
                Dttbl.Columns.Add("Display", GetType(String))
                Dttbl.Columns.Add("Value", GetType(String))

                For i = 0 To Wrk_Data1.Length - 2
                    Dttbl.Rows.Add(Wrk_Data1(Cnt), Wrk_Data1(Cnt))
                    Cntup = Cntup + 1
                    Cnt = Cntup
                Next

                Cbc.DataSource = Dttbl
                Cbc.ValueMember = "Value"
                Cbc.DisplayMember = "Display"
                Cbc = CType(DtgInput1.Columns(0), DataGridViewComboBoxColumn)
                Cbc.DataSource = Dttbl
                Cbc.ValueMember = "Value"
                Cbc.DisplayMember = "Display"
                Cbc = CType(DtgInput1.Columns(0), DataGridViewComboBoxColumn)
            End If

            If PnlDelete1.Visible = True Then
                Me.Text = "店舗管理－削除"
                'パネルの表示＆非表示
                PnlUpdate1.Visible = False
                PnlInput1.Visible = False
                PnlDelete1.Visible = True
                DtgDelete1.AllowUserToAddRows = False
                DtgDelete1.Rows.Clear()

                '接続文字列を設定
                Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
                'オープン
                Connection.Open()
                'コマンド作成
                Command = Connection.CreateCommand

                'SQL作成 OrderBYあり　
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""

                'SQL作成
                If CmbCen1.Text = "全て表示" Then
                    '各ＳＱＬ文の構文設定　
                    sqlField1 = "*"
                    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID "
                    sqlOrderByCon = "Tbl_StrMgt.CenID,Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement
                Else
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "*"
                    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                                  "Tbl_CenMas.CenName = '" & CmbCen1.Text & "' AND " &
                                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                    sqlOrderByCon = "Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                End If

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    DtgDelete1.Rows.Add()
                    Idx = DtgDelete1.Rows.Count - 1
                    DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("StrID").ToString
                    DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("CenNameD").ToString
                    DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("YokoCen").ToString
                    DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("KenName").ToString
                    DtgDelete1.Rows(Idx).Cells(4).Value = DataReader.Item("StrNo").ToString
                    DtgDelete1.Rows(Idx).Cells(5).Value = DataReader.Item("StrName").ToString
                Loop

                'ＤＢ切断
                DataReader.Close()
                Connection.Close()

                DataReader.Dispose()
                Command.Dispose()
                Connection.Dispose()

                Me.DtgDelete1.Focus()

            End If

            '変更画面の時にコンボボックスを変更する場合
            If PnlUpdate1.Visible = True Then
                Me.Text = "店舗管理－変更"
                DtgUpdate1.AllowUserToAddRows = False
                'パネルの表示＆非表示
                PnlUpdate1.Visible = True
                PnlInput1.Visible = False
                PnlDelete1.Visible = False
                DtgUpdate1.Rows.Clear()

                '変数宣言
                Cnt = 0
                Cntup = 0
                Dim Cbc As New DataGridViewComboBoxColumn

                Dim Dttbl As New DataTable
                Dttbl.Columns.Add("Display", GetType(String))
                Dttbl.Columns.Add("Value", GetType(String))

                For i = 0 To Wrk_Data1.Length - 2
                    Dttbl.Rows.Add(Wrk_Data1(Cnt), Wrk_Data1(Cnt))
                    Cntup = Cntup + 1
                    Cnt = Cntup
                Next

                '接続文字列を設定
                Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
                'オープン
                Connection.Open()
                'コマンド作成
                Command = Connection.CreateCommand

                'SQL作成 OrderBYあり　
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""

                'SQL作成
                If CmbCen1.Text = "全て表示" Then
                    '各ＳＱＬ文の構文設定　
                    sqlField1 = "*"
                    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID "
                    sqlOrderByCon = "Tbl_StrMgt.CenID,Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement
                Else
                    '各ＳＱＬ文の構文設定 
                    sqlField1 = "*"
                    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                                  "Tbl_CenMas.CenName = '" & CmbCen1.Text & "' AND " &
                                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"

                    sqlOrderByCon = "Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                End If
                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Cbc.DataSource = Dttbl
                Cbc.ValueMember = "Value"
                Cbc.DisplayMember = "Display"
                Cbc = CType(DtgUpdate1.Columns(1), DataGridViewComboBoxColumn)
                Cbc.DataSource = Dttbl
                Cbc.ValueMember = "Value"
                Cbc.DisplayMember = "Display"
                Cbc = CType(DtgUpdate1.Columns(1), DataGridViewComboBoxColumn)

                Do Until Not DataReader.Read
                    DtgUpdate1.Rows.Add()
                    Idx = DtgUpdate1.Rows.Count - 1
                    DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("StrID").ToString
                    DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("CenNameD").ToString
                    DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("YokoCen").ToString
                    DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("KenName").ToString
                    DtgUpdate1.Rows(Idx).Cells(4).Value = DataReader.Item("StrNo").ToString
                    DtgUpdate1.Rows(Idx).Cells(5).Value = DataReader.Item("StrName").ToString
                Loop

                'ＤＢ切断
                DataReader.Close()
                Connection.Close()

                DataReader.Dispose()
                Command.Dispose()
                Connection.Dispose()

                Me.DtgUpdate1.Focus()

            End If

        End If

    End Sub

    Private Sub DtGVDFU3_CellCellClick _
        (sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles DtgInput1.CellClick
        'セルクリック時のイベント
        'DtGVDFU3.BeginEdit(True)
    End Sub
    Private Sub DtGVDFU2_CellCellClick _
        (sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles DtgUpdate1.CellClick
        'セルクリック時のイベント
        'DtGVDFU2.BeginEdit(True)
    End Sub
    'DataErrorイベントハンドラ
    Private Sub DtgInput1_DataError(ByVal sender As Object, _
            ByVal e As DataGridViewDataErrorEventArgs) _
            Handles DtgInput1.DataError
        
    End Sub
    'DataErrorイベントハンドラ
    Private Sub DtgUpdate1_DataError(ByVal sender As Object, _
            ByVal e As DataGridViewDataErrorEventArgs) _
            Handles DtgUpdate1.DataError

    End Sub

    'DataErrorイベントハンドラ
    Private Sub DtgDelete1_DataError(ByVal sender As Object, _
            ByVal e As DataGridViewDataErrorEventArgs) _
            Handles DtgDelete1.DataError

    End Sub
End Class