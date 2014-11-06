Imports System.Data.SQLite
Imports System.Runtime.InteropServices
Imports System
Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports System.Net
Imports System.Drawing.Imaging
Imports System.IO
Imports DllSBPL_VB

Public Class frm_LblPrint

    '====================================================================================================
    ' デバイスコンテキストにオブジェクトの選択をします
    '====================================================================================================
    <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
    Friend Shared Function SelectObject(ByVal hObject As IntPtr, ByVal hFont As IntPtr) As IntPtr
    End Function

    '====================================================================================================
    ' グラフィックオブジェクトを削除し、システムリソースの解放をします
    '====================================================================================================
    <System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")> _
    Friend Shared Function DeleteObject(ByVal hObject As IntPtr) As Boolean
    End Function

    Dim strCenName As String
    Dim strCenID As String
    Dim strRemarks1 As String
    Dim strFloorName As String
    Dim strCommentName As String

    Dim intRenewFlg As Integer = 0

    '印刷を行う印刷領域
    Dim PDoc1 As New System.Drawing.Printing.PrintDocument
    '出力対象件数の最大値
    Dim Dmax As Integer
    '共通のカウンタ
    Dim WIdx As Integer
    Dim Fst_sw As Boolean
    'ワークエリア（ラベルタイプD01用(ダイレックス、サンドラッグで使用)）
    Dim WrkD01_Data1() As String
    Dim WrkD01_Data2() As String
    Dim WrkD01_Data3() As String
    Dim WrkD01_Data4() As String
    Dim WrkD01_Data5() As String
    Dim WrkD01_Data6() As String
    Dim WrkD01_Data8() As String
    Dim WrkD01_Data9() As String
    Dim WrkD01_Data10() As String

    'ワークエリア（ラベルタイプG01用（汎用ラベルで使用））
    Dim WrkG01_Data1() As String
    Dim WrkG01_Data2() As String
    Dim WrkG01_Data3() As String
    Dim WrkG01_Data4() As String

    'ワークエリア（ラベルタイプA01用（アマゾンで使用））
    Dim WrkA01_Data1() As String
    Dim WrkA01_Data2() As String
    Dim WrkA01_Data3() As String
    Dim WrkA01_Data4() As String

    'ワークエリア（ラベルタイプY01用（ヤサカで使用））
    Dim WrkY01_Data1() As String
    Dim WrkY01_Data2() As String
    Dim WrkY01_Data3() As String
    Dim WrkY01_Data4() As String
    Dim WrkY01_Data5() As String
    Dim WrkY01_Data6() As String
    Dim WrkY01_Data7() As String
    Dim WrkY01_Data8() As String

    'ワークエリア（ラベルタイプM01用（マキヤで使用））
    Dim WrkM01_Data1() As String
    Dim WrkM01_Data2() As String
    Dim WrkM01_Data3() As String
    Dim WrkM01_Data4() As String
    Dim WrkM01_Data5() As String
    Dim WrkM01_Data6() As String
    Dim WrkM01_Data7() As String
    Dim WrkM01_Data8() As String
    Dim WrkM01_Data9() As String
    Dim WrkM01_Data10() As String
    Dim WrkM01_Data11() As String
    Dim WrkM01_Data12() As String
    Dim WrkM01_Data13() As String
    Dim WrkM01_Data14() As String

    'ワークエリア（ラベルタイプM02用（第２関東のMrMaxで使用））
    Dim WrkM02_Data1() As String
    Dim WrkM02_Data2() As String
    Dim WrkM02_Data3() As String
    Dim WrkM02_Data4() As String
    Dim WrkM02_Data5() As String
    Dim WrkM02_Data6() As String
    Dim WrkM02_Data7() As String
    Dim WrkM02_Data8() As String
    Dim WrkM02_Data9() As String


    '共通エラーメッセージエリア
    Dim ErrorMessage100 As String = "情報システム部に連絡して下さい。" & vbCrLf & "エラー番号：１"

    Dim Wrk_DataTok(,) As String
    Dim Wrk_DataClm(,) As String
    Dim Wrk_DataCen(,) As String
    Dim Wrk_DataRe(,) As String
    Dim Wrk_DataRe2(,) As String
    Dim intTokID As Integer
    Dim strLblTye As String

    Dim sqlStatement As String = ""
    Dim sqlSelect As String = "SELECT "
    Dim sqlFrom As String = " FROM "
    Dim sqlWhere As String = " WHERE "
    Dim sqlOrderBy As String = " ORDER BY "
    Dim sqlUpdate As String = " UPDATE "
    Dim sqlSet As String = " SET "

    Dim sqlField1 As String = ""
    Dim sqlTableName As String = ""
    Dim sqlWhereCon As String = ""
    Dim sqlOrderByCon As String = ""
    Dim sqlSetCon As String = ""

    Dim strSelPrint As String
    Dim strToriCode As String = 0

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

    Private LogoPath As String

    Private Sub TopForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterParent


        MessageBox.Show("ローカルgitテスト4")
        MessageBox.Show("ローカルgitテスト5")
        MessageBox.Show("ローカルgitテスト6")




        'パネルの初期表示設定
        LblPriPnl.Visible = False
        TopPanl.Visible = True

        ' 出力先プリンタ名を設定します
        cboPrinter.Items.Clear()
        cboPrinter.Items.Add(String.Empty)
        For Each p As String In Printing.PrinterSettings.InstalledPrinters
            cboPrinter.Items.Add(p)
        Next

    End Sub
    '印刷ボタン押下時のイベント
    Private Sub OutPutBtn1_Click(sender As Object, e As EventArgs) Handles BtnPrint1.Click
         '＊＊＊空白カラムのチェック処理＊＊＊
        '変数宣言
        Dim intSpaceCnt As Integer = 0
        Dim intSpacecnt2 As Integer = 0
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0

        'ラベルタイプＤ０１用変数（ダイレックス、サンドラッグ用）
        Dim strOrderDay1 As String
        Dim strDelivery1 As String
        Dim strOricon1 As String
        Dim strCase1 As String

        'ラベルタイプＧ０１用変数（ルミエール等汎用ラベル用）
        Dim strShipDay2 As String
        Dim strNumber As String

        'ラベルタイプＡ０１用変数（アマゾン用）
        Dim strDelivery3 As String
        Dim strPoNo As String
        Dim strKonposu As String

        'ラベルタイプＹ０１用変数（ヤサカ用）
        Dim strDelivery4 As String
        Dim strNumber2 As String

        'ラベルタイプＭ０１用変数（マキヤ用）
        Dim strStrno As String
        Dim strBUmon As String
        Dim strDelivery5 As String
        Dim strTokbaiday1 As String
        Dim strSeicon1 As String = 0
        Dim strBara1 As String = 0

        'ラベルタイプＭ０２用変数（第２関東MrMax用）
        Dim strDelivery6 As String
        Dim strConpou1 As String
        Dim strBunrui1 As String


        '共通変数
        Dim intOrderDay1 As Integer
        Dim intDelivery1 As Integer
        Dim intOrderDay2 As Integer
        Dim intDelivery2 As Integer

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        'エラーメッセージエリア
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "発注日と納品日は必ず入力して下さい"
        Dim strErrorMessage2 As String = "ケース数とオリコン数は必ずどちらか入力して下さい"
        Dim strErrorMessage4 As String = "出荷日と納品日は必ず入力して下さい"
        Dim strErrorMessage5 As String = "個数は必ず入力して下さい"
        Dim strErrorMessage6 As String = "値を入力して下さい"
        Dim strErrorMessage7 As String = "納品日は発注日以降にして下さい"
        Dim strErrorMessage8 As String = "納品日は出荷日以降にして下さい"
        Dim strErrorMessage9 As String = "ＰＯ番号、納品日、梱包数は必ず入力して下さい"
        Dim strErrorMessage10 As String = "個口数とお届日は必ず入力して下さい"
        Dim strErrorMessage11 As String = "納品日は必ず入力して下さい"
        Dim strErrorMessage12 As String = "正梱数とバラ数は必ずどちらか入力して下さい"
        Dim strErrorMessage13 As String = "定店、定本、特店、特本、客注から必ず１つチェックして下さい"
        Dim strErrorMessage14 As String = "納品日は必ず入力して下さい"
        Dim strErrorMessage15 As String = "梱包数は必ず入力して下さい"
        Dim strErrorMessage16 As String = "分類コードは必ず入力して下さい"
        Dim strErrorMessage17 As String = "客、優、通、配、新、特、手、■から必ず１つ選択して下さい"
        Dim strErrorMessage18 As String = "店番は必ず入力して下さい"
        Dim strErrorMessage19 As String = "部門番号は必ず入力して下さい"

        Dim intErrorOrdinate As String = ""
        Dim intErrorFlg As Integer = 0

        'データグリッドのマルチフォーカスをＯＦＦ
        Me.DtgLblPri.MultiSelect = False

        'データグリッドのニューメリックチェック
        For i = 0 To DtgLblPri.Rows.Count - 1

            Select Case strLblTye

                Case "D01" 'ダイレックス用

                    strOrderDay1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm5").Value
                    strDelivery1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm6").Value
                    strCase1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm7").Value
                    strOricon1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm8").Value
                    intOrderDay1 = CType(DtgLblPri.Rows(i).Cells("DtgLblPriClm5").Value, Integer)
                    intDelivery1 = CType(DtgLblPri.Rows(i).Cells("DtgLblPriClm6").Value, Integer)

                    'ケース数もしくはオリコン数に値が入っている場合、発注日と納品日の空白行チェック
                    If Not strCase1 = "" Or
                       Not strOricon1 = "" Then
                        '発注日もしくは納品日がスペースの場合
                        If strOrderDay1 = "" Or
                           strDelivery1 = "" Then

                            If intErrorFlg = 0 Then

                                ErrorMessage = strErrorMessage1
                                If strDelivery1 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(5).Selected() = True
                                    intRow = i
                                    intClm = 5
                                    intErrorFlg = 1
                                End If
                                If strOrderDay1 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(4).Selected() = True
                                    intRow = i
                                    intClm = 4
                                    intErrorFlg = 1
                                End If

                            End If

                        End If
                    Else

                    End If

                    '発注日もしくは納品日に値が入っている場合、ケース数とオリコン数の空白行をチェック。ただし、片方に値が入っていればok
                    If Not strOrderDay1 = "" Or
                       Not strDelivery1 = "" Then

                        If intErrorFlg = 0 Then
                            'ケース数とオリコン数が両方スペースの場合
                            If strCase1 = "" And
                               strOricon1 = "" Then

                                ErrorMessage = strErrorMessage2
                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(6).Selected() = True
                                Me.DtgLblPri.Rows(i).Cells(7).Selected() = True
                                intRow = i
                                intClm = 6
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '全ての発注日と納品日、オリコン、ケースがスペースの場合のチェック
                    If strCase1 = "" And
                       strOricon1 = "" And
                       strOrderDay1 = "" And
                       strDelivery1 = "" Then

                        intSpaceCnt = intSpaceCnt + 1

                    End If

                    '発注日と納品日の値チェック。発注日が納品日と同じもしくは小さいか確認
                    If intOrderDay1 > intDelivery1 Then
                        If intErrorFlg = 0 Then
                            ErrorMessage = strErrorMessage7
                            Me.DtgLblPri.MultiSelect = True
                            Me.DtgLblPri.Rows(i).Cells(4).Selected() = True
                            Me.DtgLblPri.Rows(i).Cells(5).Selected() = True
                            intRow = i
                            intClm = 4
                            intErrorFlg = 1
                        End If
                    End If


                    strCase1 = ""
                    strOricon1 = ""
                    strOrderDay1 = ""
                    strDelivery1 = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkD01_Data1(1)
                    ReDim WrkD01_Data2(1)
                    ReDim WrkD01_Data3(1)
                    ReDim WrkD01_Data4(1)
                    ReDim WrkD01_Data5(1)
                    ReDim WrkD01_Data6(1)
                    ReDim WrkD01_Data8(1)
                    ReDim WrkD01_Data9(1)

                Case "G01" '汎用ラベル用
                    strShipDay2 = DtgLblPri.Rows(i).Cells("DtgLblPriClm11").Value
                    strNumber = DtgLblPri.Rows(i).Cells("DtgLblPriClm13").Value
                    intOrderDay2 = CType(DtgLblPri.Rows(i).Cells("DtgLblPriClm11").Value, Integer)
                    intDelivery2 = CType(DtgLblPri.Rows(i).Cells("DtgLblPriClm12").Value, Integer)

                    '個数に値が入っている場合、出荷日の空白行チェック
                    If Not strNumber = "" Then
                        '出荷日がスペースの場合
                        If strShipDay2 = "" Then

                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage4

                                If strShipDay2 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(10).Selected() = True
                                    intRow = i
                                    intClm = 10
                                    intErrorFlg = 1
                                End If


                            End If

                        End If
                    Else

                    End If

                    '出荷日に値が入っている場合、個数の空白行をチェック
                    If Not strShipDay2 = "" Then

                        If intErrorFlg = 0 Then
                            '個数がスペースの場合
                            If strNumber = "" Then
                                ErrorMessage = strErrorMessage5

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(12).Selected() = True
                                intRow = i
                                intClm = 12
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '全ての発注日、個数がスペースの場合
                    If strShipDay2 = "" And
                       strNumber = "" Then
                        intSpaceCnt = intSpaceCnt + 1

                    End If

                    '発注日と納品日の値チェック。発注日が納品日と同じもしくは小さいか確認
                    If Not DtgLblPri.Rows(i).Cells("DtgLblPriClm12").Value = Nothing Then
                        If intOrderDay2 > intDelivery2 Then
                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage8
                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(10).Selected() = True
                                Me.DtgLblPri.Rows(i).Cells(11).Selected() = True
                                intRow = i
                                intClm = 10
                                intErrorFlg = 1
                            End If
                        End If
                    End If

                    strShipDay2 = ""
                    strNumber = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkG01_Data1(1)
                    ReDim WrkG01_Data2(1)
                    ReDim WrkG01_Data3(1)
                    ReDim WrkG01_Data4(1)


                Case "A01" 'アマゾン用
                    strPoNo = DtgLblPri.Rows(i).Cells("DtgLblPriClm14").Value
                    strKonposu = DtgLblPri.Rows(i).Cells("DtgLblPriClm18").Value
                    strDelivery3 = DtgLblPri.Rows(i).Cells("DtgLblPriClm19").Value
                    'ＰＯ番号と納品日、梱包数がスペースでない場合
                    If Not strPoNo = "" Or
                        Not strDelivery3 = "" Or
                         Not strKonposu = "" Then

                        'ＰＯ番号と納品日、梱包数がスペースの場合
                        If strPoNo = "" Or
                            strDelivery3 = "" Or
                             strKonposu = "" Then

                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage9

                                If strDelivery3 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(18).Selected() = True
                                    intRow = i
                                    intClm = 18
                                    intErrorFlg = 1
                                End If
                                If strKonposu = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(17).Selected() = True
                                    intRow = i
                                    intClm = 17
                                    intErrorFlg = 1
                                End If
                                If strPoNo = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(13).Selected() = True
                                    intRow = i
                                    intClm = 13
                                    intErrorFlg = 1
                                End If


                            End If

                        End If

                    End If

                    '全てのＰＯ番号と納品日、梱包数がスペースの場合
                    If strPoNo = "" And
                       strKonposu = "" And
                       strDelivery3 = "" Then

                        intSpaceCnt = intSpaceCnt + 1
                    End If

                    strPoNo = ""
                    strKonposu = ""
                    strDelivery3 = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkA01_Data1(1)
                    ReDim WrkA01_Data2(1)
                    ReDim WrkA01_Data3(1)
                    ReDim WrkA01_Data4(1)

                Case "Y01" 'ヤサカ用
                    strDelivery4 = DtgLblPri.Rows(i).Cells("DtgLblPriClm23").Value
                    strNumber2 = DtgLblPri.Rows(i).Cells("DtgLblPriClm24").Value

                    '部門とお届日、個口数がスペースでない場合
                    If Not strNumber2 = "" Or
                        Not strDelivery4 = "" Then

                        '部門とお届日、個口数がスペースの場合
                        If strNumber2 = "" Or
                            strDelivery4 = "" Then

                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage10

                                If strDelivery4 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(22).Selected() = True
                                    intRow = i
                                    intClm = 22
                                    intErrorFlg = 1
                                End If
                                If strNumber2 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(23).Selected() = True
                                    intRow = i
                                    intClm = 23
                                    intErrorFlg = 1
                                End If

                            End If

                        End If

                        '印刷ボタン用
                        '共通ワークエリアの初期化
                        ReDim WrkY01_Data1(1)
                        ReDim WrkY01_Data2(1)
                        ReDim WrkY01_Data3(1)
                        ReDim WrkY01_Data4(1)
                        ReDim WrkY01_Data5(1)
                        ReDim WrkY01_Data6(1)
                        ReDim WrkY01_Data7(1)

                    End If

                    '全ての部門とお届日、個口数がスペースの場合
                    If strNumber2 = "" And
                       strDelivery4 = "" Then

                        intSpaceCnt = intSpaceCnt + 1
                    End If

                Case "M01" 'マキヤ用
                    strStrno = DtgLblPri.Rows(i).Cells("DtgLblPriClm26").Value
                    strBUmon = DtgLblPri.Rows(i).Cells("DtgLblPriClm29").Value
                    strTokbaiday1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm35").Value
                    strDelivery5 = DtgLblPri.Rows(i).Cells("DtgLblPriClm36").Value
                    strSeicon1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm37").Value
                    strBara1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm38").Value

                    '部門名、ラベル区分、納品日、正梱数もしくはバラ数に値が入っている場合、店番の空白行チェック
                    If Not strSeicon1 = "" Or
                       Not strBara1 = "" Or
                       Not strBUmon = "" Or
                       Not strDelivery5 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm30").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm31").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm32").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm33").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm34").Value = True Then
                        '店番がスペースの場合
                        If strStrno = "" Then

                            If intErrorFlg = 0 Then

                                ErrorMessage = strErrorMessage18

                                If strStrno = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(25).Selected() = True
                                    intRow = i
                                    intClm = 25
                                    intErrorFlg = 1
                                End If

                            End If

                        End If
                  
                    End If

                    '店番、ラベル区分、納品日、正梱数もしくはバラ数に値が入っている場合、部門名の空白行チェック
                    If Not strSeicon1 = "" Or
                       Not strBara1 = "" Or
                       Not strStrno = "" Or
                       Not strDelivery5 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm30").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm31").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm32").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm33").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm34").Value = True Then
                        '部門名がスペースの場合
                        If strBUmon = "" Then

                            If intErrorFlg = 0 Then

                                ErrorMessage = strErrorMessage19

                                If strBUmon = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(28).Selected() = True
                                    intRow = i
                                    intClm = 28
                                    intErrorFlg = 1
                                End If

                            End If

                        End If


                    End If

                    '定店、定本、特店、特本、客注の入力空白チェック。
                    If Not strDelivery5 = "" Or
                       Not strTokbaiday1 = "" Or
                       Not strSeicon1 = "" Or
                       Not strBara1 = "" Or
                       Not strStrno = "" Or
                       Not strBUmon = "" Then

                        If DtgLblPri.Rows(i).Cells("DtgLblPriClm30").Value = False And
                           DtgLblPri.Rows(i).Cells("DtgLblPriClm31").Value = False And
                           DtgLblPri.Rows(i).Cells("DtgLblPriClm32").Value = False And
                           DtgLblPri.Rows(i).Cells("DtgLblPriClm33").Value = False And
                           DtgLblPri.Rows(i).Cells("DtgLblPriClm34").Value = False Then
                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage13

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(29).Selected() = True
                                intRow = i
                                intClm = 29
                                intErrorFlg = 1
                            End If

                        End If

                    End If

                    '店番、部門番号、ラベル区分、正梱数もしくはバラ数に値が入っている場合、納入日の空白行チェック
                    If Not strSeicon1 = "" Or
                       Not strBara1 = "" Or
                       Not strStrno = "" Or
                       Not strBUmon = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm30").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm31").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm32").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm33").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm34").Value = True Then
                        '納品日がスペースの場合
                        If strDelivery5 = "" Then

                            If intErrorFlg = 0 Then

                                ErrorMessage = strErrorMessage11

                                If strDelivery5 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(35).Selected() = True
                                    intRow = i
                                    intClm = 35
                                    intErrorFlg = 1
                                End If

                            End If

                        End If


                    End If

                    '店番と部門名とコメントもしくは納入日に値が入っている場合、正梱数とバラ数の空白行をチェック。
                    'ただし、片方に値が入っていれば問題なし
                    If Not strDelivery5 = "" Or
                       Not strStrno = "" Or
                       Not strBUmon = "" Or
                       Not strTokbaiday1 = "" Then

                        If intErrorFlg = 0 Then
                            '正梱数とバラ数が両方スペースの場合
                            If strSeicon1 = "" And
                               strBara1 = "" Then

                                ErrorMessage = strErrorMessage12
                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(36).Selected() = True
                                Me.DtgLblPri.Rows(i).Cells(37).Selected() = True
                                intRow = i
                                intClm = 36
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '店番、部門名、ラベル区分、特売開始日、納入日、正梱数、バラ数がスペースの場合のチェック
                    If strTokbaiday1 = "" And
                       strDelivery5 = "" And
                       strSeicon1 = "" And
                       strBara1 = "" And
                       strStrno = "" And
                       strBUmon = "" And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm30").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm31").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm32").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm33").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm34").Value = False Then

                        intSpaceCnt = intSpaceCnt + 1

                    End If


                    

                    strTokbaiday1 = ""
                    strDelivery5 = ""
                    strSeicon1 = ""
                    strBara1 = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkM01_Data1(1)
                    ReDim WrkM01_Data2(1)
                    ReDim WrkM01_Data3(1)
                    ReDim WrkM01_Data4(1)
                    ReDim WrkM01_Data5(1)
                    ReDim WrkM01_Data6(1)
                    ReDim WrkM01_Data7(1)
                    ReDim WrkM01_Data8(1)
                    ReDim WrkM01_Data9(1)
                    ReDim WrkM01_Data10(1)
                    ReDim WrkM01_Data11(1)
                    ReDim WrkM01_Data12(1)
                    ReDim WrkM01_Data13(1)
                    ReDim WrkM01_Data14(1)

                Case "M02" '第２関東MrMax用
                    strDelivery6 = DtgLblPri.Rows(i).Cells("DtgLblPriClm49").Value
                    strConpou1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm50").Value
                    strBunrui1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm51").Value

                    '梱包数、分類コード、納品区分に値が入っている場合、納品日の空白行チェック
                    If Not strConpou1 = "" Or
                       Not strBunrui1 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = True Then

                        '納品日がスペースの場合
                        If strDelivery6 = "" Then

                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage14

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(48).Selected() = True
                                intRow = i
                                intClm = 48
                                intErrorFlg = 1

                            End If

                        End If
                    Else

                    End If

                    '納品日、分類コード、納品区分に値が入っている場合、梱包数の空白行をチェック
                    If Not strDelivery6 = "" Or
                       Not strBunrui1 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = True Then

                        If intErrorFlg = 0 Then
                            '梱包数がスペースの場合
                            If strConpou1 = "" Then
                                ErrorMessage = strErrorMessage15

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(50).Selected() = True
                                intRow = i
                                intClm = 50
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '納品日、梱包数、納品区分に値が入っている場合、分類コードの空白行をチェック
                    If Not strDelivery6 = "" Or
                       Not strConpou1 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = True Then

                        If intErrorFlg = 0 Then
                            '分類コードがスペースの場合
                            If strBunrui1 = "" Then
                                ErrorMessage = strErrorMessage16

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(49).Selected() = True
                                intRow = i
                                intClm = 49
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '納品日、梱包数、納品区分に値が入っている場合、納品区分の空白行をチェック
                    If Not strDelivery6 = "" Or
                       Not strConpou1 = "" Or
                       Not strBunrui1 = "" Then

                        If intErrorFlg = 0 Then
                            '納品区分が全て選択されていない場合
                            If DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = False Then

                                ErrorMessage = strErrorMessage17

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(40).Selected() = True
                                intRow = i
                                intClm = 40
                                intErrorFlg = 1
                            End If
                        End If

                    End If


                    '全ての納品日、梱包数、分類コード、納品区分がスペースの場合
                    If strDelivery6 = "" And
                       strConpou1 = "" And
                       strBunrui1 = "" And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = False Then
                        intSpaceCnt = intSpaceCnt + 1

                    End If

                    strConpou1 = ""
                    strDelivery6 = ""
                    strBunrui1 = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkM02_Data1(1)
                    ReDim WrkM02_Data2(1)
                    ReDim WrkM02_Data3(1)
                    ReDim WrkM02_Data4(1)
                    ReDim WrkM02_Data5(1)
                    ReDim WrkM02_Data6(1)
                    ReDim WrkM02_Data7(1)
                    ReDim WrkM02_Data8(1)
                    ReDim WrkM02_Data9(1)



                Case Else
                    End

            End Select

        Next

        '必須入力項目で全てスペースの場合
        If DtgLblPri.Rows.Count = intSpaceCnt Then
            Select Case strLblTye

                Case "D01" 'ダイレックス用
                    intRow = 0
                    intClm = 4

                Case "G01" 'ルミエール用
                    intRow = 0
                    intClm = 10

                Case "A01" 'アマゾン用
                    intRow = 0
                    intClm = 13

                Case "Y01" 'ヤサカ用
                    intRow = 0
                    intClm = 22

                Case "M01" 'マキヤ用
                    intRow = 0
                    intClm = 25

                Case "M02" '第２関東MrMax用
                    intRow = 0
                    intClm = 48

            End Select
            ErrorMessage = strErrorMessage6
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

            PDoc1.PrinterSettings.PrinterName = strSelPrint

            '再印刷用に途中から印刷する為の処理(ページ指定印刷の要望があれば改造します。)
            Dim PDlg As New PrintDialog()
            PDlg.Document = PrintDocument1
            'If (PDlg.ShowDialog = DialogResult.OK) Then

            '印刷処理の実行
            PDoc1.Print()

            'PDlg.Document.Print() 'イベントを送信
            'End If
            intRenewFlg = 0
        End If



        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub FormOpenBtn1_Click(sender As Object, e As EventArgs) Handles BtnInputProcess.Click

        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer
        Dim strClmName As String
        Dim intOrdBtnCooX As Integer = 0
        Dim intOrdBtnCooY As Integer = 0
        Dim intDelBtnCooX As Integer = 0
        Dim intDelBtnCooY As Integer = 0
        Dim intCleBtnX As Integer = 0
        Dim intCleBtnY As Integer = 0
        Dim strOrdBtnTxt As String = ""
        Dim strDelBtnTxt As String = ""
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim Connection2 As New SQLiteConnection
        Dim Command2 As SQLiteCommand
        Dim DataReader2 As SQLiteDataReader
        Dim Cntup As Integer = 0
        Dim i As Integer = 0

        'プリンターの選択がされなかった場合のエラー
        If cboPrinter.SelectedItem = "" Then
            MsgBox("プリンターを選択してください", MsgBoxStyle.Critical, "エラー")
            Exit Sub
        Else
            strSelPrint = cboPrinter.SelectedItem
        End If

        If BtnRadio1.Checked = True Then
            Me.Text = BtnRadio1.Text & "－ラベル発行枚数入力画面"
            LblCen5.ForeColor = Color.BlueViolet
            LblCen10.BackColor = Color.BlueViolet
            LblCen10.Text = BtnRadio1.Text
            strToriCode = "001997"
        End If
        If BtnRadio2.Checked = True Then
            Me.Text = BtnRadio2.Text & "－ラベル発行枚数入力画面"
            LblCen5.ForeColor = Color.ForestGreen
            LblCen10.BackColor = Color.ForestGreen
            LblCen10.Text = BtnRadio2.Text
            strToriCode = "006013"
        End If

        '共通ワークエリアの初期化
        ReDim Wrk_DataRe(2, 1)
        ReDim Wrk_DataRe2(2, 1)

        '*********得意先IDの取得**********
        intTokID = 0
        strLblTye = ""
        For Cntbb = 0 To Wrk_DataTok.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmdTok1.Text = Wrk_DataTok(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_DataTok(0, Cntbb)
            End If
        Next Cntbb

        '接続文字列を設定
        Connection2.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection2.Open()

        'センターＩＤの取得
        'コマンド作成
        Command2 = Connection2.CreateCommand
        'SQL文の作成 OrderBYなし
        '初期化
        sqlStatement = ""
        sqlField1 = ""
        sqlTableName = ""
        '各ＳＱＬ文の構文設定
        sqlField1 = "CenId"
        sqlTableName = "Tbl_CenMas"
        sqlWhereCon = "CorpID = " & intTokID & " AND " &
                      "CenName = '" & CmdCen1.Text & "'"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

        Command2.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader2 = Command2.ExecuteReader


        Do Until Not DataReader2.Read
            'ワークエリアへのセット
            strCenID = DataReader2.Item("CenID").ToString
        Loop

        'ＤＢ切断
        Connection2.Close()

        DataReader2.Dispose()
        Command2.Dispose()
        Connection2.Dispose()

        'パネルの表示＆非表示
        LblPriPnl.Visible = True
        TopPanl.Visible = False
        Me.DtgLblPri.Focus()

        intRenewFlg = 0
        DtgLblPri.Rows.Clear()

        '得意先が増えた場合、項目追加が必要
        'データグリッドビューの列を初期表示する
        DtgLblPriClm1.Visible = True
        DtgLblPriClm2.Visible = True
        DtgLblPriClm3.Visible = True
        DtgLblPriClm4.Visible = True
        DtgLblPriClm5.Visible = True
        DtgLblPriClm6.Visible = True
        DtgLblPriClm7.Visible = True
        DtgLblPriClm8.Visible = True
        DtgLblPriClm9.Visible = True
        DtgLblPriClm10.Visible = True
        DtgLblPriClm11.Visible = True
        DtgLblPriClm12.Visible = True
        DtgLblPriClm13.Visible = True
        DtgLblPriClm14.Visible = True
        DtgLblPriClm15.Visible = True
        DtgLblPriClm16.Visible = True
        DtgLblPriClm17.Visible = True
        DtgLblPriClm18.Visible = True
        DtgLblPriClm19.Visible = True
        DtgLblPriClm20.Visible = True
        DtgLblPriClm21.Visible = True
        DtgLblPriClm22.Visible = True
        DtgLblPriClm23.Visible = True
        DtgLblPriClm24.Visible = True
        DtgLblPriClm25.Visible = True
        DtgLblPriClm26.Visible = True
        DtgLblPriClm27.Visible = True
        DtgLblPriClm28.Visible = True
        DtgLblPriClm29.Visible = True
        DtgLblPriClm30.Visible = True
        DtgLblPriClm31.Visible = True
        DtgLblPriClm32.Visible = True
        DtgLblPriClm33.Visible = True
        DtgLblPriClm34.Visible = True
        DtgLblPriClm35.Visible = True
        DtgLblPriClm36.Visible = True
        DtgLblPriClm37.Visible = True
        DtgLblPriClm38.Visible = True
        DtgLblPriClm39.Visible = True
        DtgLblPriClm40.Visible = True
        DtgLblPriClm41.Visible = True
        DtgLblPriClm42.Visible = True
        DtgLblPriClm43.Visible = True
        DtgLblPriClm44.Visible = True
        DtgLblPriClm45.Visible = True
        DtgLblPriClm46.Visible = True
        DtgLblPriClm47.Visible = True
        DtgLblPriClm48.Visible = True
        DtgLblPriClm49.Visible = True
        DtgLblPriClm50.Visible = True
        DtgLblPriClm51.Visible = True
        DtgLblPriClm52.Visible = True


        '共通ワークエリアの初期化
        ReDim Wrk_DataClm(2, 1)

        '接続文字列を設定
        Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection.Open()

        '*********各得意先が使用するカラム名を取得*************
        'コマンド作成
        Command = Connection.CreateCommand

        'SQL文の作成
        '初期化
        sqlStatement = ""
        sqlField1 = ""
        sqlTableName = ""
        sqlWhereCon = ""
        '各ＳＱＬ文の構文設定
        sqlField1 = "DtgClmName,Tbl_CorpMas.LblTypeID"
        sqlTableName = "Tbl_CorpMas,Tbl_DtgClmOp"
        sqlWhereCon = "Tbl_CorpMas.CorpID = " & intTokID & " AND " &
                      "Not Tbl_CorpMas.LblTypeID = Tbl_DtgClmOp.LblTypeID"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

        Command.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader = Command.ExecuteReader
        Do Until Not DataReader.Read
            strClmName = DataReader.Item("DtgClmName").ToString
            strLblTye = DataReader.Item("LblTypeID").ToString
            '得意先増えたら
            '応急処置のソース。カラム名を指定して処理するように変更する。
            Select Case strClmName
                Case "DtgLblPriClm1"
                    DtgLblPriClm1.Visible = False
                Case "DtgLblPriClm2"
                    DtgLblPriClm2.Visible = False
                Case "DtgLblPriClm3"
                    DtgLblPriClm3.Visible = False
                Case "DtgLblPriClm4"
                    DtgLblPriClm4.Visible = False
                Case "DtgLblPriClm5"
                    DtgLblPriClm5.Visible = False
                Case "DtgLblPriClm6"
                    DtgLblPriClm6.Visible = False
                Case "DtgLblPriClm7"
                    DtgLblPriClm7.Visible = False
                Case "DtgLblPriClm8"
                    DtgLblPriClm8.Visible = False
                Case "DtgLblPriClm9"
                    DtgLblPriClm9.Visible = False
                Case "DtgLblPriClm10"
                    DtgLblPriClm10.Visible = False
                Case "DtgLblPriClm11"
                    DtgLblPriClm11.Visible = False
                Case "DtgLblPriClm12"
                    DtgLblPriClm12.Visible = False
                Case "DtgLblPriClm13"
                    DtgLblPriClm13.Visible = False
                Case "DtgLblPriClm14"
                    DtgLblPriClm14.Visible = False
                Case "DtgLblPriClm15"
                    DtgLblPriClm15.Visible = False
                Case "DtgLblPriClm16"
                    DtgLblPriClm16.Visible = False
                Case "DtgLblPriClm17"
                    DtgLblPriClm17.Visible = False
                Case "DtgLblPriClm18"
                    DtgLblPriClm18.Visible = False
                Case "DtgLblPriClm19"
                    DtgLblPriClm19.Visible = False
                Case "DtgLblPriClm20"
                    DtgLblPriClm20.Visible = False
                Case "DtgLblPriClm21"
                    DtgLblPriClm21.Visible = False
                Case "DtgLblPriClm22"
                    DtgLblPriClm22.Visible = False
                Case "DtgLblPriClm23"
                    DtgLblPriClm23.Visible = False
                Case "DtgLblPriClm24"
                    DtgLblPriClm24.Visible = False
                Case "DtgLblPriClm25"
                    DtgLblPriClm25.Visible = False
                Case "DtgLblPriClm26"
                    DtgLblPriClm26.Visible = False
                Case "DtgLblPriClm27"
                    DtgLblPriClm27.Visible = False
                Case "DtgLblPriClm28"
                    DtgLblPriClm28.Visible = False
                Case "DtgLblPriClm29"
                    DtgLblPriClm29.Visible = False
                Case "DtgLblPriClm30"
                    DtgLblPriClm30.Visible = False
                Case "DtgLblPriClm31"
                    DtgLblPriClm31.Visible = False
                Case "DtgLblPriClm32"
                    DtgLblPriClm32.Visible = False
                Case "DtgLblPriClm33"
                    DtgLblPriClm33.Visible = False
                Case "DtgLblPriClm34"
                    DtgLblPriClm34.Visible = False
                Case "DtgLblPriClm35"
                    DtgLblPriClm35.Visible = False
                Case "DtgLblPriClm36"
                    DtgLblPriClm36.Visible = False
                Case "DtgLblPriClm37"
                    DtgLblPriClm37.Visible = False
                Case "DtgLblPriClm38"
                    DtgLblPriClm38.Visible = False
                Case "DtgLblPriClm39"
                    DtgLblPriClm39.Visible = False
                Case "DtgLblPriClm40"
                    DtgLblPriClm40.Visible = False
                Case "DtgLblPriClm41"
                    DtgLblPriClm41.Visible = False
                Case "DtgLblPriClm42"
                    DtgLblPriClm42.Visible = False
                Case "DtgLblPriClm43"
                    DtgLblPriClm43.Visible = False
                Case "DtgLblPriClm44"
                    DtgLblPriClm44.Visible = False
                Case "DtgLblPriClm45"
                    DtgLblPriClm45.Visible = False
                Case "DtgLblPriClm46"
                    DtgLblPriClm46.Visible = False
                Case "DtgLblPriClm47"
                    DtgLblPriClm47.Visible = False
                Case "DtgLblPriClm48"
                    DtgLblPriClm48.Visible = False
                Case "DtgLblPriClm49"
                    DtgLblPriClm49.Visible = False
                Case "DtgLblPriClm50"
                    DtgLblPriClm50.Visible = False
                Case "DtgLblPriClm51"
                    DtgLblPriClm51.Visible = False
                Case "DtgLblPriClm52"
                    DtgLblPriClm52.Visible = False
                Case Else

            End Select

        Loop

        DataReader.Close()
        DataReader.Dispose()




        '***************************************************************************************************************************

        '**********データグリッドに初期値出力***************************************************************************************
        '得意先が増えた場合、処理を追加
        'コマンド作成
        Command = Connection.CreateCommand
        'ＳＱＬ作成
        Select Case strLblTye
            Case "D01" 'ラベルタイプＤ０１用
                'ユーザ操作による行追加を無効
                DtgLblPri.AllowUserToAddRows = False
                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "YokoCen,KenName,StrNo,StrName"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_CenMas.CenName = '" & CmdCen1.Text & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"

                sqlOrderByCon = "Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                'SQL
                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read
                    DtgLblPri.Rows.Add()
                    Idx = DtgLblPri.Rows.Count - 1
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm1").Value = DataReader.Item("YokoCen").ToString
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm2").Value = DataReader.Item("KenName").ToString
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm3").Value = DataReader.Item("StrNo").ToString
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm4").Value = DataReader.Item("StrName").ToString
                Loop
                'ＤＢ切断
                DataReader.Close()
                DataReader.Dispose()

            Case "G01" 'ラベルタイプＧ０１用
                'ユーザ操作による行追加を無効
                DtgLblPri.AllowUserToAddRows = False
                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "KenName,StrName"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_CenMas.CenName = '" & CmdCen1.Text & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                sqlOrderByCon = "KenName,StrName"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read
                    DtgLblPri.Rows.Add()
                    Idx = DtgLblPri.Rows.Count - 1
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm9").Value = DataReader.Item("StrName").ToString
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm10").Value = DataReader.Item("KenName").ToString
                Loop
                'ＤＢ切断
                DataReader.Close()
                DataReader.Dispose()

            Case "A01" 'ラベルタイプＡ０１用
                'ユーザ操作による行追加を無効
                DtgLblPri.AllowUserToAddRows = False
                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "Remarks1,CenName,Remarks2"
                sqlTableName = "Tbl_CenMas"
                sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                              "CenName = '" & CmdCen1.Text & "'"
                sqlOrderByCon = "CenName"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read
                    DtgLblPri.Rows.Add()
                    Idx = DtgLblPri.Rows.Count - 1
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm15").Value = DataReader.Item("Remarks1").ToString
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm16").Value = DataReader.Item("CenName").ToString
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm17").Value = DataReader.Item("Remarks2").ToString
                Loop
                'ＤＢ切断
                DataReader.Close()
                DataReader.Dispose()

            Case "Y01" 'ラベルタイプY０１用
                'ユーザ操作による行追加を無効
                DtgLblPri.AllowUserToAddRows = False

                '部門情報を取得
                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "Remarks1,Remarks2"
                sqlTableName = "Tbl_Remarks"
                sqlWhereCon = "RemarksID = " & 1 & ""
                sqlOrderByCon = "RemarksNo"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read

                    'ワークエリアへのセット
                    Wrk_DataRe(0, i) = DataReader.Item("Remarks1").ToString
                    Wrk_DataRe(1, i) = DataReader.Item("Remarks2").ToString

                    'ワークエリアの拡張（配列を追加）
                    ReDim Preserve Wrk_DataRe(2, Cntup + 1)
                    Cntup = Cntup + 1
                    i = i + 1
                Loop

                i = 0
                Cntup = 0

                'ＤＢ切断
                DataReader.Close()
                DataReader.Dispose()

                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "StrNo,StrName"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_CenMas.CenID = '" & strCenID & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                sqlOrderByCon = "StrNo"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon &
                               sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                '画面左側コンボボックスの初期値設定
                CmbStr1.Items.Clear()
                CmbStr1.Items.Add("全ての店舗")

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read
                    CmbStr1.Items.Add(DataReader.Item("StrName").ToString)
                    For Cntcc = 0 To Wrk_DataRe.GetLength(1) - 2
                        DtgLblPri.Rows.Add()
                        Idx = DtgLblPri.Rows.Count - 1
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm20").Value = DataReader.Item("StrNo").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm21").Value = DataReader.Item("StrName").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm22").Value = Wrk_DataRe(0, Cntcc)

                    Next
                Loop
                'ＤＢ切断
                DataReader.Close()
                DataReader.Dispose()
                CmbStr1.Text = CmbStr1.Items(0)

            Case "M01" 'ラベルタイプＭ０１用（マキヤ用）
                'ユーザ操作による行追加を有効
                DtgLblPri.AllowUserToAddRows = True

                '*********センターマスターのRemarks1を取得**********
                strRemarks1 = ""
                For Cntbb = 0 To Wrk_DataCen.GetLength(1) - 1
                    '二次元配列の得意先名とコンボボックスの値を比較
                    If CmdCen1.Text = Wrk_DataCen(1, Cntbb) Then
                        '二次元配列の得意先ＩＤを出力
                        strRemarks1 = Wrk_DataCen(0, Cntbb)
                    End If
                Next Cntbb

                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                '各ＳＱＬ文の構文設定
                '名称マスターテーブルのコメントを取得
                sqlField1 = "NameTitle"
                sqlTableName = "Tbl_NameTitle"
                sqlWhereCon = "CenID = " & strRemarks1 & " AND " &
                              "DivisionID = 3 AND " &
                              "NameID = 1"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read
                    strCommentName = DataReader.Item("NameTitle").ToString
                Loop

                DataReader.Close()
                DataReader.Dispose()


                'コマンド作成
                Command = Connection.CreateCommand

                'フロア情報を取得
                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "Remarks1,Remarks2"
                sqlTableName = "Tbl_Remarks"
                sqlWhereCon = "RemarksID = " & 3 & ""

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read

                    'ワークエリアへのセット
                    Wrk_DataRe(0, i) = DataReader.Item("Remarks1").ToString
                    Wrk_DataRe(1, i) = DataReader.Item("Remarks2").ToString

                    'ワークエリアの拡張（配列を追加）
                    ReDim Preserve Wrk_DataRe(2, Cntup + 1)
                    Cntup = Cntup + 1
                    i = i + 1
                Loop

                i = 0
                Cntup = 0
                'コマンド作成
                Command = Connection.CreateCommand
                '部門情報を取得
                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "Remarks1,Remarks2,Remarks3"
                sqlTableName = "Tbl_Remarks"
                sqlWhereCon = "RemarksID = " & 2 & ""

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read

                    'ワークエリアへのセット
                    Wrk_DataRe2(0, i) = DataReader.Item("Remarks1").ToString
                    Wrk_DataRe2(1, i) = DataReader.Item("Remarks2").ToString
                    Wrk_DataRe2(2, i) = DataReader.Item("Remarks3").ToString
                    'ワークエリアの拡張（配列を追加）
                    ReDim Preserve Wrk_DataRe2(2, Cntup + 1)
                    Cntup = Cntup + 1
                    i = i + 1
                Loop

                i = 0
                Cntup = 0

                'ＤＢ切断
                DataReader.Close()
                DataReader.Dispose()

                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "StrNo,StrName"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_CenMas.CenID = '" & strCenID & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                sqlOrderByCon = "StrNo"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon &
                               sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                '画面左側コンボボックスの初期値設定
                'CmbStr1.Items.Clear()
                'CmbStr1.Items.Add("全ての店舗")

                'データリーダーにデータ取得
                'DataReader = Command.ExecuteReader
                'Do Until Not DataReader.Read
                '    '店舗名
                '    CmbStr1.Items.Add(DataReader.Item("StrName").ToString)

                '    '部門の数だけループ
                '    For Cntcc = 0 To Wrk_DataRe2.GetLength(1) - 2

                '        'フロア名の取得
                '        For Cntdd = 0 To Wrk_DataRe.GetLength(1) - 2
                '            If Wrk_DataRe2(0, Cntcc) = Wrk_DataRe(0, Cntdd) Then
                '                '8/26
                '                strFloorName = Wrk_DataRe(0, Cntdd) & "：" & Wrk_DataRe(1, Cntdd)
                '                Exit For
                '            Else
                '                strFloorName = "登録なし"
                '            End If
                '        Next

                '        DtgLblPri.Rows.Add()
                '        Idx = DtgLblPri.Rows.Count - 1
                '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm26").Value = DataReader.Item("StrNo").ToString
                '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm27").Value = DataReader.Item("StrName").ToString
                '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm28").Value = strFloorName
                '        '8/25
                '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm29").Value = Wrk_DataRe2(1, Cntcc) & "：" & Wrk_DataRe2(2, Cntcc)
                '        DtgLblPriClm35.HeaderText = strCommentName

                '    Next
                'Loop
                'ＤＢ切断
                DataReader.Close()
                DataReader.Dispose()
                'CmbStr1.Text = CmbStr1.Items(0)

            Case "M02" 'ラベルタイプＭ０２用（第2関東MrMax用）
                'ユーザ操作による行追加を無効
                DtgLblPri.AllowUserToAddRows = False

                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlField1 = ""
                sqlTableName = ""
                sqlWhereCon = ""
                sqlOrderByCon = ""
                '各ＳＱＬ文の構文設定
                sqlField1 = "StrNo,StrName"
                sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                              "Tbl_CenMas.CenName = '" & CmdCen1.Text & "' AND " &
                              "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                sqlOrderByCon = "KenName,StrName"

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader
                Do Until Not DataReader.Read
                    DtgLblPri.Rows.Add()
                    Idx = DtgLblPri.Rows.Count - 1
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm39").Value = DataReader.Item("StrNo").ToString
                    DtgLblPri.Rows(Idx).Cells("DtgLblPriClm40").Value = DataReader.Item("StrName").ToString
                Loop
                'ＤＢ切断
                DataReader.Close()
                DataReader.Dispose()


            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select
        '***************************************************************************************************************************


        '**********発注日と納品日ボタンの座標と、ボタンのテキストを取得*************************************************************
        'コマンド作成
        Command = Connection.CreateCommand
        'ダイレックスラベル用のＳＱＬ作成
        'SQL文の作成
        '初期化
        sqlStatement = ""
        sqlField1 = ""
        sqlTableName = ""
        sqlWhereCon = ""
        '各ＳＱＬ文の構文設定
        sqlField1 = "OrdBtnX,OrdBtnY,DelBtnX,DelBtnY,CleBtnX,CleBtnY,OrdBtnTxt,DelBtnTxt"
        sqlTableName = "Tbl_CorpMas,Tbl_FrmOpMgt"
        sqlWhereCon = "Tbl_CorpMas.CorpID = '" & intTokID & "' AND " &
                      "Tbl_CorpMas.LblTypeID = Tbl_FrmOpMgt.LblTypeID "

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

        Command.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader = Command.ExecuteReader
        Do Until Not DataReader.Read
            '発注日をコピーする」ボタンの印字
            If DataReader.Item("OrdBtnX").ToString = 0 And
                DataReader.Item("OrdBtnY").ToString = 0 Then

                intOrdBtnCooX = 0
                intOrdBtnCooY = 0
                BtnOrder1.Visible = False
            Else
                intOrdBtnCooX = DataReader.Item("OrdBtnX").ToString
                intOrdBtnCooY = DataReader.Item("OrdBtnY").ToString
                strOrdBtnTxt = DataReader.Item("OrdBtnTxt").ToString
                BtnOrder1.Visible = True
            End If

            '「納品日をコピーする」ボタンの印字
            If DataReader.Item("DelBtnX").ToString = 0 And
                DataReader.Item("DelBtnY").ToString = 0 Then

                intDelBtnCooX = 0
                intDelBtnCooY = 0
                BtnDel1.Visible = False
            Else
                intDelBtnCooX = DataReader.Item("DelBtnX").ToString
                intDelBtnCooY = DataReader.Item("DelBtnY").ToString
                strDelBtnTxt = DataReader.Item("DelBtnTxt").ToString
                BtnDel1.Visible = True
            End If

            'クリアボタンの印字
            If DataReader.Item("CleBtnX").ToString = 0 And
                DataReader.Item("CleBtnY").ToString = 0 Then

                intCleBtnX = 0
                intCleBtnY = 0
                BtnClear1.Visible = False
            Else
                intCleBtnX = DataReader.Item("CleBtnX").ToString
                intCleBtnY = DataReader.Item("CleBtnY").ToString
            End If

        Loop


        '発注日コピーボタンと納品日コピーボタンの配置替え
        BtnOrder1.Location = New Point(intOrdBtnCooX, intOrdBtnCooY)
        BtnDel1.Location = New Point(intDelBtnCooX, intDelBtnCooY)
        BtnClear1.Location = New Point(intCleBtnX, intCleBtnY)

        BtnOrder1.Text = strOrdBtnTxt
        BtnDel1.Text = strDelBtnTxt

        '***************************************************************************************************************************

        'ＤＢ切断
        DataReader.Close()
        Connection.Close()

        DataReader.Dispose()
        Command.Dispose()
        Connection.Dispose()

        'データグリッド左上の得意先ラベルに印字
        LblCen5.Text = CmdTok1.Text

        'データグリッド左上の得意先ラベルとセンターラベルの印字設定
        Select Case strLblTye
            Case "D01"
                LblCen7.Text = CmdCen1.Text

                LblCen6.Visible = True
                LblCen7.Visible = True
                LblCen8.Visible = False
                CmbStr1.Visible = False
                TxtDel1.Visible = False
                LblCen9.Visible = False

            Case "G01"
                LblCen6.Visible = False
                LblCen7.Visible = False
                LblCen8.Visible = False
                CmbStr1.Visible = False
                TxtDel1.Visible = False
                LblCen9.Visible = False

            Case "A01"
                LblCen7.Text = CmdCen1.Text

                LblCen6.Visible = True
                LblCen7.Visible = True
                LblCen8.Visible = False
                CmbStr1.Visible = False
                TxtDel1.Visible = False
                LblCen9.Visible = False

            Case "Y01"
                LblCen7.Text = CmdCen1.Text

                LblCen6.Visible = True
                LblCen7.Visible = True
                LblCen8.Location = New Point(187, 6)
                CmbStr1.Location = New Point(188, 24)
                LblCen8.Visible = True
                CmbStr1.Visible = True
                TxtDel1.Visible = False
                LblCen9.Visible = False

            Case "M01"
                LblCen7.Text = CmdCen1.Text

                LblCen6.Visible = True
                LblCen7.Visible = True
                LblCen8.Visible = False
                CmbStr1.Visible = False
                TxtDel1.Visible = True
                LblCen9.Visible = True
                'LblCen8.Location = New Point(187, 6)
                'CmbStr1.Location = New Point(188, 24)
                'LblCen8.Visible = True
                'CmbStr1.Visible = True
            Case "M02"
                LblCen7.Text = CmdCen1.Text

                LblCen6.Visible = True
                LblCen7.Visible = True
                LblCen8.Visible = False
                CmbStr1.Visible = False
                TxtDel1.Visible = False
                LblCen9.Visible = False

            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select

    End Sub

    Public Sub ComboBoxTok_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmdTok1.SelectedIndexChanged
        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "センターが登録されていません。" & vbCrLf & "センターを登録してからやり直してください。"
        Dim i As Integer
        Dim Cntup As Integer
        Dim strTorikbn As String = 0

        '取引先区分をセット
        If BtnRadio1.Checked = True Then
            strTorikbn = 1
        End If
        If BtnRadio2.Checked = True Then
            strTorikbn = 2
        End If

        '共通ワークエリアの初期化
        ReDim Wrk_DataCen(1, 1)

        CmdCen1.Items.Clear()
        intTokID = 0
        For Cntbb = 0 To Wrk_DataTok.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmdTok1.Text = Wrk_DataTok(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_DataTok(0, Cntbb)

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
        sqlField1 = "CenName,Remarks1"
        sqlTableName = "Tbl_CenMas"

        'マキヤ用
        If intTokID = "10" Then
            If strTorikbn = 1 Then
                sqlWhereCon = "Tbl_CenMas.CorpID = " & intTokID & " AND " &
                          "NOT DelFlg = 1 AND " &
                          "Remarks2 = " & strTorikbn & ""
            End If
            If strTorikbn = 2 Then
                sqlWhereCon = "Tbl_CenMas.CorpID = " & intTokID & " AND " &
                              "NOT DelFlg = 1 "
            End If


        Else  '共通
            sqlWhereCon = "Tbl_CenMas.CorpID = " & intTokID & " AND " &
                          "NOT DelFlg = 1"
        End If

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

        Command.CommandText = sqlStatement

        'データリーダーにデータ取得
        DataReader = Command.ExecuteReader
        Do Until Not DataReader.Read
            CmdCen1.Items.Add(DataReader.Item("CenName").ToString)
            Wrk_DataCen(0, i) = DataReader.Item("Remarks1").ToString
            Wrk_DataCen(1, i) = DataReader.Item("CenName").ToString
            ReDim Preserve Wrk_DataCen(1, Cntup + 1)
            Cntup = Cntup + 1
            i = i + 1
        Loop

        If CmdCen1.Items.Count() = 0 Then
            ErrorMessage = strErrorMessage1

        Else
            CmdCen1.Text = CmdCen1.Items(0)
        End If

        If Not ErrorMessage = "" Then
            'エラーメッセージの表示
            MessageBox.Show(ErrorMessage, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
        End If

        'ＤＢ切断
        DataReader.Close()
        Command.Dispose()
        Connection.Close()
        Connection.Dispose()

    End Sub

    'プレビューボタン押下時の処理
    Private Sub PrevBtn_Click(sender As Object, e As EventArgs) Handles BtnPrev1.Click
        '＊＊＊空白カラムのチェック処理＊＊＊
        '変数宣言
        Dim intSpaceCnt As Integer = 0
        Dim intSpacecnt2 As Integer = 0
        Dim intRow As Integer = 0
        Dim intClm As Integer = 0

        'ラベルタイプＤ０１用変数（ダイレックス、サンドラッグ用）
        Dim strOrderDay1 As String
        Dim strDelivery1 As String
        Dim strOricon1 As String
        Dim strCase1 As String

        'ラベルタイプＧ０１用変数（ルミエール等汎用ラベル用）
        Dim strShipDay2 As String
        Dim strNumber As String

        'ラベルタイプＡ０１用変数（アマゾン用）
        Dim strDelivery3 As String
        Dim strPoNo As String
        Dim strKonposu As String

        'ラベルタイプＹ０１用変数（ヤサカ用）
        Dim strDelivery4 As String
        Dim strNumber2 As String

        'ラベルタイプＭ０１用変数（マキヤ用）
        Dim strDelivery5 As String
        Dim strTokbaiday1 As String
        Dim strSeicon1 As String = 0
        Dim strBara1 As String = 0

        'ラベルタイプＭ０２用変数（第２関東MrMax用）
        Dim strDelivery6 As String
        Dim strConpou1 As String
        Dim strBunrui1 As String


        '共通変数
        Dim intOrderDay1 As Integer
        Dim intDelivery1 As Integer
        Dim intOrderDay2 As Integer
        Dim intDelivery2 As Integer

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        'エラーメッセージエリア
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim strErrorMessage1 As String = "発注日と納品日は必ず入力して下さい"
        Dim strErrorMessage2 As String = "ケース数とオリコン数は必ずどちらか入力して下さい"
        Dim strErrorMessage4 As String = "出荷日と納品日は必ず入力して下さい"
        Dim strErrorMessage5 As String = "個数は必ず入力して下さい"
        Dim strErrorMessage6 As String = "値を入力して下さい"
        Dim strErrorMessage7 As String = "納品日は発注日以降にして下さい"
        Dim strErrorMessage8 As String = "納品日は出荷日以降にして下さい"
        Dim strErrorMessage9 As String = "ＰＯ番号、納品日、梱包数は必ず入力して下さい"
        Dim strErrorMessage10 As String = "個口数とお届日は必ず入力して下さい"
        Dim strErrorMessage11 As String = "納品日は必ず入力して下さい"
        Dim strErrorMessage12 As String = "正梱数とバラ数は必ずどちらか入力して下さい"
        Dim strErrorMessage13 As String = "定店、定本、特店、特本、客注から必ず１つチェックして下さい"
        Dim strErrorMessage14 As String = "納品日は必ず入力して下さい"
        Dim strErrorMessage15 As String = "梱包数は必ず入力して下さい"
        Dim strErrorMessage16 As String = "分類コードは必ず入力して下さい"
        Dim strErrorMessage17 As String = "客、優、通、配、新、特、手、■から必ず１つ選択して下さい"


        Dim intErrorOrdinate As String = ""
        Dim intErrorFlg As Integer = 0

        'データグリッドのマルチフォーカスをＯＦＦ
        Me.DtgLblPri.MultiSelect = False

        'データグリッドのニューメリックチェック
        For i = 0 To DtgLblPri.Rows.Count - 1

            Select Case strLblTye

                Case "D01" 'ダイレックス用

                    strOrderDay1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm5").Value
                    strDelivery1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm6").Value
                    strCase1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm7").Value
                    strOricon1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm8").Value
                    intOrderDay1 = CType(DtgLblPri.Rows(i).Cells("DtgLblPriClm5").Value, Integer)
                    intDelivery1 = CType(DtgLblPri.Rows(i).Cells("DtgLblPriClm6").Value, Integer)

                    'ケース数もしくはオリコン数に値が入っている場合、発注日と納品日の空白行チェック
                    If Not strCase1 = "" Or
                       Not strOricon1 = "" Then
                        '発注日もしくは納品日がスペースの場合
                        If strOrderDay1 = "" Or
                           strDelivery1 = "" Then

                            If intErrorFlg = 0 Then

                                ErrorMessage = strErrorMessage1
                                If strDelivery1 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(5).Selected() = True
                                    intRow = i
                                    intClm = 5
                                    intErrorFlg = 1
                                End If
                                If strOrderDay1 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(4).Selected() = True
                                    intRow = i
                                    intClm = 4
                                    intErrorFlg = 1
                                End If

                            End If

                        End If
                    Else

                    End If

                    '発注日もしくは納品日に値が入っている場合、ケース数とオリコン数の空白行をチェック。ただし、片方に値が入っていればok
                    If Not strOrderDay1 = "" Or
                       Not strDelivery1 = "" Then

                        If intErrorFlg = 0 Then
                            'ケース数とオリコン数が両方スペースの場合
                            If strCase1 = "" And
                               strOricon1 = "" Then

                                ErrorMessage = strErrorMessage2
                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(6).Selected() = True
                                Me.DtgLblPri.Rows(i).Cells(7).Selected() = True
                                intRow = i
                                intClm = 6
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '全ての発注日と納品日、オリコン、ケースがスペースの場合のチェック
                    If strCase1 = "" And
                       strOricon1 = "" And
                       strOrderDay1 = "" And
                       strDelivery1 = "" Then

                        intSpaceCnt = intSpaceCnt + 1

                    End If

                    '発注日と納品日の値チェック。発注日が納品日と同じもしくは小さいか確認
                    If intOrderDay1 > intDelivery1 Then
                        If intErrorFlg = 0 Then
                            ErrorMessage = strErrorMessage7
                            Me.DtgLblPri.MultiSelect = True
                            Me.DtgLblPri.Rows(i).Cells(4).Selected() = True
                            Me.DtgLblPri.Rows(i).Cells(5).Selected() = True
                            intRow = i
                            intClm = 4
                            intErrorFlg = 1
                        End If
                    End If


                    strCase1 = ""
                    strOricon1 = ""
                    strOrderDay1 = ""
                    strDelivery1 = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkD01_Data1(1)
                    ReDim WrkD01_Data2(1)
                    ReDim WrkD01_Data3(1)
                    ReDim WrkD01_Data4(1)
                    ReDim WrkD01_Data5(1)
                    ReDim WrkD01_Data6(1)
                    ReDim WrkD01_Data8(1)
                    ReDim WrkD01_Data9(1)

                Case "G01" '汎用ラベル用
                    strShipDay2 = DtgLblPri.Rows(i).Cells("DtgLblPriClm11").Value
                    strNumber = DtgLblPri.Rows(i).Cells("DtgLblPriClm13").Value
                    intOrderDay2 = CType(DtgLblPri.Rows(i).Cells("DtgLblPriClm11").Value, Integer)
                    intDelivery2 = CType(DtgLblPri.Rows(i).Cells("DtgLblPriClm12").Value, Integer)

                    '個数に値が入っている場合、出荷日の空白行チェック
                    If Not strNumber = "" Then
                        '出荷日がスペースの場合
                        If strShipDay2 = "" Then

                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage4

                                If strShipDay2 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(10).Selected() = True
                                    intRow = i
                                    intClm = 10
                                    intErrorFlg = 1
                                End If


                            End If

                        End If
                    Else

                    End If

                    '出荷日に値が入っている場合、個数の空白行をチェック
                    If Not strShipDay2 = "" Then

                        If intErrorFlg = 0 Then
                            '個数がスペースの場合
                            If strNumber = "" Then
                                ErrorMessage = strErrorMessage5

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(12).Selected() = True
                                intRow = i
                                intClm = 12
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '全ての発注日、個数がスペースの場合
                    If strShipDay2 = "" And
                       strNumber = "" Then
                        intSpaceCnt = intSpaceCnt + 1

                    End If

                    '発注日と納品日の値チェック。発注日が納品日と同じもしくは小さいか確認
                    If Not DtgLblPri.Rows(i).Cells("DtgLblPriClm12").Value = Nothing Then
                        If intOrderDay2 > intDelivery2 Then
                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage8
                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(10).Selected() = True
                                Me.DtgLblPri.Rows(i).Cells(11).Selected() = True
                                intRow = i
                                intClm = 10
                                intErrorFlg = 1
                            End If
                        End If
                    End If

                    strShipDay2 = ""
                    strNumber = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkG01_Data1(1)
                    ReDim WrkG01_Data2(1)
                    ReDim WrkG01_Data3(1)
                    ReDim WrkG01_Data4(1)


                Case "A01" 'アマゾン用
                    strPoNo = DtgLblPri.Rows(i).Cells("DtgLblPriClm14").Value
                    strKonposu = DtgLblPri.Rows(i).Cells("DtgLblPriClm18").Value
                    strDelivery3 = DtgLblPri.Rows(i).Cells("DtgLblPriClm19").Value
                    'ＰＯ番号と納品日、梱包数がスペースでない場合
                    If Not strPoNo = "" Or
                        Not strDelivery3 = "" Or
                         Not strKonposu = "" Then

                        'ＰＯ番号と納品日、梱包数がスペースの場合
                        If strPoNo = "" Or
                            strDelivery3 = "" Or
                             strKonposu = "" Then

                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage9

                                If strDelivery3 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(18).Selected() = True
                                    intRow = i
                                    intClm = 18
                                    intErrorFlg = 1
                                End If
                                If strKonposu = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(17).Selected() = True
                                    intRow = i
                                    intClm = 17
                                    intErrorFlg = 1
                                End If
                                If strPoNo = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(13).Selected() = True
                                    intRow = i
                                    intClm = 13
                                    intErrorFlg = 1
                                End If


                            End If

                        End If

                    End If

                    '全てのＰＯ番号と納品日、梱包数がスペースの場合
                    If strPoNo = "" And
                       strKonposu = "" And
                       strDelivery3 = "" Then

                        intSpaceCnt = intSpaceCnt + 1
                    End If

                    strPoNo = ""
                    strKonposu = ""
                    strDelivery3 = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkA01_Data1(1)
                    ReDim WrkA01_Data2(1)
                    ReDim WrkA01_Data3(1)
                    ReDim WrkA01_Data4(1)

                Case "Y01" 'ヤサカ用
                    strDelivery4 = DtgLblPri.Rows(i).Cells("DtgLblPriClm23").Value
                    strNumber2 = DtgLblPri.Rows(i).Cells("DtgLblPriClm24").Value

                    '部門とお届日、個口数がスペースでない場合
                    If Not strNumber2 = "" Or
                        Not strDelivery4 = "" Then

                        '部門とお届日、個口数がスペースの場合
                        If strNumber2 = "" Or
                            strDelivery4 = "" Then

                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage10

                                If strDelivery4 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(22).Selected() = True
                                    intRow = i
                                    intClm = 22
                                    intErrorFlg = 1
                                End If
                                If strNumber2 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(23).Selected() = True
                                    intRow = i
                                    intClm = 23
                                    intErrorFlg = 1
                                End If

                            End If

                        End If

                        '印刷ボタン用
                        '共通ワークエリアの初期化
                        ReDim WrkY01_Data1(1)
                        ReDim WrkY01_Data2(1)
                        ReDim WrkY01_Data3(1)
                        ReDim WrkY01_Data4(1)
                        ReDim WrkY01_Data5(1)
                        ReDim WrkY01_Data6(1)
                        ReDim WrkY01_Data7(1)

                    End If

                    '全ての部門とお届日、個口数がスペースの場合
                    If strNumber2 = "" And
                       strDelivery4 = "" Then

                        intSpaceCnt = intSpaceCnt + 1
                    End If

                Case "M01" 'マキヤ用
                    strTokbaiday1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm35").Value
                    strDelivery5 = DtgLblPri.Rows(i).Cells("DtgLblPriClm36").Value
                    strSeicon1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm37").Value
                    strBara1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm38").Value


                    'ラベル区分、正梱数もしくはバラ数に値が入っている場合、納入日の空白行チェック
                    If Not strSeicon1 = "" Or
                       Not strBara1 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm30").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm31").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm32").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm33").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm34").Value = True Then
                        '納品日がスペースの場合
                        If strDelivery5 = "" Then

                            If intErrorFlg = 0 Then

                                ErrorMessage = strErrorMessage11

                                If strDelivery5 = "" Then
                                    Me.DtgLblPri.MultiSelect = True
                                    Me.DtgLblPri.Rows(i).Cells(35).Selected() = True
                                    intRow = i
                                    intClm = 35
                                    intErrorFlg = 1
                                End If

                            End If

                        End If
                    Else

                    End If

                    'コメントもしくは納入日に値が入っている場合、正梱数とバラ数の空白行をチェック。
                    'ただし、片方に値が入っていれば問題なし
                    If Not strDelivery5 = "" Or
                       Not strTokbaiday1 = "" Then

                        If intErrorFlg = 0 Then
                            '正梱数とバラ数が両方スペースの場合
                            If strSeicon1 = "" And
                               strBara1 = "" Then

                                ErrorMessage = strErrorMessage12
                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(36).Selected() = True
                                Me.DtgLblPri.Rows(i).Cells(37).Selected() = True
                                intRow = i
                                intClm = 36
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    'ラベル区分と特売開始日、納入日、正梱数、バラ数がスペースの場合のチェック
                    If strTokbaiday1 = "" And
                       strDelivery5 = "" And
                       strSeicon1 = "" And
                       strBara1 = "" And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm30").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm31").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm32").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm33").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm34").Value = False Then

                        intSpaceCnt = intSpaceCnt + 1

                    End If


                    '定店、定本、特店、特本、客注の入力空白チェック。
                    If Not strDelivery5 = "" Or
                       Not strTokbaiday1 = "" Then

                        If DtgLblPri.Rows(i).Cells("DtgLblPriClm30").Value = False And
                           DtgLblPri.Rows(i).Cells("DtgLblPriClm31").Value = False And
                           DtgLblPri.Rows(i).Cells("DtgLblPriClm32").Value = False And
                           DtgLblPri.Rows(i).Cells("DtgLblPriClm33").Value = False And
                           DtgLblPri.Rows(i).Cells("DtgLblPriClm34").Value = False Then

                            ErrorMessage = strErrorMessage13

                            Me.DtgLblPri.MultiSelect = True
                            Me.DtgLblPri.Rows(i).Cells(29).Selected() = True
                            intRow = i
                            intClm = 29
                            intErrorFlg = 1


                        End If

                    End If

                    strTokbaiday1 = ""
                    strDelivery5 = ""
                    strSeicon1 = ""
                    strBara1 = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkM01_Data1(1)
                    ReDim WrkM01_Data2(1)
                    ReDim WrkM01_Data3(1)
                    ReDim WrkM01_Data4(1)
                    ReDim WrkM01_Data5(1)
                    ReDim WrkM01_Data6(1)
                    ReDim WrkM01_Data7(1)
                    ReDim WrkM01_Data8(1)
                    ReDim WrkM01_Data9(1)
                    ReDim WrkM01_Data10(1)
                    ReDim WrkM01_Data11(1)
                    ReDim WrkM01_Data12(1)
                    ReDim WrkM01_Data13(1)
                    ReDim WrkM01_Data14(1)

                Case "M02" '第２関東MrMax用
                    strDelivery6 = DtgLblPri.Rows(i).Cells("DtgLblPriClm49").Value
                    strConpou1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm50").Value
                    strBunrui1 = DtgLblPri.Rows(i).Cells("DtgLblPriClm51").Value

                    '梱包数、分類コード、納品区分に値が入っている場合、納品日の空白行チェック
                    If Not strConpou1 = "" Or
                       Not strBunrui1 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = True Then

                        '納品日がスペースの場合
                        If strDelivery6 = "" Then

                            If intErrorFlg = 0 Then
                                ErrorMessage = strErrorMessage14

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(48).Selected() = True
                                intRow = i
                                intClm = 48
                                intErrorFlg = 1

                            End If

                        End If
                    Else

                    End If

                    '納品日、分類コード、納品区分に値が入っている場合、梱包数の空白行をチェック
                    If Not strDelivery6 = "" Or
                       Not strBunrui1 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = True Then

                        If intErrorFlg = 0 Then
                            '梱包数がスペースの場合
                            If strConpou1 = "" Then
                                ErrorMessage = strErrorMessage15

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(50).Selected() = True
                                intRow = i
                                intClm = 50
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '納品日、梱包数、納品区分に値が入っている場合、分類コードの空白行をチェック
                    If Not strDelivery6 = "" Or
                       Not strConpou1 = "" Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = True Or
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = True Then

                        If intErrorFlg = 0 Then
                            '分類コードがスペースの場合
                            If strBunrui1 = "" Then
                                ErrorMessage = strErrorMessage16

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(49).Selected() = True
                                intRow = i
                                intClm = 49
                                intErrorFlg = 1
                            End If
                        End If

                    End If

                    '納品日、梱包数、納品区分に値が入っている場合、納品区分の空白行をチェック
                    If Not strDelivery6 = "" Or
                       Not strConpou1 = "" Or
                       Not strBunrui1 = "" Then

                        If intErrorFlg = 0 Then
                            '納品区分が全て選択されていない場合
                            If DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = False And
                               DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = False Then

                                ErrorMessage = strErrorMessage17

                                Me.DtgLblPri.MultiSelect = True
                                Me.DtgLblPri.Rows(i).Cells(40).Selected() = True
                                intRow = i
                                intClm = 40
                                intErrorFlg = 1
                            End If
                        End If

                    End If


                    '全ての納品日、梱包数、分類コード、納品区分がスペースの場合
                    If strDelivery6 = "" And
                       strConpou1 = "" And
                       strBunrui1 = "" And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm41").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm42").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm43").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm44").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm45").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm46").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm47").Value = False And
                       DtgLblPri.Rows(i).Cells("DtgLblPriClm48").Value = False Then
                        intSpaceCnt = intSpaceCnt + 1

                    End If

                    strConpou1 = ""
                    strDelivery6 = ""
                    strBunrui1 = ""

                    '印刷ボタン用
                    '共通ワークエリアの初期化
                    ReDim WrkM02_Data1(1)
                    ReDim WrkM02_Data2(1)
                    ReDim WrkM02_Data3(1)
                    ReDim WrkM02_Data4(1)
                    ReDim WrkM02_Data5(1)
                    ReDim WrkM02_Data6(1)
                    ReDim WrkM02_Data7(1)
                    ReDim WrkM02_Data8(1)
                    ReDim WrkM02_Data9(1)



                Case Else
                    End

            End Select

        Next

        '必須入力項目で全てスペースの場合
        If DtgLblPri.Rows.Count = intSpaceCnt Then
            Select Case strLblTye

                Case "D01" 'ダイレックス用
                    intRow = 0
                    intClm = 4

                Case "G01" 'ルミエール用
                    intRow = 0
                    intClm = 10

                Case "A01" 'アマゾン用
                    intRow = 0
                    intClm = 13

                Case "Y01" 'ヤサカ用
                    intRow = 0
                    intClm = 22

                Case "M01" 'マキヤ用
                    intRow = 0
                    intClm = 34

                Case "M02" '第２関東MrMax用
                    intRow = 0
                    intClm = 48

            End Select
            ErrorMessage = strErrorMessage6
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
                    AddressOf PrintDocument2_PrintPage
                Fst_sw = True
            End If



            '印刷する内容をプレビュー表示する
            PPre1.Document = PDoc1
            PPre1.ShowDialog()
        End If

        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub
    '印刷データの出力
    Private Sub PrintDocument2_PrintPage(ByVal sender As Object, _
           ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        '得意先増えた場合は処理を追加する
        Select Case strLblTye
            Case "D01" 'ラベルタイプＤ０１用（ダイレックス用）
                Dim Img As New Bitmap(frm_D01Print.Width, frm_D01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_D01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_D01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                e.Graphics.DrawImage(Img, 0, 0, 370, 470)
                Img.Dispose()

                '印刷用画面の消去
                frm_D01Print.Dispose()

                '印刷するページ数のチェック
                If Dmax <= WIdx Then
                    '追加での印刷無し
                    e.HasMorePages = False
                Else
                    '追加での印刷有り
                    e.HasMorePages = True
                End If

                WIdx = WIdx + 1

            Case "G01" 'ラベルタイプＧ０１用（汎用ラベル用）
                Dim Img As New Bitmap(frm_G01Print.Width, frm_G01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_G01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_G01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                e.Graphics.DrawImage(Img, 0, 0, 370, 470)
                Img.Dispose()

                '印刷用画面の消去
                frm_G01Print.Dispose()

                '印刷するページ数のチェック
                If Dmax <= WIdx Then
                    '追加での印刷無し
                    e.HasMorePages = False
                Else
                    '追加での印刷有り
                    e.HasMorePages = True
                End If

                WIdx = WIdx + 1

            Case "A01" 'ラベルタイプＡ０１用（アマゾン用）
                Dim Img As New Bitmap(frm_A01Print.Width, frm_A01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_A01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_A01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                e.Graphics.DrawImage(Img, 0, 0, 370, 470)
                Img.Dispose()

                '印刷用画面の消去
                frm_A01Print.Dispose()

                '印刷するページ数のチェック
                If Dmax <= WIdx Then
                    '追加での印刷無し
                    e.HasMorePages = False
                Else
                    '追加での印刷有り
                    e.HasMorePages = True
                End If

                WIdx = WIdx + 1

            Case "M01" 'ラベルタイプＭ０１用（マキヤ用）
                Dim Img As New Bitmap(frm_M01Print.Width, frm_M01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_M01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_M01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                e.Graphics.DrawImage(Img, 0, 0, 370, 470)
                Img.Dispose()

                '印刷用画面の消去
                frm_M01Print.Dispose()

                '印刷するページ数のチェック
                If Dmax <= WIdx Then
                    '追加での印刷無し
                    e.HasMorePages = False
                Else
                    '追加での印刷有り
                    e.HasMorePages = True
                End If

                WIdx = WIdx + 1

            Case "M02" 'ラベルタイプＭ０２用（MrMax用）
                Dim Img As New Bitmap(frm_M02Print.Width, frm_M02Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_M02Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_M02Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                e.Graphics.DrawImage(Img, 0, 0, 370, 470)
                Img.Dispose()

                '印刷用画面の消去
                frm_M02Print.Dispose()

                '印刷するページ数のチェック
                If Dmax <= WIdx Then
                    '追加での印刷無し
                    e.HasMorePages = False
                Else
                    '追加での印刷有り
                    e.HasMorePages = True
                End If

                WIdx = WIdx + 1

            Case "Y01" 'ラベルタイプＹ０１用(ヤサカ用)
                Dim Img As New Bitmap(frm_Y01Print.Width, frm_Y01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_Y01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_Y01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                e.Graphics.DrawImage(Img, 0, 0, 370, 470)
                Img.Dispose()

                '印刷用画面の消去
                frm_Y01Print.Dispose()

                '印刷するページ数のチェック
                If Dmax <= WIdx Then
                    '追加での印刷無し
                    e.HasMorePages = False
                Else
                    '追加での印刷有り
                    e.HasMorePages = True
                End If

                WIdx = WIdx + 1
            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select

    End Sub

    '印刷データの退避
    Private Sub Data_Set()

        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Rencnt As String
        Dim Rencntmoto As String
        Dim strBarUnder4 As String
        Dim strBarTop4 As String

        Dim strKonpouName As String = ""
        Dim strKonpouSName As String = ""
        Dim strKonpouBName As String = ""

        Dim strOrderDay As String
        Dim strOrderCD As String = ""

        Dim CntMax As Integer
        Dim Cnt As Integer

        intTokID = 0
        For Cntbb = 0 To Wrk_DataTok.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmdTok1.Text = Wrk_DataTok(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_DataTok(0, Cntbb)
            End If
        Next Cntbb

        Select Case strLblTye
            Case "D01" 'ラベルタイプＤ０１用（ダイレックス用）

                '変数宣言
                Dim Idx As Integer
                Dim Idx2 As Integer
                Dim intNisugata As Integer
                Dim intCNisugata As Integer
                Dim intONisugata As Integer

                Dim strNisugataName As String = ""
                Dim strNisugataCName As String = ""
                Dim strNisugataOName As String = ""

                Dim CntCMax As Integer
                Dim CntOMax As Integer
                Dim strStrNo As String

                Dim datCnv As DateTime

                Idx2 = 0

                '荷番の取得
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
                sqlField1 = "Niban"
                sqlTableName = "Tbl_NibanMas"
                sqlWhereCon = "CorpID = " & intTokID & ""

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                DataReader.Read()
                Rencntmoto = DataReader.Item("Niban").ToString
                Rencnt = Rencntmoto

                'ＤＢ切断
                DataReader.Close()
                Connection.Close()

                DataReader.Dispose()
                Command.Dispose()
                Connection.Dispose()

                'データグリッドビューの行数分ループ
                For Idx = 0 To Me.DtgLblPri.RowCount - 1

                    '***共通値設定項目***
                    'バーコードの頭４桁の設定
                    strBarTop4 = "(98)"
                    'バーコードの下４桁の設定
                    strBarUnder4 = "0000"
                    'バーコードの発注先ＣＤの設定
                    '沖縄物流センターの場合は4157
                    'その他については4156
                    Select Case strCenID
                        Case "32"
                            strOrderCD = "4157"
                        Case "33"
                            strOrderCD = "2924"

                        Case Else
                            If intTokID = 1 Then
                                strOrderCD = "4156"
                            ElseIf intTokID = 2 Then
                                strOrderCD = "2924"
                            End If

                    End Select


                    'ケースの処理の場合

                    'ループ処理の条件設定
                    Cnt = 1
                    'ケース数
                    CntCMax = CType(Me.DtgLblPri(6, Idx).Value, Integer)
                    'オリコン数
                    CntOMax = CType(Me.DtgLblPri(7, Idx).Value, Integer)
                    CntMax = CntCMax + CntOMax

                    '荷姿の種類名　ケース
                    strNisugataCName = Me.DtgLblPri.Columns(6).HeaderText.Trim("数")
                    '荷姿番号
                    intCNisugata = 2

                    '荷姿の種類名 オリコン
                    strNisugataOName = Me.DtgLblPri.Columns(7).HeaderText.Trim("数")
                    '荷姿番号
                    intONisugata = 1


                    'ケース数またはオリコン数が１以上の場合処理する
                    If CntCMax >= 1 Or
                        CntOMax >= 1 Then

                        'ケース数とオリコン数の合計数値だけループ
                        For Cnt = 1 To CntMax

                            '条件　ケース数が１以上かつ、cntがケース数と同じか低い場合の処理
                            If CntCMax >= 1 And
                                Cnt <= CntCMax Then

                                strNisugataName = strNisugataCName
                                intNisugata = intCNisugata
                            End If

                            '条件　オリコン数が１以上かつ、ケース数がcntよりも低い場合
                            If CntOMax >= 1 And
                                    CntCMax < Cnt Then

                                strNisugataName = strNisugataOName
                                intNisugata = intONisugata
                            End If

                            'ここにデータをセットするロジックを追加
                            'ワークエリアの拡張（配列を追加）
                            ReDim Preserve WrkD01_Data1(Idx2 + 1) 'センター名
                            ReDim Preserve WrkD01_Data2(Idx2 + 1) '店番
                            ReDim Preserve WrkD01_Data3(Idx2 + 1) '納品日
                            ReDim Preserve WrkD01_Data4(Idx2 + 1) 'バーコードの値
                            ReDim Preserve WrkD01_Data5(Idx2 + 1) '荷番
                            ReDim Preserve WrkD01_Data6(Idx2 + 1) '荷姿
                            ReDim Preserve WrkD01_Data8(Idx2 + 1) '横持ち
                            ReDim Preserve WrkD01_Data9(Idx2 + 1) '店舗名
                            ReDim Preserve WrkD01_Data10(Idx2 + 1) '発注先ＣＤ

                            '荷姿の値を設定


                            'ワークエリアへのセット
                            'センター名
                            WrkD01_Data1(Idx2) = Me.LblCen7.Text

                            '店番(ラベル印字用)
                            WrkD01_Data2(Idx2) = StrConv(Me.DtgLblPri(2, Idx).Value, VbStrConv.Wide)

                            '納品日
                            Me.DtgLblPri(5, Idx).Value = StrConv(Me.DtgLblPri(5, Idx).Value, VbStrConv.Narrow)
                            datCnv = DateTime.ParseExact(Me.DtgLblPri(5, Idx).Value, "yyMMdd", Nothing)
                            WrkD01_Data3(Idx2) = datCnv.ToString("yyyy/MM/dd")

                            '***************バーコードの出力値を設定*******************'
                            '店番（バーコード設定用）
                            strStrNo = Me.DtgLblPri(2, Idx).Value

                            '発注日のデータ型変換
                            strOrderDay = Me.DtgLblPri(4, Idx).Value

                            '荷番
                            Rencnt = Rencnt.PadLeft(5, "0")
                            WrkD01_Data5(Idx2) = Rencnt

                            '荷姿
                            WrkD01_Data6(Idx2) = strNisugataName

                            'バーコードの値設定　　頭４桁　　　店番　     発注先　   　　発注日　　　荷番　　 荷姿コード　　下４ケタ   　　
                            WrkD01_Data4(Idx2) = strBarTop4 & strStrNo & strOrderCD & strOrderDay & Rencnt & intNisugata & strBarUnder4
                            '************************END************\******************'
                            '横持ち
                            If Not Me.DtgLblPri(0, Idx).Value = Nothing Then
                                WrkD01_Data8(Idx2) = Me.DtgLblPri(0, Idx).Value
                            Else
                                WrkD01_Data8(Idx2) = ""
                            End If
                            '店舗名
                            WrkD01_Data9(Idx2) = Me.DtgLblPri(3, Idx).Value

                            '発注先ＣＤ
                            WrkD01_Data10(Idx2) = strOrderCD

                            Rencnt = Rencnt + 1
                            '荷番の最大値を制御  *最大値は90000
                            If Rencnt = 100000 Then
                                Rencnt = 1
                            End If

                            Idx2 = Idx2 + 1
                        Next

                    End If

                Next
                'ループのおわり

                '印刷対象件数をグローバル変数へセット
                Dmax = Idx2 - 1

                '********************荷番の更新***************************'
                Dim Connection2 As New SQLiteConnection
                Dim Command2 As SQLiteCommand

                '接続文字列を設定
                Connection2.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
                'オープン
                Connection2.Open()
                'コマンド作成
                Command2 = Connection2.CreateCommand

                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlTableName = ""
                sqlSetCon = ""
                sqlWhereCon = ""
                '各ＳＱＬ文の構文設定
                sqlTableName = "Tbl_NibanMas"
                sqlSetCon = "Niban = '" & Rencnt & "' "
                sqlWhereCon = "CorpID = " & intTokID & " AND " &
                              "Niban = " & Rencntmoto & ""

                sqlStatement = sqlUpdate & sqlTableName & sqlSet & sqlSetCon & sqlWhere & sqlWhereCon

                Command2.CommandText = sqlStatement

                Command2.ExecuteNonQuery()
                'ＤＢ切断
                Connection2.Close()
                Command2.Dispose()
                Connection2.Dispose()
                '*************************END************\******************'

            Case "G01" 'ラベルタイプＧ０１用（ルミエール用）
                '変数宣言
                Dim Idx = 0
                Dim Idx2 As Integer = 0
                Dim strSyuDay1 As String = ""
                Dim strSyuDay2 As String = ""
                Dim Cntko As Integer = 0
                Dim strTikHen As String = ""
                Dim strShipDay As DateTime
                Cnt = 0

                'データグリッドの行数分ループ処理
                For Idx = 0 To Me.DtgLblPri.RowCount - 1

                    '個口に入力された数だけループ
                    '型変換
                    Cnt = 1
                    Cntko = CType(Me.DtgLblPri(12, Idx).Value, Integer)

                    For Cnt = 1 To Cntko

                        ReDim Preserve WrkG01_Data1(Idx2 + 1) '店舗名
                        ReDim Preserve WrkG01_Data2(Idx2 + 1) '地区名
                        ReDim Preserve WrkG01_Data3(Idx2 + 1) '出荷日
                        ReDim Preserve WrkG01_Data4(Idx2 + 1) '個数

                        '店舗名の設定
                        WrkG01_Data1(Idx2) = Me.DtgLblPri(8, Idx).Value
                        '地区名の設定
                        strTikHen = Me.DtgLblPri(9, Idx).Value
                        If strTikHen = " " Or
                            strTikHen = Nothing Then

                            WrkG01_Data2(Idx2) = ""

                        Else
                            WrkG01_Data2(Idx2) = "(" & strTikHen & ")"
                        End If

                        '出荷日の設定
                        Me.DtgLblPri(10, Idx).Value = StrConv(Me.DtgLblPri(10, Idx).Value, VbStrConv.Narrow)
                        strShipDay = DateTime.ParseExact(Me.DtgLblPri(10, Idx).Value, "yyMMdd", Nothing)
                        strShipDay = strShipDay.ToString("yyyy/MM/dd")

                        strSyuDay1 = strShipDay.ToString("yyyyMMdd")
                        strSyuDay2 = strSyuDay1.Substring(0, 4) & "年" & strSyuDay1.Substring(4, 2) & "月" & strSyuDay1.Substring(6, 2) & "日"
                        WrkG01_Data3(Idx2) = strSyuDay2
                        '個口の設定
                        WrkG01_Data4(Idx2) = Cnt

                        Idx2 = Idx2 + 1

                    Next

                Next
                '印刷対象件数をグローバル変数へセット
                Dmax = Idx2 - 1

            Case "A01" 'ラベルタイプＡ０１用（アマゾン用）
                '変数宣言
                Dim Idx = 0
                Dim Idx2 As Integer = 0
                Dim strSyuDay1 As String = ""
                Dim strSyuDay2 As String = ""
                Dim CntKoMax As Integer = 0
                Dim strDelDay As DateTime
                Cnt = 0

                'データグリッドの行数分ループ処理
                For Idx = 0 To Me.DtgLblPri.RowCount - 1

                    '梱包数に入力された数だけループ
                    '型変換
                    Cnt = 1
                    CntKoMax = CType(Me.DtgLblPri(18, Idx).Value, Integer)

                    For Cnt = 1 To CntKoMax

                        ReDim Preserve WrkA01_Data1(Idx2 + 1) 'ＰＯ番号
                        ReDim Preserve WrkA01_Data2(Idx2 + 1) '印字名
                        ReDim Preserve WrkA01_Data3(Idx2 + 1) '納品日
                        ReDim Preserve WrkA01_Data4(Idx2 + 1) '梱包数

                        'ＰＯ番号の設定
                        WrkA01_Data1(Idx2) = Me.DtgLblPri(13, Idx).Value
                        '印字名の設定
                        WrkA01_Data2(Idx2) = Me.DtgLblPri(16, Idx).Value

                        '納品日の設定
                        Me.DtgLblPri(17, Idx).Value = StrConv(Me.DtgLblPri(17, Idx).Value, VbStrConv.Narrow)
                        strDelDay = DateTime.ParseExact(Me.DtgLblPri(17, Idx).Value, "yyMMdd", Nothing)
                        strDelDay = strDelDay.ToString("yyyy/MM/dd")

                        strSyuDay1 = strDelDay.ToString("yyyyMMdd")
                        strSyuDay2 = strSyuDay1.Substring(4, 2) & "/" & strSyuDay1.Substring(6, 2)
                        '納品日（ワークエリアに設定）
                        WrkA01_Data3(Idx2) = strSyuDay2

                        '梱包数の設定
                        WrkA01_Data4(Idx2) = Cnt & "/" & CntKoMax

                        Idx2 = Idx2 + 1

                    Next

                Next
                '印刷対象件数をグローバル変数へセット
                Dmax = Idx2 - 1

            Case "Y01" 'ラベルタイプＹ０１用（ヤサカ用）
                '変数宣言
                Dim Idx = 0
                Dim Idx2 As Integer = 0
                Dim CntKoMax As Integer = 0
                Dim strTikHen As String = ""
                Dim strDelDay1 As DateTime
                Dim strDelDay2 As String
                Dim strDelDay3 As String
                Cnt = 0

                'データグリッドの行数分ループ処理
                For Idx = 0 To Me.DtgLblPri.RowCount - 1

                    '個口数に入力された数だけループ
                    '型変換
                    Cnt = 1
                    CntKoMax = CType(Me.DtgLblPri(23, Idx).Value, Integer)

                    For Cnt = 1 To CntKoMax

                        ReDim Preserve WrkY01_Data1(Idx2 + 1) '店番
                        ReDim Preserve WrkY01_Data2(Idx2 + 1) '店舗名
                        ReDim Preserve WrkY01_Data3(Idx2 + 1) '部門
                        ReDim Preserve WrkY01_Data4(Idx2 + 1) 'お届日
                        ReDim Preserve WrkY01_Data5(Idx2 + 1) '個口数
                        ReDim Preserve WrkY01_Data6(Idx2 + 1) '最大個口数
                        ReDim Preserve WrkY01_Data7(Idx2 + 1) 'ＦＡＸ
                        ReDim Preserve WrkY01_Data8(Idx2 + 1) 'お届日（月）

                        '店番の設定
                        'WrkY01_Data1(Idx2) = Me.DtgLblPri(19, Idx).Value
                        WrkY01_Data1(Idx2) = StrConv(Me.DtgLblPri(19, Idx).Value, VbStrConv.Wide)
                        '店舗名の設定
                        WrkY01_Data2(Idx2) = Me.DtgLblPri(20, Idx).Value
                        '部門の設定
                        WrkY01_Data3(Idx2) = StrConv(Me.DtgLblPri(21, Idx).Value, VbStrConv.Narrow)

                        'お届日の設定
                        Me.DtgLblPri(22, Idx).Value = StrConv(Me.DtgLblPri(22, Idx).Value, VbStrConv.Narrow)
                        strDelDay1 = DateTime.ParseExact(Me.DtgLblPri(22, Idx).Value, "yyMMdd", Nothing)
                        strDelDay2 = strDelDay1.ToString("yyyyMMdd")
                        strDelDay2 = strDelDay2.Substring(6, 2)
                        strDelDay2 = strDelDay2.Trim("0")
                        'お届日（ワークエリアに設定）
                        WrkY01_Data4(Idx2) = strDelDay2

                        'お届日（月）の設定
                        strDelDay1 = DateTime.ParseExact(Me.DtgLblPri(22, Idx).Value, "yyMMdd", Nothing)
                        strDelDay3 = strDelDay1.ToString("yyyyMMdd")
                        strDelDay3 = strDelDay3.Substring(4, 2)
                        strDelDay3 = strDelDay3.Trim("0")
                        'お届日（ワークエリアに設定）
                        WrkY01_Data8(Idx2) = strDelDay3

                        '個口数の設定
                        WrkY01_Data5(Idx2) = Cnt

                        '最大個口数の設定 半角へ変換
                        WrkY01_Data6(Idx2) = StrConv(Me.DtgLblPri(23, Idx).Value, VbStrConv.Narrow)

                        'ＦＡＸの設定
                        If Me.DtgLblPri(24, Idx).Value = True Then
                            WrkY01_Data7(Idx2) = "ＦＡＸ"
                        Else : Me.DtgLblPri(24, Idx).Value = False
                            WrkY01_Data7(Idx2) = ""
                        End If

                        Idx2 = Idx2 + 1

                    Next

                Next
                '印刷対象件数をグローバル変数へセット
                Dmax = Idx2 - 1

            Case "M01" 'ラベルタイプＭ０１用（マキヤ用）

                '変数宣言
                Dim Idx As Integer
                Dim Idx2 As Integer
                Dim strLblBarcodeA1 As String
                Dim strLblBarcodeB1 As String
                Dim strLblBarcodeC1 As String
                Dim strLblBarcodeD1 As String
                Dim strLblBarcodeE3 As String
                Dim strLblBarcodeF2 As String
                Dim strLblBarcodeG2 As String
                Dim strLblBarcodeH6 As String
                Dim strLblBarcodeI2 As String
                Dim strLblBarcodeJ4 As String
                Dim intKonpoukei As Integer
                Dim intSKonpoukei As Integer
                Dim intBKonpoukei As Integer
                Dim intLblKubun As Integer
                Dim strWork1 As String
                Dim intTotal As Integer
                Dim strSubstr1 As String
                Dim strSubstr2 As String

                Dim CntSMax As Integer
                Dim CntBMax As Integer

                Dim datCnv As DateTime

                Idx2 = 0

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

                '得意先ごとの荷番を取得するＳＱＬ
                sqlField1 = "Niban"
                sqlTableName = "Tbl_NibanMas"
                sqlWhereCon = "CorpID = " & intTokID & ""

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                DataReader.Read()
                Rencntmoto = DataReader.Item("Niban").ToString
                Rencnt = Rencntmoto

                'ＤＢ切断
                DataReader.Close()
                Connection.Close()

                DataReader.Dispose()
                Command.Dispose()
                Connection.Dispose()

                'データグリッドビューの行数分ループ
                For Idx = 0 To Me.DtgLblPri.RowCount - 1


                    'ループ処理の条件設定
                    Cnt = 1
                    '正梱数
                    CntSMax = CType(Me.DtgLblPri(36, Idx).Value, Integer)

                    'バラ数
                    CntBMax = CType(Me.DtgLblPri(37, Idx).Value, Integer)

                    '正梱数とバラ数の合計値がループの最大値
                    CntMax = CntSMax + CntBMax

                    '梱包形式の名前
                    strKonpouSName = Me.DtgLblPri.Columns(36).HeaderText.Trim("数")
                    '荷姿番号
                    intSKonpoukei = 1

                    '荷姿の種類名
                    strKonpouBName = Me.DtgLblPri.Columns(37).HeaderText.Trim("数")
                    '荷姿番号
                    intBKonpoukei = 2


                    '正梱数またはバラ数が１以上の場合処理する
                    If CntSMax >= 1 Or
                        CntBMax >= 1 Then

                        '正梱数とバラ数の合計数値だけループ
                        For Cnt = 1 To CntMax


                            '条件　正梱数が１以上かつ、cntが正梱数と同じか低い場合の処理
                            If CntSMax >= 1 And
                                Cnt <= CntSMax Then

                                strKonpouName = strKonpouSName
                                intKonpoukei = intSKonpoukei
                            End If

                            '条件　バラ数が１以上かつ、正梱数がcntよりも低い場合
                            If CntBMax >= 1 And
                                    CntSMax < Cnt Then

                                strKonpouName = strKonpouBName
                                intKonpoukei = intBKonpoukei
                            End If

                            'ここにデータをセットするロジックを追加
                            'ワークエリアの拡張（配列を追加）
                            ReDim Preserve WrkM01_Data1(Idx2 + 1) '業態名
                            ReDim Preserve WrkM01_Data2(Idx2 + 1) '店舗名
                            ReDim Preserve WrkM01_Data3(Idx2 + 1) '店番
                            ReDim Preserve WrkM01_Data4(Idx2 + 1) 'フロアコード
                            ReDim Preserve WrkM01_Data5(Idx2 + 1) '部門コード
                            ReDim Preserve WrkM01_Data6(Idx2 + 1) 'ラベル区分
                            ReDim Preserve WrkM01_Data7(Idx2 + 1) '梱包形式
                            ReDim Preserve WrkM01_Data8(Idx2 + 1) '取引先コード
                            ReDim Preserve WrkM01_Data9(Idx2 + 1) '荷番
                            ReDim Preserve WrkM01_Data10(Idx2 + 1) '納入日
                            ReDim Preserve WrkM01_Data11(Idx2 + 1) '個口数
                            ReDim Preserve WrkM01_Data12(Idx2 + 1) '個口ＮＯ
                            ReDim Preserve WrkM01_Data13(Idx2 + 1) 'コメント
                            ReDim Preserve WrkM01_Data14(Idx2 + 1) 'ＪＡＮコード


                            '業態名の設定
                            WrkM01_Data1(Idx2) = Me.LblCen7.Text

                            '店舗名の設定
                            WrkM01_Data2(Idx2) = Me.DtgLblPri(26, Idx).Value

                            '店番の設定
                            WrkM01_Data3(Idx2) = Me.DtgLblPri(25, Idx).Value.PadLeft(3, "0")


                            'フロアコードの設定
                            '8/26
                            strSubstr2 = Me.DtgLblPri(27, Idx).Value
                            WrkM01_Data4(Idx2) = strSubstr2.Substring(0, 2)

                            ''フロアコードの設定
                            'For Cntbb = 0 To Wrk_DataRe.GetLength(1) - 1
                            '    If Me.DtgLblPri(27, Idx).Value = Wrk_DataRe(1, Cntbb) Then
                            '        '二次元配列のフロアコードを出力
                            '        WrkM01_Data4(Idx2) = Wrk_DataRe(0, Cntbb)
                            '        Exit For

                            '    End If

                            'Next Cntbb

                            '部門コードの設定
                            '8/25
                            ' For Cntbb = 0 To Wrk_DataRe2.GetLength(1) - 1

                            strSubstr1 = Me.DtgLblPri(28, Idx).Value
                            WrkM01_Data5(Idx2) = strSubstr1.Substring(0, 2)
                            '二次元配列の部門名とデータグリッドの値を比較
                            'If strSubstr = Wrk_DataRe2(2, Cntbb) Then
                            'If Me.DtgLblPri(28, Idx).Value = Wrk_DataRe2(2, Cntbb) Then
                            '二次元配列の部門コードを出力
                            '     WrkM01_Data5(Idx2) = Wrk_DataRe2(1, Cntbb)
                            '     Exit For

                            ' End If

                            'Next Cntbb

                            'ラベル区分の設定
                            If Me.DtgLblPri(29, Idx).Value = "true" Then
                                WrkM01_Data6(Idx2) = DtgLblPriClm30.HeaderText
                                intLblKubun = 1
                            End If
                            If Me.DtgLblPri(30, Idx).Value = "true" Then
                                WrkM01_Data6(Idx2) = DtgLblPriClm31.HeaderText
                                intLblKubun = 2
                            End If
                            If Me.DtgLblPri(31, Idx).Value = "true" Then
                                WrkM01_Data6(Idx2) = DtgLblPriClm32.HeaderText
                                intLblKubun = 3
                            End If
                            If Me.DtgLblPri(32, Idx).Value = "true" Then
                                WrkM01_Data6(Idx2) = DtgLblPriClm33.HeaderText
                                intLblKubun = 4
                            End If
                            If Me.DtgLblPri(33, Idx).Value = "true" Then
                                WrkM01_Data6(Idx2) = DtgLblPriClm34.HeaderText
                                intLblKubun = 5
                            End If

                            '梱包形式の設定
                            WrkM01_Data7(Idx2) = strKonpouName

                            '取引先コードの設定
                            WrkM01_Data8(Idx2) = strToriCode

                            '荷番の設定
                            Rencnt = Rencnt.PadLeft(4, "0")
                            WrkM01_Data9(Idx2) = Rencnt

                            '納入日の設定
                            Me.DtgLblPri(35, Idx).Value = StrConv(Me.DtgLblPri(35, Idx).Value, VbStrConv.Narrow)
                            datCnv = DateTime.ParseExact(Me.DtgLblPri(35, Idx).Value, "yyMMdd", Nothing)
                            WrkM01_Data10(Idx2) = datCnv.ToString("yy/MM/dd")

                            '個口数の設定
                            WrkM01_Data11(Idx2) = CntMax

                            '個口ＮＯの設定
                            WrkM01_Data12(Idx2) = Cnt

                            'コメントの設定
                            If Me.DtgLblPri(34, Idx).Value = "" Then
                                WrkM01_Data13(Idx2) = ""

                            Else
                                WrkM01_Data13(Idx2) = Me.DtgLblPri(34, Idx).Value

                            End If


                            'JANコードの値設定
                            'ＩＴＦの数字２４桁を使用

                            '先方未使用項目で０を固定出力
                            strLblBarcodeA1 = "0"

                            '梱包形式
                            strLblBarcodeB1 = intKonpoukei

                            'ラベル区分
                            strLblBarcodeC1 = intLblKubun

                            strRemarks1 = ""
                            For Cntbb = 0 To Wrk_DataCen.GetLength(1) - 1
                                '二次元配列の業態名とラベルの値を比較
                                If LblCen7.Text = Wrk_DataCen(1, Cntbb) Then
                                    '二次元配列の業態コードを出力
                                    strRemarks1 = Wrk_DataCen(0, Cntbb)
                                End If
                            Next Cntbb
                            '業態コード
                            strLblBarcodeD1 = strRemarks1

                            '店舗コード
                            strLblBarcodeE3 = Me.DtgLblPri(25, Idx).Value.PadLeft(3, "0"c)

                            'フロアコード
                            strLblBarcodeF2 = WrkM01_Data4(Idx2)

                            '部門コード
                            strLblBarcodeG2 = WrkM01_Data5(Idx2)

                            '取引先コード
                            strLblBarcodeH6 = strToriCode

                            '取引先枝番　先方指定で固定００を出力
                            strLblBarcodeI2 = "00"

                            '荷番
                            strLblBarcodeJ4 = Rencnt

                            'バーコドの値 WrkM01_Data14(Idx2)
                            strWork1 = strLblBarcodeA1 & strLblBarcodeB1 & strLblBarcodeC1 & strLblBarcodeD1 &
                                       strLblBarcodeE3 & strLblBarcodeF2 & strLblBarcodeG2 & strLblBarcodeH6 &
                                       strLblBarcodeI2 & strLblBarcodeJ4

                            'チェックディジットの付与
                            'モジュラス１０　ウエイト3.1

                            '計算方法
                            '① データの末尾の桁からウエイトを3.1.3.1.とかけてゆき総和を求める。 
                            '② 総和を"10"で割りその 余りを求める。 
                            '③ "10"より余りを引いた値がチェックデジットとなる。 

                            '計算方法①の処理
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(22, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(21, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(20, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(19, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(18, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(17, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(16, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(15, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(14, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(13, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(12, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(11, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(10, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(9, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(8, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(7, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(6, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(5, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(4, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(3, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(2, 1)) * 3
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(1, 1)) * 1
                            intTotal = intTotal + Integer.Parse(strWork1.Substring(0, 1)) * 3

                            '計算方法②の処理
                            intTotal = intTotal Mod 10


                            '計算方法③の処理
                            If intTotal = 0 Then
                                intTotal = 0
                            Else
                                intTotal = 10 - intTotal
                            End If

                            'JANコードの設定
                            strWork1 = strWork1 & intTotal
                            WrkM01_Data14(Idx2) = strWork1

                            Rencnt = Rencnt + 1

                            '荷番の最大値を制御  *最大値は9999
                            If Rencnt = 10000 Then
                                Rencnt = 1
                            End If

                            Idx2 = Idx2 + 1
                        Next

                    End If

                Next
                'ループのおわり

                '印刷対象件数をグローバル変数へセット
                Dmax = Idx2 - 1

                '********************荷番の更新***************************'
                Dim Connection2 As New SQLiteConnection
                Dim Command2 As SQLiteCommand

                '接続文字列を設定
                Connection2.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
                'オープン
                Connection2.Open()
                'コマンド作成
                Command2 = Connection2.CreateCommand

                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlTableName = ""
                sqlSetCon = ""
                sqlWhereCon = ""
                '各ＳＱＬ文の構文設定
                sqlTableName = "Tbl_NibanMas"
                sqlSetCon = "Niban = '" & Rencnt & "' "
                sqlWhereCon = "CorpID = " & intTokID & " AND " &
                              "Niban = " & Rencntmoto & ""

                sqlStatement = sqlUpdate & sqlTableName & sqlSet & sqlSetCon & sqlWhere & sqlWhereCon

                Command2.CommandText = sqlStatement

                Command2.ExecuteNonQuery()
                'ＤＢ切断
                Connection2.Close()
                Command2.Dispose()
                Connection2.Dispose()
                '*************************END************\******************'

            Case "M02" 'ラベルタイプＭ０２用（第２関東MrMax用）

                Dim Idx As Integer
                Dim Idx2 As Integer
                Dim strLblBarcodeA1 As String
                Dim strLblBarcodeB1 As String
                Dim strLblBarcodeC1 As String
                Dim strLblBarcodeD1 As String
                Dim strLblBarcodeE1 As String
                Dim strLblBarcodeF1 As String
                Dim strLblBarcodeG1 As String
                Dim strLblBarcodeH1 As String
                Dim strLblBarcodeI1 As String
                Dim strLblBarcodeJ1 As String
                Dim strLblBarcodeK1 As String
                Dim strLblBarcodeL1 As String
                Dim intNouhinkbn As Integer
                Dim strShohinbuCD As String
                Dim strWork1 As String

                Dim CntSMax As Integer

                Dim datCnv As DateTime

                Idx2 = 0

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

                '得意先ごとの荷番を取得するＳＱＬ
                sqlField1 = "Niban"
                sqlTableName = "Tbl_NibanMas"
                sqlWhereCon = "CorpID = " & intTokID & ""

                sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                Command.CommandText = sqlStatement

                'データリーダーにデータ取得
                DataReader = Command.ExecuteReader

                DataReader.Read()
                Rencntmoto = DataReader.Item("Niban").ToString
                Rencnt = Rencntmoto

                'ＤＢ切断
                DataReader.Close()
                Connection.Close()

                DataReader.Dispose()
                Command.Dispose()
                Connection.Dispose()

                'データグリッドビューの行数分ループ
                For Idx = 0 To Me.DtgLblPri.RowCount - 1


                    'ループ処理の条件設定
                    Cnt = 1
                    '梱包数
                    CntSMax = CType(Me.DtgLblPri(50, Idx).Value, Integer)


                    '梱包数が１以上の場合処理する
                    If CntSMax >= 1 Then

                        '梱包数だけループ
                        For Cnt = 1 To CntSMax

                            'ここにデータをセットするロジックを追加
                            'ワークエリアの拡張（配列を追加）
                            ReDim Preserve WrkM02_Data1(Idx2 + 1) '物流センター名
                            ReDim Preserve WrkM02_Data2(Idx2 + 1) '店舗名
                            ReDim Preserve WrkM02_Data3(Idx2 + 1) '店番
                            ReDim Preserve WrkM02_Data4(Idx2 + 1) '納品日
                            ReDim Preserve WrkM02_Data5(Idx2 + 1) '分類コード
                            ReDim Preserve WrkM02_Data6(Idx2 + 1) 'ラベル表示文字
                            ReDim Preserve WrkM02_Data7(Idx2 + 1) '納品区分
                            ReDim Preserve WrkM02_Data8(Idx2 + 1) 'バーコードの値
                            ReDim Preserve WrkM02_Data9(Idx2 + 1) '備考

                            '物流センター名の設定
                            WrkM02_Data1(Idx2) = Me.LblCen7.Text

                            '店番の設定
                            WrkM02_Data3(Idx2) = Me.DtgLblPri(38, Idx).Value

                            '店舗名の設定
                            WrkM02_Data2(Idx2) = Me.DtgLblPri(39, Idx).Value

                            '分類コードの設定
                            WrkM02_Data5(Idx2) = Me.DtgLblPri(49, Idx).Value

                            '備考の設定　納品区分で「手」を選択した場合、「手書き」を出力
                            If Me.DtgLblPri(46, Idx).Value = "true" Then
                                WrkM02_Data9(Idx2) = "手書き"
                            Else
                                WrkM02_Data9(Idx2) = Me.DtgLblPri(51, Idx).Value
                            End If


                            '納品区分の設定
                            If Me.DtgLblPri(40, Idx).Value = "true" Then
                                WrkM02_Data7(Idx2) = DtgLblPriClm41.HeaderText
                                intNouhinkbn = 1
                            End If
                            If Me.DtgLblPri(41, Idx).Value = "true" Then
                                WrkM02_Data7(Idx2) = DtgLblPriClm42.HeaderText
                                intNouhinkbn = 2
                            End If
                            If Me.DtgLblPri(42, Idx).Value = "true" Then
                                WrkM02_Data7(Idx2) = DtgLblPriClm43.HeaderText
                                intNouhinkbn = 3
                            End If
                            If Me.DtgLblPri(43, Idx).Value = "true" Then
                                WrkM02_Data7(Idx2) = DtgLblPriClm44.HeaderText
                                intNouhinkbn = 4
                            End If
                            If Me.DtgLblPri(44, Idx).Value = "true" Then
                                WrkM02_Data7(Idx2) = DtgLblPriClm45.HeaderText
                                intNouhinkbn = 5
                            End If
                            If Me.DtgLblPri(45, Idx).Value = "true" Then
                                WrkM02_Data7(Idx2) = DtgLblPriClm46.HeaderText
                                intNouhinkbn = 6
                            End If
                            If Me.DtgLblPri(46, Idx).Value = "true" Then
                                WrkM02_Data7(Idx2) = DtgLblPriClm47.HeaderText
                                intNouhinkbn = 7
                            End If
                            If Me.DtgLblPri(47, Idx).Value = "true" Then
                                WrkM02_Data7(Idx2) = DtgLblPriClm48.HeaderText
                                intNouhinkbn = 8
                            End If


                            '荷番の設定 
                            Rencnt = Rencnt.PadLeft(7, "0")

                            '納入日の設定
                            Me.DtgLblPri(48, Idx).Value = StrConv(Me.DtgLblPri(48, Idx).Value, VbStrConv.Narrow)
                            datCnv = DateTime.ParseExact(Me.DtgLblPri(48, Idx).Value, "yyMMdd", Nothing)
                            WrkM02_Data4(Idx2) = datCnv.ToString("MM/dd")

                            'バーコードの値設定
                            'EAN128を使用

                            '先方仕様書の項目「AI」の設定
                            strLblBarcodeA1 = "(98)"

                            '先方仕様書の項目「データ区分」の設定
                            strLblBarcodeB1 = "1"

                            '先方仕様書の項目「会社コード」の設定
                            strLblBarcodeC1 = "0000"

                            '先方仕様書の項目「店舗コード」の設定
                            strLblBarcodeD1 = Me.DtgLblPri(38, Idx).Value

                            '先方仕様書の項目「分類コード」の設定
                            strLblBarcodeE1 = Me.DtgLblPri(49, Idx).Value

                            '先方仕様書の項目「納品区分」の設定
                            strLblBarcodeF1 = intNouhinkbn

                            '先方仕様書の項目「商品部コード」の設定
                            Select Case Me.DtgLblPri(49, Idx).Value
                                Case "01", "02", "03", "04", "05", "06", "07", "08"
                                    strShohinbuCD = "01"
                                    WrkM02_Data6(Idx2) = "A"
                                    strLblBarcodeG1 = strShohinbuCD

                                Case "11", "12", "13", "14", "15", "16"
                                    strShohinbuCD = "02"
                                    WrkM02_Data6(Idx2) = "B"
                                    strLblBarcodeG1 = strShohinbuCD

                                Case "21", "22", "23", "24", "25", "26", "27"
                                    strShohinbuCD = "03"
                                    WrkM02_Data6(Idx2) = "C"
                                    strLblBarcodeG1 = strShohinbuCD

                                Case "41", "42", "43", "44"
                                    strShohinbuCD = "04"
                                    WrkM02_Data6(Idx2) = "D"
                                    strLblBarcodeG1 = strShohinbuCD

                                Case "51", "52", "53", "54", "55"
                                    strShohinbuCD = "05"
                                    WrkM02_Data6(Idx2) = "E"
                                    strLblBarcodeG1 = strShohinbuCD

                                Case "61", "62", "63", "64", "65", "66", "67", "71", "72", "73", "74", "75"
                                    strShohinbuCD = "06"
                                    WrkM02_Data6(Idx2) = "F"
                                    strLblBarcodeG1 = strShohinbuCD

                                Case Else
                                    strShohinbuCD = "99"
                                    WrkM02_Data6(Idx2) = "他"
                                    strLblBarcodeG1 = strShohinbuCD

                            End Select


                            '先方仕様書の項目「ラベルタイプ」の設定
                            strLblBarcodeH1 = "6"

                            '先方仕様書の項目「取引先コード」の設定
                            strLblBarcodeI1 = "2016"

                            '先方仕様書の項目「納品センターコード」の設定
                            strLblBarcodeJ1 = "0005"

                            '先方仕様書の項目「センター納品日」の設定
                            strLblBarcodeK1 = datCnv.ToString("MMdd")

                            '先方仕様書の項目「荷番」の設定
                            strLblBarcodeL1 = Rencnt

                            'バーコドの値 WrkM01_Data14(Idx2)
                            strWork1 = strLblBarcodeA1 & strLblBarcodeB1 & strLblBarcodeC1 & strLblBarcodeD1 &
                                       strLblBarcodeE1 & strLblBarcodeF1 & strLblBarcodeG1 & strLblBarcodeH1 &
                                       strLblBarcodeI1 & strLblBarcodeJ1 & strLblBarcodeK1 & strLblBarcodeL1

                            'バーコードコードの設定
                            WrkM02_Data8(Idx2) = strWork1

                            Rencnt = Rencnt + 1

                            '荷番の最大値を制御  *最大値は9999999
                            If Rencnt = 10000000 Then
                                Rencnt = 1
                            End If

                            Idx2 = Idx2 + 1
                        Next

                    End If

                Next
                'ループのおわり

                '印刷対象件数をグローバル変数へセット
                Dmax = Idx2 - 1

                '********************荷番の更新***************************'
                Dim Connection2 As New SQLiteConnection
                Dim Command2 As SQLiteCommand

                '接続文字列を設定
                Connection2.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
                'オープン
                Connection2.Open()
                'コマンド作成
                Command2 = Connection2.CreateCommand

                'SQL文の作成
                '初期化
                sqlStatement = ""
                sqlTableName = ""
                sqlSetCon = ""
                sqlWhereCon = ""
                '各ＳＱＬ文の構文設定
                sqlTableName = "Tbl_NibanMas"
                sqlSetCon = "Niban = '" & Rencnt & "' "
                sqlWhereCon = "CorpID = " & intTokID & " AND " &
                              "Niban = " & Rencntmoto & ""

                sqlStatement = sqlUpdate & sqlTableName & sqlSet & sqlSetCon & sqlWhere & sqlWhereCon

                Command2.CommandText = sqlStatement

                Command2.ExecuteNonQuery()
                'ＤＢ切断
                Connection2.Close()
                Command2.Dispose()
                Connection2.Dispose()

            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select

    End Sub
    '印刷データの出力
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, _
           ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Dim hdc As IntPtr

        ' デバイスコンテキストを識別するハンドルを取得します
        hdc = e.Graphics.GetHdc()

        '得意先増えた場合は処理を追加する
        Select Case strLblTye
            Case "D01" 'ラベルタイプＤ０１用
                Dim Img As New Bitmap(frm_D01Print.Width, frm_D01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_D01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_D01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                'e.Graphics.DrawImage(Img, 0, 0, 370, 470)

                '画像を印刷する（SBPLを使用）
                '画像1ﾋﾞｯﾄに変換する（ラベルプリンターの仕様に合わせる）
                Dim aRct As RectangleF
                aRct = New RectangleF(0, 0, Img.Width, Img.Height)
                Dim aBM As Bitmap
                aBM = Img.Clone(aRct, Imaging.PixelFormat.Format1bppIndexed)
                aBM.Save(".\work.bmp", System.Drawing.Imaging.ImageFormat.Bmp)

                Img.Dispose()
                aBM.Dispose()

            Case "G01" 'ラベルタイプＧ０１用
                Dim Img As New Bitmap(frm_G01Print.Width, frm_G01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_G01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_G01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                'e.Graphics.DrawImage(Img, 0, 0, 370, 470)

                '画像を印刷する（SBPLを使用）
                '画像1ﾋﾞｯﾄに変換する（ラベルプリンターの仕様に合わせる）
                Dim aRct As RectangleF
                aRct = New RectangleF(0, 0, Img.Width, Img.Height)
                Dim aBM As Bitmap
                aBM = Img.Clone(aRct, Imaging.PixelFormat.Format1bppIndexed)
                aBM.Save(".\work.bmp", System.Drawing.Imaging.ImageFormat.Bmp)

                Img.Dispose()
                aBM.Dispose()

            Case "A01" 'ラベルタイプＡ０１用
                Dim Img As New Bitmap(frm_A01Print.Width, frm_A01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_A01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_A01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                'e.Graphics.DrawImage(Img, 0, 0, 370, 470)

                '画像を印刷する（SBPLを使用）
                '画像1ﾋﾞｯﾄに変換する（ラベルプリンターの仕様に合わせる）
                Dim aRct As RectangleF
                aRct = New RectangleF(0, 0, Img.Width, Img.Height)
                Dim aBM As Bitmap
                aBM = Img.Clone(aRct, Imaging.PixelFormat.Format1bppIndexed)
                aBM.Save(".\work.bmp", System.Drawing.Imaging.ImageFormat.Bmp)

                Img.Dispose()
                aBM.Dispose()

            Case "Y01" 'ラベルタイプＹ０１用
                Dim Img As New Bitmap(frm_Y01Print.Width, frm_Y01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_Y01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_Y01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                'e.Graphics.DrawImage(Img, 0, 0, 370, 470)

                '画像を印刷する（SBPLを使用）
                '画像1ﾋﾞｯﾄに変換する（ラベルプリンターの仕様に合わせる）
                Dim aRct As RectangleF
                aRct = New RectangleF(0, 0, Img.Width, Img.Height)
                Dim aBM As Bitmap
                aBM = Img.Clone(aRct, Imaging.PixelFormat.Format1bppIndexed)
                aBM.Save(".\work.bmp", System.Drawing.Imaging.ImageFormat.Bmp)

                Img.Dispose()
                aBM.Dispose()

            Case "M01" 'ラベルタイプＭ０１用
                Dim Img As New Bitmap(frm_M01Print.Width, frm_M01Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()

                '印刷用画面の表示
                frm_M01Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_M01Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                'e.Graphics.DrawImage(Img, 0, 0, 370, 470)

                '画像を印刷する（SBPLを使用）
                '画像1ﾋﾞｯﾄに変換する（ラベルプリンターの仕様に合わせる）
                Dim aRct As RectangleF
                aRct = New RectangleF(0, 0, Img.Width, Img.Height)
                Dim aBM As Bitmap
                aBM = Img.Clone(aRct, Imaging.PixelFormat.Format1bppIndexed)
                aBM.Save(".\work.bmp", System.Drawing.Imaging.ImageFormat.Bmp)

                Img.Dispose()
                aBM.Dispose()

            Case "M02" 'ラベルタイプＭ０２用
                Dim Img As New Bitmap(frm_M02Print.Width, frm_M02Print.Height)
                Dim Memg As Graphics = Graphics.FromImage(Img)
                Dim dc As IntPtr = Memg.GetHdc()


                '印刷用画面の表示
                frm_M02Print.Show()

                'データを取得する（引数はグローバルのカウンタ）
                Call Data_Set2(WIdx)

                '印刷用画面のパネルイメージを取得する
                PrintWindow(frm_M02Print.Handle, dc, 0)
                Memg.ReleaseHdc(dc)
                Memg.Dispose()

                'イメージを２７０度回転させる
                'Img.RotateFlip(RotateFlipType.Rotate270FlipNone)
                'パネルのイメージを印刷する
                'e.Graphics.DrawImage(Img, 0, 0, 370, 470)

                '画像を印刷する（SBPLを使用）
                '画像1ﾋﾞｯﾄに変換する（ラベルプリンターの仕様に合わせる）
                Dim aRct As RectangleF
                aRct = New RectangleF(0, 0, Img.Width, Img.Height)
                Dim aBM As Bitmap
                aBM = Img.Clone(aRct, Imaging.PixelFormat.Format1bppIndexed)
                aBM.Save(".\work.bmp", System.Drawing.Imaging.ImageFormat.Bmp)

                Img.Dispose()
                aBM.Dispose()

            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select

        Try
            Dim Handle As IntPtr
            Dim ErrMsg As String
            Dim EditWk As String

            Dim STX As String = Chr(&H2)
            Dim ESC As String = Chr(&H1B)
            Dim ETX As String = Chr(&H3)

            'ドキュメント名を取得します（アセンブリ名より）
            Dim Assembly As Reflection.Assembly = Me.GetType.Assembly
            Dim AssemblyName As String = Assembly.GetName.Name

            ' オープン処理
            ErrMsg = String.Empty
            If Print.SatoOpen(Handle, cboPrinter.Text, AssemblyName, ErrMsg) = True Then

                'ロゴファイルを設定します
                Dim fi As New System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
                LogoPath = fi.DirectoryName & "\work.bmp"

                ' STXを設定します
                EditWk = STX
                ' データ送信の開始を設定します
                EditWk &= ESC & "A"

                ' センサー無視
                'EditWk &= ESC & "IG2"

                ' 用紙サイズを設定します。
                ' 縦(424ﾄﾞｯﾄ)横(400)
                'EditWk &= ESC & "PG000001000000004103000000000000049C033E0000000000000000000800"

                'EditWk &= ESC & "V100H200BG02120>GABCD123456"

                Select Case strLblTye
                    Case "M02" 'Ｂ－１(MrMax)
                        EditWk &= ESC & "A105040760"
                    Case Else 'Ｃ－１
                        EditWk &= ESC & "A109440644"
                End Select

                'EditWk &= ESC & "L0202" & ESC & "GM" & Format(FileLen(LogoPath), "00000") & ","
                EditWk &= ESC & "GM" & Format(FileLen(LogoPath), "00000") & ","
                ' STX ～ 画像の編集までをバイト型配列に変換します
                Dim bStData() As Byte = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(EditWk)

                ' 画像ファイルを開きます(work.bmp)
                Dim fs As New System.IO.FileStream(LogoPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                ' ファイルを読み込むバイト型配列を作成します
                Dim bimgData(fs.Length - 1) As Byte
                ' ファイルの内容をすべて読み込みます
                fs.Read(bimgData, 0, bimgData.Length)
                ' ファイルを閉じます
                fs.Close()

                ' 枚数を設定(1枚)します
                EditWk = ESC & "Q1"

                ' カットと使用せず
                'EditWk &= ESC & "~0"

                ' データ送信の終了を設定します
                EditWk &= ESC & "Z"

                ' ETXを設定します
                EditWk &= ETX

                ' 枚数 ～ ETXまでをバイト型配列に変換します
                Dim bEnData() As Byte = System.Text.Encoding.GetEncoding("Shift_JIS").GetBytes(EditWk)

                ' 文字データ及びグラフィックデータをバイト型配列に結合します
                Dim bPrData(bStData.Length + bimgData.Length + bEnData.Length) As Byte
                Array.Copy(bStData, 0, bPrData, 0, bStData.Length)
                Array.Copy(bimgData, 0, bPrData, bStData.Length, bimgData.Length)
                Array.Copy(bEnData, 0, bPrData, bStData.Length + bimgData.Length, bEnData.Length)

                ' 出力処理
                ErrMsg = String.Empty
                If Print.SatoSend(Handle, bPrData, ErrMsg) = False Then
                    '====================================================================================================
                    ' エラーが発生した場合にエラー時の処理を記述します
                    '====================================================================================================
                    MsgBox(ErrMsg, MsgBoxStyle.OkOnly, "ラベル発行")

                End If
            Else
                '====================================================================================================
                ' エラーが発生した場合にエラー時の処理を記述します
                '====================================================================================================
                MsgBox(ErrMsg, MsgBoxStyle.OkOnly, "ラベル発行")
                Exit Try

            End If

            ' クローズ処理
            ErrMsg = String.Empty
            If Print.SatoClose(Handle, ErrMsg) = False Then
                MsgBox(ErrMsg, MsgBoxStyle.OkOnly, "ラベル発行")

            End If

        Catch ex As Exception
            '====================================================================================================
            ' エラーが発生した場合にエラー時の処理を記述します
            '====================================================================================================
            MsgBox("ラベル発行に失敗しました。" & vbCrLf & ex.Message, MsgBoxStyle.OkOnly, "ラベル発行")

        End Try

        Select Case strLblTye
            Case "D01" 'ラベルタイプＤ０１用
                '印刷用画面の消去
                frm_D01Print.Dispose()
            Case "G01" 'ラベルタイプＧ０１用
                frm_G01Print.Dispose()
            Case "A01" 'ラベルタイプＡ０１用
                frm_A01Print.Dispose()
            Case "Y01" 'ラベルタイプＹ０１用
                frm_Y01Print.Dispose()
            Case "M01" 'ラベルタイプＭ０１用
                frm_M01Print.Dispose()
            Case "M02" 'ラベルタイプＭ０２用
                frm_M02Print.Dispose()
            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select

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

        Select Case strLblTye
            Case "D01" 'ラベルタイプＤ０１用

                frm_D01Print.OPCenNameLBL.Text = WrkD01_Data1(Idx)
                frm_D01Print.CenLBL2.Text = WrkD01_Data2(Idx)
                frm_D01Print.NouDayLBL.Text = WrkD01_Data3(Idx)
                frm_D01Print.AxPsyBcLbl1._Value = WrkD01_Data4(Idx)
                frm_D01Print.LowLBL4.Text = WrkD01_Data5(Idx)
                frm_D01Print.NisuLBL.Text = WrkD01_Data6(Idx)
                frm_D01Print.YOKOLBL.Text = WrkD01_Data8(Idx)
                frm_D01Print.CenLBL4.Text = WrkD01_Data9(Idx)
                frm_D01Print.LowLBL2.Text = WrkD01_Data10(Idx)

                If Not WrkD01_Data8(Idx) = "" Then
                    frm_D01Print.YOKOLBL.BackColor = Color.Black
                    frm_D01Print.YOKOLBL.ForeColor = Color.White
                Else
                    frm_D01Print.YOKOLBL.BackColor = Color.White
                    frm_D01Print.YOKOLBL.ForeColor = Color.White
                End If

            Case "G01" 'ラベルタイプＧ０１用
                frm_G01Print.TokLbl1.Text = CmdTok1.Text & "　御中"
                frm_G01Print.StrLbl1.Text = WrkG01_Data1(Idx)
                frm_G01Print.TikLbl1.Text = WrkG01_Data2(Idx)
                frm_G01Print.SyuLbl1.Text = WrkG01_Data3(Idx)
                frm_G01Print.KogLbl1.Text = WrkG01_Data4(Idx)

            Case "A01" 'ラベルタイプＡ０１用
                frm_A01Print.CenterLbl1.Text = WrkA01_Data1(Idx)
                frm_A01Print.TopLbl2.Text = WrkA01_Data2(Idx)
                frm_A01Print.TopLbl1.Text = WrkA01_Data3(Idx)
                frm_A01Print.UnderLbl1.Text = WrkA01_Data4(Idx)

            Case "Y01" 'ラベルタイプＹ０１用
                frm_Y01Print.LblTop1.Text = WrkY01_Data1(Idx) '店番
                frm_Y01Print.LblUnder1.Text = WrkY01_Data3(Idx) '部門
                frm_Y01Print.LblTop3.Text = WrkY01_Data4(Idx) 'お届日
                frm_Y01Print.LblTop4.Text = WrkY01_Data8(Idx) 'お届日（月）
                frm_Y01Print.LblCen2.Text = WrkY01_Data5(Idx) '個口数
                frm_Y01Print.LblCen3.Text = WrkY01_Data6(Idx) '最大個口数

                '店舗名
                If WrkY01_Data2(Idx).Length < 5 Then
                    frm_Y01Print.LblTop2.Font = New Font(frm_Y01Print.LblTop2.Font.FontFamily, 60, frm_Y01Print.LblTop2.Font.Style)
                    frm_Y01Print.LblTop2.TextAlign = ContentAlignment.BottomRight
                    frm_Y01Print.LblTop2.Text = WrkY01_Data2(Idx)

                ElseIf WrkY01_Data2(Idx).Length > 4 Then
                    frm_Y01Print.LblTop2.Font = New Font(frm_Y01Print.LblTop2.Font.FontFamily, 30, frm_Y01Print.LblTop2.Font.Style)
                    frm_Y01Print.LblTop2.TextAlign = ContentAlignment.BottomLeft
                    frm_Y01Print.LblTop2.Text = WrkY01_Data2(Idx)

                End If

                'ＦＡＸ
                If WrkY01_Data7(Idx) = "ＦＡＸ" Then
                    frm_Y01Print.LblCen1.Text = "Ｆ"
                    frm_Y01Print.LblCen4.Text = "Ａ"
                    frm_Y01Print.LblCen5.Text = "Ｘ"
                ElseIf WrkY01_Data7(Idx) = "" Then
                    frm_Y01Print.LblCen1.Text = ""
                    frm_Y01Print.LblCen4.Text = ""
                    frm_Y01Print.LblCen5.Text = ""
                End If

            Case "M01" 'ラベルタイプＭ０１用


                frm_M01Print.LblTop1.Text = WrkM01_Data1(Idx)
                frm_M01Print.LblTop2.Text = WrkM01_Data2(Idx)
                frm_M01Print.LblCen1.Text = WrkM01_Data3(Idx)
                frm_M01Print.LblCen2.Text = WrkM01_Data4(Idx)
                frm_M01Print.LblCen7.Text = WrkM01_Data5(Idx)
                frm_M01Print.LblCen6.Text = WrkM01_Data6(Idx)
                frm_M01Print.LblUnder2.Text = WrkM01_Data7(Idx)
                frm_M01Print.LblCen5.Text = WrkM01_Data8(Idx)
                frm_M01Print.LblUnder1.Text = WrkM01_Data9(Idx)
                frm_M01Print.LblUnder3.Text = WrkM01_Data10(Idx)
                frm_M01Print.LblUnder4.Text = WrkM01_Data11(Idx)
                frm_M01Print.LblUnder5.Text = WrkM01_Data12(Idx)
                frm_M01Print.AxPsyBcLbl1._Value = WrkM01_Data14(Idx)


                'メモが１２桁以上の場合は文字サイズと表示位置を調整
                If WrkM01_Data13(Idx).Length < 12 Then
                    frm_M01Print.LblCen3.Font = New Font(frm_M01Print.LblCen3.Font.FontFamily, 20, frm_M01Print.LblCen3.Font.Style)
                    frm_M01Print.LblCen3.TextAlign = ContentAlignment.MiddleLeft
                    frm_M01Print.LblCen3.Text = WrkM01_Data13(Idx)

                ElseIf WrkM01_Data13(Idx).Length > 11 Then
                    frm_M01Print.LblCen3.Font = New Font(frm_M01Print.LblCen3.Font.FontFamily, 12, frm_M01Print.LblCen3.Font.Style)
                    frm_M01Print.LblCen3.TextAlign = ContentAlignment.MiddleLeft
                    frm_M01Print.LblCen3.Text = WrkM01_Data13(Idx)

                End If


                '店舗名が１２桁以上の場合は文字サイズと表示位置を変更する
                If WrkM01_Data2(Idx).Length < 13 Then
                    frm_M01Print.LblTop2.Font = New Font(frm_M01Print.LblTop2.Font.FontFamily, 48, frm_M01Print.LblTop2.Font.Style)
                    frm_M01Print.LblTop2.TextAlign = ContentAlignment.MiddleLeft
                    frm_M01Print.LblTop2.Text = WrkM01_Data2(Idx)

                ElseIf WrkM01_Data2(Idx).Length > 12 Then
                    frm_M01Print.LblTop2.Font = New Font(frm_M01Print.LblTop2.Font.FontFamily, 24, frm_M01Print.LblTop2.Font.Style)
                    frm_M01Print.LblTop2.TextAlign = ContentAlignment.MiddleLeft
                    frm_M01Print.LblTop2.Text = WrkM01_Data2(Idx)

                End If


            Case "M02" 'ラベルタイプＭ０２用



                frm_M02Print.TopLbl1.Text = "ＭｒＭａｘ" & WrkM02_Data1(Idx)  '物流センター名
                frm_M02Print.CenLbl1.Text = WrkM02_Data2(Idx)  '店舗名
                frm_M02Print.CenLbl2.Text = WrkM02_Data3(Idx).Substring(0, 1)  '店番
                frm_M02Print.CenLbl3.Text = WrkM02_Data3(Idx).Substring(1, 1)  '店番
                frm_M02Print.CenLbl4.Text = WrkM02_Data3(Idx).Substring(2, 1)  '店番
                frm_M02Print.CenLbl5.Text = WrkM02_Data3(Idx).Substring(3, 1)  '店番
                frm_M02Print.TopLbl2.Text = WrkM02_Data4(Idx)  '納品日
                frm_M02Print.CenLbl7.Text = WrkM02_Data5(Idx)  '分類コード
                frm_M02Print.AxPsyBcLbl1._Value = WrkM02_Data8(Idx) 'バーコードの値
                frm_M02Print.UnderLbl1.Text = WrkM02_Data8(Idx) 'バーコードの表示用の値
                frm_M02Print.CenLbl9.Text = WrkM02_Data9(Idx) '備考

                If WrkM02_Data6(Idx) = "他" Then
                    frm_M02Print.CenLbl6.Visible = False
                    frm_M02Print.CenLbl10.Visible = True
                    frm_M02Print.CenLbl10.Text = WrkM02_Data6(Idx)  '商品部コードのラベル印字用文字

                Else

                    frm_M02Print.CenLbl6.Visible = True
                    frm_M02Print.CenLbl10.Visible = False
                    frm_M02Print.CenLbl6.Text = WrkM02_Data6(Idx)  '商品部コードのラベル印字用文字

                End If

                '納品区分の設定
                Select Case WrkM02_Data7(Idx)
                    Case "客"
                        frm_M02Print.CenLbl8.Text = WrkM02_Data7(Idx)
                        frm_M02Print.Centxt1.BackColor = Color.Black
                        frm_M02Print.CenLbl8.BackColor = Color.Black
                        frm_M02Print.CenLbl8.ForeColor = Color.White

                    Case "優"
                        frm_M02Print.CenLbl8.Text = WrkM02_Data7(Idx)
                        frm_M02Print.Centxt1.BackColor = Color.Black
                        frm_M02Print.CenLbl8.BackColor = Color.Black
                        frm_M02Print.CenLbl8.ForeColor = Color.White

                    Case "通"
                        frm_M02Print.CenLbl8.Text = WrkM02_Data7(Idx)
                        frm_M02Print.Centxt1.BackColor = Color.White
                        frm_M02Print.CenLbl8.BackColor = Color.White
                        frm_M02Print.CenLbl8.ForeColor = Color.Black

                    Case "配"
                        frm_M02Print.CenLbl8.Text = WrkM02_Data7(Idx)
                        frm_M02Print.Centxt1.BackColor = Color.Black
                        frm_M02Print.CenLbl8.BackColor = Color.Black
                        frm_M02Print.CenLbl8.ForeColor = Color.White

                    Case "新"
                        frm_M02Print.CenLbl8.Text = WrkM02_Data7(Idx)
                        frm_M02Print.Centxt1.BackColor = Color.White
                        frm_M02Print.CenLbl8.BackColor = Color.White
                        frm_M02Print.CenLbl8.ForeColor = Color.Black


                    Case "特"
                        frm_M02Print.CenLbl8.Text = WrkM02_Data7(Idx)
                        frm_M02Print.Centxt1.BackColor = Color.Black
                        frm_M02Print.CenLbl8.BackColor = Color.Black
                        frm_M02Print.CenLbl8.ForeColor = Color.White


                    Case "手"
                        frm_M02Print.CenLbl8.Text = WrkM02_Data7(Idx)
                        frm_M02Print.Centxt1.BackColor = Color.Black
                        frm_M02Print.CenLbl8.BackColor = Color.Black
                        frm_M02Print.CenLbl8.ForeColor = Color.White

                    Case "■"
                        frm_M02Print.CenLbl8.Text = WrkM02_Data7(Idx)
                        frm_M02Print.Centxt1.BackColor = Color.Black
                        frm_M02Print.CenLbl8.BackColor = Color.Black
                        frm_M02Print.CenLbl8.ForeColor = Color.Black


                End Select

            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select

    End Sub

    Private Sub EndBtn1_Click(sender As System.Object, e As System.EventArgs) Handles EndBtn1.Click

        If intRenewFlg = 1 Then
            If MessageBox.Show("印刷されていないデータが残っています。入力内容が消えますがよろしいですか", _
                               "確認", _
                               MessageBoxButtons.YesNo, _
                               MessageBoxIcon.Question) = DialogResult.Yes Then
                TopPanl.Visible = True
                LblPriPnl.Visible = False
                Me.CmdTok1.Focus()
            End If
        Else
            Me.Text = "ラベル発行枚数入力画面"
            TopPanl.Visible = True
            LblPriPnl.Visible = False
            Me.CmdTok1.Focus()
        End If
    End Sub

    Private Sub EndBtn2_Click(sender As System.Object, e As System.EventArgs) Handles BtnBac1.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles BtnOrder1.Click
        '発注日をコピーするボタンを押下した際のイベント
        Dim intHatyuday As Integer
        Dim intCell1 As Integer 'コピーする発注日
        Dim intCell2 As Integer '値が入力されているかチェックを行うセル
        Dim intCell3 As Integer '値が入力されているかチェックを行うセル

        Select Case strLblTye
            Case "D01" 'ラベルタイプＤ０１用
                intCell1 = 4
                intCell2 = 6
                intCell3 = 7
            Case "G01" 'ラベルタイプＧ０１用
                intCell1 = 10
                intCell2 = 12
                intCell3 = 12

            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select

        '一番上に入力されている発注日(出荷日)を取得
        For i = 0 To DtgLblPri.Rows.Count - 1
            If Not DtgLblPri.Rows(i).Cells(intCell1).Value = Nothing Then
                intHatyuday = DtgLblPri.Rows(i).Cells(intCell1).Value
                i = DtgLblPri.Rows.Count - 1
            End If
        Next
        'ケース数又はオリコン数が入力されているカラムを取得し、発注日（出荷日）を挿入
        For i = 0 To DtgLblPri.Rows.Count - 1
            If Not DtgLblPri.Rows(i).Cells(intCell2).Value = Nothing Or
                Not DtgLblPri.Rows(i).Cells(intCell3).Value = Nothing Then
                DtgLblPri.Rows(i).Cells(intCell1).Value = intHatyuday
            End If
        Next

    End Sub

    Private Sub CopyBtn2_Click(sender As System.Object, e As System.EventArgs) Handles BtnDel1.Click
        '納品日をコピーするボタンを押下した際のイベント
        Dim intHatyuDay As Integer
        Dim intCell1 As Integer 'コピーする列
        Dim intCell2 As Integer '該当セルがスペース以外の場合、同じ行の納品日へ納品日をコピーする
        Dim intCell3 As Integer '該当セルがスペース以外の場合、同じ行の納品日へ納品日をコピーする
        Dim dt As DateTime
        Dim f As String = "yyMMdd"
        Dim strhani As String
        Dim strErrorMessage As String = ""
        Dim strErrorMessage1 As String = "納入日がスペースです。入力して下さい。"

        Dim strErrorMessage2 As String = "入力された日付がカレンダーの範囲外です"

        Select Case strLblTye
            Case "D01" 'ラベルタイプＤ０１用
                intCell1 = 5
                intCell2 = 6
                intCell3 = 7
            Case "G01" 'ラベルタイプＧ０１用
                intCell1 = 11
                intCell2 = 12
                intCell3 = 12
            Case "A01" 'ラベルタイプＡ０１用
                intCell1 = 17
                intCell2 = 18
                intCell3 = 18
            Case "Y01" 'ラベルタイプＹ０１用
                intCell1 = 22
                intCell2 = 23
                intCell3 = 23
            Case "M01" 'ラベルタイプＭ０１用
                intCell1 = 35
                'intCell2 = 36
                'intCell3 = 37


                'カレンダーの範囲内チェック
                strhani = TxtDel1.Text
                Try
                    dt = DateTime.ParseExact(strhani, f, Nothing)
                Catch ex As System.FormatException
                    strErrorMessage = strErrorMessage2
                End Try

                If TxtDel1.Text = "" Then
                    strErrorMessage = strErrorMessage1

                End If

                If Not strErrorMessage = "" Then
                    MessageBox.Show(strErrorMessage, _
                    "エラー", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Error)
                    Exit Sub
                End If


                intHatyuDay = TxtDel1.Text

                For i = 0 To DtgLblPri.Rows.Count - 1

                    DtgLblPri.Rows(i).Cells(intCell1).Value = intHatyuDay

                Next

                Exit Sub

            Case "M02" 'ラベルタイプＭ０２用
                intCell1 = 48
                intCell2 = 49
                intCell3 = 50

            Case Else
                MessageBox.Show(ErrorMessage100, _
                "エラー", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)
                Exit Sub
        End Select

        '一番上に入力されている納品日を取得
        For i = 0 To DtgLblPri.Rows.Count - 1
            If Not DtgLblPri.Rows(i).Cells(intCell1).Value = Nothing Then
                intHatyuDay = DtgLblPri.Rows(i).Cells(intCell1).Value
                i = DtgLblPri.Rows.Count - 1
            End If
        Next
        'ケース数又はオリコン数が入力されているカラムを取得し、発注日を挿入
        For i = 0 To DtgLblPri.Rows.Count - 1
            If Not DtgLblPri.Rows(i).Cells(intCell2).Value = Nothing Or
                Not DtgLblPri.Rows(i).Cells(intCell3).Value = Nothing Then
                DtgLblPri.Rows(i).Cells(intCell1).Value = intHatyuDay
            End If
        Next
    End Sub

    Private Sub ClearBtn_Click(sender As System.Object, e As System.EventArgs) Handles BtnClear1.Click
        Dim strCName As String = ""
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer = 0
        Dim intChkFlg As Integer = 0
        Dim i As Integer = 0
        Dim Cntup As Integer = 0

        '接続文字列を設定
        Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection.Open()

        '初期化エリア
        intChkFlg = 0

        strCName = CmdCen1.Text

        '*********得意先IDの取得****************************************************************************************************
        intTokID = 0
        For Cntbb = 0 To Wrk_DataTok.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmdTok1.Text = Wrk_DataTok(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_DataTok(0, Cntbb)
            End If
        Next Cntbb
        '***************************************************************************************************************************

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
            '**********データグリッドに初期値出力***************************************************************************************

            '得意先増えたら
            'コマンド作成
            Command = Connection.CreateCommand

            'ＳＱＬ作成
            Select Case strLblTye
                Case "D01" 'ラベルタイプＤ０１用
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "YokoCen,KenName,StrNo,StrName"
                    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    sqlWhereCon = "Tbl_CenMas.CenName = '" & strCName & "' AND " &
                                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID "
                    sqlOrderByCon = "Tbl_StrMgt.YokoCen,Tbl_StrMgt.KenName,Tbl_StrMgt.StrNo"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        DtgLblPri.Rows.Add()
                        Idx = DtgLblPri.Rows.Count - 1
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm1").Value = DataReader.Item("YokoCen").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm2").Value = DataReader.Item("KenName").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm3").Value = DataReader.Item("StrNo").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm4").Value = DataReader.Item("StrName").ToString

                    Loop


                    'ＤＢ切断
                    DataReader.Close()
                    Connection.Close()

                    DataReader.Dispose()
                    Command.Dispose()
                    Connection.Dispose()

                Case "G01" 'ラベルタイプＧ０１用
                    'SQL作成 OrderByあり
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "StrName,KenName"
                    sqlTableName = "Tbl_StrMgt"
                    sqlWhereCon = "CorpID = '" & intTokID & "' "
                    sqlOrderByCon = "KenName,StrName"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        DtgLblPri.Rows.Add()
                        Idx = DtgLblPri.Rows.Count - 1
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm9").Value = DataReader.Item("StrName").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm10").Value = DataReader.Item("KenName").ToString

                    Loop


                    'ＤＢ切断
                    DataReader.Close()
                    Connection.Close()

                    DataReader.Dispose()
                    Command.Dispose()
                    Connection.Dispose()

                Case "A01" 'ラベルタイプＡ０１用
                    'SQL作成 OrderByあり
                    '初期化
                    'SQL文の作成
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "Remarks1,CenName,Remarks2"
                    sqlTableName = "Tbl_CenMas"
                    sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                                  "CenName = '" & CmdCen1.Text & "'"
                    sqlOrderByCon = "CenName"

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read
                        DtgLblPri.Rows.Add()
                        Idx = DtgLblPri.Rows.Count - 1
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm15").Value = DataReader.Item("Remarks1").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm16").Value = DataReader.Item("CenName").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm17").Value = DataReader.Item("Remarks2").ToString

                    Loop

                    'ＤＢ切断
                    DataReader.Close()
                    Connection.Close()

                    DataReader.Dispose()
                    Command.Dispose()
                    Connection.Dispose()

                Case "Y01" 'ラベルタイプＹ０１用
                    i = 0
                    Cntup = 0
                    '部門情報を取得
                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "Tbl_Remarks.Remarks1"
                    sqlTableName = "TBL_CorpMas,Tbl_Remarks"
                    sqlWhereCon = "Tbl_CorpMas.CorpID = '" & intTokID & "' AND " &
                                  "Tbl_CorpMas.LblTypeID = Tbl_Remarks.LblTypeID"
                    sqlOrderByCon = "RemarksID"

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        'ワークエリアへのセット
                        Wrk_DataRe(0, i) = DataReader.Item("Remarks1").ToString

                        'ワークエリアの拡張（配列を追加）
                        ReDim Preserve Wrk_DataRe(2, Cntup + 1)
                        Cntup = Cntup + 1
                        i = i + 1
                    Loop

                    i = 0
                    Cntup = 0

                    'ＤＢ切断
                    DataReader.Close()
                    DataReader.Dispose()

                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""

                    '各ＳＱＬ文の構文設定
                    sqlField1 = "StrNo,StrName"

                    If CmbStr1.Text = "全ての店舗" Then

                        sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                        sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                                      "Tbl_CenMas.CenID = '" & strCenID & "' AND " &
                                      "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                        sqlOrderByCon = "StrNo"

                    Else
                        sqlTableName = "Tbl_StrMgt"
                        sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                                      "StrName = '" & CmbStr1.Text & "'"
                        sqlOrderByCon = "StrNo"
                    End If

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                    DtgLblPri.Rows.Clear()

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read
                        For Cntcc = 0 To Wrk_DataRe.GetLength(1) - 2
                            DtgLblPri.Rows.Add()
                            Idx = DtgLblPri.Rows.Count - 1
                            DtgLblPri.Rows(Idx).Cells("DtgLblPriClm20").Value = DataReader.Item("StrNo").ToString
                            DtgLblPri.Rows(Idx).Cells("DtgLblPriClm21").Value = DataReader.Item("StrName").ToString
                            DtgLblPri.Rows(Idx).Cells("DtgLblPriClm22").Value = Wrk_DataRe(0, Cntcc)
                        Next
                    Loop

                    'ＤＢ切断
                    DataReader.Close()
                    Connection.Close()

                    DataReader.Dispose()
                    Command.Dispose()
                    Connection.Dispose()

                Case "M01" 'ラベルタイプＭ０１用　マキヤ用

                    '*********センターマスターのRemarks1を取得**********
                    strRemarks1 = ""
                    For Cntbb = 0 To Wrk_DataCen.GetLength(1) - 1
                        '二次元配列の得意先名とコンボボックスの値を比較
                        If CmdCen1.Text = Wrk_DataCen(1, Cntbb) Then
                            '二次元配列の得意先ＩＤを出力
                            strRemarks1 = Wrk_DataCen(0, Cntbb)
                        End If
                    Next Cntbb

                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    '各ＳＱＬ文の構文設定
                    '名称マスターテーブルのコメントを取得
                    sqlField1 = "NameTitle"
                    sqlTableName = "Tbl_NameTitle"
                    sqlWhereCon = "CenID = " & strRemarks1 & " AND " &
                                  "DivisionID = 3 AND " &
                                  "NameID = 1"

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read
                        strCommentName = DataReader.Item("NameTitle").ToString
                    Loop

                    DataReader.Close()
                    DataReader.Dispose()


                    'コマンド作成
                    Command = Connection.CreateCommand
                    'フロア情報を取得
                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "Remarks1,Remarks2"
                    sqlTableName = "Tbl_Remarks"
                    sqlWhereCon = "RemarksID = " & 3 & ""

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        'ワークエリアへのセット
                        Wrk_DataRe(0, i) = DataReader.Item("Remarks1").ToString
                        Wrk_DataRe(1, i) = DataReader.Item("Remarks2").ToString

                        'ワークエリアの拡張（配列を追加）
                        ReDim Preserve Wrk_DataRe(2, Cntup + 1)
                        Cntup = Cntup + 1
                        i = i + 1
                    Loop

                    i = 0
                    Cntup = 0
                    'コマンド作成
                    Command = Connection.CreateCommand
                    '部門情報を取得
                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "Remarks1,Remarks2,Remarks3"
                    sqlTableName = "Tbl_Remarks"
                    sqlWhereCon = "RemarksID = " & 2 & ""

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        'ワークエリアへのセット
                        Wrk_DataRe2(0, i) = DataReader.Item("Remarks1").ToString
                        Wrk_DataRe2(1, i) = DataReader.Item("Remarks2").ToString
                        Wrk_DataRe2(2, i) = DataReader.Item("Remarks3").ToString
                        'ワークエリアの拡張（配列を追加）
                        ReDim Preserve Wrk_DataRe2(2, Cntup + 1)
                        Cntup = Cntup + 1
                        i = i + 1
                    Loop

                    i = 0
                    Cntup = 0

                    'ＤＢ切断
                    DataReader.Close()
                    DataReader.Dispose()

                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定

                    sqlField1 = "StrNo,StrName"

                    'If CmbStr1.Text = "全ての店舗" Then

                    '    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    '    sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                    '                  "Tbl_CenMas.CenID = '" & strCenID & "' AND " &
                    '                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                    '    sqlOrderByCon = "StrNo"

                    'Else
                    '    sqlTableName = "Tbl_StrMgt"
                    '    sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                    '                  "StrName = '" & CmbStr1.Text & "'"
                    '    sqlOrderByCon = "StrNo"
                    'End If

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon &
                                   sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement


                    'データリーダーにデータ取得
                    'DataReader = Command.ExecuteReader
                    'Do Until Not DataReader.Read

                    '    '部門の数だけループ
                    '    For Cntcc = 0 To Wrk_DataRe2.GetLength(1) - 2

                    '        'フロア名の取得
                    '        For Cntdd = 0 To Wrk_DataRe.GetLength(1) - 2
                    '            If Wrk_DataRe2(0, Cntcc) = Wrk_DataRe(0, Cntdd) Then
                    '                '8/26
                    '                strFloorName = Wrk_DataRe(0, Cntdd) & "：" & Wrk_DataRe(1, Cntdd)
                    '                Exit For
                    '            Else
                    '                strFloorName = "登録なし"
                    '            End If
                    '        Next

                    '        DtgLblPri.Rows.Add()
                    '        Idx = DtgLblPri.Rows.Count - 1
                    '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm26").Value = DataReader.Item("StrNo").ToString
                    '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm27").Value = DataReader.Item("StrName").ToString
                    '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm28").Value = strFloorName
                    '        '8/25
                    '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm29").Value = Wrk_DataRe2(1, Cntcc) & "：" & Wrk_DataRe2(2, Cntcc)
                    '        'DtgLblPri.Rows(Idx).Cells("DtgLblPriClm29").Value = Wrk_DataRe2(2, Cntcc)
                    '        DtgLblPriClm35.HeaderText = strCommentName

                    '    Next
                    'Loop
                    'ＤＢ切断
                    DataReader.Close()
                    Connection.Close()

                    DataReader.Dispose()
                    Command.Dispose()
                    Connection.Dispose()

                Case "M02" 'ラベルタイプＭ０２用
                    'SQL作成 OrderByあり
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "StrNo,StrName"
                    sqlTableName = "Tbl_StrMgt"
                    sqlWhereCon = "CorpID = '" & intTokID & "' "
                    sqlOrderByCon = "KenName,StrName"
                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        DtgLblPri.Rows.Add()
                        Idx = DtgLblPri.Rows.Count - 1
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm39").Value = DataReader.Item("StrNo").ToString
                        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm40").Value = DataReader.Item("StrName").ToString

                    Loop


                    'ＤＢ切断
                    DataReader.Close()
                    Connection.Close()

                    DataReader.Dispose()
                    Command.Dispose()
                    Connection.Dispose()


                Case Else
                    MessageBox.Show(ErrorMessage100, _
                    "エラー", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Error)
                    Exit Sub
            End Select

        End If

    End Sub
    'Excel出力処理
    Private Sub ExlBtn_Click_1(sender As System.Object, e As System.EventArgs) Handles BtnExcelPrint.Click
        Dim objExcel As Excel.Application = Nothing
        Dim objWorkBook As Excel.Workbook = Nothing
        Dim intRow As Integer
        Dim intClm As Integer
        Dim strDta As String

        objExcel = New Excel.Application
        objWorkBook = objExcel.Workbooks.Add

        'マウスカーソルを変更する
        Me.Cursor = Cursors.WaitCursor

        ' DataGridViewのセルのデータ取得
        Dim strOutData As String(,) = New String( _
            DtgLblPri.Rows.Count - 1, DtgLblPri.Columns.Count - 1) {}
        For intRow = 0 To DtgLblPri.Rows.Count - 1
            For intClm = 0 To DtgLblPri.Columns.Count - 1
                strDta = ""
                If DtgLblPri.Rows(intRow).Cells(intClm).Value _
                    Is Nothing = False Then
                    strDta = DtgLblPri.Rows(intRow).Cells(intClm).Value.ToString()
                End If
                Select Case strLblTye
                    Case "D01"
                        strOutData(intRow, intClm) = strDta

                    Case "G01"

                        If intClm > 7 Then
                            strOutData(intRow, intClm - 7) = strDta
                        Else
                            If intClm = 7 Then
                                strOutData(intRow, intClm - 7) = CmdTok1.Text
                            Else
                                strOutData(intRow, intClm) = strDta
                            End If
                        End If

                    Case "A01"
                        If intClm > 12 Then
                            strOutData(intRow, intClm - 13) = strDta
                        End If

                    Case "Y01"
                        If intClm > 18 Then
                            strOutData(intRow, intClm - 19) = strDta
                        End If

                    Case "M01"
                        If intClm > 24 Then
                            strOutData(intRow, intClm - 25) = strDta
                        End If

                    Case "M02"
                        If intClm > 37 Then
                            strOutData(intRow, intClm - 38) = strDta
                        End If


                    Case Else
                        MessageBox.Show(ErrorMessage100, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
                        Exit Sub
                End Select


            Next
        Next



        ' EXCELにデータ転送
        Select Case strLblTye

            Case "M01"
                Dim ran As String = "A1:M" & DtgLblPri.Rows.Count
                objWorkBook.Sheets(1).Range(ran) = strOutData
            Case Else
                Dim ran As String = "A1:" & _
                    Chr(Asc("A") + DtgLblPri.Columns.Count - 1) & DtgLblPri.Rows.Count
                objWorkBook.Sheets(1).Range(ran) = strOutData

        End Select


        ' エクセル表示
        objExcel.Visible = True

        ' EXCEL解放
        Marshal.ReleaseComObject(objWorkBook)
        Marshal.ReleaseComObject(objExcel)
        objWorkBook = Nothing
        objExcel = Nothing

        'マウスカーソルを元に戻す
        Me.Cursor = Cursors.Default
    End Sub

    '******************データグリッドビューの入力項目、ＩＭＥ制御************************
    '登録画面の入力制御
    Private Sub DtgLblPri_CellEnter(ByVal sender As Object, _
             ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
             Handles DtgLblPri.CellEnter

        '---- 列番号を調べて制御 ------
        Select Case e.ColumnIndex
            Case 0, 34
                'この列は日本語入力ON
                DtgLblPri.ImeMode = Windows.Forms.ImeMode.Hiragana

            Case 4, 5, 6, 7, 10, 11, 12, 13, 17, 18, 21, 22, 23, 25, 28, 35, 36, 37
                'この列はIME無効(半角英数のみ)
                DtgLblPri.ImeMode = Windows.Forms.ImeMode.Disable
        End Select
    End Sub
    '*****************END*******************************************
    Private Sub DtgLblPri_CellCellClick _
        (sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles DtgLblPri.CellClick

        'セルマウスクリック時のイベント
        'DtgLblPri.BeginEdit(True)
    End Sub

    'CellValidatedイベントハンドラ 
    Private Sub DtgLblPri_CellValidated(ByVal sender As Object, _
        ByVal e As DataGridViewCellEventArgs)


        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        'エラーテキストを消す 
        Dgv.Rows(e.RowIndex).ErrorText = Nothing
    End Sub
    '*******************データ型エラーチェック（正規表現）**********************
    'CellValidatingイベントハンドラ 
    Private Sub DtgLblPri_CellValidating(ByVal sender As Object, _
        ByVal e As DataGridViewCellValidatingEventArgs) _
             Handles DtgLblPri.CellValidating

        '変数宣言
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Dgv As DataGridView = DirectCast(sender, DataGridView)
        Dim dt As DateTime
        Dim f As String
        Dim Errflg As Integer = 0
        Dim Dtgtai As String
        Dim ErrorMessage As String = "" '出力用エラーメッセージ変数
        Dim idx As Integer
        Dim strErrorMessage1 As String = "入力された値に誤りがあります。" & Environment.NewLine &
                                         "入力例：2014年１月１２日の場合" & Environment.NewLine &
                                         "　　　　①140112"
        Dim strErrorMessage2 As String = "入力された日付がカレンダーの範囲外です"
        Dim strErrorMessage3 As String = "桁数がオーバーしています。５桁が入力可能な最大値です"
        Dim strErrorMessage4 As String = "数値以外入力できません。再入力して下さい"
        Dim strErrorMessage5 As String = "桁数がオーバーしています。999が入力可能な最大値です"
        Dim strErrorMessage6 As String = "文章中に空白は入力できません。空白を削除して下さい"
        Dim strErrorMessage7 As String = "空白は登録できません。何か文字を入力して下さい"
        Dim strErrorMessage8 As String = "登録していない店番です。再入力して下さい"
        Dim strErrorMessage9 As String = "登録していない部門番号です。再入力して下さい"


        intTokID = 0
        For Cntbb = 0 To Wrk_DataTok.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmdTok1.Text = Wrk_DataTok(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_DataTok(0, Cntbb)
            End If
        Next Cntbb


        f = "yyMMdd"

        '新しい行のセルでなく、セルの内容が変更されている時だけ検証する 
        If e.RowIndex = Dgv.NewRowIndex OrElse Not Dgv.IsCurrentCellDirty Then
            Exit Sub
        End If

        '***発注日の正規表現による制御(ラベルタイプD01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm5" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            Errflg = 0
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm5" And _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[1-2][0-9][0-1][0-9][0-3][0-9]") Then
            Else
                ErrorMessage = strErrorMessage1
                e.Cancel = True
                Errflg = 1
            End If

            If Errflg = 0 Then
                'カレンダーの範囲内チェック
                Try
                    dt = DateTime.ParseExact(e.FormattedValue.ToString(), f, Nothing)
                Catch ex As System.FormatException
                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End Try
            End If

        End If

        '***横持の正規表現による制御(ラベルタイプD01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm1" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm1" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\w{5,}") Then

                ErrorMessage = strErrorMessage3
                e.Cancel = True
            End If


        End If


        '***納品日の正規表現による制御(ラベルタイプD01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm6" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            Errflg = 0
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm6" AndAlso _
                Not System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[1-2][0-9][0-1][0-9][0-3][0-9]") Then

                ErrorMessage = strErrorMessage1
                e.Cancel = True
                Errflg = 1
            End If

            If Errflg = 0 Then
                Try
                    dt = DateTime.ParseExact(e.FormattedValue.ToString(), f, Nothing)
                Catch ex As System.FormatException
                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End Try
            End If

        End If


        '***ケース数の正規表現による制御(ラベルタイプD01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            '入力された値の桁数をチェック。１～５ケタ以外はエラー
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm7" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\w{4,}") Then

                ErrorMessage = strErrorMessage5
                e.Cancel = True
            End If
        End If

        '***オリコン数の正規表現による制御(ラベルタイプD01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm8" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm8" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm8" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm8" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

            '入力された値の桁数をチェック。１～５ケタ以上はエラー
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm8" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\w{4,}") Then

                ErrorMessage = strErrorMessage5
                e.Cancel = True
            End If

        End If

        '***出荷日の正規表現による制御(ラベルタイプG01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm11" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
            Errflg = 0
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm11" And _
                Not System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[1-2][0-9][0-1][0-9][0-3][0-9]") Then

                ErrorMessage = strErrorMessage1
                e.Cancel = True
                Errflg = 1
            End If

            If Errflg = 0 Then
                Try
                    dt = DateTime.ParseExact(e.FormattedValue.ToString(), f, Nothing)
                Catch ex As System.FormatException

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End Try
            End If

        End If

        '***納品日の正規表現による制御(ラベルタイプG01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm12" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            Errflg = 0
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm12" AndAlso _
                Not System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[1-2][0-9][0-1][0-9][0-3][0-9]") Then

                ErrorMessage = strErrorMessage1
                e.Cancel = True
                Errflg = 1
            End If

            If Errflg = 0 Then
                Try
                    dt = DateTime.ParseExact(e.FormattedValue.ToString(), f, Nothing)
                Catch ex As System.FormatException

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End Try
            End If

        End If

        '***個数の正規表現による制御(ラベルタイプG01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm13" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm13" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm13" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm13" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            '入力された値の桁数をチェック。１～５ケタ以外はエラー
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm13" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\w{4,}") Then

                ErrorMessage = strErrorMessage5
                e.Cancel = True
            End If

        End If

        '***PO番号制御(ラベルタイプA01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm14" AndAlso _
                Not e.FormattedValue.ToString() = "" Then

            intRenewFlg = 1
        End If

        '***納品日の正規表現による制御(ラベルタイプA01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm18" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            Errflg = 0
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm18" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[1-2][0-9][0-1][0-9][0-3][0-9]") Then
            Else
                ErrorMessage = strErrorMessage1
                e.Cancel = True
                Errflg = 1
            End If

            If Errflg = 0 Then
                Try
                    dt = DateTime.ParseExact(e.FormattedValue.ToString(), f, Nothing)
                Catch ex As System.FormatException

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End Try
            End If

        End If

        '***個数の正規表現による制御(ラベルタイプA01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm19" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm19" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm19" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm19" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            '入力された値の桁数をチェック。１～５ケタ以外はエラー
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm19" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\w{4,}") Then

                ErrorMessage = strErrorMessage5
                e.Cancel = True
            End If

        End If

        '***部門数の正規表現による制御(ラベルタイプY01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm22" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm22" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm22" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm22" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

        End If

        '***お届日の正規表現による制御(ラベルタイプY01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm23" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            Errflg = 0
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm23" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[1-2][0-9][0-1][0-9][0-3][0-9]") Then
            Else
                ErrorMessage = strErrorMessage1
                e.Cancel = True
                Errflg = 1
            End If

            If Errflg = 0 Then
                Try
                    dt = DateTime.ParseExact(e.FormattedValue.ToString(), f, Nothing)
                Catch ex As System.FormatException

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End Try
            End If

        End If

        '***個口数の正規表現による制御(ラベルタイプY01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm24" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm24" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm24" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm24" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

        End If

        '***店番の正規表現
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm26" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            '空白チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm26" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage7
                End If

                e.Cancel = True
            End If

            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm26" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm26" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm26" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

        End If

        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm26" AndAlso _
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

            '選択しているデータグリッドビューの行を取得
            idx = Me.DtgLblPri.CurrentCell.RowIndex

            Do Until Not DataReader.Read

                DtgLblPri.Rows(idx).Cells(26).Value = DataReader.Item("StrName").ToString
            Loop

            If DtgLblPri.Rows(idx).Cells(26).Value = "" Then

                ErrorMessage = strErrorMessage8
                e.Cancel = True
            End If

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If

        '***部門名の正規表現
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm29" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            '空白チェック
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm29" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "\s") Then
                If e.FormattedValue.ToString().Length >= 2 Then

                    ErrorMessage = strErrorMessage6
                Else

                    ErrorMessage = strErrorMessage7
                End If

                e.Cancel = True
            End If

            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm29" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm29" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

            If Not ErrorMessage = "" Then
                GoTo EndLine
            End If

        End If

        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm29" AndAlso _
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

            '選択しているデータグリッドビューの行を取得
            idx = Me.DtgLblPri.CurrentCell.RowIndex

            '店番が既に登録されているか確認するＳＱＬ
            sqlField1 = "Remarks1,Remarks2,Remarks3"
            sqlTableName = "Tbl_Remarks"
            sqlWhereCon = "RemarksID = " & 2 & " AND " &
                          "Remarks2 = " & e.FormattedValue.ToString()

            sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon
            'SQL
            Command.CommandText = sqlStatement

            'データリーダーにデータ取得
            DataReader = Command.ExecuteReader

            If DataReader.HasRows = 0 Then
                ErrorMessage = strErrorMessage9
                e.Cancel = True
            End If
            '入力された部門番号に対して、部門名とフロア名を出力
            Do Until Not DataReader.Read

                Me.DtgLblPri.Rows(idx).Cells(28).Value = e.FormattedValue.ToString() &
                                                      "：" &
                                                      DataReader.Item("Remarks3").ToString
                Dtgtai = Me.DtgLblPri.Rows(idx).Cells(28).Value
                Me.DtgLblPri.CancelEdit()
                e.Cancel = False

                'フロア名の取得
                For Cntdd = 0 To Wrk_DataRe.GetLength(1) - 2
                    If Wrk_DataRe(0, Cntdd) = DataReader.Item("Remarks1") Then
                        Me.DtgLblPri.Rows(idx).Cells(27).Value = Wrk_DataRe(0, Cntdd) &
                                                                 "：" &
                                                                 Wrk_DataRe(1, Cntdd)
                    End If


                Next

            Loop

            'If DtgLblPri.Rows(idx).Cells(28).Value = "" Then
            '    ErrorMessage = strErrorMessage9
            '    e.Cancel = True

            'End If

            'ＤＢ切断
            DataReader.Close()
            Connection.Close()

            DataReader.Dispose()
            Command.Dispose()
            Connection.Dispose()

        End If




        '***正梱数の正規表現による制御(ラベルタイプM01用)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm37" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm37" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm37" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm37" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

        End If

        '***バラ数の正規表現による制御(ラベルタイプM01用（マキヤ用）)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm38" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm38" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm38" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm38" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

        End If

        '***納入日の正規表現による制御(ラベルタイプM01用（マキヤ用）)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm36" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            Errflg = 0
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm36" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[1-2][0-9][0-1][0-9][0-3][0-9]") Then
            Else
                ErrorMessage = strErrorMessage1
                e.Cancel = True
                Errflg = 1
            End If

            If Errflg = 0 Then
                Try
                    dt = DateTime.ParseExact(e.FormattedValue.ToString(), f, Nothing)
                Catch ex As System.FormatException

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End Try
            End If

        End If
        '***納入日の正規表現による制御(ラベルタイプM02用（第２関東MrMax用）)
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm49" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            Errflg = 0
            If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm49" AndAlso _
                System.Text.RegularExpressions.Regex.IsMatch( _
                e.FormattedValue.ToString(), "[1-2][0-9][0-1][0-9][0-3][0-9]") Then
            Else
                ErrorMessage = strErrorMessage1
                e.Cancel = True
                Errflg = 1
            End If

            If Errflg = 0 Then
                Try
                    dt = DateTime.ParseExact(e.FormattedValue.ToString(), f, Nothing)
                Catch ex As System.FormatException

                    ErrorMessage = strErrorMessage2
                    e.Cancel = True
                End Try
            End If

        End If

        '***分類コードの正規表現による制御(ラベルタイプM02用(第２関東MrMax用))
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm50" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm50" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm50" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If


        End If

        '***梱包数の正規表現による制御(ラベルタイプM02用(第２関東MrMax用))
        If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm51" AndAlso _
                Not e.FormattedValue.ToString() = "" Then
            intRenewFlg = 1
            '入力された値が数字かチェック
            If e.FormattedValue.ToString().Length = 1 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm51" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

            If e.FormattedValue.ToString().Length = 2 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm51" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If
            If e.FormattedValue.ToString().Length = 3 Then
                If Dgv.Columns(e.ColumnIndex).Name = "DtgLblPriClm51" AndAlso _
                    Not System.Text.RegularExpressions.Regex.IsMatch( _
                    e.FormattedValue.ToString(), "[0-9][0-9][0-9]") Then

                    ErrorMessage = strErrorMessage4
                    e.Cancel = True
                End If
            End If

        End If

EndLine:

        If Not ErrorMessage = "" Then
            'エラーメッセージの表示
            MessageBox.Show(ErrorMessage, _
                            "エラー", _
                            MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub CmbStr1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbStr1.SelectedIndexChanged
        Dim strCName As String = ""
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim Idx As Integer = 0
        Dim intChkFlg As Integer = 0
        Dim i As Integer = 0
        Dim Cntup As Integer = 0

        '接続文字列を設定
        Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection.Open()

        '初期化エリア
        intChkFlg = 0

        strCName = CmdCen1.Text

        '*********得意先IDの取得****************************************************************************************************
        intTokID = 0
        For Cntbb = 0 To Wrk_DataTok.GetLength(1) - 1
            '二次元配列の得意先名とコンボボックスの値を比較
            If CmdTok1.Text = Wrk_DataTok(1, Cntbb) Then
                '二次元配列の得意先ＩＤを出力
                intTokID = Wrk_DataTok(0, Cntbb)
            End If
        Next Cntbb
        '***************************************************************************************************************************

        If intRenewFlg = 1 Then

            If MessageBox.Show("印刷されていないデータが残っています。画面を切り替えてもよろしいですか？", _
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
            '**********データグリッドに初期値出力***************************************************************************************
            'コマンド作成
            Command = Connection.CreateCommand

            'ＳＱＬ作成
            Select Case strLblTye
                Case "D01" 'ラベルタイプＤ０１用

                Case "G01" 'ラベルタイプＧ０１用

                Case "A01" 'ラベルタイプＡ０１用

                Case "M02" 'ラベルタイプＭ０２用

                Case "Y01" 'ラベルタイプＹ０１用
                    '部門情報を取得
                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "Remarks1,Remarks2,Remarks3"
                    sqlTableName = "Tbl_Remarks"
                    sqlWhereCon = "RemarksID = " & 1 & ""
                    sqlOrderByCon = "RemarksID"

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        'ワークエリアへのセット
                        Wrk_DataRe(0, i) = DataReader.Item("Remarks1").ToString

                        'ワークエリアの拡張（配列を追加）
                        ReDim Preserve Wrk_DataRe(2, Cntup + 1)
                        Cntup = Cntup + 1
                        i = i + 1
                    Loop

                    i = 0
                    Cntup = 0

                    'ＤＢ切断
                    DataReader.Close()
                    DataReader.Dispose()

                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""

                    '各ＳＱＬ文の構文設定
                    sqlField1 = "StrNo,StrName"

                    If CmbStr1.Text = "全ての店舗" Then

                        sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                        sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                                      "Tbl_CenMas.CenID = '" & strCenID & "' AND " &
                                      "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                        sqlOrderByCon = "StrNo"

                    Else
                        sqlTableName = "Tbl_StrMgt"
                        sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                                      "StrName = '" & CmbStr1.Text & "'"
                        sqlOrderByCon = "StrNo"
                    End If

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon & sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement

                    DtgLblPri.Rows.Clear()

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read
                        For Cntcc = 0 To Wrk_DataRe.GetLength(1) - 2
                            DtgLblPri.Rows.Add()
                            Idx = DtgLblPri.Rows.Count - 1
                            DtgLblPri.Rows(Idx).Cells("DtgLblPriClm20").Value = DataReader.Item("StrNo").ToString
                            DtgLblPri.Rows(Idx).Cells("DtgLblPriClm21").Value = DataReader.Item("StrName").ToString
                            DtgLblPri.Rows(Idx).Cells("DtgLblPriClm22").Value = Wrk_DataRe(0, Cntcc)
                        Next
                    Loop

                    'ＤＢ切断
                    DataReader.Close()
                    Connection.Close()

                    DataReader.Dispose()
                    Command.Dispose()
                    Connection.Dispose()

                Case "M01" 'ラベルタイプＭ０１用　マキヤ用

                    '*********センターマスターのRemarks1を取得**********
                    strRemarks1 = ""
                    For Cntbb = 0 To Wrk_DataCen.GetLength(1) - 1
                        '二次元配列の得意先名とコンボボックスの値を比較
                        If CmdCen1.Text = Wrk_DataCen(1, Cntbb) Then
                            '二次元配列の得意先ＩＤを出力
                            strRemarks1 = Wrk_DataCen(0, Cntbb)
                        End If
                    Next Cntbb

                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    '各ＳＱＬ文の構文設定
                    '名称マスターテーブルのコメントを取得
                    sqlField1 = "NameTitle"
                    sqlTableName = "Tbl_NameTitle"
                    sqlWhereCon = "CenID = " & strRemarks1 & " AND " &
                                  "DivisionID = 3 AND " &
                                  "NameID = 1"

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read
                        strCommentName = DataReader.Item("NameTitle").ToString
                    Loop

                    DataReader.Close()
                    DataReader.Dispose()


                    'コマンド作成
                    Command = Connection.CreateCommand
                    'フロア情報を取得
                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "Remarks1,Remarks2"
                    sqlTableName = "Tbl_Remarks"
                    sqlWhereCon = "RemarksID = " & 3 & ""

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        'ワークエリアへのセット
                        Wrk_DataRe(0, i) = DataReader.Item("Remarks1").ToString
                        Wrk_DataRe(1, i) = DataReader.Item("Remarks2").ToString

                        'ワークエリアの拡張（配列を追加）
                        ReDim Preserve Wrk_DataRe(2, Cntup + 1)
                        Cntup = Cntup + 1
                        i = i + 1
                    Loop

                    i = 0
                    Cntup = 0
                    'コマンド作成
                    Command = Connection.CreateCommand
                    '部門情報を取得
                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定
                    sqlField1 = "Remarks1,Remarks2,Remarks3"
                    sqlTableName = "Tbl_Remarks"
                    sqlWhereCon = "RemarksID = " & 2 & ""

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

                    Command.CommandText = sqlStatement

                    'データリーダーにデータ取得
                    DataReader = Command.ExecuteReader
                    Do Until Not DataReader.Read

                        'ワークエリアへのセット
                        Wrk_DataRe2(0, i) = DataReader.Item("Remarks1").ToString
                        Wrk_DataRe2(1, i) = DataReader.Item("Remarks2").ToString
                        Wrk_DataRe2(2, i) = DataReader.Item("Remarks3").ToString
                        'ワークエリアの拡張（配列を追加）
                        ReDim Preserve Wrk_DataRe2(2, Cntup + 1)
                        Cntup = Cntup + 1
                        i = i + 1
                    Loop

                    i = 0
                    Cntup = 0

                    'ＤＢ切断
                    DataReader.Close()
                    DataReader.Dispose()

                    'SQL文の作成
                    '初期化
                    sqlStatement = ""
                    sqlField1 = ""
                    sqlTableName = ""
                    sqlWhereCon = ""
                    sqlOrderByCon = ""
                    '各ＳＱＬ文の構文設定

                    sqlField1 = "StrNo,StrName"

                    'If CmbStr1.Text = "全ての店舗" Then

                    '    sqlTableName = "Tbl_StrMgt,Tbl_CenMas"
                    '    sqlWhereCon = "Tbl_StrMgt.CorpID = '" & intTokID & "' AND " &
                    '                  "Tbl_CenMas.CenID = '" & strCenID & "' AND " &
                    '                  "Tbl_StrMgt.CenID = Tbl_CenMas.CenID"
                    '    sqlOrderByCon = "StrNo"

                    'Else
                    '    sqlTableName = "Tbl_StrMgt"
                    '    sqlWhereCon = "CorpID = '" & intTokID & "' AND " &
                    '                  "StrName = '" & CmbStr1.Text & "'"
                    '    sqlOrderByCon = "StrNo"
                    'End If

                    sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon &
                                   sqlOrderBy & sqlOrderByCon

                    Command.CommandText = sqlStatement


                    'データリーダーにデータ取得
                    'DataReader = Command.ExecuteReader
                    'Do Until Not DataReader.Read

                    '    '部門の数だけループ
                    '    For Cntcc = 0 To Wrk_DataRe2.GetLength(1) - 2

                    '        'フロア名の取得
                    '        For Cntdd = 0 To Wrk_DataRe.GetLength(1) - 2
                    '            If Wrk_DataRe2(0, Cntcc) = Wrk_DataRe(0, Cntdd) Then
                    '                '8/26
                    '                strFloorName = Wrk_DataRe(0, Cntdd) & "：" & Wrk_DataRe(1, Cntdd)
                    '                Exit For
                    '            Else
                    '                strFloorName = "登録なし"
                    '            End If
                    '        Next

                    '        DtgLblPri.Rows.Add()
                    '        Idx = DtgLblPri.Rows.Count - 1
                    '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm26").Value = DataReader.Item("StrNo").ToString
                    '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm27").Value = DataReader.Item("StrName").ToString
                    '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm28").Value = strFloorName
                    '        '8/25
                    '        DtgLblPri.Rows(Idx).Cells("DtgLblPriClm29").Value = Wrk_DataRe2(1, Cntcc) & "：" & Wrk_DataRe2(2, Cntcc)
                    '        'DtgLblPri.Rows(Idx).Cells("DtgLblPriClm29").Value = Wrk_DataRe2(2, Cntcc)
                    '        DtgLblPriClm35.HeaderText = strCommentName

                    '    Next
                    'Loop
                    'ＤＢ切断
                    DataReader.Close()
                    Connection.Close()

                    DataReader.Dispose()
                    Command.Dispose()
                    Connection.Dispose()

                Case Else
                    MessageBox.Show(ErrorMessage100, _
                    "エラー", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Error)
                    Exit Sub
            End Select


            DtgLblPri.Focus()
        End If
    End Sub
    'DataErrorイベントハンドラ
    Private Sub DtgLblPri_DataError(ByVal sender As Object, _
            ByVal e As DataGridViewDataErrorEventArgs) _
            Handles DtgLblPri.DataError

    End Sub
    Private Sub DtgLblPri_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DtgLblPri.CellContentClick

        Dim intClmIdx As Integer = DtgLblPri.CurrentCell.ColumnIndex
        Dim intRowIdx As Integer = DtgLblPri.CurrentCell.RowIndex

        intRenewFlg = 1

        'カラムのタイプがチェックボックスの場合に処理
        If TypeOf DtgLblPri.Columns(intClmIdx) Is DataGridViewCheckBoxColumn Then
            '定店のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 29 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(30, intRowIdx).Value = False
                Me.DtgLblPri(31, intRowIdx).Value = False
                Me.DtgLblPri(32, intRowIdx).Value = False
                Me.DtgLblPri(33, intRowIdx).Value = False

            End If

            '定本のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If DtgLblPri.CurrentCell.ColumnIndex = 30 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(29, intRowIdx).Value = False
                Me.DtgLblPri(31, intRowIdx).Value = False
                Me.DtgLblPri(32, intRowIdx).Value = False
                Me.DtgLblPri(33, intRowIdx).Value = False

            End If

            '特店のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If DtgLblPri.CurrentCell.ColumnIndex = 31 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(29, intRowIdx).Value = False
                Me.DtgLblPri(30, intRowIdx).Value = False
                Me.DtgLblPri(32, intRowIdx).Value = False
                Me.DtgLblPri(33, intRowIdx).Value = False

            End If

            '特本のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If DtgLblPri.CurrentCell.ColumnIndex = 32 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(29, intRowIdx).Value = False
                Me.DtgLblPri(30, intRowIdx).Value = False
                Me.DtgLblPri(31, intRowIdx).Value = False
                Me.DtgLblPri(33, intRowIdx).Value = False

            End If

            '客注のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If DtgLblPri.CurrentCell.ColumnIndex = 33 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(29, intRowIdx).Value = False
                Me.DtgLblPri(30, intRowIdx).Value = False
                Me.DtgLblPri(31, intRowIdx).Value = False
                Me.DtgLblPri(32, intRowIdx).Value = False

            End If

            '納品区分「客」のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 40 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(41, intRowIdx).Value = False
                Me.DtgLblPri(42, intRowIdx).Value = False
                Me.DtgLblPri(43, intRowIdx).Value = False
                Me.DtgLblPri(44, intRowIdx).Value = False
                Me.DtgLblPri(45, intRowIdx).Value = False
                Me.DtgLblPri(46, intRowIdx).Value = False
                Me.DtgLblPri(47, intRowIdx).Value = False

            End If

            '納品区分「優」のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 41 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(40, intRowIdx).Value = False
                Me.DtgLblPri(42, intRowIdx).Value = False
                Me.DtgLblPri(43, intRowIdx).Value = False
                Me.DtgLblPri(44, intRowIdx).Value = False
                Me.DtgLblPri(45, intRowIdx).Value = False
                Me.DtgLblPri(46, intRowIdx).Value = False
                Me.DtgLblPri(47, intRowIdx).Value = False

            End If

            '納品区分「通」のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 42 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(40, intRowIdx).Value = False
                Me.DtgLblPri(41, intRowIdx).Value = False
                Me.DtgLblPri(43, intRowIdx).Value = False
                Me.DtgLblPri(44, intRowIdx).Value = False
                Me.DtgLblPri(45, intRowIdx).Value = False
                Me.DtgLblPri(46, intRowIdx).Value = False
                Me.DtgLblPri(47, intRowIdx).Value = False

            End If

            '納品区分「配」のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 43 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(40, intRowIdx).Value = False
                Me.DtgLblPri(41, intRowIdx).Value = False
                Me.DtgLblPri(42, intRowIdx).Value = False
                Me.DtgLblPri(44, intRowIdx).Value = False
                Me.DtgLblPri(45, intRowIdx).Value = False
                Me.DtgLblPri(46, intRowIdx).Value = False
                Me.DtgLblPri(47, intRowIdx).Value = False

            End If

            '納品区分「新」のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 44 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(40, intRowIdx).Value = False
                Me.DtgLblPri(41, intRowIdx).Value = False
                Me.DtgLblPri(42, intRowIdx).Value = False
                Me.DtgLblPri(43, intRowIdx).Value = False
                Me.DtgLblPri(45, intRowIdx).Value = False
                Me.DtgLblPri(46, intRowIdx).Value = False
                Me.DtgLblPri(47, intRowIdx).Value = False

            End If

            '納品区分「特」のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 45 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(40, intRowIdx).Value = False
                Me.DtgLblPri(41, intRowIdx).Value = False
                Me.DtgLblPri(42, intRowIdx).Value = False
                Me.DtgLblPri(43, intRowIdx).Value = False
                Me.DtgLblPri(44, intRowIdx).Value = False
                Me.DtgLblPri(46, intRowIdx).Value = False
                Me.DtgLblPri(47, intRowIdx).Value = False

            End If

            '納品区分「手」のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 46 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(40, intRowIdx).Value = False
                Me.DtgLblPri(41, intRowIdx).Value = False
                Me.DtgLblPri(42, intRowIdx).Value = False
                Me.DtgLblPri(43, intRowIdx).Value = False
                Me.DtgLblPri(44, intRowIdx).Value = False
                Me.DtgLblPri(45, intRowIdx).Value = False
                Me.DtgLblPri(47, intRowIdx).Value = False

            End If

            '納品区分「■」のチェックボックスが押下された場合、他のチェックボックスをFalseに変更する
            If intClmIdx = 47 And
                DtgLblPri.CurrentCell.Value = False Then

                Me.DtgLblPri(40, intRowIdx).Value = False
                Me.DtgLblPri(41, intRowIdx).Value = False
                Me.DtgLblPri(42, intRowIdx).Value = False
                Me.DtgLblPri(43, intRowIdx).Value = False
                Me.DtgLblPri(44, intRowIdx).Value = False
                Me.DtgLblPri(45, intRowIdx).Value = False
                Me.DtgLblPri(46, intRowIdx).Value = False

            End If

        End If

    End Sub

    Private Sub BtnRadio1_CheckedChanged(sender As Object, e As EventArgs) Handles BtnRadio1.CheckedChanged
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim i As Integer = 0
        Dim Cntup As Integer = 0
        Dim strToriKbn As String = 0

        '初期化
        CmdTok1.Items.Clear()

        '取引先区分をセット
        If BtnRadio1.Checked = True Then
            strToriKbn = 1
        End If

        '共通ワークエリアの初期化
        ReDim Wrk_DataTok(1, 1)

        '接続文字列を設定
        Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection.Open()
        'コマンド作成
        Command = Connection.CreateCommand

        sqlStatement = ""
        sqlField1 = ""
        sqlTableName = ""
        sqlWhereCon = ""

        '得意先を選択するＳＱＬ
        sqlTableName = "Tbl_CorpMas"
        sqlField1 = "CorpID,CorpName"
        sqlWhereCon = "Not LblTypeID = 'N01' AND " &
                      "ToriKbn = " & strToriKbn & " OR " &
                      "ToriKbn = '9'"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

        'SQL作成
        Command.CommandText = sqlStatement
        'データリーダーにデータ取得

        DataReader = Command.ExecuteReader
        Do Until Not DataReader.Read
            CmdTok1.Items.Add(DataReader.Item("CorpName").ToString)
            'ワークエリアへのセット
            Wrk_DataTok(0, i) = DataReader.Item("CorpID").ToString
            Wrk_DataTok(1, i) = DataReader.Item("CorpName").ToString

            'ワークエリアの拡張（配列を追加）
            ReDim Preserve Wrk_DataTok(1, Cntup + 1)
            Cntup = Cntup + 1
            i = i + 1
        Loop

        CmdTok1.Text = CmdTok1.Items(0)

        'ＤＢ切断
        DataReader.Close()
        Connection.Close()

        DataReader.Dispose()
        Command.Dispose()
        Connection.Dispose()
    End Sub

    Private Sub BtnRadio2_CheckedChanged(sender As Object, e As EventArgs) Handles BtnRadio2.CheckedChanged
        Dim Connection As New SQLiteConnection
        Dim Command As SQLiteCommand
        Dim DataReader As SQLiteDataReader
        Dim i As Integer = 0
        Dim Cntup As Integer = 0
        Dim strToriKbn As String = 0

        '初期化
        CmdTok1.Items.Clear()

        '取引先区分をセット
        If BtnRadio1.Checked = True Then
            strToriKbn = 2
        End If

        '共通ワークエリアの初期化
        ReDim Wrk_DataTok(1, 1)

        '接続文字列を設定
        Connection.ConnectionString = "Version=3;Data Source=Lbl_Print_KAB001.db;New=False;Compress=True;"
        'オープン
        Connection.Open()
        'コマンド作成
        Command = Connection.CreateCommand

        sqlStatement = ""
        sqlField1 = ""
        sqlTableName = ""
        sqlWhereCon = ""

        '得意先を選択するＳＱＬ
        sqlTableName = "Tbl_CorpMas"
        sqlField1 = "CorpID,CorpName"
        sqlWhereCon = "Not LblTypeID = 'N01' AND " &
                      "ToriKbn = " & strToriKbn & " OR " &
                      "ToriKbn = '9'"

        sqlStatement = sqlSelect & sqlField1 & sqlFrom & sqlTableName & sqlWhere & sqlWhereCon

        'SQL作成
        Command.CommandText = sqlStatement
        'データリーダーにデータ取得

        DataReader = Command.ExecuteReader
        Do Until Not DataReader.Read
            CmdTok1.Items.Add(DataReader.Item("CorpName").ToString)
            'ワークエリアへのセット
            Wrk_DataTok(0, i) = DataReader.Item("CorpID").ToString
            Wrk_DataTok(1, i) = DataReader.Item("CorpName").ToString

            'ワークエリアの拡張（配列を追加）
            ReDim Preserve Wrk_DataTok(1, Cntup + 1)
            Cntup = Cntup + 1
            i = i + 1
        Loop

        CmdTok1.Text = CmdTok1.Items(0)

        'ＤＢ切断
        DataReader.Close()
        Connection.Close()

        DataReader.Dispose()
        Command.Dispose()
        Connection.Dispose()
    End Sub
End Class
