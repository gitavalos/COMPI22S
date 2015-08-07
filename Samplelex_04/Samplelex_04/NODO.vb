Public Class NODO
    Public hijo_izquierdo As NODO
    Public hijo_derecho As NODO
    Public layer As String
    Public id As String
    Public correlativo As Integer
    '---------------------------------------
    Public FIRST As New List(Of Integer)
    Public LAST As New List(Of Integer)
    Public ANULABLE As Boolean
    Public NUMERO As Integer
End Class
