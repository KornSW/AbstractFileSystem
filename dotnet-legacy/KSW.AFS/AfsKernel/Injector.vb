Imports System
Imports System.Collections.Generic
Imports System.Linq

Public NotInheritable Class Injector
  Implements IAfsCompositionInjector

  Public Function GetVersion() As Version Implements IAfsCompositionInjector.GetVersion
    Return Version.Parse("1.0")
  End Function

  Public Sub New(applicationIdentifier As String)
  End Sub

#Region "IDisposable Support"
  Private disposedValue As Boolean ' To detect redundant calls

  ' IDisposable
  Protected Sub Dispose(disposing As Boolean)
    If Not Me.disposedValue Then
      If disposing Then
        ' TODO: dispose managed state (managed objects).
      End If

      ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
      ' TODO: set large fields to null.
    End If
    Me.disposedValue = True
  End Sub

  ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
  'Protected Overrides Sub Finalize()
  '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
  '    Dispose(False)
  '    MyBase.Finalize()
  'End Sub

  ' This code added by Visual Basic to correctly implement the disposable pattern.
  Public Sub Dispose() Implements IDisposable.Dispose
    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    Dispose(True)
    GC.SuppressFinalize(Me)
  End Sub
#End Region

  Private _PipeProviderFactories As New List(Of IAfsPipeProviderFactory)
  Public ReadOnly Property PipeProviderFactories As IEnumerable(Of IAfsPipeProviderFactory) Implements IAfsCompositionInjector.PipeProviderFactories
    Get
      Return _PipeProviderFactories
    End Get
  End Property


  Private _ServerFactories As New List(Of IAfsServerFactory)
  Public ReadOnly Property ServerFactories As IEnumerable(Of IAfsServerFactory) Implements IAfsCompositionInjector.ServerFactories
    Get
      Return _ServerFactories
    End Get
  End Property


  Private _SourceProviderFactories As New List(Of IAfsSourceProviderFactory)
  Public ReadOnly Property SourceProviderFactories As IEnumerable(Of IAfsSourceProviderFactory) Implements IAfsCompositionInjector.SourceProviderFactories
    Get
      Return _SourceProviderFactories
    End Get
  End Property



  Public ReadOnly Property PipeProviderFactories(identifier As Guid) As IAfsPipeProviderFactory Implements IAfsCompositionInjector.PipeProviderFactories
    Get
      Return (From f In _PipeProviderFactories Where f.Identifier = identifier).SingleOrDefault()
    End Get
  End Property

  Public ReadOnly Property ServerFactories(identifier As Guid) As IAfsServerFactory Implements IAfsCompositionInjector.ServerFactories
    Get
      Return (From f In _ServerFactories Where f.Identifier = identifier).SingleOrDefault()
    End Get
  End Property

  Public ReadOnly Property SourceProviderFactories(identifier As Guid) As IAfsSourceProviderFactory Implements IAfsCompositionInjector.SourceProviderFactories
    Get
      Return (From f In _SourceProviderFactories Where f.Identifier = identifier).SingleOrDefault()
    End Get
  End Property
End Class
