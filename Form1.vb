Public Class Form1

    ' Variables for calculator state
    Private firstOperand As Nullable(Of Double) = Nothing
    Private currentOperator As String = ""
    Private isNewEntry As Boolean = True

    ' Constructor – always keep this
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub

    ' === DIGIT & DOT BUTTONS ===
    Private Sub Digit_Click(sender As Object, e As EventArgs) _
        Handles btn0.Click, btn1.Click, btn2.Click, btn3.Click,
                btn4.Click, btn5.Click, btn6.Click, btn7.Click,
                btn8.Click, btn9.Click, btnDot.Click

        Dim btn As Button = CType(sender, Button)
        Dim digit As String = btn.Text

        If isNewEntry Then
            If digit = "." Then
                lblResult.Text = "0."
            Else
                lblResult.Text = digit
            End If
            isNewEntry = False
        Else
            ' Prevent multiple dots
            If digit = "." AndAlso lblResult.Text.Contains(".") Then Exit Sub
            lblResult.Text &= digit
        End If
    End Sub

    ' === OPERATOR BUTTONS (+, -, x, /) ===
    Private Sub Operator_Click(sender As Object, e As EventArgs) _
        Handles btnAdd.Click, btnSub.Click, btnMul.Click, btnDiv.Click

        Dim btn As Button = CType(sender, Button)
        Dim op As String = btn.Text
        Dim currentValue As Double

        If Not Double.TryParse(lblResult.Text, currentValue) Then
            lblResult.Text = "Error"
            Exit Sub
        End If

        If firstOperand Is Nothing Then
            firstOperand = currentValue
            currentOperator = op
            lblExpression.Text = firstOperand.ToString() & " " & currentOperator
            isNewEntry = True
        Else
            Dim result As Double = Compute(firstOperand.Value, currentValue, currentOperator)
            lblResult.Text = FormatResult(result)
            firstOperand = result
            currentOperator = op
            lblExpression.Text = firstOperand.ToString() & " " & currentOperator
            isNewEntry = True
        End If
    End Sub

    ' === EQUALS BUTTON ===
    Private Sub btnEquals_Click(sender As Object, e As EventArgs) Handles btnEquals.Click
        If firstOperand Is Nothing OrElse currentOperator = "" Then Exit Sub

        Dim secondOperand As Double
        If Not Double.TryParse(lblResult.Text, secondOperand) Then
            lblResult.Text = "Error"
            Exit Sub
        End If

        Dim result As Double = Compute(firstOperand.Value, secondOperand, currentOperator)
        lblExpression.Text = firstOperand.ToString() & " " & currentOperator & " " & secondOperand & " ="
        lblResult.Text = FormatResult(result)

        ' Reset for next calculation
        firstOperand = Nothing
        currentOperator = ""
        isNewEntry = True
    End Sub

    ' === CLEAR BUTTON (CLR) ===
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        lblResult.Text = "0"
        lblExpression.Text = ""
        firstOperand = Nothing
        currentOperator = ""
        isNewEntry = True
    End Sub

    ' === BACKSPACE BUTTON (⌫) ===
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If isNewEntry Then
            lblResult.Text = "0"
        ElseIf lblResult.Text.Length > 1 Then
            lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1)
        Else
            lblResult.Text = "0"
            isNewEntry = True
        End If
    End Sub

    ' === HELPER: COMPUTE ===
    Private Function Compute(a As Double, b As Double, op As String) As Double
        Select Case op
            Case "+"
                Return a + b
            Case "-"
                Return a - b
            Case "x", "*"
                Return a * b
            Case "/"
                If b = 0 Then Return Double.NaN
                Return a / b
            Case Else
                Return b
        End Select
    End Function

    ' === HELPER: FORMAT RESULT ===
    Private Function FormatResult(val As Double) As String
        If Double.IsNaN(val) OrElse Double.IsInfinity(val) Then Return "Error"
        If val = Math.Truncate(val) Then
            Return val.ToString("0")
        Else
            Return val.ToString()
        End If
    End Function

End Class
