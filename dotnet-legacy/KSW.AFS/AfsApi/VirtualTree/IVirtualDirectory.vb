Imports System.Collections.Generic
Imports System

Public Interface IVirtualDirectory
  Inherits IAfsSource

  ReadOnly Property VirtualSubDirectories As Dictionary(Of String, AfsVirtualDirectory)

  Property MountedSource As IAfsSource

End Interface
