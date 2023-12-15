Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection

Public NotInheritable Class AfsCompositionScope

#Region " Injection "

  Shared Sub New()
    AddHandler AppDomain.CurrentDomain.DomainUnload, Sub(s, e) ShutDownInjector()
  End Sub

  Private _ApplicationIdentifier As String = AppDomain.CurrentDomain.ApplicationIdentity.FullName
  Public Property ApplicationIdentifier As String
    Get
      Return _ApplicationIdentifier
    End Get
    Set(value As String)
      _ApplicationIdentifier = value
      If (_Injector IsNot Nothing) Then
        ShutDownInjector()
        InitializeInjector(value)
      End If
    End Set
  End Property

  Public Sub New()
  End Sub

  Private Shared _Injector As IAfsCompositionInjector = Nothing

  Public ReadOnly Property Injector As IAfsCompositionInjector
    Get
      If (_Injector Is Nothing) Then
        _Injector = InitializeInjector(ApplicationIdentifier)
      End If
      Return _Injector
    End Get
  End Property

  Private Shared Function InitializeInjector(applicationIdentifier As String) As IAfsCompositionInjector

    Dim targetAssembly As Assembly
    Dim instance As IAfsCompositionInjector = Nothing

    targetAssembly = Assembly.LoadFile("C:\TKP\Prototyping\AbstractFs\AfsKernel\bin\Debug\AfsKernel.dll")

    For Each t As Type In targetAssembly.GetTypes
      If (t.GetInterfaces.Contains(GetType(IAfsCompositionInjector))) Then
        instance = DirectCast(Activator.CreateInstance(t, {applicationIdentifier}), IAfsCompositionInjector)

        Exit For
      End If
    Next

    Return instance
  End Function

  Private Shared Sub ShutDownInjector()
    If (_Injector IsNot Nothing) Then
      _Injector.Dispose()
      _Injector = Nothing
    End If
  End Sub

#End Region

  Public Function BuildCommonAfsTree() As IAfsSource

    'ApplicationIdentifier

    Dim commonTree As New AfsVirtualDirectory

    ' commonTree.
    Dim localFilesystem As IAfsSource

    With Me.Injector.SourceProviderFactories(
      WellKnownClientProviderIdentifiers.LocalFilesystem)
      Dim configObject = .CreateConfigObject()
      localFilesystem = .InitializeProvider(configObject)
    End With

    ' commonTree.MountedSource = localFilesystem

    'If (anyCloudConfigrate) Then

    '  Dim cloudDirectory As New AfsVirtualDirectory
    '  commonTree.VirtualSubDirectories.Add("CloudStorage", cloudDirectory)

    '    for each configured privider
    '          Dim provDir As New AfsVirtualDirectory
    '          cloudDirectory.VirtualSubDirectories.Add("Dropbox", provDir)
    '    next

    ' End If

    'Return commonTree
  End Function

End Class
