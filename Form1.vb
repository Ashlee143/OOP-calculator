Public Class Form1

    ' ==============================
    ' 🔹 GLOBAL VARIABLES
    ' ==============================
    Dim dblFirstNumber As Double   ' First number
    Dim dblSecondNumber As Double  ' Second number
    Dim dblResult As Double        ' Result
    Dim strOperator As String = "" ' Operator (+, -, ×, ÷)
    Dim blnNewEntry As Boolean = True ' New entry flag (True = replace, False = append)


    ' ==============================
    ' 🔹 CONSTRUCTOR
    ' ==============================
    Public Sub New()
        InitializeComponent()
        Me.KeyPreview = True ' Enable keyboard input
    End Sub



    ' ==============================
    ' 🔹 DIGIT & DECIMAL BUTTONS
    ' ==============================
    Private Sub Digit_Click(sender As Object, e As EventArgs) _
        Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click,
                btn4.Click, btn5.Click, btn6.Click, btn7.Click,
                btn8.Click, btn9.Click, btnDecimal.Click

        Dim btn As Button = CType(sender, Button)
        Dim digit As String = btn.Text

        If blnNewEntry Then
            If digit = "." Then
                lblResult.Text = "0."
            Else
                lblResult.Text = digit
            End If
            blnNewEntry = False
        Else
            ' Prevent multiple dots
            If digit = "." AndAlso lblResult.Text.Contains(".") Then Exit Sub
            lblResult.Text &= digit
        End If
    End Sub


    ' ==============================
    ' 🔹 OPERATOR BUTTONS (+, -, ×, ÷)
    ' ==============================
    Private Sub Operator_Click(sender As Object, e As EventArgs) _
    Handles btnAdd.Click, btnSub.Click, btnMul.Click, btnDiv.Click

        dblFirstNumber = Val(lblResult.Text) ' Save current number
        Dim btn As Button = CType(sender, Button)

        ' Store the operator internally for computation
        Select Case btn.Text
            Case "X" : strOperator = "*"   ' internal value
            Case "÷" : strOperator = "/"   ' internal value
            Case Else : strOperator = btn.Text
        End Select

        ' Show expression with pretty operator
        lblExpression.Text = dblFirstNumber & " " & GetDisplayOperator(strOperator)
        blnNewEntry = True
    End Sub

    Private Function GetDisplayOperator(op As String) As String
        Select Case op
            Case "*" : Return "X"
            Case "/" : Return "÷"
            Case Else : Return op
        End Select
    End Function


    ' ==============================
    ' 🔹 EQUALS BUTTON (=)
    ' ==============================
    Private Sub btnEquals_Click(sender As Object, e As EventArgs) Handles btnEquals.Click
        dblSecondNumber = Val(lblResult.Text)

        Select Case strOperator
            Case "+"
                dblResult = dblFirstNumber + dblSecondNumber
            Case "-"
                dblResult = dblFirstNumber - dblSecondNumber
            Case "*"
                dblResult = dblFirstNumber * dblSecondNumber
            Case "/"
                If dblSecondNumber = 0 Then
                    lblResult.Text = "Error"
                    Exit Sub
                End If
                dblResult = dblFirstNumber / dblSecondNumber
        End Select

        lblExpression.Text = dblFirstNumber & " " & GetDisplayOperator(strOperator) & " " & dblSecondNumber & " ="
        lblResult.Text = dblResult.ToString()
        blnNewEntry = True
    End Sub


    ' ==============================
    ' 🔹 CLEAR BUTTON (C)
    ' ==============================
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lblResult.Text = "0"
        lblExpression.Text = ""
        dblFirstNumber = 0
        dblSecondNumber = 0
        dblResult = 0
        strOperator = ""
        blnNewEntry = True
    End Sub


    ' ==============================
    ' 🔹 BACKSPACE BUTTON (⌫)
    ' ==============================
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If blnNewEntry Then
            lblResult.Text = "0"
        ElseIf lblResult.Text.Length > 1 Then
            lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1)
        Else
            lblResult.Text = "0"
            blnNewEntry = True
        End If
    End Sub


    ' ==============================
    ' 🔹 KEYBOARD HANDLING
    ' ==============================
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter : btnEquals.PerformClick()
            Case Keys.Escape : btnClear.PerformClick()
            Case Keys.Back : btnBack.PerformClick()
            Case Keys.D0, Keys.NumPad0 : btn0.PerformClick()
            Case Keys.D1, Keys.NumPad1 : btn1.PerformClick()
            Case Keys.D2, Keys.NumPad2 : btn2.PerformClick()
            Case Keys.D3, Keys.NumPad3 : btn3.PerformClick()
            Case Keys.D4, Keys.NumPad4 : btn4.PerformClick()
            Case Keys.D5, Keys.NumPad5 : btn5.PerformClick()
            Case Keys.D6, Keys.NumPad6 : btn6.PerformClick()
            Case Keys.D7, Keys.NumPad7 : btn7.PerformClick()
            Case Keys.D8, Keys.NumPad8 : btn8.PerformClick()
            Case Keys.D9, Keys.NumPad9 : btn9.PerformClick()
            Case Keys.Decimal, Keys.OemPeriod : btnDecimal.PerformClick()
            Case Keys.Add : btnAdd.PerformClick()
            Case Keys.Subtract, Keys.OemMinus : btnSub.PerformClick()
            Case Keys.Multiply : btnMul.PerformClick()
            Case Keys.Divide, Keys.OemQuestion : btnDiv.PerformClick()
        End Select
    End Sub

End Class
