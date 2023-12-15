Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection

Public Interface IAfsServerFactory

  ReadOnly Property DisplayName As String
  ReadOnly Property Identifier As Guid
  Function CreateConfigObject() As Object

  Function InitializeServer(configObject As Object) As IAfsSource

End Interface
