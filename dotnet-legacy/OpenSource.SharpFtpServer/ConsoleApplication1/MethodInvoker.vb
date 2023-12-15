Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Text

Public Class MethodInvoker(Of T As Class)

  Private _Targets As New Dictionary(Of String, Func(Of Object, Object()))

  Public Sub New(instance As T)
    Dim typeToAnalyze As Type = GetType(T)
    For Each mi As MethodInfo In typeToAnalyze.GetMethods()

      Dim identifier As New StringBuilder
      identifier.AppendLine(mi.Name.ToLower())
      For Each p As ParameterInfo In mi.GetParameters
        identifier.AppendLine(p.ParameterType.Name)
      Next

      _Targets.Add(identifier.ToString(), Function(p() As Object) mi.Invoke(instance, p))
    Next
  End Sub

  Public Function Invoke(Of TReturn)(methodName As String, ParamArray arguments() As Object) As TReturn
    Return DirectCast(Me.Invoke(methodName), TReturn)
  End Function

  Public Function Invoke(methodName As String, ParamArray arguments() As Object) As Object

    Dim identifier As New StringBuilder
    identifier.AppendLine(methodName.ToLower())
    For Each argument As Object In arguments
      identifier.AppendLine(argument.GetType().Name)
    Next

    If (_Targets.ContainsKey(identifier.ToString())) Then
      Return _Targets(identifier.ToString()).Invoke(arguments)
    Else
      Throw New MissingMethodException(GetType(T).Name, methodName)
    End If

  End Function

End Class
