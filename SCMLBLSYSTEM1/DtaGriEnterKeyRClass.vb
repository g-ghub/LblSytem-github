Imports System
Imports System.Windows.Forms

Public Class DtaGriEnterKeyRClass
    Inherits DataGridView
    Private Sub ExDataGridView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control And e.KeyCode = Keys.V Then
            'Ctl+v押下時は貼付
            Me.Paste()
        ElseIf e.KeyCode = Keys.Delete Then
            'Delete押下時は削除
            Me.Delete()
        End If
    End Sub


    <System.Security.Permissions.UIPermission( _
        System.Security.Permissions.SecurityAction.Demand, _
        Window:=System.Security.Permissions.UIPermissionWindow.AllWindows)> _
    Protected Overrides Function ProcessDialogKey( _
            ByVal keyData As Keys) As Boolean
        'Enterキーが押された時は、Tabキーが押されたようにする
        If (keyData And Keys.KeyCode) = Keys.Enter Then
            If Me.RowCount = 0 Then

            ElseIf Me.RowCount >= 1 Then
                Me.ProcessTabKey(keyData)
                Return Me.BeginEdit(True)
            End If
        End If

        'Tabキーが押された時
        If (keyData And Keys.KeyCode) = Keys.Tab Then
            
        End If
        'Deleteキーが押された時は即削除
        If (keyData And Keys.KeyCode) = Keys.Delete Then
            If Me.CurrentCell.ReadOnly = True Then

            Else
                Me.Delete()
            End If
        End If
        'Backキーが押された時は、フォーカスしてからDeleteキーが押されたようにする
        If (keyData And Keys.KeyCode) = Keys.Back Then
            If Me.CurrentCell.ReadOnly = True Then

            Else
                Me.Delete()
            End If
        End If
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    <System.Security.Permissions.SecurityPermission( _
        System.Security.Permissions.SecurityAction.Demand, _
        Flags:=System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)> _
    Protected Overrides Function ProcessDataGridViewKey( _
            ByVal e As KeyEventArgs) As Boolean
        'Enterキーが押された時は、Tabキーが押されたようにする
        If e.KeyCode = Keys.Enter Then
            If Me.RowCount = 0 Then

            ElseIf Me.RowCount >= 1 Then
                Me.ProcessTabKey(e.KeyCode)
                Return Me.BeginEdit(True)
            End If
        End If
        'Tabキーが押された時
        If e.KeyCode = Keys.Tab Then
            
        End If
        'Deleteキーが押された時は即削除
        If e.KeyCode = Keys.Delete Then
            If Me.CurrentCell.ReadOnly = True Then

            Else
                Me.Delete()
            End If
        End If
        'Backキーが押された時は、フォーカスを押してからDeleteキーが押されたようにする
        If e.KeyCode = Keys.Back Then
            Me.BeginEdit(True)
            Return Me.ProcessDeleteKey(e.KeyData)
        End If
        Return MyBase.ProcessDataGridViewKey(e)
    End Function


    Private Sub Delete()

        '全体選択の場合は対象外
        If Me.AreAllCellsSelected(True) Then Exit Sub

        '行選択の場合は対象外
        If Me.SelectedRows.Count > 0 Then Exit Sub

        '選択したセルの値を削除
        For i As Integer = 0 To Me.SelectedCells.Count - 1
            Me.SelectedCells.Item(i).Value = String.Empty
        Next

    End Sub


    Private Sub Paste()

        'クリップボードの値を二次元配列化
        Dim Cells As ArrayList = New ArrayList
        Dim Rows() As String = Split(Clipboard.GetText, vbCrLf)
        For i As Integer = 0 To Rows.Length - 1
            Dim Columns() As String = Split(Rows(i), vbTab)
            Cells.Add(Columns)
        Next

        '貼り付け
        With Me
            If .AreAllCellsSelected(True) Then
                '全選択状態の場合はグリッドをクリアしてから貼り付け
                .Rows.Clear()
                For i As Integer = 0 To Cells.Count - 1
                    Dim Columns() As String = Cells.Item(i)
                    .Rows.Add()
                    For j As Integer = 0 To Columns.Length - 1
                        Me(j, i).Value = Columns(j).ToString
                    Next
                Next
            Else
                '上記以外の場合は選択したセルから貼り付け
                Dim StartRow As Integer = .SelectedCells(0).RowIndex
                Dim StartCol As Integer = .SelectedCells(0).ColumnIndex
                Dim StartRow2 As Integer = StartRow

                For i As Integer = 0 To Cells.Count - 1
                    Dim Columns() As String = Cells.Item(i)
                    For j As Integer = 0 To Columns.Length - 1
                        'グリッドの列数と行数を超えた列の値は切り捨て
                        If Not StartCol + j <= Me.Columns.Count - 1 Or
                            Not StartRow2 <= Me.Rows.Count - 1 Then
                        Else
                            Me(StartCol + j, StartRow + i).Value = Columns(j).ToString
                        End If
                    Next
                    StartRow2 = StartRow2 + 1
                Next
            End If

        End With
    End Sub
End Class
