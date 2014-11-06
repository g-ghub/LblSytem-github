Imports System.Data.SQLite

Public Class frm_RemarksMainte
    Dim intRenewFlg As Integer = 0
    Dim Wrk_Data1(,) As String
    Dim Wrk_Data2(,) As String
    Dim intTokID As Integer
    Dim intChkFlg As Integer = 0
    Dim intcbxflg As Integer = 0
    Dim intRemarksID As Integer = 0
    Dim strCbxTxt As String
    Dim strRemTxt As String
    Dim strRemarksName As String = ""
    Dim strLblTypeID As String = ""

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

    Private Sub frm_RemarksMainte_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
        '各ＳＱＬ文の構文設定
        sqlField1 = "CorpID,CorpName,LblTypeID"
        sqlTableName = "Tbl_CorpMas"
        'where句でニトリ(N01)、アマゾン(A01)、ダイレックス(D01)、MrMaxe(第２)(M02)のラベルタイプを除外
        sqlWhereCon = "Not LblTypeID = 'N01' AND Not LblTypeID = 'A01' AND " &
                      "Not LblTypeID = 'D01' AND Not LblTypeID = 'G01' AND " &
                      "Not LblTypeID = 'M02'"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
        Command.CommandText = sqlStatement

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

        'ＤＢ切断
        DataReader.Close()
        Connection.Close()

        DataReader.Dispose()
        Command.Dispose()
        Connection.Dispose()

        CmbTok1.Text = CmbTok1.Items(0)


    End Sub

    Private Sub CmbTok1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbTok1.SelectedIndexChanged

        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim i As Integer = 0
        Dim cntup As Integer = 0

        '共通ワークエリアの初期化
        ReDim Wrk_Data2(1, 1)

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

            intRenewFlg = 0

            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand

            '得意先ＩＤの取得
            intTokID = 0
            strLblTypeID = ""
            For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data1(0, Cntbb)
                    strLblTypeID = Wrk_Data1(2, Cntbb)
                End If
            Next Cntbb

            'SQL文の作成 OrderBYなし
            '初期化
            sqlStatement = ""
            sqlField1 = ""
            sqlTableName = ""
            sqlWhereCon = ""
            '各ＳＱＬ文の構文設定
            sqlTableName = "Tbl_RemarksMas"
            sqlField1 = "RemarksID,RemarksName"
            sqlWhereCon = "LblTypeID = '" & strLblTypeID & "'"

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            CmbRem1.Items.Clear()

            Do Until Not DataReader.Read

                CmbRem1.Items.Add(DataReader.Item("RemarksName").ToString)
                'ワークエリアへのセット
                Wrk_Data2(0, i) = DataReader.Item("RemarksID").ToString
                Wrk_Data2(1, i) = DataReader.Item("RemarksName").ToString

                'ワークエリアの拡張（配列を追加）
                ReDim Preserve Wrk_Data2(1, cntup + 1)
                cntup = cntup + 1
                i = i + 1
            Loop

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

            CmbRem1.Text = CmbRem1.Items(0)
        End If




    End Sub

    Private Sub CmbRem1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbRem1.SelectedIndexChanged

        Dim strClmName1 As String = ""
        Dim strClmName2 As String = ""
        Dim strClmName3 As String = ""
        Dim intClm1MaxValue As Integer = 0
        Dim intClm2MaxValue As Integer = 0
        Dim intClm3MaxValue As Integer = 0
        Dim intClm1NameValue As Integer = 0
        Dim intClm1CellValue As Integer = 0
        Dim intClm2NameValue As Integer = 0
        Dim intClm2CellValue As Integer = 0
        Dim intClm3NameValue As Integer = 0
        Dim intClm3CellValue As Integer = 0
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader


        '得意先ＩＤの取得
        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤとラベルタイプＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
                strLblTypeID = Wrk_Data1(2, Cntbb)
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
        sqlField1 = "DtgRemarksClmName1,DtgRemarksClmName2,DtgRemarksClmName3," &
                     "DtgRemarksMaxValue1,DtgRemarksMaxValue2,DtgRemarksMaxValue3"

        sqlTableName = "Tbl_CorpMas,Tbl_DtgRemarksClmOp"
        sqlWhereCon = "Tbl_CorpMas.CorpID = " & intTokID & " AND " &
                      "Tbl_CorpMas.LblTypeID = Tbl_DtgRemarksClmOp.LblTypeID"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
        Command.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader = Command.ExecuteReader

        Do Until Not DataReader.Read

            'カラム名の取得
            strClmName1 = DataReader.Item("DtgRemarksClmName1").ToString
            strClmName2 = DataReader.Item("DtgRemarksClmName2").ToString
            strClmName3 = DataReader.Item("DtgRemarksClmName3").ToString
            'カラムのマックス入力値を取得
            intClm1MaxValue = DataReader.Item("DtgRemarksMaxValue1").ToString
            intClm2MaxValue = DataReader.Item("DtgRemarksMaxValue2").ToString
            intClm3MaxValue = DataReader.Item("DtgRemarksMaxValue3").ToString
        Loop

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
                    CmbRem1.Text = strRemTxt

                End If

            ElseIf intcbxflg = 1 Then
                intcbxflg = 0
            End If

        ElseIf intRenewFlg = 0 Then
            intChkFlg = 1
        End If

        If intChkFlg = 1 Then

            intRenewFlg = 0

            intTokID = 0
            For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                    '二次元配列の得意先ＩＤと備考名を出力
                    intTokID = Wrk_Data1(0, Cntbb)
                End If
            Next Cntbb

            '列の幅を設定
            intClm1NameValue = strClmName1.Length * 20
            intClm1CellValue = intClm1MaxValue * 13
            intClm2NameValue = strClmName2.Length * 20
            intClm2CellValue = intClm2MaxValue * 13
            intClm3NameValue = strClmName3.Length * 20
            intClm3CellValue = intClm3MaxValue * 13

            If intClm1NameValue >= intClm1CellValue Then
                DtgInputClm1.Width = intClm1NameValue
                DtgUpdateClm2.Width = intClm1NameValue
                DtgDeleteClm2.Width = intClm1NameValue
            Else
                DtgInputClm1.Width = intClm1CellValue
                DtgUpdateClm2.Width = intClm1CellValue
                DtgDeleteClm2.Width = intClm1CellValue
            End If

            If intClm2NameValue >= intClm2CellValue Then
                DtgInputClm2.Width = intClm2NameValue
                DtgUpdateClm3.Width = intClm2NameValue
                DtgDeleteClm3.Width = intClm2NameValue
            Else
                DtgInputClm2.Width = intClm2CellValue
                DtgUpdateClm3.Width = intClm2CellValue
                DtgDeleteClm3.Width = intClm2CellValue
            End If

            If intClm3NameValue >= intClm3CellValue Then
                DtgInputClm3.Width = intClm3NameValue
                DtgUpdateClm4.Width = intClm3NameValue
                DtgDeleteClm4.Width = intClm3NameValue
            Else
                DtgInputClm3.Width = intClm3CellValue
                DtgUpdateClm4.Width = intClm3CellValue
                DtgDeleteClm4.Width = intClm3CellValue
            End If


            '備考１の列名を設定
            If strClmName1 = "" Then
                '各画面の列を非表示
                DtgInputClm1.Visible = False
                DtgUpdateClm2.Visible = False
                DtgDeleteClm2.Visible = False
            Else
                '各画面のデータグリッドの列名を設定
                DtgInputClm1.HeaderText = strClmName1
                DtgInputClm1.Visible = True
                DtgInputClm1.MaxInputLength = intClm1MaxValue

                DtgUpdateClm2.HeaderText = strClmName1
                DtgUpdateClm2.Visible = True
                DtgUpdateClm2.MaxInputLength = intClm1MaxValue

                DtgDeleteClm2.HeaderText = strClmName1
                DtgDeleteClm2.Visible = True
                DtgDeleteClm2.MaxInputLength = intClm1MaxValue
            End If

            '備考２の列名を設定
            If strClmName2 = "" Then
                '各画面の列を非表示
                DtgInputClm2.Visible = False
                DtgUpdateClm3.Visible = False
                DtgDeleteClm3.Visible = False
            Else
                '各画面のデータグリッドの列名を設定
                DtgInputClm2.HeaderText = strClmName2
                DtgInputClm2.Visible = True
                DtgInputClm2.MaxInputLength = intClm2MaxValue

                DtgUpdateClm3.HeaderText = strClmName2
                DtgUpdateClm3.Visible = True
                DtgUpdateClm3.MaxInputLength = intClm2MaxValue

                DtgDeleteClm3.HeaderText = strClmName2
                DtgDeleteClm3.Visible = True
                DtgDeleteClm3.MaxInputLength = intClm2MaxValue
            End If

            '備考３の列名を設定
            If strClmName3 = "" Then
                '各画面の列を非表示
                DtgInputClm3.Visible = False
                DtgUpdateClm4.Visible = False
                DtgDeleteClm4.Visible = False
            Else
                Select Case strLblTypeID

                    Case "M01" 'マキヤ用のラベルタイプの処理

                        '部門とフロアでは使用する列が異なる為、選択されている名前によって表示列を変更する
                        If CmbRem1.Text = "フロア" Then 'フロアが選択されている場合は３列目を表示しない

                            '各画面の列を非表示
                            DtgInputClm3.Visible = False
                            DtgUpdateClm4.Visible = False
                            DtgDeleteClm4.Visible = False


                        ElseIf CmbRem1.Text = "部門" Then '部門の場合は列を表示する

                            '各画面のデータグリッドの列名を設定
                            DtgInputClm3.HeaderText = strClmName3
                            DtgInputClm3.Visible = True
                            DtgInputClm3.MaxInputLength = intClm3MaxValue

                            DtgUpdateClm4.HeaderText = strClmName3
                            DtgUpdateClm4.Visible = True
                            DtgUpdateClm4.MaxInputLength = intClm3MaxValue

                            DtgDeleteClm4.HeaderText = strClmName3
                            DtgDeleteClm4.Visible = True
                            DtgDeleteClm4.MaxInputLength = intClm3MaxValue

                        End If

                    Case Else 'マキヤ用のラベルタイプ以外の処理

                        '各画面のデータグリッドの列名を設定
                        DtgInputClm3.HeaderText = strClmName3
                        DtgInputClm3.Visible = True
                        DtgInputClm3.MaxInputLength = intClm3MaxValue

                        DtgUpdateClm4.HeaderText = strClmName3
                        DtgUpdateClm4.Visible = True
                        DtgUpdateClm4.MaxInputLength = intClm3MaxValue

                        DtgDeleteClm4.HeaderText = strClmName3
                        DtgDeleteClm4.Visible = True
                        DtgDeleteClm4.MaxInputLength = intClm3MaxValue

                End Select


            End If

            '表示中のパネルに合して処理
            If PnlInput1.Visible = True Then
                Me.Text = CmbRem1.Text & "の管理－登録"

                PnlInput1.Visible = True
                PnlUpdate1.Visible = False
                PnlDelete1.Visible = False
                'データグリッドの値を初期化
                DtgInput1.Rows.Clear()
            End If

            If PnlDelete1.Visible = True Then
                Me.Text = CmbRem1.Text & "の管理－削除"

                'パネルの表示＆非表示
                PnlInput1.Visible = False
                PnlUpdate1.Visible = False
                PnlDelete1.Visible = True

                DtgDelete1.AllowUserToAddRows = False

                DtgDelete1.Rows.Clear()

                Dim Idx As Integer

                '得意先IDの取得
                intTokID = 0
                For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                        '二次元配列の得意先ＩＤと備考名を出力
                        intTokID = Wrk_Data1(0, Cntbb)
                    End If
                Next Cntbb

                '備考IDの取得
                intRemarksID = 0
                For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                        '二次元配列の得意先ＩＤと備考名を出力
                        intRemarksID = Wrk_Data2(0, Cntbb)
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

                '備考テーブルから各フィールドを取得するＳＱＬ
                sqlField1 = "RemarksNo,Remarks1,Remarks2,Remarks3"
                sqlTableName = "Tbl_Remarks"
                sqlWhereCon = "RemarksID = " & intRemarksID & ""
                sqlOrderByCon = "Remarks1,Remarks2"

                'SQL
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    DtgDelete1.Rows.Add()
                    Idx = DtgDelete1.Rows.Count - 1
                    DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("RemarksNo").ToString
                    DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("Remarks1").ToString
                    DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("Remarks2").ToString
                    DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks3").ToString

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
                Me.Text = CmbRem1.Text & "の管理－変更"

                DtgUpdate1.AllowUserToAddRows = False
                'パネルの表示＆非表示
                PnlInput1.Visible = False
                PnlUpdate1.Visible = True
                PnlDelete1.Visible = False

                DtgUpdate1.Rows.Clear()
                Dim Idx As Integer
                Dim Cbc As New DataGridViewComboBoxColumn

                '得意先IDの取得
                intTokID = 0
                For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                        '二次元配列の得意先ＩＤと備考名を出力
                        intTokID = Wrk_Data1(0, Cntbb)
                    End If
                Next Cntbb

                '備考IDの取得
                intRemarksID = 0
                For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                        '二次元配列の得意先ＩＤと備考名を出力
                        intRemarksID = Wrk_Data2(0, Cntbb)
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

                '備考テーブルから各フィールドを取得するＳＱＬ
                sqlField1 = "RemarksNo,Remarks1,Remarks2,Remarks3"
                sqlTableName = "Tbl_Remarks"
                sqlWhereCon = "RemarksID = " & intRemarksID & ""
                sqlOrderByCon = "Remarks1,Remarks2"

                'SQL
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    DtgUpdate1.Rows.Add()
                    Idx = DtgUpdate1.Rows.Count - 1
                    DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("RemarksNo").ToString
                    DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("Remarks1").ToString
                    DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("Remarks2").ToString
                    DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks3").ToString

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

        '得意先ＩＤの取得
        intTokID = 0
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intTokID = Wrk_Data1(0, Cntbb)
            End If
        Next Cntbb

        If intChkFlg = 1 Then
            Me.Text = CmbRem1.Text & "の管理－登録"

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

    Private Sub CmbTok1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbTok1.Enter
        If CmbTok1.Focused Then
            strCbxTxt = CmbTok1.Text
        End If
    End Sub
    Private Sub CmbRem1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbRem1.Enter
        If CmbRem1.Focused Then
            strRemTxt = CmbRem1.Text
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

        '得意先ＩＤの取得
        intTokID = 0
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intTokID = Wrk_Data1(0, Cntbb)
            End If
        Next Cntbb

        If intChkFlg = 1 Then

            Me.Text = CmbRem1.Text & "の管理－変更"

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

            '備考IDの取得
            intRemarksID = 0
            For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                    '二次元配列の得意先ＩＤと備考名を出力
                    intRemarksID = Wrk_Data2(0, Cntbb)
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

            '備考テーブルから各フィールドを取得するＳＱＬ
            sqlField1 = "RemarksNo,Remarks1,Remarks2,Remarks3"
            sqlTableName = "Tbl_Remarks"
            sqlWhereCon = "RemarksID = " & intRemarksID & ""
            sqlOrderByCon = "Remarks1,Remarks2"

            'SQL
            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                DtgUpdate1.Rows.Add()
                Idx = DtgUpdate1.Rows.Count - 1
                DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("RemarksNo").ToString
                DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("Remarks1").ToString
                DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("Remarks2").ToString
                DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks3").ToString
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

        '得意先ＩＤの取得
        intTokID = 0
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intTokID = Wrk_Data1(0, Cntbb)
            End If
        Next Cntbb

        If intChkFlg = 1 Then
            Me.Text = CmbRem1.Text & "の管理－削除"

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

            '備考テーブルから各フィールドを取得するＳＱＬ
            sqlField1 = "RemarksNo,Remarks1,Remarks2,Remarks3"
            sqlTableName = "Tbl_Remarks"
            sqlWhereCon = "RemarksID = " & intRemarksID & ""
            sqlOrderByCon = "Remarks1,Remarks2"

            'SQL
            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read
                DtgDelete1.Rows.Add()
                Idx = DtgDelete1.Rows.Count - 1
                DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("RemarksNo").ToString
                DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("Remarks1").ToString
                DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("Remarks2").ToString
                DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks3").ToString

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

        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤとラベルタイプＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
                strLblTypeID = Wrk_Data1(2, Cntbb)
            End If

        Next Cntbb

        '備考IDの取得
        intRemarksID = 0
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intRemarksID = Wrk_Data2(0, Cntbb)
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

            '空白チェック
            If DtgInput1.Rows(i).Cells(0).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 0
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
                    sqlTableName = "Tbl_Remarks"
                    sqlField1 = "RemarksID,Remarks1,Remarks2,Remarks3"
                    sqlValuesCon = "('" & intRemarksID & "'," &
                                         "'" & DtgInput1.Rows(i).Cells(0).Value & "', " &
                                         "'" & DtgInput1.Rows(i).Cells(1).Value & "', " &
                                         "'" & DtgInput1.Rows(i).Cells(2).Value & "')"

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

        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
                strLblTypeID = Wrk_Data1(2, Cntbb)
            End If
        Next Cntbb

        'データグリッドのニューメリックチェック
        For i = 0 To DtgUpdate1.Rows.Count - 1

            '空白チェック
            If DtgUpdate1.Rows(i).Cells(1).Value = "" Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 1
                    intErrorFlg = 1
                End If
            End If


            If DtgUpdate1.Rows(i).Cells(2).Value = "" And
                DtgUpdate1.Rows(i).Cells(2).Visible = True Then
                If intErrorFlg = 0 Then
                    ErrorMessage = strErrorMessage1
                    intRow = i
                    intClm = 2
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

                '備考IDの取得
                intRemarksID = 0
                For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                        '二次元配列の得意先ＩＤと備考名を出力
                        intRemarksID = Wrk_Data2(0, Cntbb)
                    End If
                Next Cntbb

                For i = 0 To DtgUpdate1.Rows.Count - 1


                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlTableName = ""
                    sqlSetCon = ""
                    sqlWhereCon = ""
                    '各ＳＱＬ文の構文設定()
                    sqlTableName = "Tbl_Remarks"
                    sqlSetCon = "RemarksID = '" & intRemarksID & "'," &
                                "Remarks1 = '" & DtgUpdate1.Rows(i).Cells(1).Value & "'," &
                                "Remarks2 = '" & DtgUpdate1.Rows(i).Cells(2).Value & "'," &
                                "Remarks3 = '" & DtgUpdate1.Rows(i).Cells(3).Value & "' "

                    sqlWhereCon = "RemarksNo = " & DtgUpdate1.Rows(i).Cells(0).Value & ""
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

                '備考ＩＤを指定して、備考テーブルから該当のフィールドを取得するＳＱＬ
                sqlField1 = "RemarksNo,Remarks1,Remarks2,Remarks3"
                sqlTableName = "Tbl_Remarks"
                sqlWhereCon = "RemarksID = " & intRemarksID & ""
                sqlOrderByCon = "Remarks1,Remarks2"

                'SQL
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                Do Until Not DataReader.Read
                    '該当のあったデータをデータグリッドへ出力する
                    DtgUpdate1.Rows.Add()
                    Idx = DtgUpdate1.Rows.Count - 1
                    DtgUpdate1.Rows(Idx).Cells(0).Value = DataReader.Item("RemarksNo").ToString
                    DtgUpdate1.Rows(Idx).Cells(1).Value = DataReader.Item("Remarks1").ToString
                    DtgUpdate1.Rows(Idx).Cells(2).Value = DataReader.Item("Remarks2").ToString
                    DtgUpdate1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks3").ToString
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

    Private Sub BtnDelete1_Click(sender As System.Object, e As System.EventArgs) Handles BtnDelete1.Click
        '削除するボタンを押下時のイベント
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

        '備考IDの取得
        intRemarksID = 0
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intRemarksID = Wrk_Data2(0, Cntbb)
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

            'SQL文の作成 OrderBYなし
            '初期化
            sqlStatement = ""
            sqlTableName = ""
            sqlWhereCon = ""

            Command.CommandText = sqlStatement
            'データグリッドの削除項目でチェックされている列を処理
            For i = 0 To DtgDelete1.Rows.Count - 1
                If DtgDelete1.Rows(i).Cells(4).Value = True Then
                    '各ＳＱＬ文の構文設定
                    sqlTableName = "Tbl_Remarks"
                    sqlWhereCon = "RemarksNo = '" & DtgDelete1.Rows(i).Cells(0).Value & "'"

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

            '備考ＩＤを指定して、備考テーブルから該当のフィールドを取得するＳＱＬ
            sqlField1 = "RemarksNo,Remarks1,Remarks2,Remarks3"
            sqlTableName = "Tbl_Remarks"
            sqlWhereCon = "RemarksID = " & intRemarksID & ""
            sqlOrderByCon = "Remarks1,Remarks2"

            'SQL
            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            Do Until Not DataReader.Read

                '該当のあったフィールドをデータグリッドへ出力する
                DtgDelete1.Rows.Add()
                Idx = DtgDelete1.Rows.Count - 1
                DtgDelete1.Rows(Idx).Cells(0).Value = DataReader.Item("RemarksNo").ToString
                DtgDelete1.Rows(Idx).Cells(1).Value = DataReader.Item("Remarks1").ToString
                DtgDelete1.Rows(Idx).Cells(2).Value = DataReader.Item("Remarks2").ToString
                DtgDelete1.Rows(Idx).Cells(3).Value = DataReader.Item("Remarks3").ToString

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
        Dim strErrorMessage1 As String = "空白は登録できません。何か文字を入力して下さい"
        Dim strErrorMessage2 As String = "数値以外入力出来ません。再入力して下さい"
        Dim strErrorMessage3 As String = "未登録のフロア番号は入力できません。" & Environment.NewLine &
                                         "フロア番号を登録してからやり直してください"
        Dim strErrorMessage4 As String = "既に登録されています。再入力して下さい"
        Dim strErrorMessage5 As String = "３文字以上入力出来ません。再入力して下さい"
        Dim strErrorMessage6 As String = "既に同じ値が入力されています。再入力して下さい"
        Dim strErrorMessage7 As String = "既に同じ部門番号が登録されています。再入力して下さい"
        Dim strErrorMessage8 As String = "文章中に空白は入力できません。空白を削除して下さい。"


        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If

        'データグリッドの変更フラグ
        intRenewFlg = 1

        '得意先ＩＤの取得
        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
                strLblTypeID = Wrk_Data1(2, Cntbb)
            End If
        Next Cntbb

        '備考IDの取得
        intRemarksID = 0
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intRemarksID = Wrk_Data2(0, Cntbb)
            End If
        Next Cntbb

        '***備考１の重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand


            Select Case strLblTypeID

                Case "D01", "G01", "A01"

                Case "Y01"
                    'SQL文の作成 OrderBYなし
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "*"
                    sqlTableName = "Tbl_Remarks"
                    sqlWhereCon = "RemarksID = '" & intRemarksID & "' AND " &
                                  "Remarks1 = '" & e.FormattedValue.ToString() & "'"

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                    'SQL
                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader

                    Do Until Not DataReader.Read
                        ErrorMessage = strErrorMessage4
                        e.Cancel = True
                    Loop

                    DataReader.Close()
                    Connection.Close()
                    DataReader.Dispose()

                Case "M01" 'ラベルタイプM01のみフロア番号が登録されているかチェック

                    'RemarksIDが「２」は部門。「３」はフロア。備考のテーブルを共通で使っている為、ＩＤを直接指定。
                    If intRemarksID = 2 Then
                        'SQL文の作成 OrderBYなし
                        '初期化
                        sqlStatement = ""
                        sqlField1 = ""
                        sqlTableName = ""
                        sqlWhereCon = ""
                        '各ＳＱＬ文の構文設定
                        sqlField1 = "*"
                        sqlTableName = "Tbl_Remarks"
                        'RemarksIDが「３」のフロアを取得
                        sqlWhereCon = "RemarksID = '" & 3 & "' AND " &
                                      "Remarks1 = '" & e.FormattedValue.ToString() & "'"

                        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                        'SQL
                        Command.CommandText = sqlStatement

                        'データリーダーにデータ取得
                        DataReader = Command.ExecuteReader

                        Do Until Not DataReader.Read

                            intChkFlg = 1
                        Loop

                        If intChkFlg = 0 Then
                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If

                        DataReader.Close()
                        Connection.Close()
                        DataReader.Dispose()
                    End If

                    'RemarksIDが「２」は部門。「３」はフロア。備考のテーブルを共通で使っている為、ＩＤを直接指定。
                    If intRemarksID = 3 Then
                        'SQL文の作成 OrderBYなし
                        '初期化
                        sqlStatement = ""
                        sqlField1 = ""
                        sqlTableName = ""
                        sqlWhereCon = ""
                        '各ＳＱＬ文の構文設定
                        sqlField1 = "*"
                        sqlTableName = "Tbl_Remarks"
                        sqlWhereCon = "RemarksID = '" & intRemarksID & "' AND " &
                                      "Remarks1 = '" & e.FormattedValue.ToString() & "'"

                        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                        'SQL
                        Command.CommandText = sqlStatement

                        'データリーダーにデータ取得
                        DataReader = Command.ExecuteReader

                        Do Until Not DataReader.Read
                            ErrorMessage = strErrorMessage4
                            e.Cancel = True
                        Loop

                        DataReader.Close()
                        Connection.Close()
                        DataReader.Dispose()
                    End If

            End Select

            'ＤＢ切断

            Command.Dispose()
            Connection.Dispose()

        End If

        '***備考２の重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand


            Select Case strLblTypeID


                Case "D01", "G01", "A01", "Y01" '左からダイレックス、ルミエール、アマゾン、ヤサカ用のラベルタイプ

                Case "M01" 'ラベルタイプM01のみ部門番号が登録されているかチェック

                    'RemarksIDが「２」は部門。「３」はフロア。備考のテーブルを共通で使っている為、ＩＤを直接指定。
                    If intRemarksID = 2 Then
                        'SQL文の作成 OrderBYなし
                        '初期化
                        sqlStatement = ""
                        sqlField1 = ""
                        sqlTableName = ""
                        sqlWhereCon = ""
                        '各ＳＱＬ文の構文設定
                        sqlField1 = "*"
                        sqlTableName = "Tbl_Remarks"

                        'RemarksIDが「2」の部門番号を取得するＳＱＬ
                        sqlWhereCon = "RemarksID = '" & 2 & "' AND " &
                                      "Remarks2 = '" & e.FormattedValue.ToString() & "'"

                        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                        'SQL
                        Command.CommandText = sqlStatement

                        'データリーダーにデータ取得
                        DataReader = Command.ExecuteReader

                        Do Until Not DataReader.Read

                            intChkFlg = 1
                        Loop

                        If intChkFlg = 1 Then
                            ErrorMessage = strErrorMessage7
                            e.Cancel = True
                        End If

                        DataReader.Close()
                        Connection.Close()
                        DataReader.Dispose()
                    End If

                    'RemarksIDが「２」は部門。「３」はフロア。備考のテーブルを共通で使っている為、ＩＤを直接指定。
                    If intRemarksID = 3 Then
                        
                    End If

            End Select

            'ＤＢ切断

            Command.Dispose()
            Connection.Dispose()

        End If

        '***備考１の重複入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            'RemarksIDが「２」の場合の処理。「２」は部門番号で、
            '部門はフロアに対して複数登録する為、重複入力チェックはしない
            If intRemarksID = 2 Then

            Else
                For i = 0 To DtgInput1.Rows.Count - 2
                    If e.FormattedValue.ToString() = DtgInput1.Rows(i).Cells(0).Value Then
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

        End If
        '***備考２の重複入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            'RemarksIDが「２」の場合の処理。「２」は部門番号で、
            '部門は部門番号の重複チェックを行う
            If intRemarksID = 2 Then
                For i = 0 To DtgInput1.Rows.Count - 2
                    If e.FormattedValue.ToString() = DtgInput1.Rows(i).Cells(1).Value Then
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
            Else
               
            End If

            

        End If

            '***備考３の重複入力チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                    Not e.FormattedValue.ToString() = "" Then

                For i = 0 To DtgInput1.Rows.Count - 2
                    If e.FormattedValue.ToString() = DtgInput1.Rows(i).Cells(2).Value Then
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
            '***備考１の正規表現による制御
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                    Not e.FormattedValue.ToString() = "" Then
                intRenewFlg = 1
                Select Case strLblTypeID

                    Case "D01", "G01", "A01"

                    Case "Y01"
                        '入力された値が数字かチェック
                        If e.FormattedValue.ToString().Length = 1 Then
                            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                                Not System.Text.RegularExpressions.Regex.IsMatch( _
                                e.FormattedValue.ToString(), "[0-9]") Then

                                ErrorMessage = strErrorMessage2
                                e.Cancel = True
                            End If
                        End If
                        If e.FormattedValue.ToString().Length = 2 Then
                            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                                Not System.Text.RegularExpressions.Regex.IsMatch( _
                                e.FormattedValue.ToString(), "[0-9][0-9]") Then

                                ErrorMessage = strErrorMessage2
                                e.Cancel = True
                            End If
                        End If
                        If e.FormattedValue.ToString().Length = 3 Then
                            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                                Not System.Text.RegularExpressions.Regex.IsMatch( _
                                e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                                ErrorMessage = strErrorMessage2
                                e.Cancel = True
                            End If
                        End If

                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                                System.Text.RegularExpressions.Regex.IsMatch( _
                                e.FormattedValue.ToString(), "\s") Then
                        
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage8
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                        End If
                    Case "M01"
                        '入力された値が数字かチェック
                        If e.FormattedValue.ToString().Length = 1 Then
                            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                                Not System.Text.RegularExpressions.Regex.IsMatch( _
                                e.FormattedValue.ToString(), "[0-9]") Then

                                ErrorMessage = strErrorMessage2
                                e.Cancel = True
                            End If
                        End If
                        If e.FormattedValue.ToString().Length = 2 Then
                            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                                Not System.Text.RegularExpressions.Regex.IsMatch( _
                                e.FormattedValue.ToString(), "[0-9][0-9]") Then

                                ErrorMessage = strErrorMessage2
                                e.Cancel = True
                            End If
                        End If
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm1" AndAlso _
                                System.Text.RegularExpressions.Regex.IsMatch( _
                                e.FormattedValue.ToString(), "\s") Then
                        
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage8
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                        End If
                End Select


            End If

            '***備考２の正規表現による制御
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                    Not e.FormattedValue.ToString() = "" Then

                Select Case strLblTypeID

                    Case "D01", "G01", "A01"

                    Case "Y01"

                        '空白チェック
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                             System.Text.RegularExpressions.Regex.IsMatch( _
                             e.FormattedValue.ToString(), "\s") Then

                            ErrorMessage = strErrorMessage1
                            e.Cancel = True
                        End If

                    Case "M01"
                        If intRemarksID = 2 Then
                            '部門の場合は数字２桁かチェック
                            If intRemarksID = 2 Then

                                '空白チェック
                                If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                                    System.Text.RegularExpressions.Regex.IsMatch( _
                                    e.FormattedValue.ToString(), "\s") Then
                                
                                If e.FormattedValue.ToString().Length >= 2 Then

                                    ErrorMessage = strErrorMessage8
                                Else

                                    ErrorMessage = strErrorMessage1
                                End If

                                e.Cancel = True
                                End If
                                '入力された値が数字かチェック
                                If e.FormattedValue.ToString().Length = 1 Then
                                    If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                                        Not System.Text.RegularExpressions.Regex.IsMatch( _
                                        e.FormattedValue.ToString(), "[0-9]") Then

                                        ErrorMessage = strErrorMessage2
                                        e.Cancel = True
                                    End If
                                End If
                                If e.FormattedValue.ToString().Length = 2 Then
                                    If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                                        Not System.Text.RegularExpressions.Regex.IsMatch( _
                                        e.FormattedValue.ToString(), "[0-9][0-9]") Then

                                        ErrorMessage = strErrorMessage2
                                        e.Cancel = True
                                    End If
                                End If

                                If e.FormattedValue.ToString().Length >= 3 Then
                                    If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm2" AndAlso _
                                        System.Text.RegularExpressions.Regex.IsMatch( _
                                        e.FormattedValue.ToString(), "\w{3,}") Then

                                        ErrorMessage = strErrorMessage5
                                        e.Cancel = True
                                    End If
                                End If
                            End If


                        End If

                End Select

            End If

            '***備考３の正規表現による制御
            If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                    Not e.FormattedValue.ToString() = "" Then

                Select Case strLblTypeID

                    Case "D01", "G01", "A01"

                    Case "Y01", "M01"
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgInputClm3" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                        
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage8
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                        End If
                End Select



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
        Dim strErrorMessage3 As String = "未登録のフロア番号は入力できません。" & Environment.NewLine &
                                         "フロア番号を登録してからやり直してください"
        Dim strErrorMessage4 As String = "既に登録されています。再入力して下さい"
        Dim strErrorMessage5 As String = "３文字以上入力出来ません。再入力して下さい"
        Dim strErrorMessage6 As String = "既に同じ値が入力されています。再入力して下さい"
        Dim strErrorMessage7 As String = "文章中に空白は入力できません。空白を削除して下さい。"

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If

        'データグリッドの変更フラグ
        intRenewFlg = 1

        ''***備考１の重複チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            '得意先ＩＤの取得
            intTokID = 0
            strLblTypeID = ""
            For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                    '二次元配列の得意先ＩＤを出力
                    intTokID = Wrk_Data1(0, Cntbb)
                    strLblTypeID = Wrk_Data1(2, Cntbb)
                End If
            Next Cntbb

            '備考IDの取得
            intRemarksID = 0
            For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
                '二次元配列の得意先名とコンボボックスの値を比較
                If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                    '二次元配列の得意先ＩＤと備考名を出力
                    intRemarksID = Wrk_Data2(0, Cntbb)
                End If
            Next Cntbb



            '接続文字列を設定
            Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
            'オープン
            Connection.Open()
            'コマンド作成
            Command = Connection.CreateCommand


            Select Case strLblTypeID

                Case "D01", "G01", "A01" 'ダイレックス、ルミエール、アマゾン用

                Case "Y01" 'ヤサカ用
                    'SQL文の作成 OrderBYなし
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "*"
                    sqlTableName = "Tbl_Remarks"
                    sqlWhereCon = "RemarksID = '" & intRemarksID & "' AND " &
                                  "Remarks1 = '" & e.FormattedValue.ToString() & "'"

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                    'SQL
                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader

                    Do Until Not DataReader.Read
                        ErrorMessage = strErrorMessage4
                        e.Cancel = True
                    Loop

                    DataReader.Close()
                    Connection.Close()
                    DataReader.Dispose()

                Case "M01" 'ラベルタイプM01(マキヤ用)のみフロア番号が登録されているかチェック
                    If intRemarksID = 2 Then
                        'SQL文の作成 OrderBYなし
                        '初期化
                        sqlStatement = ""
                        sqlField1 = ""
                        sqlTableName = ""
                        sqlWhereCon = ""
                        '各ＳＱＬ文の構文設定
                        sqlField1 = "*"
                        sqlTableName = "Tbl_Remarks"
                        sqlWhereCon = "RemarksID = '" & 3 & "' AND " &
                                      "Remarks1 = '" & e.FormattedValue.ToString() & "'"

                        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                        'SQL
                        Command.CommandText = sqlStatement

                        'データリーダーにデータ取得
                        DataReader = Command.ExecuteReader

                        Do Until Not DataReader.Read

                            intChkFlg = 1
                        Loop

                        If intChkFlg = 0 Then
                            ErrorMessage = strErrorMessage3
                            e.Cancel = True
                        End If

                        DataReader.Close()
                        Connection.Close()
                        DataReader.Dispose()
                    End If

                    If intRemarksID = 3 Then
                        'SQL文の作成 OrderBYなし
                        '初期化
                        sqlStatement = ""
                        sqlField1 = ""
                        sqlTableName = ""
                        sqlWhereCon = ""
                        '各ＳＱＬ文の構文設定
                        sqlField1 = "*"
                        sqlTableName = "Tbl_Remarks"
                        sqlWhereCon = "RemarksID = '" & intRemarksID & "' AND " &
                                      "Remarks1 = '" & e.FormattedValue.ToString() & "'"

                        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
                        'SQL
                        Command.CommandText = sqlStatement

                        'データリーダーにデータ取得
                        DataReader = Command.ExecuteReader

                        Do Until Not DataReader.Read
                            ErrorMessage = strErrorMessage4
                            e.Cancel = True
                        Loop

                        DataReader.Close()
                        Connection.Close()
                        DataReader.Dispose()
                    End If

            End Select

            'ＤＢ切断

            Command.Dispose()
            Connection.Dispose()

        End If

        '***備考１の重複入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            'RemarksIDが「２」の場合の処理。「２」は部門番号で、
            '部門はフロアに対して複数登録する為、重複入力チェックはしない
            If intRemarksID = 2 Then

            Else
                For i = 0 To DtgUpdate1.Rows.Count - 2
                    If e.FormattedValue.ToString() = DtgUpdate1.Rows(i).Cells(1).Value Then
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



        End If

        '***備考２の重複入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            'RemarksIDが「２」の場合の処理。「２」は部門番号で、
            '部門は部門番号の重複チェックを行う
            If intRemarksID = 2 Then
                For i = 0 To DtgUpdate1.Rows.Count - 2
                    If e.FormattedValue.ToString() = DtgUpdate1.Rows(i).Cells(2).Value Then
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

            Else

            End If

        End If

        '***備考３の重複入力チェック
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm4" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            For i = 0 To DtgUpdate1.Rows.Count - 2
                If e.FormattedValue.ToString() = DtgUpdate1.Rows(i).Cells(3).Value Then
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


        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤとラベルタイプＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
                strLblTypeID = Wrk_Data1(2, Cntbb)
            End If

        Next Cntbb

        '備考IDの取得
        intRemarksID = 0
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intRemarksID = Wrk_Data2(0, Cntbb)
            End If
        Next Cntbb

        '***備考１の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            Select Case strLblTypeID

                Case "D01", "G01", "A01"

                Case "Y01"
                    '入力された値が数字かチェック
                    If e.FormattedValue.ToString().Length = 1 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If
                    If e.FormattedValue.ToString().Length = 2 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If
                    If e.FormattedValue.ToString().Length = 3 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                    If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                       System.Text.RegularExpressions.Regex.IsMatch( _
                       e.FormattedValue.ToString(), "\s") Then
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage7
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                    End If

                Case "M01"

                    '入力された値が数字かチェック
                    If e.FormattedValue.ToString().Length = 1 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                    If e.FormattedValue.ToString().Length = 2 Then
                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                            Not System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "[0-9][0-9]") Then

                            ErrorMessage = strErrorMessage2
                            e.Cancel = True
                        End If
                    End If

                    If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm2" AndAlso _
                      System.Text.RegularExpressions.Regex.IsMatch( _
                      e.FormattedValue.ToString(), "\s") Then
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage7
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                    End If


            End Select

        End If

        '***備考２の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            Select Case strLblTypeID

                Case "D01", "G01", "A01"

                Case "Y01"

                    If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                        System.Text.RegularExpressions.Regex.IsMatch( _
                        e.FormattedValue.ToString(), "\s") Then
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage7
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                    End If

                Case "M01"
                    If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                       System.Text.RegularExpressions.Regex.IsMatch( _
                       e.FormattedValue.ToString(), "\s") Then
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage7
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                    End If

                    '部門の場合は数字２桁かチェック
                    If intRemarksID = 2 Then

                        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                            System.Text.RegularExpressions.Regex.IsMatch( _
                            e.FormattedValue.ToString(), "\s") Then
                            If e.FormattedValue.ToString().Length >= 2 Then

                                ErrorMessage = strErrorMessage7
                            Else

                                ErrorMessage = strErrorMessage1
                            End If

                            e.Cancel = True
                        End If

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

                        If e.FormattedValue.ToString().Length >= 3 Then
                            If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm3" AndAlso _
                                System.Text.RegularExpressions.Regex.IsMatch( _
                                e.FormattedValue.ToString(), "\w{3,}") Then

                                ErrorMessage = strErrorMessage5
                                e.Cancel = True
                            End If
                        End If
                    End If


            End Select

        End If

        '***備考３の正規表現による制御
        If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm4" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            Select Case strLblTypeID

                Case "D01", "G01", "A01"

                Case "Y01"
                    If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm4" AndAlso _
                       System.Text.RegularExpressions.Regex.IsMatch( _
                       e.FormattedValue.ToString(), "\s") Then
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage7
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                    End If

                Case "M01"
                    If Dgv.Columns(e.ColumnIndex).Name = "DtgUpdateClm4" AndAlso _
                       System.Text.RegularExpressions.Regex.IsMatch( _
                       e.FormattedValue.ToString(), "\s") Then
                        If e.FormattedValue.ToString().Length >= 2 Then

                            ErrorMessage = strErrorMessage7
                        Else

                            ErrorMessage = strErrorMessage1
                        End If

                        e.Cancel = True
                    End If

            End Select

        End If

        If Not ErrorMessage = "" Then
            'エラーメッセージの表示
            MessageBox.Show(ErrorMessage, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
        End If


    End Sub

    '削除画面のデータグリッドビュー
    'CellValidatingイベントハンドラ
    '対象のセルからフォーカスが移動した際に処理を実行
    Private Sub DtgDelete1_CellValidating(ByVal sender As Object, _
        ByVal e As DataGridViewCellValidatingEventArgs) _
        Handles DtgDelete1.CellValidating
        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If
        '***削除チェックボックスの確認
        If DtgDelete1.Columns(e.ColumnIndex).Name = "DtgDeleteClm5" AndAlso _
                Not e.FormattedValue.ToString() = Nothing Then

            intRenewFlg = 1

        End If

    End Sub
    '******************データグリッドビューの入力項目、ＩＭＥ制御************************
    '登録画面の入力制御
    Private Sub DtgInput1_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
             Handles DtgInput1.CellEnter
        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤとラベルタイプＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
                strLblTypeID = Wrk_Data1(2, Cntbb)
            End If

        Next Cntbb

        '備考IDの取得
        intRemarksID = 0
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intRemarksID = Wrk_Data2(0, Cntbb)
            End If
        Next Cntbb

        Select Case strLblTypeID
            Case "D01", "G01", "A01"

            Case "Y01"
                '---- 列番号を調べて制御 ------
                Select Case e.ColumnIndex
                    Case 1, 2
                        'この列は日本語入力ON
                        DtgInput1.ImeMode = Windows.Forms.ImeMode.Hiragana
                    Case 0
                        'この列はIME無効(半角英数のみ)
                        DtgInput1.ImeMode = Windows.Forms.ImeMode.Disable
                End Select

            Case "M01"
                If intRemarksID = 2 Then
                    '---- 列番号を調べて制御 ------
                    Select Case e.ColumnIndex
                        Case 2
                            'この列は日本語入力ON
                            DtgInput1.ImeMode = Windows.Forms.ImeMode.Hiragana
                        Case 0, 1
                            'この列はIME無効(半角英数のみ)
                            DtgInput1.ImeMode = Windows.Forms.ImeMode.Disable
                    End Select

                Else
                    '---- 列番号を調べて制御 ------
                    Select Case e.ColumnIndex
                        Case 1, 2
                            'この列は日本語入力ON
                            DtgInput1.ImeMode = Windows.Forms.ImeMode.Hiragana
                        Case 0
                            'この列はIME無効(半角英数のみ)
                            DtgInput1.ImeMode = Windows.Forms.ImeMode.Disable
                    End Select
                End If


        End Select
        
    End Sub
    '変更画面の入力制御
    Private Sub DtgUpdate1_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
             Handles DtgUpdate1.CellEnter
        intTokID = 0
        strLblTypeID = ""
        For Cntbb = 0 To Wrk_Data1.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbTok1.Text = Wrk_Data1(1, Cntbb) Then
                '二次元配列の得意先ＩＤとラベルタイプＩＤを出力
                intTokID = Wrk_Data1(0, Cntbb)
                strLblTypeID = Wrk_Data1(2, Cntbb)
            End If

        Next Cntbb

        '備考IDの取得
        intRemarksID = 0
        For Cntbb = 0 To Wrk_Data2.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmbRem1.Text = Wrk_Data2(1, Cntbb) Then
                '二次元配列の得意先ＩＤと備考名を出力
                intRemarksID = Wrk_Data2(0, Cntbb)
            End If
        Next Cntbb

        Select Case strLblTypeID
            Case "D01", "G01", "A01"

            Case "Y01"

                '---- 列番号を調べて制御 ------
                Select Case e.ColumnIndex
                    Case 2, 3
                        'この列は日本語入力ON
                        DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Hiragana
                    Case 1
                        'この列はIME無効(半角英数のみ)
                        DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Disable
                End Select

            Case "M01"
                If intRemarksID = 2 Then
                    '---- 列番号を調べて制御 ------
                    Select Case e.ColumnIndex
                        Case 3
                            'この列は日本語入力ON
                            DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Hiragana
                        Case 1, 2
                            'この列はIME無効(半角英数のみ)
                            DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Disable
                    End Select

                Else
                    '---- 列番号を調べて制御 ------
                    Select Case e.ColumnIndex
                        Case 2, 3
                            'この列は日本語入力ON
                            DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Hiragana
                        Case 1
                            'この列はIME無効(半角英数のみ)
                            DtgUpdate1.ImeMode = Windows.Forms.ImeMode.Disable
                    End Select
                End If
                

        End Select

        
    End Sub
    '*****************END*******************************************
    'DataErrorイベントハンドラ
    Private Sub DtgInput1_DataError(ByVal sender As Object, _
            ByVal e As DataGridViewDataErrorEventArgs) _
            Handles DtgUpdate1.DataError

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