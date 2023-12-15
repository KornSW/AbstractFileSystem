Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Reflection

Public Interface IAfsFileInfo

  Function GetSource() As IAfsSource
  Function GetParentDirectory() As IAfsDirectoryInfo

  Function OpenRead() As Stream
  Function OpenWrite() As Stream

  'Sub Delete()

End Interface
