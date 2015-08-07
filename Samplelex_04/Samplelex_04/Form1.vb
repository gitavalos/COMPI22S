Imports System.IO

Public Class Form1
    Private Sub Graficar(raiz As NODO)
        Dim cad As String = "digraph G {" + vbNewLine + recorrido(raiz) + vbNewLine + "}"

        File.Create("Grafica1.dot").Dispose()
        Dim tw As New StreamWriter("Grafica1.dot")
        tw.WriteLine(cad)
        tw.Close()

        Dim startinfo As New ProcessStartInfo("C:\\Program Files (x86)\\Graphviz2.38\\bin\\dot.exe")
        startinfo.RedirectStandardOutput = True
        startinfo.UseShellExecute = False
        startinfo.CreateNoWindow = True
        startinfo.Arguments = "-Tpng Grafica1.dot -o grafo1.png"
        Process.Start(startinfo)
    End Sub

    Private Function recorrido(nodo As NODO) As String
        Dim cadena As String = ""
        If Not (IsNothing(nodo)) Then
            cadena = nodo.correlativo & "[label = """ & nodo.layer + """ ]" & vbNewLine
            If Not (IsNothing(nodo.hijo_izquierdo)) Then
                cadena = cadena & nodo.correlativo & " -> " & nodo.hijo_izquierdo.correlativo & vbNewLine
                cadena = cadena & vbNewLine & recorrido(nodo.hijo_izquierdo)
            End If
            If Not (IsNothing(nodo.hijo_derecho)) Then
                cadena = cadena & nodo.correlativo & " -> " & nodo.hijo_derecho.correlativo & vbNewLine
                cadena = cadena & vbNewLine & recorrido(nodo.hijo_derecho)
            End If
        End If
        Return cadena
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim miparser As New MyParser
        Try
            miparser.Setup()
            If (miparser.Parse(New StringReader(RichTextBox1.Text))) Then
                MessageBox.Show("ok!")
                Dim padre As New NODO
                padre = GENERADOR_ARBOL(miparser.LISTA)
                Dim metodo As New METODO_ARBOL
                metodo.PRIMERA_PASADA(padre)
                metodo.SEGUNDA_PASADA(padre)
            Else
                MessageBox.Show("Error")

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Function GENERADOR_ARBOL(ByVal MILISTA As List(Of LISTA_NODO)) As NODO
        Dim TEMPORAL As New NODO
        Dim identificador As Integer = 19216800
        Dim primero As Boolean = False
        For Each LIST As LISTA_NODO In MILISTA
            If (primero = False) Then
                TEMPORAL = LIST.RAIZ
                primero = True
            Else
                TEMPORAL = CONCAT_EXPRESIONES(identificador, TEMPORAL, LIST.RAIZ)
            End If
        Next
        Dim finalizacion As New NODO
        finalizacion.correlativo = identificador + 1
        finalizacion.id = "FIN"
        finalizacion.layer = "#"

        Dim SUPER_NODO As New NODO
        SUPER_NODO.correlativo = identificador + 2
        SUPER_NODO.id = "EXPRESION"
        SUPER_NODO.layer = "."
        SUPER_NODO.hijo_izquierdo = TEMPORAL
        SUPER_NODO.hijo_derecho = finalizacion

        Graficar(SUPER_NODO)
        Return SUPER_NODO
    End Function

    Private Function CONCAT_EXPRESIONES(ByRef cont As Integer, ByVal NODO_CABEZA As NODO, ByVal NODO_CUERPO As NODO) As NODO
        cont = cont + 1
        Dim temporal As New NODO
        temporal.correlativo = cont
        temporal.id = "SUPER_EXPRESION"
        temporal.layer = "|"
        temporal.hijo_izquierdo = NODO_CABEZA
        temporal.hijo_derecho = NODO_CUERPO

        Return temporal
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
