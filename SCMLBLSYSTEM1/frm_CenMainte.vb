Imports System.Data.SQLite
Public Class frm_CenMainte
    Dim intRenewFlg As Integer = 0
    Dim Wrk_Data1(,) As String
    Dim intTokID As Integer
    Dim strCbxTxt As String
    Dim strLblTypeID As String = ""
    Dim intcbxflg As Integer = 0
    Dim intChkFlg As Integer = 0


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

    Private Sub TourokuBtn2_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD1.Click
        'センター登録ボタン押下時イベント
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
            Me.Text = "物流センターの管理－登録"
            intRenewFlg = 0
            DtgInput1.Rows.Clear()
            'パネルを表示＆非表示
            PnlInput1.Visible = True
            PnlUpdate1.Visible = False
            PnlDelete1.Visible = False
            DtgInput1.AllowUserToAddRows = True
            Me.DtgInput1.Focus()
        End If

    End Sub

    Private Sub UpdateBtn5_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD2.Click
        'センター変更ボタン押下時イベント
        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer

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

            Me.Text = "物流センターの管理－変更"
            intRenewFlg = 0
            'パネルを表示＆非表示
            PnlInput1.Visible = False
            PnlUpdate1.Visible = True
            PnlDelete1.Visible = False
            DtgUpdate1.AllowUserToAddRows = False
            DtgUpdate1.Rows.Clear()

            intTokID = 0
            For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data1(0, Cntbb)
                End If
            Next Cntbb

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
            sqlField1 = "CenID,CenName,CorpID,Remarks1,Remarks2,Remarks3"
            sqlTableName = "Tbl_CenMas"
            sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                          "NOT DelFlg = 1"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

            Command.CommandText = sqlStatement

            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                DtgUpdate1.Rows.Add()
                Idx = DtgUpdate1.Rows.Count - 1
                DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("CenID").ToString
                DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("CenName").ToString
                DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("CorpID").ToString
                DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks1").ToString
                DtgUpdate1.Rows(Idx).Cells(4).Value = DataReader.Item("Remarks2").ToString
                DtgUpdate1.Rows(Idx).Cells(5).Value = DataReader.Item("Remarks3").ToString
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

            Me.DtgUpdate1.Focus()

        End If

    End Sub

    Private Sub DeleteBtn2_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD3.Click
        'センター削除ボタン押下時イベント
        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer

        '初期化エリア
        intChkFlg = 0

        intTokID = 0
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
            End If
        Next Cntbb

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
            Me.Text = "物流センターの管理－削除"

            intRenewFlg = 0

            'パネルを表示＆非表示
            PnlInput1.Visible = False
            PnlUpdate1.Visible = False
            PnlDelete1.Visible = True
            DtgDelete1.AllowUserToAddRows = False
            DtgDelete1.Rows.Clear()

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
            sqlField1 = "CenID,CenName,CorpID,DelFlg,Remarks1,Remarks2,Remarks3"

            sqlTableName = "Tbl_CenMas"
            sqlWhereCon = "CorpID = '" & intTokID & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader
            Do Until Not DataReader.Read
                DtgDelete1.Rows.Add()
                Idx = DtgDelete1.Rows.Count - 1
                DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("CenID").ToString
                DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("CenName").ToString
                DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("CorpID").ToString
                DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("DelFlg").ToString
                DtgDelete1.Rows(Idx).Cells(4).Value = DataReader.Item("Remarks1").ToString
                DtgDelete1.Rows(Idx).Cells(5).Value = DataReader.Item("Remarks2").ToString
                DtgDelete1.Rows(Idx).Cells(6).Value = DataReader.Item("Remarks3").ToString
                If DataReader.Item("DelFlg").ToString = 1 Then
                    DtgDelete1.Rows(Idx).Cells(1).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(4).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(5).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(6).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(7).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(7).Value = True
                End If
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

    Private Sub UpdateBtn6_Click(sender As System.Object, e As System.EventArgs) Handles BtnUpdate1.Click
        '変更ボタンを押下した際のイベント
        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0
        Dim intErrorFlg As Integer = 0
        Dim ErrorMessage As String = "" 'エラーメッセージ出力用変数
        Dim strErrorMessage1 As String = "センター名は必ず入力して下さい"
        Dim strErrorMessage2 As String = "センターＩＤは必ず入力して下さい"
        Dim strErrorMessage3 As String = "印字名は必ず入力して下さい"
        Dim strErrorMessage4 As String = "空白の項目があります。値を入力して下さい"
        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        intTokID = 0
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
            End If
        Next Cntbb

        'データグリッドのニューメリックチェック
        For i = 0 To DtgUpdate1.Rows.Count - 1
            'センター名の空白チェック
            If DtgUpdate1.Rows(i).Cells(1).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 1
                    intErrorFlg = 1
                End If

            End If

            '備考１の空白チェック
            If DtgUpdate1.Rows(i).Cells(3).Visible = True And
                 DtgUpdate1.Rows(i).Cells(3).Value = "" Then

                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage4
                    intRow = i
                    intClm = 3
                    intErrorFlg = 1
                End If
            End If


            '備考２の空白チェック
            If DtgUpdate1.Rows(i).Cells(4).Visible = True And
                DtgUpdate1.Rows(i).Cells(4).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage4
                    intRow = i
                    intClm = 4
                    intErrorFlg = 1
                End If

            End If

            '備考３の空白チェック
            If DtgUpdate1.Rows(i).Cells(5).Visible = True And
               DtgUpdate1.Rows(i).Cells(5).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage4
                    intRow = i
                    intClm = 5
                    intErrorFlg = 1
                End If

            End If

        Next

        Me.DtgUpdate1.MultiSelect = True
        If Not ErrorMessage = "" Then
            MessageBox.Show(ErrorMessage, "エラー", _
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
            If MessageBox.Show("変更しますよろしいですか？", _
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
                    sqlTableName = "Tbl_CenMas"
                    sqlSetCon = "CenName = '" & DtgUpdate1.Rows(i).Cells(1).Value & "'," &
                                "Remarks1 = '" & DtgUpdate1.Rows(i).Cells(3).Value & "'," &
                                "Remarks2 = '" & DtgUpdate1.Rows(i).Cells(4).Value & "'," &
                                "Remarks3 = '" & DtgUpdate1.Rows(i).Cells(5).Value & "' "
                    sqlWhereCon = "CenID = " & DtgUpdate1.Rows(i).Cells(0).Value & ""
                    'SQL
                    sqlStatement = sqlUpdate & sqlTableName & sqlSet & sqlSetCon & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    Command.ExecuteNonQuery()

                Next

                DtgUpdate1.Rows.Clear()

                'データグリッドの更新
                'SQL文の作成 OrderBYなし
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "CenID,CenName,CorpID,Remarks1,Remarks2,Remarks3"
                sqlTableName = "Tbl_CenMas"
                sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                              "NOT DelFlg = 1"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    DtgUpdate1.Rows.Add()
                    Idx = DtgUpdate1.Rows.Count - 1
                    DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("CenID").ToString
                    DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("CenName").ToString
                    DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("CorpID").ToString
                    DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks1").ToString
                    DtgUpdate1.Rows(Idx).Cells(4).Value = DataReader.Item("Remarks2").ToString
                    DtgUpdate1.Rows(Idx).Cells(5).Value = DataReader.Item("Remarks3").ToString
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
        Me.DtgUpdate1.Focus()

    End Sub

    Private Sub UpdateBtn4_Click(sender As System.Object, e As System.EventArgs) Handles BtnInput1.Click
        '登録ボタン押下時のイベント
        '変数宣言
        Dim ErrorMessage As String = "" 'エラーメッセージ出力用変数
        Dim strErrorMessage1 As String = "空白の項目があります。値を入力して下さい"
        Dim strErrorMessage2 As String = "既に登録されている名前は登録出来ません"
        Dim strErrorMessage3 As String = "センターＩＤは必ず入力して下さい"
        Dim strErrorMessage4 As String = "印字名は必ず入力して下さい"

        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim i As Integer = 0

        Dim intErrorFlg As Integer = 0
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0
        Dim intCnt As Integer = 0
        Dim intCntUp As Integer = 0

        intTokID = 0
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1

            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
            End If

        Next Cntbb

        '接続文字列を設定
        Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection.Open()

        'コマンド作成
        Command = Connection.CreateCommand

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        'データグリッドのニューメリックチェック
        For i = 0 To DtgInput1.Rows.Count - 2

            'センター名の空白チェック
            If DtgInput1.Rows(i).Cells(0).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 0
                    intErrorFlg = 1
                End If

            End If

            '備考１の空白チェック
            If DtgInput1.Rows(i).Cells(1).Visible = True And
                 DtgInput1.Rows(i).Cells(1).Value = "" Then

                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage3
                    intRow = i
                    intClm = 1
                    intErrorFlg = 1
                End If
            End If


            '備考２の空白チェック
            If DtgInput1.Rows(i).Cells(2).Visible = True And
                DtgInput1.Rows(i).Cells(2).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage4
                    intRow = i
                    intClm = 2
                    intErrorFlg = 1
                End If

            End If

            '備考３の空白チェック
            If DtgInput1.Rows(i).Cells(3).Visible = True And
                DtgInput1.Rows(i).Cells(3).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage4
                    intRow = i
                    intClm = 3
                    intErrorFlg = 1
                End If

            End If
        Next

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
            ' どのボタンを選択したかを判断する
            If MessageBox.Show("センターを登録しますよろしいですか？", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then

                intRenewFlg = 0

                'コマンド作成
                Command = Connection.CreateCommand

                For i = 0 To DtgInput1.Rows.Count - 2

                    sqlStatement = ""
                    sqlField1 = ""
                    sqlField2 = ""
                    sqlTableName = ""
                    sqlTableName2 = ""
                    sqlWhereCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "CenName,CenNameD,CorpID,Remarks1,Remarks2,Remarks3,DelFlg"
                    sqlTableName = "Tbl_CenMas"
                    sqlValuesCon = "('" & DtgInput1.Rows(i).Cells(0).Value & "'," &
                                   "'" & DtgInput1.Rows(i).Cells(0).Value & "'," &
                                   "'" & intTokID & "'," &
                                   "'" & DtgInput1.Rows(i).Cells(1).Value & "'," &
                                   "'" & DtgInput1.Rows(i).Cells(2).Value & "'," &
                                   "'" & DtgInput1.Rows(i).Cells(3).Value & "'," &
                                   "0)"

                    sqlStatement = sqlInsertInto & sqlTableName & " (" & sqlField1 & ") " & sqlValues & sqlValuesCon


                    Command.CommandText = sqlStatement

                    Command.ExecuteNonQuery()

                Next

                'データグリッドの値を初期化
                DtgInput1.Rows.Clear()

                MessageBox.Show("登録が完了しました", _
                                "登録完了", _
                                MessageBoxButtons.OK)

                Me.DtgInput1.Focus()
            End If
        End If

        'ＤＢ切断
        Connection.Close()

        Command.Dispose()
        Connection.Dispose()

        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DTGCAN4_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub DleBtn3_Click(sender As System.Object, e As System.EventArgs) Handles BtnDelete1.Click
        '削除ボタンを押下時のイベント
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer
        Dim Cmd As Integer
        Dim intDeleteFlg As Integer = 0
        Dim intUpdateFlg As Integer = 0

        intTokID = 0
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
            End If
        Next Cntbb

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        ' どのボタンを選択したかを判断する
        If MessageBox.Show("更新しますよろしいですか？", _
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

            For i = 0 To DtgDelete1.Rows.Count - 1
                If DtgDelete1.Rows(i).Cells(7).Value = True Then
                    If DtgDelete1.Rows(i).Cells(3).Value = 0 Then
                        'SQL作成
                        '削除フラグを０にもどし、表示用センター名にセンター名を出力する

                        'アップデート文
                        '初期化
                        sqlStatement = ""
                        sqlTableName = ""
                        sqlSetCon = ""
                        sqlWhereCon = ""
                        '各ＳＱＬ文の構文設定
                        sqlTableName = "Tbl_CenMas"
                        sqlSetCon = "CenNameD = '削除済みセンター'," &
                                    "DelFlg = '1' "
                        sqlWhereCon = "CenID = " & DtgDelete1.Rows(i).Cells(0).Value & ""
                        'SQL
                        sqlStatement = sqlUpdate & sqlTableName & sqlSet & sqlSetCon & sqlWhere & sqlWhereCon

                        Command.CommandText = sqlStatement

                        Command.ExecuteNonQuery()
                        intUpdateFlg = 1
                    End If

                    '削除のチェックボックスがチェックされていない場合。且つ削除フラグが立っている場合
                ElseIf DtgDelete1.Rows(i).Cells(7).Value = False Then
                    If DtgDelete1.Rows(i).Cells(3).Value = 1 Then
                        'SQL作成
                        '削除フラグを０にもどし、表示用センター名にセンター名を出力する
                        '初期化
                        sqlStatement = ""
                        sqlTableName = ""
                        sqlSetCon = ""
                        sqlWhereCon = ""
                        '各ＳＱＬ文の構文設定
                        sqlTableName = "Tbl_CenMas"
                        sqlSetCon = "CenNameD = '" & DtgDelete1.Rows(i).Cells(1).Value & "'," &
                                    "DelFlg = '0' "
                        sqlWhereCon = "CenID = " & DtgDelete1.Rows(i).Cells(0).Value & ""

                        sqlStatement = sqlUpdate & sqlTableName & sqlSet & sqlSetCon & sqlWhere & sqlWhereCon

                        Command.CommandText = sqlStatement

                        Command.ExecuteNonQuery()
                        intDeleteFlg = 1
                    End If

                End If

            Next

            If intDeleteFlg = 0 And
                    intUpdateFlg = 0 Then
                MessageBox.Show("削除項目にチェックをするか、チェックを外してください", _
                                "エラー", _
                                MessageBoxButtons.OK, _
                                MessageBoxIcon.Error)
            End If

            If intUpdateFlg = 1 Or
                intDeleteFlg = 1 Then
                MessageBox.Show("データを削除、または更新をしました", _
                                "更新完了", _
                                MessageBoxButtons.OK)
            End If

            DtgDelete1.Rows.Clear()

            'SQL文の作成 OrderBYなし
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""
            '各ＳＱＬ文の構文設定
            sqlField1 = "CenID,CenName,CorpID,DelFlg,Remarks1,Remarks2,Remarks3"
            sqlTableName = "Tbl_CenMas"
            sqlWhereCon = "CorpID = '" & intTokID & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                DtgDelete1.Rows.Add()
                Idx = DtgDelete1.Rows.Count - 1
                DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("CenID").ToString
                DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("CenName").ToString
                DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("CorpID").ToString
                DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("DelFlg").ToString
                DtgDelete1.Rows(Idx).Cells(4).Value = DataReader.Item("Remarks1").ToString
                DtgDelete1.Rows(Idx).Cells(5).Value = DataReader.Item("Remarks2").ToString
                DtgDelete1.Rows(Idx).Cells(6).Value = DataReader.Item("Remarks3").ToString

                If DataReader.Item("DelFlg").ToString = 1 Then
                    DtgDelete1.Rows(Idx).Cells(1).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(4).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(5).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(6).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(7).Style.BackColor = Color.Silver
                    DtgDelete1.Rows(Idx).Cells(7).Value = True
                End If
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

    Private Sub CloseBtn3_Click(sender As System.Object, e As System.EventArgs) Handles BtnBac1.Click
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

    Private Sub frm_CenMainte_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterParent
        'フォームロード時のイベント
        'パネルを表示＆非表示
        PnlInput1.Visible = False
        PnlUpdate1.Visible = True
        PnlDelete1.Visible = False

        Dim Cnt As Integer = 0
        Dim Cntup As Integer = 0
        Dim i As Integer = 0
        Dim Ccn As Integer = 0
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader

        '共通ワークエリアの初期化
        ReDim Wrk_Data1(2, 1)


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

        '得意先名を取得するＳＱＬ。where句はマキヤ用とニトリ用を除外する条件式
        sqlField1 = "CorpName,CorpID,CorpName,LblTypeID"
        sqlTableName = "Tbl_CorpMas"
        sqlWhereCon = "Not LblTypeID = 'M01' AND " &
                      "Not LblTypeID = 'N01'"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
        Command.CommandText = sqlStatement


        ''SQL作成
        'データリーダーにデータ取得
        DataReader = Command.ExecuteReader

        Do Until Not DataReader.Read

            CmbTok1.Items.Add(DataReader.Item("CorpName").ToString)
            'ワークエリアへのセット
            Wrk_Data1(0, i) = DataReader.Item("CorpID").ToString
            Wrk_Data1(1, i) = DataReader.Item("CorpName").ToString
            Wrk_Data1(2, i) = DataReader.Item("LblTypeID").ToString

            'ワークエリアの拡張（配列を追加）
            ReDim Preserve Wrk_Data1(2, Cntup + 1)
            Cntup = Cntup + 1
            i = i + 1
        Loop

        CmbTok1.Text = CmbTok1.Items(0)

        'ＤＢ切断
        DataReader.Close()
        Connection.Close()

        DataReader.Dispose()
        Command.Dispose()
        Connection.Dispose()

    End Sub

    '******************データグリッドビューの入力項目、ＩＭＥ制御************************
    '登録画面の入力制御
    Private Sub DtGVDFU5_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        '---- 列番号を調べて制御 ------
        Select Case e.ColumnIndex
            Case 0
                'この列は日本語入力ON
                DtgInput1.ImeMode = Windows.Forms.ImeMode.Hiragana
        End Select
        'DtGVDFU5.BeginEdit(True)
    End Sub
    '変更画面の入力制御
    Private Sub DtGVDFU6_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        '---- 列番号を調べて制御 ------
        Select Case e.ColumnIndex
            Case 1
                'この列は日本語入力ON
                DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Hiragana
        End Select
        'DtGVDFU6.BeginEdit(True)
    End Sub
    '***************************************END****************************************

    '******************データグリッドビューのマウスクリック制御************************

    Private Sub DtGVDFU6_CellCellClick _
       (sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs)

        'セルクリック時のイベント
        'DTGVDFU6.BeginEdit(True)
    End Sub
    Private Sub DtGVDFU5_CellCellClick _
        (sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs)

        'セルクリック時のイベント
        'DTGVDFU5.BeginEdit(True)
    End Sub
    '***************************************END****************************************

    '登録画面のデータグリッドビュー
    'CellValidatingイベントハンドラ 
    Private Sub DtgInput1_CellValidating(ByVal sender As Object, _
        ByVal e As DataGridViewCellValidatingEventArgs) _
        Handles DtgInput1.CellValidating

        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim intErrorFlg As Integer = 0
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader

        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "桁数がオーバーしています。１０桁が入力可能な最大値です。"
        Dim strErrorMessage2 As String = "空白は登録できません。何か文字を入力して下さい"
        Dim strErrorMessage3 As String = "既に同じ名前が入力されています。再入力して下さい"
        Dim strErrorMessage4 As String = "既に同じセンター名が登録されています。再入力して下さい"
        Dim strErrorMessage5 As String = "文章中に空白は入力できません。空白を削除して下さい。"

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If

        intRenewFlg = 1

        '***センター名の重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            '得意先ＩＤの取得
            intTokID = 0
            For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data1(0, Cntbb)
                End If
            Next Cntbb

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
            sqlField1 = "*"
            sqlTableName = "Tbl_CenMas"
            sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                          "CenName = '" & e.FormattedValue.ToString() & "'"

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
        'センター名の二重入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            For i = 0 To DtgInput1.Rows.Count - 2
                If e.FormattedValue.ToString() = DtgInput1.Rows(i).Cells(0).Value Then
                    If e.RowIndex = i Then

                    ElseIf Not e.RowIndex = i Then
                        intErrorFlg = intErrorFlg + 1
                    End If
                End If
                If intErrorFlg > 0 Then
                    ErrorMessage = strErrorMessage3
                    e.Cancel = True
                End If
            Next

        End If

        '***物流センター名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\w{11,}") Then

                ErrorMessage = strErrorMessage1
                e.Cancel = True
            End If
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage5
                Else

                    ErrorMessage = strErrorMessage2
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

    Private Sub DtgUpdate1Validating(ByVal sender As Object, _
       ByVal e As DataGridViewCellValidatingEventArgs) _
       Handles DtgUpdate1.CellValidating

        Dim Connection2 As New SQLiteConnection
        Dim Command2 As SQLiteCommand
        Dim DataReader2 As SQLiteDataReader

        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)

        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "桁数がオーバーしています。１０桁が入力可能な最大値です。"
        Dim strErrorMessage2 As String = "空白は登録できません。何か文字を入力して下さい"
        Dim strErrorMessage3 As String = "既に登録されている名前は登録出来ません"
        Dim strErrorMessage4 As String = "文章中に空白は入力できません。空白を削除して下さい。"

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If
        intRenewFlg = 1

        '***センター名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\w{11,}") Then

                ErrorMessage = strErrorMessage1
                e.Cancel = True
            End If

            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage4
                Else

                    ErrorMessage = strErrorMessage2
                End If

                e.Cancel = True
            End If

        End If

        'データの重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intTokID = 0
            For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data1(0, Cntbb)
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
            sqlField1 = "*"
            sqlTableName = "Tbl_CenMas"
            sqlWhereCon = "CorpID = " & intTokID & " AND " &
                          "CenName = '" & e.FormattedValue.ToString() & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
            'SQL
            Command2.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader2 = Command2.ExecuteReader

            Do Until Not DataReader2.Read
                ErrorMessage = strErrorMessage3
            Loop

            'ＤＢ切断
            DataReader2.Close()
            Connection2.Close()

            DataReader2.Dispose()
            Command2.Dispose()
            Connection2.Dispose()

            If Not ErrorMessage = "" Then
                'エラーメッセージの表示
                MessageBox.Show(ErrorMessage, _
                                "エラー", _
                                MessageBoxButtons.OK, _
                                MessageBoxIcon.Error)

                e.Cancel = True
            End If
        End If
    End Sub
    Private Sub DtGVDFU7_CellValidating(ByVal sender As Object, _
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
    Private Sub DtGVDFU5_CellValidated(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs)

        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        'エラーテキストを消す 
        Dgv.Rows(e.RowIndex).ErrorText = Nothing
    End Sub
    'CellValidatedイベントハンドラ 
    Private Sub DtGVDFU6_CellValidated(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs)

        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        'エラーテキストを消す 
        Dgv.Rows(e.RowIndex).ErrorText = Nothing
    End Sub
    'CellValidatedイベントハンドラ 
    Private Sub DtGVDFU7_CellValidated(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs) _
        Handles DtgDelete1.CellValidated

        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        'エラーテキストを消す 
        Dgv.Rows(e.RowIndex).ErrorText = Nothing
    End Sub
    Private Sub TokCbx2_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbTok1.Enter
        If CmbTok1.Focused Then
            strCbxTxt = CmbTok1.Text
        End If
    End Sub
    Private Sub TokCbx2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbTok1.SelectedIndexChanged

        If intRenewFlg = 1 Then
            '一度コンボボックスの値を戻すとセレクトイベントが発生するので、２回目は流さない
            If intcbxflg = 0 Then
                If MessageBox.Show("処理が途中です。入力内容が消えますがよろしいですか？", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then
                    intChkFlg = 1
                Else
                    intChkFlg = 0
                    intcbxflg = 1
                    'コンボボックスの表示をもとに戻す
                    CmbTok1.Text = strCbxTxt

                End If

            ElseIf intcbxflg = 1 Then
                intcbxflg = 0
            End If

        ElseIf intRenewFlg = 0 Then
            intChkFlg = 1
        End If

        If intChkFlg = 1 Then

            '各データグリッドのカラム名を設定
            Dim Connection As New SQLiteConnection
            Dim Command As SQLiteCommand
            Dim DataReader As SQLiteDataReader
            Dim strClmName As String = ""
            Dim strClmDName As String = ""

            intRenewFlg = 0
            intTokID = 0
            For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data1(0, Cntbb)
                End If
            Next Cntbb

            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand

            'SQL文の作成
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""
            '各ＳＱＬ文の構文設定
            sqlField1 = "DtgClmName,DtgClmDName"
            sqlTableName = "Tbl_CorpMas,Tbl_DtgClmOp"
            sqlWhereCon = "Tbl_CorpMas.CorpID = " & intTokID & " AND " &
                          "Tbl_CorpMas.LblTypeID = Tbl_DtgClmOp.LblTypeID"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader
            Do Until Not DataReader.Read
                strClmName = DataReader.Item("DtgClmName").ToString
                strClmDName = DataReader.Item("DtgClmDName").ToString

                Select Case strClmName
                    Case "DtgInputClm2"  '登録画面のデータグリッド
                        If strClmDName = "" Then
                            DtgInputClm2.Visible = False
                        Else
                            DtgInputClm2.HeaderText = strClmDName
                            DtgInputClm2.Visible = True
                        End If
                    Case "DtgInputClm3"
                        If strClmDName = "" Then
                            DtgInputClm3.Visible = False
                        Else
                            DtgInputClm3.HeaderText = strClmDName
                            DtgInputClm3.Visible = True
                        End If
                    Case "DtgInputClm4"
                        If strClmDName = "" Then
                            DtgInputClm4.Visible = False
                        Else
                            DtgInputClm4.HeaderText = strClmDName
                            DtgInputClm4.Visible = True
                        End If

                    Case "DtgUpdateClm4"   '変更画面のデータグリッド
                        If strClmDName = "" Then
                            DtgUpdateClm4.Visible = False
                        Else
                            DtgUpdateClm4.HeaderText = strClmDName
                            DtgUpdateClm4.Visible = True
                        End If
                    Case "DtgUpdateClm5"
                        If strClmDName = "" Then
                            DtgUpdateClm5.Visible = False
                        Else
                            DtgUpdateClm5.HeaderText = strClmDName
                            DtgUpdateClm5.Visible = True
                        End If
                    Case "DtgUpdateClm6"
                        If strClmDName = "" Then
                            DtgUpdateClm6.Visible = False
                        Else
                            DtgUpdateClm6.HeaderText = strClmDName
                            DtgUpdateClm6.Visible = True
                        End If

                    Case "DtgDeleteClm5"   '削除画面のデータグリッド
                        If strClmDName = "" Then
                            DtgDeleteClm5.Visible = False
                        Else
                            DtgDeleteClm5.HeaderText = strClmDName
                            DtgDeleteClm5.Visible = True
                        End If
                    Case "DtgDeleteClm6"
                        If strClmDName = "" Then
                            DtgDeleteClm6.Visible = False
                        Else
                            DtgDeleteClm6.HeaderText = strClmDName
                            DtgDeleteClm6.Visible = True
                        End If
                    Case "DtgDeleteClm7"
                        If strClmDName = "" Then
                            DtgDeleteClm7.Visible = False
                        Else
                            DtgDeleteClm7.HeaderText = strClmDName
                            DtgDeleteClm7.Visible = True
                        End If

                    Case Else
                End Select

            Loop

            '表示中のパネルに合して処理
            If PnlInput1.Visible = True Then
                Me.Text = "物流センターの管理－登録"
                PnlInput1.Visible = True
                PnlUpdate1.Visible = False
                PnlDelete1.Visible = False
                'データグリッドの値を初期化
                DtgInput1.Rows.Clear()
            End If

            If PnlDelete1.Visible = True Then
                Me.Text = "物流センターの管理－削除"
                'パネルの表示＆非表示
                PnlInput1.Visible = False
                PnlUpdate1.Visible = False
                PnlDelete1.Visible = True

                DtgDelete1.AllowUserToAddRows = False

                DtgDelete1.Rows.Clear()

                Dim Idx As Integer

                intTokID = 0
                For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                        '二次元配列の得意先ＩＤを出力
                        intTokID = Wrk_Data1(0, Cntbb)
                    End If
                Next Cntbb

                'コマンド作成
                Command = Connection.CreateCommand

                'SQL文の作成 OrderBYなし
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "CenID,CenName,CorpID,DelFlg,Remarks1,Remarks2,Remarks3"
                sqlTableName = "Tbl_CenMas"
                sqlWhereCon = "CorpID = '" & intTokID & "'"
                'SQL
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    DtgDelete1.Rows.Add()
                    Idx = DtgDelete1.Rows.Count - 1
                    DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("CenID").ToString
                    DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("CenName").ToString
                    DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("CorpID").ToString
                    DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("DelFlg").ToString
                    DtgDelete1.Rows(Idx).Cells(4).Value = DataReader.Item("Remarks1").ToString
                    DtgDelete1.Rows(Idx).Cells(5).Value = DataReader.Item("Remarks2").ToString
                    DtgDelete1.Rows(Idx).Cells(6).Value = DataReader.Item("Remarks3").ToString
                    If DataReader.Item("DelFlg").ToString = 1 Then
                        DtgDelete1.Rows(Idx).Cells(1).Style.BackColor = Color.Silver
                        DtgDelete1.Rows(Idx).Cells(4).Style.BackColor = Color.Silver
                        DtgDelete1.Rows(Idx).Cells(5).Style.BackColor = Color.Silver
                        DtgDelete1.Rows(Idx).Cells(6).Style.BackColor = Color.Silver
                        DtgDelete1.Rows(Idx).Cells(7).Style.BackColor = Color.Silver
                        DtgDelete1.Rows(Idx).Cells(7).Value = True
                    End If
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
                Me.Text = "物流センターの管理－変更"
                DtgUpdate1.AllowUserToAddRows = False
                'パネルの表示＆非表示
                PnlInput1.Visible = False
                PnlUpdate1.Visible = True
                PnlDelete1.Visible = False

                DtgUpdate1.Rows.Clear()
                Dim Idx As Integer
                Dim Cbc As New DataGridViewComboBoxColumn

                intTokID = 0
                For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                        '二次元配列の得意先ＩＤを出力
                        intTokID = Wrk_Data1(0, Cntbb)
                    End If
                Next Cntbb

                'コマンド作成
                Command = Connection.CreateCommand

                'SQL文の作成 OrderBYなし
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "CenID,CenName,CorpID,Remarks1,Remarks2,Remarks3"
                sqlTableName = "Tbl_CenMas"
                sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                              "NOT DelFlg = 1"
                'SQL
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    DtgUpdate1.Rows.Add()
                    Idx = DtgUpdate1.Rows.Count - 1
                    DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("CenID").ToString
                    DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("CenName").ToString
                    DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("CorpID").ToString
                    DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks1").ToString
                    DtgUpdate1.Rows(Idx).Cells(4).Value = DataReader.Item("Remarks2").ToString
                    DtgUpdate1.Rows(Idx).Cells(5).Value = DataReader.Item("Remarks3").ToString

                Loop

                'ＤＢ切断
                DataReader.Close()
                Connection.Close()

                DataReader.Dispose()
                Command.Dispose()
                Connection.Dispose()

            End If
        End If

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