Imports KSW.CtExpert.Views
Imports KSW.Base
Imports KSW.CtExpert
Imports KSW.CtExpert.WebApplication
Imports KSW.Base.Ui.Web

Public Class CtExpertMainView
  Inherits AbstractWebControl
  Implements ICtExpertMainView

  Public WriteOnly Property Actions As IEnumerable(Of UseCaseAction) Implements IView.Actions
    Set(value As IEnumerable(Of UseCaseAction))

    End Set
  End Property

  Public ReadOnly Property Status As String
    Get
      Return "alles Cool"
    End Get
  End Property


  Public Property Disabled As Boolean Implements IView.Disabled

  Public ReadOnly Property DisplayTitle As String Implements IView.DisplayTitle
    Get

    End Get
  End Property

  Public Property ParentContainer As IViewContainer Implements IView.ParentContainer

  Public ReadOnly Property ViewContainers As IViewContainer() Implements IView.ViewContainers
    Get

    End Get
  End Property

  Public ReadOnly Property ViewContainers(containerName As String) As IViewContainer Implements IView.ViewContainers
    Get

    End Get
  End Property

  Public ReadOnly Property ViewInstanceId As Guid Implements IView.ViewInstanceId
    Get

    End Get
  End Property

  Public WriteOnly Property ViewController As ICtExpertMainViewPresenter Implements ICtExpertMainView.Presenter
    Set(value As ICtExpertMainViewPresenter)

    End Set
  End Property

#Region "IDisposable Support"
  Private disposedValue As Boolean ' To detect redundant calls

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
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

End Class

<AutoInject(GetType(Factory(Of ICtExpertMainView)))>
Public Class CtExpertMainViewFactoryForAsp
  Inherits Factory(Of ICtExpertMainView)

  Public Sub New()
    MyBase.New(Function() New CtExpertMainView)
  End Sub

End Class
