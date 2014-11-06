Imports System.Data.SQLite

Public Class frm_ShoNefuMainte

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
    Dim intChkFlg As Integer


    Private Sub frm_ShoNefuMainte_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '変数宣言
        Dim Cnt As Integer = 0
        Dim Cntup As Integer = 0
        Dim i As Integer = 0
        Dim Ccn As Integer = 0
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim idx As Integer = 0

        'フォームロード時のイベント
        'パネルを表示＆非表示
        PnlInput1.Visible = False
        PnlUpdate1.Visible = True
        PnlDelete1.Visible = False

        Me.Text = "値札商品の管理－変更"

        DtgUpdate1.AllowUserToAddRows = False
        DtgUpdate1.Rows.Clear()

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
        sqlTableName = "Tbl_Shonefu"
        sqlField1 = "ShoCD,ShoName,ShoHacyu,ShoKbn,ShoTori,ShoGaku"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName

        Command.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader = Command.ExecuteReader

        Do Until Not DataReader.Read
            DtgUpdate1.Rows.Add()
            idx = DtgUpdate1.Rows.Count - 1
            DtgUpdate1.Rows(idx).Cells(0).Value = DataReader.Item("ShoCD").ToString
            DtgUpdate1.Rows(idx).Cells(1).Value = DataReader.Item("ShoName").ToString
            DtgUpdate1.Rows(idx).Cells(2).Value = DataReader.Item("ShoHacyu").ToString
            DtgUpdate1.Rows(idx).Cells(3).Value = DataReader.Item("ShoKbn").ToString
            DtgUpdate1.Rows(idx).Cells(4).Value = DataReader.Item("ShoTori").ToString
            DtgUpdate1.Rows(idx).Cells(5).Value = DataReader.Item("ShoGaku").ToString
        Loop

        'ＤＢ切断
        DataReader.Close()
        Connection.Close()

        DataReader.Dispose()
        Command.Dispose()
        Connection.Dispose()

        DtgUpdate1.Focus()

    End Sub

    Private Sub BtnBac1_Click(sender As System.Object, e As System.EventArgs) Handles BtnBac1.Click
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

    Private Sub BtnPanelD1_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD1.Click
        '登録ボタン押下時イベント
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
            Me.Text = "値札商品の管理－登録"

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

    Private Sub BtnPanelD2_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD2.Click
        '変更ボタン押下時イベント
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

            Me.Text = "値札商品の管理－変更"

            intRenewFlg = 0
            'パネルを表示＆非表示
            PnlInput1.Visible = False
            PnlUpdate1.Visible = True
            PnlDelete1.Visible = False
            DtgUpdate1.AllowUserToAddRows = False
            DtgUpdate1.Rows.Clear()

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
            sqlTableName = "Tbl_Shonefu"
            sqlField1 = "ShoCD,ShoName,ShoHacyu,ShoKbn,ShoTori,ShoGaku"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                DtgUpdate1.Rows.Add()
                Idx = DtgUpdate1.Rows.Count - 1
                DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("ShoCD").ToString
                DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("ShoName").ToString
                DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("ShoHacyu").ToString
                DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("ShoKbn").ToString
                DtgUpdate1.Rows(Idx).Cells(4).Value = DataReader.Item("ShoTori").ToString
                DtgUpdate1.Rows(Idx).Cells(5).Value = DataReader.Item("ShoGaku").ToString
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

    Private Sub BtnPanelD3_Click(sender As System.Object, e As System.EventArgs) Handles BtnPanelD3.Click
        '削除ボタン押下時イベント
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
            Me.Text = "値札商品の管理－削除"

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
            sqlTableName = "Tbl_Shonefu"
            sqlField1 = "ShoCD,ShoName,ShoHacyu,ShoKbn,ShoTori,ShoGaku"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                DtgDelete1.Rows.Add()
                Idx = DtgDelete1.Rows.Count - 1
                DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("ShoCD").ToString
                DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("ShoName").ToString
                DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("ShoHacyu").ToString
                DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("ShoKbn").ToString
                DtgDelete1.Rows(Idx).Cells(4).Value = DataReader.Item("ShoTori").ToString
                DtgDelete1.Rows(Idx).Cells(5).Value = DataReader.Item("ShoGaku").ToString
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

    Private Sub BtnInput1_Click(sender As System.Object, e As System.EventArgs) Handles BtnInput1.Click
        '登録ボタン押下時のイベント
        '変数宣言
        Dim ErrorMessage As String = "" 'エラーメッセージ出力用変数
        Dim strErrorMessage1 As String = "空白の項目があります。値を入力して下さい"

        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim i As Integer = 0

        Dim intErrorFlg As Integer = 0
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0
        Dim intCnt As Integer = 0
        Dim intCntUp As Integer = 0

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

            '空白チェック
            '商品コード
            If DtgInput1.Rows(i).Cells(0).Value = "" Then

                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 0
                    intErrorFlg = 1
                End If
            End If

            '商品名
            If DtgInput1.Rows(i).Cells(1).Value = "" Then

                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 1
                    intErrorFlg = 1
                End If
            End If

            '発注単位
            If DtgInput1.Rows(i).Cells(2).Value = "" Then

                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 2
                    intErrorFlg = 1
                End If
            End If

            '商品区分
            If DtgInput1.Rows(i).Cells(3).Value = "" Then

                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 3
                    intErrorFlg = 1
                End If
            End If

            '取引先コード
            If DtgInput1.Rows(i).Cells(4).Value = "" Then

                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 4
                    intErrorFlg = 1
                End If
            End If

            '金額
            If DtgInput1.Rows(i).Cells(5).Value = "" Then

                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 5
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
            If MessageBox.Show("登録します。よろしいですか？", _
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
                    sqlWhereCon = ""

                    '各ＳＱＬ文の構文設定
                    sqlTableName = "Tbl_Shonefu"
                    sqlField1 = "ShoCD,ShoName,ShoHacyu,ShoKbn,ShoTori,ShoGaku"
                    sqlValuesCon = "( '" & DtgInput1.Rows(i).Cells(0).Value & "', " &
                                     "'" & DtgInput1.Rows(i).Cells(1).Value & "', " &
                                     "'" & DtgInput1.Rows(i).Cells(2).Value & "', " &
                                     "'" & DtgInput1.Rows(i).Cells(3).Value & "', " &
                                     "'" & DtgInput1.Rows(i).Cells(4).Value & "', " &
                                     "'" & DtgInput1.Rows(i).Cells(5).Value & "')"

                    'SQL
                    sqlStatement = sqlInsertInto & sqlTableName & "(" & sqlField1 & ") " & sqlValues & sqlValuesCon

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
        Me.DtgDelete1.Focus()
    End Sub

    Private Sub BtnDelete1_Click(sender As System.Object, e As System.EventArgs) Handles BtnDelete1.Click
        '削除するボタンを押下時のイベント
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer
        Dim Cmd As Integer
        Dim intDeleteFlg As Integer = 0
        Dim intUpdateFlg As Integer = 0

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
                    sqlTableName = "Tbl_Shonefu"
                    sqlWhereCon = "ShoCD = '" & DtgDelete1.Rows(i).Cells(0).Value & "'"

                    sqlStatement = sqlDelete & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                    Command.CommandText = sqlStatement

                    Command.ExecuteNonQuery()
                    Cmd = Cmd + 1
                    intUpdateFlg = 1
                End If

            Next

            If intDeleteFlg = 0 And
                    intUpdateFlg = 0 Then
                MessageBox.Show("削除項目にチェックをして下さい", _
                                "エラー", _
                                MessageBoxButtons.OK, _
                                MessageBoxIcon.Error)
            End If

            If intUpdateFlg = 1 Or
                intDeleteFlg = 1 Then
                MessageBox.Show("データを削除しました", _
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
            sqlTableName = "Tbl_Shonefu"
            sqlField1 = "ShoCD,ShoName,ShoHacyu,ShoKbn,ShoTori,ShoGaku"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                DtgDelete1.Rows.Add()
                Idx = DtgDelete1.Rows.Count - 1
                DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("ShoCD").ToString
                DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("ShoName").ToString
                DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("ShoHacyu").ToString
                DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("ShoKbn").ToString
                DtgDelete1.Rows(Idx).Cells(4).Value = DataReader.Item("ShoTori").ToString
                DtgDelete1.Rows(Idx).Cells(5).Value = DataReader.Item("ShoGaku").ToString
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
        Me.DtgDelete1.Focus()
    End Sub

    Private Sub BtnUpdate1_Click(sender As System.Object, e As System.EventArgs) Handles BtnUpdate1.Click
        '変更するボタンを押下した際のイベント
        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0
        Dim intErrorFlg As Integer = 0
        Dim ErrorMessage As String = "" 'エラーメッセージ出力用変数
        Dim strErrorMessage1 As String = "空白の項目があります。値を入力して下さい"
        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor


        'データグリッドのニューメリックチェック
        For i = 0 To DtgUpdate1.Rows.Count - 1

            '空白チェック
            '商品コード
            If DtgUpdate1.Rows(i).Cells(0).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 0
                    intErrorFlg = 1
                End If
            End If

            '商品名
            If DtgUpdate1.Rows(i).Cells(1).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 1
                    intErrorFlg = 1
                End If
            End If

            '発注単位
            If DtgUpdate1.Rows(i).Cells(2).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 2
                    intErrorFlg = 1
                End If
            End If

            '商品区分
            If DtgUpdate1.Rows(i).Cells(3).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 3
                    intErrorFlg = 1
                End If
            End If

            '取引先コード
            If DtgUpdate1.Rows(i).Cells(4).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 4
                    intErrorFlg = 1
                End If
            End If

            '金額
            If DtgUpdate1.Rows(i).Cells(5).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
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
                    sqlTableName = "Tbl_Shonefu"
                    sqlSetCon = "ShoName = '" & DtgUpdate1.Rows(i).Cells(1).Value & "'," &
                                "ShoHacyu = " & DtgUpdate1.Rows(i).Cells(2).Value & "," &
                                "ShoKbn = '" & DtgUpdate1.Rows(i).Cells(3).Value & "'," &
                                "ShoTori = '" & DtgUpdate1.Rows(i).Cells(4).Value & "'," &
                                "ShoGaku = " & DtgUpdate1.Rows(i).Cells(5).Value & " "

                    sqlWhereCon = "ShoCD = " & DtgUpdate1.Rows(i).Cells(0).Value & ""
                    'SQL
                    sqlStatement = sqlUpdate & sqlTableName & sqlSet & sqlSetCon & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    Command.ExecuteNonQuery()

                Next

                DtgUpdate1.Rows.Clear()

                'SQL文の作成 OrderBYなし
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""

                '各ＳＱＬ文の構文設定
                sqlTableName = "Tbl_Shonefu"
                sqlField1 = "ShoCD,ShoName,ShoHacyu,ShoKbn,ShoTori,ShoGaku"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    DtgUpdate1.Rows.Add()
                    Idx = DtgUpdate1.Rows.Count - 1
                    DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("ShoCD").ToString
                    DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("ShoName").ToString
                    DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("ShoHacyu").ToString
                    DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("ShoKbn").ToString
                    DtgUpdate1.Rows(Idx).Cells(4).Value = DataReader.Item("ShoTori").ToString
                    DtgUpdate1.Rows(Idx).Cells(5).Value = DataReader.Item("ShoGaku").ToString
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

    '登録画面のデータグリッドビュー
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
        Dim intChkFlg As Integer = 0
        Dim strErrorMessage1 As String = "空白は登録できません。入力して下さい。"
        Dim strErrorMessage2 As String = "数値以外入力出来ません。再入力して下さい"
        Dim strErrorMessage3 As String = "既に登録されている商品コードです。再入力して下さい"
        Dim strErrorMessage4 As String = "既に同じ商品コードが入力されています。再入力して下さい"
        Dim strErrorMessage5 As String = "商品コードは必ず数字８桁で入力して下さい"
        Dim strErrorMessage6 As String = "文章中に空白は入力できません。空白を削除して下さい。"

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If

        'データグリッドの変更フラグ
        intRenewFlg = 1

        ''***商品コードの重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
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
            '各ＳＱＬ文の構文設定
            sqlField1 = "*"
            sqlTableName = "Tbl_Shonefu"
            sqlWhereCon = "ShoCD = '" & e.FormattedValue.ToString() & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
            'SQL
            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                ErrorMessage = strErrorMessage3
                e.Cancel = True
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If

        '***商品コードの重複入力チェック
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
                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            Next

        End If

        '***商品コードの正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1

            '数字８桁がチェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                Not System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]") Then

                ErrorMessage = strErrorMessage5
                e.Cancel = True
            End If

            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                        System.Text.RegularExpressions.Regex.IsMatch( _
                        e.FormattedValue.ToString(), "\s") Then

                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***商品名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***発注単位の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***商品区分の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm4" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***取引先コードの正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm5" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm5" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***金額の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 4 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 5 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 6 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 7 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 8 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm6" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
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
    '変更画面のデータグリッドビュー
    'CellValidatingイベントハンドラ
    '対象のセルからフォーカスが移動した際に処理を実行
    Private Sub DtgUpdate1_CellValidating(ByVal sender As Object, _
        ByVal e As DataGridViewCellValidatingEventArgs) _
        Handles DtgUpdate1.CellValidating

        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim intErrorFlg As Integer = 0
        Dim intChkFlg As Integer = 0
        Dim strErrorMessage1 As String = "空白は登録できません。何か文字を入力して下さい"
        Dim strErrorMessage2 As String = "数値以外入力出来ません。再入力して下さい"
        Dim strErrorMessage3 As String = "既に登録されている商品コードです。再入力して下さい"
        Dim strErrorMessage4 As String = "既に同じ商品コードが入力されています。再入力して下さい"
        Dim strErrorMessage5 As String = "商品コードは必ず数字８桁で入力して下さい"
        Dim strErrorMessage6 As String = "文章中に空白は入力できません。空白を削除して下さい。"

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If

        'データグリッドの変更フラグ
        intRenewFlg = 1

        ''***商品コードの重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm1" AndAlso _
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
            '各ＳＱＬ文の構文設定
            sqlField1 = "*"
            sqlTableName = "Tbl_Shonefu"
            sqlWhereCon = "ShoCD = '" & e.FormattedValue.ToString() & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
            'SQL
            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                ErrorMessage = strErrorMessage3
                e.Cancel = True
            Loop


            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If

        '***商品コードの重複入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            For i = 0 To DtgInput1.Rows.Count - 2
                If e.FormattedValue.ToString() = DtgInput1.Rows(i).Cells(1).Value Then
                    If e.RowIndex = i Then

                    ElseIf Not e.RowIndex = i Then
                        intErrorFlg = intErrorFlg + 1
                    End If
                End If
                If intErrorFlg > 0 Then
                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            Next

        End If

        '***商品コードの正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '数字８桁かチェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm1" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]") Then

                ErrorMessage = strErrorMessage5
                e.Cancel = True
            End If


            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm1" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***商品名の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***発注単位の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***商品区分の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm4" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm4" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***取引先コードの正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm5" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage1
                End If

                e.Cancel = True
            End If

        End If

        '***金額の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 4 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 5 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 6 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 7 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 8 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End If
            End If

            '空白入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm6" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
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

    '******************データグリッドビューの入力項目、ＩＭＥ制御************************
    '登録画面の入力制御
    Private Sub DtgInput1_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
             Handles DtgInput1.CellEnter

        '---- 列番号を調べて制御 ------
        Select Case e.ColumnIndex
            Case 1
                'この列は日本語入力ON
                DtgInput1.ImeMode = Windows.Forms.ImeMode.Hiragana
            Case 0, 2, 3, 4, 5
                'この列はIME無効(半角英数のみ)
                DtgInput1.ImeMode = Windows.Forms.ImeMode.Disable
        End Select
    End Sub
    '登録画面の入力制御
    Private Sub DtgUpdate1_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
             Handles DtgUpdate1.CellEnter

        '---- 列番号を調べて制御 ------
        Select Case e.ColumnIndex
            Case 1
                'この列は日本語入力ON
                DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Hiragana
            Case 2, 3, 4, 5
                'この列はIME無効(半角英数のみ)
                DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Disable
        End Select
    End Sub

End Class