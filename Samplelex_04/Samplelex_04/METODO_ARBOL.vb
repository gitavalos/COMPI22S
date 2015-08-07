Public Class METODO_ARBOL
    Private ID_HOJA As String = 0
    Private Function CONTADOR() As Integer 'AUMENTA EL CONTADOR
        ID_HOJA = ID_HOJA + 1
        Return ID_HOJA
    End Function
    Private Sub PASAR_LISTAS(ByVal LISTA1 As List(Of Integer), ByVal LISTA2 As List(Of Integer)) 'PASA LOS ELEMENTOS DE LA LISTA2 A LAS LISTA1
        For Each ITEM As Integer In LISTA2
            LISTA1.Add(ITEM)
        Next
    End Sub
    Public Function PRIMERA_PASADA(ByVal RAIZ As NODO) As NODO 'INGRESA FIRST & LAST ADEMAS DEL NUMERO DE HOJA A CADA HOJA
        Dim b1 As Boolean = False
        Dim b2 As Boolean = False
        If Not (IsNothing(RAIZ.hijo_izquierdo)) Then
            PRIMERA_PASADA(RAIZ.hijo_izquierdo)
        End If
        If Not (IsNothing(RAIZ.hijo_derecho)) Then
            PRIMERA_PASADA(RAIZ.hijo_derecho)
        End If
        '/////////////////////////////////////////////////////////////////////////
        Select Case RAIZ.layer
            Case "."
                If RAIZ.id = "EXPRESION" Then
                    Dim izq As Boolean = RAIZ.hijo_izquierdo.ANULABLE
                    Dim der As Boolean = RAIZ.hijo_derecho.ANULABLE
                    If (izq = True And der = True) Then
                        RAIZ.ANULABLE = True
                    Else
                        RAIZ.ANULABLE = False
                    End If
                    PASAR_LISTAS(RAIZ.FIRST, RAIZ.hijo_izquierdo.FIRST)
                    PASAR_LISTAS(RAIZ.LAST, RAIZ.hijo_derecho.LAST)
                    If (izq = True) Then
                        PASAR_LISTAS(RAIZ.FIRST, RAIZ.hijo_derecho.FIRST)
                    End If
                    If (der = True) Then
                        PASAR_LISTAS(RAIZ.LAST, RAIZ.hijo_izquierdo.LAST)
                    End If
                End If
            Case "*"
                PASAR_LISTAS(RAIZ.FIRST, RAIZ.hijo_izquierdo.FIRST)
                PASAR_LISTAS(RAIZ.LAST, RAIZ.hijo_izquierdo.LAST)
                RAIZ.ANULABLE = True
            Case "|"
                Dim izq As Boolean = RAIZ.hijo_izquierdo.ANULABLE
                Dim der As Boolean = RAIZ.hijo_derecho.ANULABLE
                If (izq = True And der = True) Then
                    RAIZ.ANULABLE = True
                Else
                    RAIZ.ANULABLE = False
                End If
                PASAR_LISTAS(RAIZ.FIRST, RAIZ.hijo_izquierdo.FIRST)
                If (izq = True) Then
                    PASAR_LISTAS(RAIZ.FIRST, RAIZ.hijo_derecho.FIRST)
                End If
                PASAR_LISTAS(RAIZ.LAST, RAIZ.hijo_derecho.LAST)
                If (der = True) Then
                    PASAR_LISTAS(RAIZ.LAST, RAIZ.hijo_izquierdo.LAST)
                End If
            Case "+"
                PASAR_LISTAS(RAIZ.FIRST, RAIZ.hijo_izquierdo.FIRST)
                PASAR_LISTAS(RAIZ.LAST, RAIZ.hijo_izquierdo.LAST)
                RAIZ.ANULABLE = True
            Case "?"
                PASAR_LISTAS(RAIZ.FIRST, RAIZ.hijo_izquierdo.FIRST)
                PASAR_LISTAS(RAIZ.LAST, RAIZ.hijo_izquierdo.LAST)
                RAIZ.ANULABLE = True
            Case Else
                Dim NUM As Integer = CONTADOR()
                RAIZ.ANULABLE = False
                RAIZ.FIRST.Add(NUM)
                RAIZ.LAST.Add(NUM)
                RAIZ.NUMERO = NUM
        End Select
        Return RAIZ
    End Function
    Public Function SEGUNDA_PASADA(ByVal RAIZ As NODO) As NODO 'HACER LOS FOLLOWS

        Return RAIZ
    End Function
End Class
